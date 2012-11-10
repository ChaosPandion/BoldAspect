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

        public Slice GetSlice(int length)
        {
            return new Slice(Data, Offset, length);
        }

        public Slice GetSlice(int offset, int length)
        {
            return new Slice(Data, Offset + offset, length);
        }

        public BlobReader CreateReader()
        {
            return new BlobReader(this);
        }
    }
}
