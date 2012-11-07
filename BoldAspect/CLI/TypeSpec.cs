using System;
using System.IO;

namespace BoldAspect.CLI.Metadata
{
    class TypeSpecTable : Table<TypeSpecRecord>
    {
        public TypeSpecTable()
            : base(TableID.TypeSpec)
        {

        }
    }

    struct TypeSpecRecord
    {
        [BlobHeapIndex]
        public uint Signature;
    }
}