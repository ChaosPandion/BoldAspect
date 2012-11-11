using System;
using System.IO;

namespace BoldAspect.CLI
{
    [Flags]
    public enum ManifestResourceAttributes : uint
    {
        VisibilityMask = 0x0007,
        Public = 0x0001,
        Private = 0x0002,
    }

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