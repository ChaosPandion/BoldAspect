using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using BoldAspect.PE;

namespace BoldAspect.CLI.PE
{
    internal sealed class PeImageReader
    {
        private readonly byte[] _data;
        private readonly DosHeader _dosHeader;
        private readonly Signature _signature;
        private readonly FileHeader _fileHeader;
        private readonly ImageType _imageType;
        private readonly OptionalHeader32 _optionalHeader32;
        private readonly OptionalHeader64 _optionalHeader64;
        private readonly DataDirectoryHeader[] _dataDirectories;
        private readonly SectionHeader[] _sectionHeaders;
        private readonly Section[] _sections;

        public PeImageReader(Stream stream)
        {
            Read(stream, ref _data);

            var handle = GCHandle.Alloc(_data, GCHandleType.Pinned);

            try
            {
                var ptr = handle.AddrOfPinnedObject();

                Read(ref ptr, ref _dosHeader);
                Read(ref ptr, ref _signature);
                Read(ref ptr, ref _fileHeader);
                _sectionHeaders = new SectionHeader[_fileHeader.SectionCount];
                _sections = new Section[_fileHeader.SectionCount];
                Read(ref ptr, ref _imageType);
                switch (_imageType)
                {
                    case ImageType.PE32:
                        Read(ref ptr, ref _optionalHeader32);
                        _dataDirectories = new DataDirectoryHeader[_optionalHeader32.DataDirectoryCount];
                        break;
                    case ImageType.PE64:
                        Read(ref ptr, ref _optionalHeader64);
                        _dataDirectories = new DataDirectoryHeader[_optionalHeader64.DataDirectoryCount];
                        break;
                    default:
                        throw new ArgumentException("UnknownImageType");
                }
                Read(ref ptr, _dataDirectories);
            }
            finally
            {
                handle.Free();
            }
        }

        private void Read(Stream stream, ref byte[] result)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");
            if (!stream.CanRead)
                throw new ArgumentException("The stream cannot be read.", "stream");
            var len = (int)stream.Length;
            var total = 0;
            var count = 0;
            result = new byte[len];
            while (total < len)
            {
                count = stream.Read(result, total, len);
                total += count;
            }
        }

        private void Read(ref IntPtr ptr, ref DosHeader result)
        {
            result = (DosHeader)Marshal.PtrToStructure(ptr, typeof(DosHeader));
            if (result.e_magic != DosHeader.MagicConstant)
                throw new ArgumentException("InvalidDosHeader");
            ptr += (int)result.e_lfanew;
        }

        private void Read(ref IntPtr ptr, ref Signature result)
        {

        }

        private void Read(ref IntPtr ptr, ref FileHeader result)
        {

        }

        private void Read(ref IntPtr ptr, ref ImageType result)
        {

        }

        private void Read(ref IntPtr ptr, ref OptionalHeader32 result)
        {

        }

        private void Read(ref IntPtr ptr, ref OptionalHeader64 result)
        {

        }

        private void Read(ref IntPtr ptr, DataDirectoryHeader[] result)
        {

        }

        private void Read(ref IntPtr ptr, SectionHeader[] result)
        {

        }

        private void Read(ref IntPtr ptr, Section[] result)
        {

        }
    }
}