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

    struct TypeSpecRecord : IMetadataRecord
    {
        [BlobHeapIndex]
        public uint Signature;

        void IMetadataRecord.Read(BinaryReader reader, TableStream stream)
        {

        }
    }
}