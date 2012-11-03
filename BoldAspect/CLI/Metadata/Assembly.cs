using System;
using System.IO;

namespace BoldAspect.CLI.Metadata
{
    class AssemblyTable : Table<AssemblyRecord>
    {
        public AssemblyTable()
            : base(TableID.Assembly)
        {

        }
    }

    struct AssemblyRecord : IMetadataRecord
    {
        [ConstantColumn(typeof(AssemblyHashAlgorithm))]
        public AssemblyHashAlgorithm HashAlgId;

        [ConstantColumn(typeof(ushort))]
        public ushort MajorVersion;

        [ConstantColumn(typeof(ushort))]
        public ushort MinorVersion;

        [ConstantColumn(typeof(ushort))]
        public ushort BuildNumber;

        [ConstantColumn(typeof(ushort))]
        public ushort RevisionNumber;

        [ConstantColumn(typeof(AssemblyFlags))]
        public AssemblyFlags Flags;

        [BlobHeapIndex]
        public uint PublicKey;

        [StringHeapIndex]
        public uint Name;

        [StringHeapIndex]
        public uint Culture;

        void IMetadataRecord.Read(BinaryReader reader, TableStream stream)
        {

        }
    }
}