using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect
{
    public struct Slice
    {
        public readonly byte[] Data;
        public readonly int Offset;
        public readonly int Length;

        public Slice(byte[] data, int offset, int length)
        {
            Data = data;
            Offset = offset;
            Length = length;
        }
    }
}
