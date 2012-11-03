using System;
using System.IO;

namespace BoldAspect.CLI.Metadata
{
    class FieldRVATable : Table<FieldRVARecord>
    {
        public FieldRVATable()
            : base(TableID.FieldRVA)
        {

        }
    }

    struct FieldRVARecord : IMetadataRecord
    {
        [ConstantColumn(typeof(uint))]
        public uint RVA;

        [SimpleIndex(TableID.Field)]
        public uint Field;

        void IMetadataRecord.Read(BinaryReader reader, TableStream stream)
        {

        }
    }
}