using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI.Metadata.MetadataStreams
{
    public sealed class MetadataRoot
    {
        private const int _validSignature = 0x424A5342;
        private const int _validMajorVersion = 1;
        private const int _validMinorVersion = 1;
        private const int _validReserved = 0;
        private const int _validFlags = 0;

        public readonly int _signature;
        public readonly short _majorVersion;
        public readonly short _minorVersion;
        public readonly int _reserved;
        public readonly int _length;
        public readonly string _version;
        public readonly short _flags;
        public readonly short _streams;
        public readonly StreamHeader[] _headers;
        public readonly StringHeapStream _stringHeap;
        public readonly UserStringHeapStream _userStringHeap;
        public readonly BlobHeapStream _blobHeap;
        public readonly GuidHeapStream _guidHeap;
        public readonly TableStream _tables;

        public MetadataRoot(BinaryReader reader)
        {
            var str = reader.BaseStream;
            var start = str.Position;

            _signature = reader.ReadInt32();
            if (_signature != _validSignature)
                throw new MetadataException();
            _majorVersion = reader.ReadInt16();
            if (_majorVersion != _validMajorVersion)
                throw new MetadataException();
            _minorVersion = reader.ReadInt16();
            if (_minorVersion != _validMinorVersion)
                throw new MetadataException();
            _reserved = reader.ReadInt32();
            if (_reserved != _validReserved)
                throw new MetadataException();

            _length = reader.ReadInt32();
            var data = reader.ReadBytes(_length);
            var vIndex = data.Length - 1;
            while (data[vIndex] == 0)
            {
                vIndex--;
            }
            _version = Encoding.UTF8.GetString(data, 0, vIndex + 1);
            _flags = reader.ReadInt16();
            if (_flags != _validFlags)
                throw new MetadataException();
            _streams = reader.ReadInt16();

            _headers = new StreamHeader[_streams];
            for (int i = 0; i < _headers.Length; i++)
                _headers[i] = new StreamHeader(reader);

            for (int i = 0; i < _headers.Length; i++)
            {
                var header = _headers[i];
                str.Seek(start, SeekOrigin.Begin);
                str.Seek(header.Offset, SeekOrigin.Current);
                var streamData = reader.ReadBytes(header.Size);
                switch (header.Name)
                {
                    case "#Strings":
                        _stringHeap = new StringHeapStream(streamData);
                        break;
                    case "#US":
                        _userStringHeap = new UserStringHeapStream(streamData);
                        break;
                    case "#Blob":
                        _blobHeap = new BlobHeapStream(streamData);
                        break;
                    case "#GUID":
                        _guidHeap = new GuidHeapStream(streamData);
                        break;
                    case "#~":
                        _tables = new TableStream(streamData);
                        break;
                    default:
                        throw new MetadataException();
                }
            }
        }



        public IModule GetModule()
        {
            //var schema = _tables.Tables[
            return null;
        }


        public IAssembly GetAssembly()
        {
            return null;
        }
    }
}