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

    struct NestedClassRecord
    {
        [SimpleIndex(TableID.TypeDef)]
        public uint NestedClass;

        [SimpleIndex(TableID.TypeDef)]
        public uint EnclosingClass;
    }
}