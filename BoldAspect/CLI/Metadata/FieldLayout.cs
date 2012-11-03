using System;
using System.IO;

namespace BoldAspect.CLI.Metadata
{
    class FieldLayoutTable : Table<FieldLayoutRecord>
    {
        public FieldLayoutTable()
            : base(TableID.FieldLayout)
        {

        }
    }

    struct FieldLayoutRecord : IMetadataRecord
    {
        [ConstantColumn(typeof(uint))]
        public uint Offset;

        [SimpleIndex(TableID.Field)]
        public uint Field;

        void IMetadataRecord.Read(BinaryReader reader, TableStream stream)
        {

        }
    }
}