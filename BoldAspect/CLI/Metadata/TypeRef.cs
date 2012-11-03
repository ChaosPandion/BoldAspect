using System.IO;

namespace BoldAspect.CLI.Metadata
{
    class TypeRefTable : Table<TypeRefRecord>
    {
        public TypeRefTable()
            : base(TableID.TypeRef)
        {

        }
    }

    struct TypeRefRecord : IMetadataRecord
    {
        [CodedIndex(typeof(ResolutionScope))]
        public uint ResolutionScope;

        [StringHeapIndex]
        public uint TypeName;

        [StringHeapIndex]
        public uint TypeNameSpace;

        public void Read(BinaryReader reader, TableStream stream)
        {

        }
    }
}