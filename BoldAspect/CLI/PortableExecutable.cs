using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace BoldAspect.CLI
{
    /// <summary>
    /// Represents a Microsoft Portable Executable image.
    /// </summary>
    public sealed class PortableExecutable
    {
        public const uint SignatureConstant = 0x4550;
        
        readonly byte[] _data;
        readonly DosHeader _dosHeader;
        readonly uint _signature;
        readonly FileHeader _fileHeader;
        readonly ImageType _imageType;
        readonly OptionalHeader32 _optionalHeader32;
        readonly OptionalHeader64 _optionalHeader64;
        readonly DataDirectoryHeader[] _dataDirectories;
        readonly SectionHeader[] _sectionHeaders;
        readonly CliHeader _cliHeader;
        readonly MetadataRoot _metadataRoot;
        readonly ResourceDirectory _resourceDirectory;
                
        /// <summary>
        /// Creates a new immutable instance of this class.
        /// </summary>
        /// <param name="filePath">A file path the points to a PE file.</param>
        public PortableExecutable(string filePath)
        {
            _data = File.ReadAllBytes(filePath);
            using (var br = new BlobReader(_data, 0, _data.Length))
            {
                br.Read(ref _dosHeader);
                br.Seek((int)_dosHeader.e_lfanew);
                br.Read(ref _signature);
                br.Read(ref _fileHeader);
                if (_fileHeader.OptionalHeaderSize > 0)
                {
                    br.Read(ref _imageType);
                    switch (_imageType)
                    {
                        case ImageType.PE32:
                            br.Read(ref _optionalHeader32);
                            br.Read(ref _dataDirectories, (int)_optionalHeader32.DataDirectoryCount);
                            break;
                        case ImageType.PE64:
                            br.Read(ref _optionalHeader64);
                            br.Read(ref _dataDirectories, (int)_optionalHeader64.DataDirectoryCount);
                            break;
                        default:
                            throw new ArgumentException("UnknownImageType");
                    }
                }
                br.Read(ref _sectionHeaders, _fileHeader.SectionCount);

                if (_dataDirectories[(int)DataDirectoryType.CliHeader].Exists)
                {
                    var cliHeaderOffset = FindDataDirectoryOffset(DataDirectoryType.CliHeader);
                    br.Seek(cliHeaderOffset);
                    br.Read(ref _cliHeader);

                    var metaDataRootOffset = FindRvaOffset(_cliHeader.MetadataRVA);
                    _metadataRoot = new MetadataRoot(new Slice(_data, metaDataRootOffset, (int)_cliHeader.MetadataSize));
                }

                var resourceHeader = _dataDirectories[(int)DataDirectoryType.Resource];
                if (resourceHeader.Exists)
                {
                    var offset = FindDataDirectoryOffset(DataDirectoryType.Resource);
                    br.Seek(offset);
                    _resourceDirectory = new ResourceDirectory(br.ReadSlice((int)resourceHeader.Size), this);
                }
            }
        }

        public MetadataRoot MetadataRoot
        {
            get { return _metadataRoot; }
        }


        internal Slice GetSliceByRVA(uint rva)
        {
            var offset = FindRvaOffset(rva);
            return new Slice(_data, offset, _data.Length - offset);
        }


        internal Slice GetResourceData()
        {
            var header = _dataDirectories[(int)DataDirectoryType.Resource];
            var offset = FindRvaOffset(header.RVA);
            return new Slice(_data, offset, _data.Length - offset);
        }

        int FindDataDirectoryOffset(DataDirectoryType type)
        {
            var rva = (uint)_dataDirectories[(int)type].RVA;
            return FindRvaOffset(rva);
        }

        int FindRvaOffset(uint rva)
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