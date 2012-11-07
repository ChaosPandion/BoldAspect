using System;
using System.IO;

namespace BoldAspect.CLI.Metadata
{
    class MethodSpecTable : Table<MethodSpecRecord>
    {
        public MethodSpecTable()
            : base(TableID.MethodSpec)
        {

        }
    }

    struct MethodSpecRecord
    {
        [CodedIndex(typeof(MethodDefOrRef))]
        public uint Method;

        [BlobHeapIndex]
        public uint Instantiation;
    }
}