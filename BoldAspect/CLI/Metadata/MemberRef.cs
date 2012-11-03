using System;
using System.IO;
using BoldAspect.CLI.Metadata;

namespace BoldAspect.CLI.Metadata
{
    class MemberRefTable : Table<MemberRefRecord>
    {
        public MemberRefTable()
            : base(TableID.MemberRef)
        {

        }
    }

    struct MemberRefRecord : IMetadataRecord
    {
        [CodedIndex(typeof(MemberRefParent))]
        public uint Class;

        [StringHeapIndex]
        public uint Name;

        [BlobHeapIndex]
        public uint Signature;

        void IMetadataRecord.Read(BinaryReader reader, TableStream stream)
        {

        }
    }
}