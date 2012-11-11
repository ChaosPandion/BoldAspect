using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ArrayShape
    {
        public readonly int Rank;
        public readonly int NumSizes;
        public readonly int Size;
        public readonly int NumLowBounds;
        public readonly int LowBound;

        public ArrayShape(int rank, int numSizes, int size, int numLowBounds, int lowBound)
        {
            Rank = rank;
            NumSizes = numSizes;
            Size = size;
            NumLowBounds = numLowBounds;
            LowBound = lowBound;
        }
    }
}