using System;
using System.IO;
using BoldAspect.CLI.Metadata;

namespace BoldAspect.CLI.Metadata
{
    class ConstantTable : Table<ConstantRecord>
    {
        public ConstantTable()
            : base(TableID.Constant)
        {

        }
    }

    struct ConstantRecord : IMetadataRecord
    {
        [ConstantColumn(typeof(ushort))]
        public ushort Type;

        [CodedIndex(typeof(HasConstant))]
        public uint Parent;

        [BlobHeapIndex]
        public uint Value;

        void IMetadataRecord.Read(BinaryReader reader, TableStream stream)
        {

        }
    }
}