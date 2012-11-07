using System;
using System.IO;

namespace BoldAspect.CLI.Metadata
{
    class AssemblyRefTable : Table<AssemblyRefRecord>
    {
        public AssemblyRefTable()
            : base(TableID.AssemblyRef)
        {

        }
    }

    struct AssemblyRefRecord
    {
        [ConstantColumn(typeof(ushort))]
        public ushort MajorVersion;

        [ConstantColumn(typeof(ushort))]
        public ushort MinorVersion;

        [ConstantColumn(typeof(ushort))]
        public ushort BuildNumber;

        [ConstantColumn(typeof(ushort))]
        public ushort RevisionNumber;

        [ConstantColumn(typeof(uint))]
        public uint Flags;

        [BlobHeapIndex]
        public uint PublicKeyOrToken;

        [StringHeapIndex]
        public uint Name;

        [StringHeapIndex]
        public uint Culture;

        [BlobHeapIndex]
        public uint HashValue;
    }

    class AssemblyRefOSTable : Table<AssemblyRefOSRecord>
    {
        public AssemblyRefOSTable()
            : base(TableID.AssemblyRefOS)
        {

        }
    }

    struct AssemblyRefOSRecord
    {
        [ConstantColumn(typeof(uint))]
        public uint OSPlatformID;

        [ConstantColumn(typeof(uint))]
        public uint OSMajorVersion;

        [ConstantColumn(typeof(uint))]
        public uint OSMinorVersion;

        [SimpleIndex(TableID.AssemblyRef)]
        public uint AssemblyRef;
    }

    class AssemblyRefProcessorTable : Table<AssemblyRefProcessorRecord>
    {
        public AssemblyRefProcessorTable()
            : base(TableID.AssemblyRefProcessor)
        {

        }
    }

    struct AssemblyRefProcessorRecord
    {
        [ConstantColumn(typeof(uint))]
        public uint Processor;

        [SimpleIndex(TableID.AssemblyRef)]
        public uint AssemblyRef;
    }
}