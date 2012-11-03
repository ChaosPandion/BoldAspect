using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI.Metadata
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct MetadataRoot
    {
        public static int ValidSignature = 0x424A5342;
        public static int ValidMajorVersion = 1;
        public static int ValidMinorVersion = 1;
        public static int ValidReserved = 0;
        public static int ValidFlags = 0;
        public static int Size = 16;

        public uint Signature;
        public ushort MajorVersion;
        public ushort MinorVersion;
        public uint Reserved;
        public uint Length;
        public string Version;
        public ushort Flags;
        public ushort Streams;

        public void Read(System.IO.BinaryReader reader)
        {
            Signature = reader.ReadUInt32();
            if (Signature != ValidSignature)
                throw new MetadataException();
            MajorVersion = reader.ReadUInt16();
            if (MajorVersion != ValidMajorVersion)
                throw new MetadataException();
            MinorVersion = reader.ReadUInt16();
            if (MinorVersion != ValidMinorVersion)
                throw new MetadataException();
            Reserved = reader.ReadUInt32();
            if (Reserved != ValidReserved)
                throw new MetadataException();
            Length = reader.ReadUInt32();
            var len = (int)Length + (int)Length % 4;
            var data = new byte[len];
            var count = reader.Read(data, 0, len);
            if (count != len)
                throw new MetadataException();
            Version = Encoding.UTF8.GetString(data, 0, len);
            Flags = reader.ReadUInt16();
            if (Flags != ValidFlags)
                throw new MetadataException();
            Streams = reader.ReadUInt16();
        }
    }
}
