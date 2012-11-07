using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI.Metadata
{
    [Flags]
    public enum HeapSizeFlags : byte
    {
        BlobHeapIsWide = 0x02,
        GuidHeapIsWide = 0x04,
        StringHeapIsWide = 0x08,
    }
}