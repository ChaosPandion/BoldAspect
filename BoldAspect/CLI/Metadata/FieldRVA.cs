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

    struct FieldRVARecord
    {
        [ConstantColumn(typeof(uint))]
        public uint RVA;

        [SimpleIndex(TableID.Field)]
        public uint Field;
    }
}