﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI.Metadata
{
    sealed class StringHeapIndexAttribute : ColumnAttribute
    {
        public override ulong GetIndex(BinaryReader reader, BoldAspect.CLI.Metadata.MetadataStreams.TableStream stream)
        {
            if (stream.HeapSizeFlags.HasFlag(HeapSizeFlags.StringHeapIsWide))
            {
                return reader.ReadUInt32();
            }
            return reader.ReadUInt16();
        }
    }
}