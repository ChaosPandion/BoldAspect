using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI.Metadata
{
    sealed class BlobHeapIndexAttribute : ColumnAttribute
    {
        public override ulong GetIndex(BinaryReader reader, TableStream stream)
        {
            if (stream.BlobHeapIsWide)
            {
                return reader.ReadUInt32();
            }
            return reader.ReadUInt16();
        }
    }
}
