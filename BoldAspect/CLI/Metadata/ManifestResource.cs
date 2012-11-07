using System;
using System.IO;

namespace BoldAspect.CLI.Metadata
{
    class ManifestResourceTable : Table<ManifestResourceRecord>
    {
        public ManifestResourceTable()
            : base(TableID.ManifestResource)
        {

        }
    }

    struct ManifestResourceRecord 
    {
        [ConstantColumn(typeof(uint))]
        public uint Offset;

        [ConstantColumn(typeof(ManifestResourceAttributes))]
        public ManifestResourceAttributes Flags;

        [StringHeapIndex]
        public uint Name;

        [CodedIndex(typeof(Implementation))]
        public uint Implementation;


        
        
    }
}