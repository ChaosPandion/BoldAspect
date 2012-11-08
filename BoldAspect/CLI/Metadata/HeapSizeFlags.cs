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
        StringHeapIsWide = 0x01,
        GuidHeapIsWide = 0x02,
        BlobHeapIsWide = 0x04,
    }
}