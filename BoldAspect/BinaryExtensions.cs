using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace BoldAspect
{
    static class BinaryExtensions
    {
        public static byte[] GetBigEndianBytes(uint value)
        {
            var result = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                result[3 - i] = (byte)(value & 0x000000FF);
                value >>= 8;
            }
            return result;
        }

        public static uint ReadCompressedUInt32(this byte[] data, int offset, out int length)
        {
            Debug.Assert(data != null);
            Debug.Assert(offset < data.Length);
            length = 1;
            var b1 = data[offset];
            if ((b1 & 0x80) == 0)
                return (uint)b1;
            Debug.Assert(offset + 1 < data.Length);
            length = 2;
            if ((b1 & 0x40) == 0)
                return ((uint)(b1 & ~0x80) << 8) 
                    | (uint)data[offset + 1];
            Debug.Assert(offset + 3 < data.Length);
            length = 4;
            return ((uint)(b1 & ~0xC0) << 24) 
                | ((uint)data[offset + 1] << 16) 
                | ((uint)data[offset + 2] << 8) 
                | (uint)data[offset + 3];
        }
    }
}