using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using BoldAspect.CLI.Metadata;
using BoldAspect.CLI.Metadata.Blobs;

namespace BoldAspect.PE
{
    /// <summary>
    /// Represents a Microsoft Portable Executable image.
    /// </summary>
    public sealed class PortableExecutable
    {
        readonly DosHeader _dosHeader;
        readonly Signature _signature;
        readonly FileHeader _fileHeader;
        readonly ImageType _imageType;
        readonly OptionalHeader32 _optionalHeader32;
        readonly OptionalHeader64 _optionalHeader64;
        readonly DataDirectoryInfo[] _dataDirectories;
        readonly SectionHeader[] _sectionHeaders;
        readonly CliHeader _cliHeader;
        readonly MetadataRoot _metadataRoot;
        readonly MetadataStreamHeader[] _metadataStreamHeaders;
        readonly TableStream _tableStream;
        readonly StringHeap _stringHeap;
        readonly UserStringHeap _userStringHeap;
        readonly GuidHeap _guidHeap;
        readonly BlobHeap _blobHeap;
        readonly MethodEntry[] _methodEntries;

                
        /// <summary>
        /// Creates a new immutable instance of this class.
        /// </summary>
        /// <param name="filePath">A file path the points to a PE file.</param>
        public PortableExecutable(string filePath)
        {
            if (filePath == null)
                throw new ArgumentNullException("filePath");
            var data = File.ReadAllBytes(filePath);
            if (data.Length == 0)
                throw new ArgumentException("ZeroLengthFile");
            var handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            try
            {
                var ptr = handle.AddrOfPinnedObject();
                _dosHeader = (DosHeader)Marshal.PtrToStructure(ptr, typeof(DosHeader));
                if (_dosHeader.e_magic != DosHeader.MagicConstant)
                    throw new ArgumentException("InvalidDosHeader");
                ptr += (int)_dosHeader.e_lfanew;
                _signature = (Signature)Marshal.PtrToStructure(ptr, typeof(Signature));
                if (_signature.Value != Signature.Constant)
                    throw new ArgumentException("InvalidSignature");
                ptr += Marshal.SizeOf(typeof(Signature));
                _fileHeader = (FileHeader)Marshal.PtrToStructure(ptr, typeof(FileHeader));
                if (_fileHeader.SectionCount <= 0)
                    throw new ArgumentException("InvalidSectionCount");
                ptr += Marshal.SizeOf(typeof(FileHeader));
                _sectionHeaders = new SectionHeader[_fileHeader.SectionCount];
                if (_fileHeader.OptionalHeaderSize != 0)
                {
                    _imageType = (ImageType)Marshal.PtrToStructure(ptr, typeof(ushort));
                    ptr += Marshal.SizeOf(typeof(ushort));
                    switch (_imageType)
                    {
                        case ImageType.PE32:
                            _optionalHeader32 = (OptionalHeader32)Marshal.PtrToStructure(ptr, typeof(OptionalHeader32));
                            ptr += Marshal.SizeOf(typeof(OptionalHeader32));
                            _dataDirectories = new DataDirectoryInfo[_optionalHeader32.DataDirectoryCount];
                            break;
                        case ImageType.PE64:
                            _optionalHeader64 = (OptionalHeader64)Marshal.PtrToStructure(ptr, typeof(OptionalHeader64));
                            ptr += Marshal.SizeOf(typeof(OptionalHeader64));
                            _dataDirectories = new DataDirectoryInfo[_optionalHeader64.DataDirectoryCount];
                            break;
                        default:
                            throw new ArgumentException("UnknownImageType");
                    }
                    var dataDirectorySize = Marshal.SizeOf(typeof(DataDirectoryInfo));
                    for (int i = 0; i < _dataDirectories.Length; i++)
                    {
                        _dataDirectories[i] = (DataDirectoryInfo)Marshal.PtrToStructure(ptr, typeof(DataDirectoryInfo));
                        ptr += dataDirectorySize;
                    }
                }
                var sectionHeaderSize = Marshal.SizeOf(typeof(SectionHeader));
                for (int i = 0; i < _sectionHeaders.Length; i++)
                {
                    _sectionHeaders[i] = (SectionHeader)Marshal.PtrToStructure(ptr, typeof(SectionHeader));
                    ptr += sectionHeaderSize;
                }

                if (_dataDirectories[(int)DataDirectoryType.CliHeader].Exists)
                {
                    var cliHeaderOffset = FindDataDirectoryOffset(DataDirectoryType.CliHeader);
                    ptr = handle.AddrOfPinnedObject() + cliHeaderOffset;
                    _cliHeader = (CliHeader)Marshal.PtrToStructure(ptr, typeof(CliHeader));

                    var metaDataRootOffset = FindRvaOffset((int)_cliHeader.MetadataRVA);
                    using (var ms = new MemoryStream(data))
                    using (var r = new BinaryReader(ms, Encoding.Default, true))
                    {
                        ms.Seek(metaDataRootOffset, SeekOrigin.Begin);
                        _metadataRoot.Read(r);
                        _metadataStreamHeaders = new MetadataStreamHeader[_metadataRoot.Streams];
                        for (int i = 0; i < _metadataStreamHeaders.Length; i++)
                        {
                            _metadataStreamHeaders[i].Read(r);
                        }

                        foreach (var sh in _metadataStreamHeaders)
                        {
                            ms.Seek(metaDataRootOffset, SeekOrigin.Begin);
                            if (sh.Name == "#~")
                            {
                                _tableStream = new TableStream();
                                ms.Seek(sh.Offset, SeekOrigin.Current);
                                _tableStream.Read(r, sh);
                            }
                            else if (sh.Name == "#Strings")
                            {
                                ms.Seek(sh.Offset, SeekOrigin.Current);
                                _stringHeap = new StringHeap(r.ReadBytes((int)sh.Size));
                            }
                            else if (sh.Name == "#US")
                            {
                                _userStringHeap = new UserStringHeap();
                                ms.Seek(sh.Offset, SeekOrigin.Current);
                                var limit = ms.Position + sh.Size;
                                while (ms.Position < limit)
                                {
                                    var size = (int)r.ReadCompressedUInt32();
                                    var d = r.ReadBytes(size);
                                    _userStringHeap.Add(Encoding.Unicode.GetString(d, 0, Math.Max(d.Length - 1, 0)));
                                }
                            }
                            else if (sh.Name == "#Blob")
                            {
                                ms.Seek(sh.Offset, SeekOrigin.Current);
                                _blobHeap = new BlobHeap(r.ReadBytes((int)sh.Size));
                            }
                            else if (sh.Name == "#GUID")
                            {
                                _guidHeap = new GuidHeap();
                                ms.Seek(sh.Offset, SeekOrigin.Current);
                                var limit = ms.Position + sh.Size;
                                while (ms.Position < limit)
                                {
                                    var d = r.ReadBytes(16);
                                    _guidHeap.Add(new Guid(d));
                                }
                            }
                        }

                        var methodDefTable = _tableStream.MethodDefTable;


                        _methodEntries = new MethodEntry[methodDefTable.Count];
                        for (int i = 0; i < _methodEntries.Length; i++)
                        {
                            var methodDef = methodDefTable[i];
                            var name = _stringHeap.Get(methodDef.Name);
                            if (methodDef.Signature > 0)
                            {
                                var blob = _blobHeap.Get(methodDef.Signature);
                                var s = new MethodDefSigBlob(blob);
                                //using (var ms1 = new MemoryStream(blob))
                                //using (var r1 = new BinaryReader(ms1))
                                //{
                                //    var s = new MethodDefSigBlob(r1);
                                //}
                            }
                            if (methodDef.RVA > 0)
                            {
                                var rva = (int)methodDef.RVA;
                                var offset = FindRvaOffset(rva);
                                ms.Seek(offset, SeekOrigin.Begin);
                                _methodEntries[i] = new MethodEntry(rva, r);
                            }
                        }


                    }
                }
            }
            finally
            {
                handle.Free();
            }
        }

        int FindDataDirectoryOffset(DataDirectoryType type)
        {
            var rva = (int)_dataDirectories[(int)type].RVA;
            return FindRvaOffset(rva);
        }

        int FindRvaOffset(int rva)
        {
            var sectionCount = _sectionHeaders.Length;
            var index = 0;
            var section = default(SectionHeader);

            while (index++ < sectionCount)
            {
                section = _sectionHeaders[index - 1];
                if ((section.VirtualAddress <= rva) && (section.VirtualAddress + section.RawDataSize > rva))
                    break;
            }

            if (index > sectionCount)
                return -1;

            var result = rva - (section.VirtualAddress - section.RawDataPointer);
            return (int)result;
        }
    }
}