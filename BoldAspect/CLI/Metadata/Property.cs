using System;
using System.IO;

namespace BoldAspect.CLI.Metadata
{
    class PropertyTable : Table<PropertyRecord>
    {
        public PropertyTable()
            : base(TableID.Property)
        {

        }
    }

    struct PropertyRecord
    {
        [ConstantColumn(typeof(PropertyAttributes))]
        public PropertyAttributes Flags;

        [StringHeapIndex]
        public uint Name;

        [BlobHeapIndex]
        public uint Type;
    }
}