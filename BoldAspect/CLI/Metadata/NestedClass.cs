using System;
using System.IO;

namespace BoldAspect.CLI.Metadata
{
    class NestedClassTable : Table<NestedClassRecord>
    {
        public NestedClassTable()
            : base(TableID.NestedClass)
        {

        }
    }

    struct NestedClassRecord : IMetadataRecord
    {
        [SimpleIndex(TableID.TypeDef)]
        public uint NestedClass;

        [SimpleIndex(TableID.TypeDef)]
        public uint EnclosingClass;

        void IMetadataRecord.Read(BinaryReader reader, TableStream stream)
        {

        }
    }
}