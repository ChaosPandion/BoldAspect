using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI
{
    public sealed class ArrayShape
    {
        public int Rank { get; set; }
        public int NumSizes { get; set; }
        public int Size { get; set; }
        public int NumLowBounds { get; set; }
        public int LowBound { get; set; }
    }
}