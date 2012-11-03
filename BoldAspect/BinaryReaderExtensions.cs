using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BoldAspect
{
    static class BinaryReaderExtensions
    {
        public static int ReadCompressedInt32(this BinaryReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");
            var b1 = reader.ReadByte();
            if ((b1 & 0x80) == 0x00)
                return b1;
            var b2 = reader.ReadByte();
            if ((b1 & 0xC0) == 0x00)
                return (int)(b1 << 8) + b2;
            var b3 = reader.ReadByte();
            var b4 = reader.ReadByte();
            return (int)(b1 << 24) + (int)(b2 << 16) + (int)(b3 << 8) + b4;
        }

        public static int ReadCompressedInt32BE(this BinaryReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");
            var b1 = reader.ReadByte();
            if ((b1 & 0x01) == 0x00)
                return b1;
            var b2 = reader.ReadByte();
            if ((b1 & 0x03) == 0x00)
                return (int)(b1 << 8) + b2;
            var b3 = reader.ReadByte();
            var b4 = reader.ReadByte();
            return (int)(b1 << 24) + (int)(b2 << 16) + (int)(b3 << 8) + b4;
        }

        public static uint ReadCompressedUInt32(this BinaryReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");
            var b1 = reader.ReadByte();
            if ((b1 & 0x80) == 0x00)
                return b1;
            var b2 = reader.ReadByte();
            if ((b1 & 0xC0) == 0x00)
                return (uint)(b1 << 8) + b2;
            var b3 = reader.ReadByte();
            var b4 = reader.ReadByte();
            return (uint)(b1 << 24) + (uint)(b2 << 16) + (uint)(b3 << 8) + b4;
        }

        public static uint ReadCompressedUInt32BE(this BinaryReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");
            var b1 = reader.ReadByte();
            if ((b1 & 0x80) == 0x00)
                return b1;
            var b2 = reader.ReadByte();
            if ((b1 & 0xC0) == 0x00)
                return b2 + (uint)(b1 << 8);
            var b3 = reader.ReadByte();
            var b4 = reader.ReadByte();
            return (uint)(b1 << 24) + (uint)(b2 << 16) + (uint)(b3 << 8) + b4;
        }

        public static string ReadNullTerminatedUTF8(this BinaryReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");
            var s = reader.BaseStream;
            var data = new byte[32];
            var cnt = 0;
            while (s.Position != s.Length)
            {
                var b = reader.ReadByte();
                if (b == 0)
                    break;
                if (cnt == data.Length)
                    Array.Resize(ref data, data.Length * 2);
                data[cnt++] = b;
            }
            return Encoding.UTF8.GetString(data, 0, cnt);
        }
    }
}