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

    struct AssemblyRefRecord : IMetadataRecord
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

        void IMetadataRecord.Read(BinaryReader reader, TableStream stream)
        {

        }
    }
}