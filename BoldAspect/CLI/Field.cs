using System;
using System.IO;
using BoldAspect.CLI.Metadata;


namespace BoldAspect.CLI.Metadata
{
    class FieldTable : Table<FieldRecord>
    {
        public FieldTable()
            : base(TableID.Field)
        {

        }
    }

    struct FieldRecord 
    {
        [ConstantColumn(typeof(FieldAttributes))]
        public FieldAttributes Flags;

        [StringHeapIndex]
        public uint Name;

        [BlobHeapIndex]
        public uint Signature;
    }
}