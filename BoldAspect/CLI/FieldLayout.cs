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

    struct FieldLayoutRecord
    {
        [ConstantColumn(typeof(uint))]
        public uint Offset;

        [SimpleIndex(TableID.Field)]
        public uint Field;
    }
}