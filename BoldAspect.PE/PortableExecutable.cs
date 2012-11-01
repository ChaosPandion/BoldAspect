using System;
using System.Runtime.InteropServices;

namespace BoldAspect.PE
{
    /// <summary>
    /// Represents a Microsoft Portable Executable image.
    /// </summary>
    public sealed class PortableExecutable
    {
        private readonly byte[] _data;
        private readonly DosHeader _dosHeader;
        private readonly Signature _signature;
        private readonly FileHeader _fileHeader;
        private readonly ImageType _imageType;
        private readonly OptionalHeader32 _optionalHeader32;
        private readonly OptionalHeader64 _optionalHeader64;
        private readonly SectionHeader[] _sections;
                
        /// <summary>
        /// Creates a new immutable instance of this class.
        /// </summary>
        /// <param name="getData">A delegate that returns an array of bytes that will be encapsulated.</param>
        public PortableExecutable(Func<byte[]> getData)
        {
            if (getData == null)
                throw new ArgumentNullException("getData");
            _data = getData();
            if (_data == null)
                throw new ArgumentException("NullInputArray");
            if (_data.Length == 0)
                throw new ArgumentException("ZeroLengthInputArray");
            var handle = GCHandle.Alloc(_data, GCHandleType.Pinned);
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
                _sections = new SectionHeader[_fileHeader.SectionCount];
                if (_fileHeader.OptionalHeaderSize != 0)
                {
                    _imageType = (ImageType)Marshal.PtrToStructure(ptr, typeof(ushort));
                    ptr += Marshal.SizeOf(typeof(ushort));
                    switch (_imageType)
                    {
                        case ImageType.PE32:
                            _optionalHeader32 = (OptionalHeader32)Marshal.PtrToStructure(ptr, typeof(OptionalHeader32));
                            ptr += Marshal.SizeOf(typeof(OptionalHeader32));
                            break;
                        case ImageType.PE64:
                            _optionalHeader64 = (OptionalHeader64)Marshal.PtrToStructure(ptr, typeof(OptionalHeader64));
                            ptr += Marshal.SizeOf(typeof(OptionalHeader64));
                            break;
                        default:
                            throw new ArgumentException("UnknownImageType");
                    }
                }
                var sectionHeaderSize = Marshal.SizeOf(typeof(SectionHeader));
                for (int i = 0; i < _fileHeader.SectionCount; i++)
                {
                    _sections[i] = (SectionHeader)Marshal.PtrToStructure(ptr, typeof(SectionHeader));
                    ptr += sectionHeaderSize;
                }
            }
            finally
            {
                handle.Free();
            }
        }
    }
}