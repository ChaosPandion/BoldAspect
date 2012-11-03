
using System;
using System.IO;

namespace BoldAspect.CLI.Metadata
{

    class TypeDefTable : Table<TypeDefRecord>
    {
        public TypeDefTable()
            : base(TableID.TypeDef)
        {

        }
    }







 

    struct TypeDefRecord : IMetadataRecord
    {
        [ConstantColumn(typeof(TypeAttributes))]
        public TypeAttributes Flags;

        [StringHeapIndex]
        public uint TypeName;

        [StringHeapIndex]
        public uint TypeNameSpace;

        [CodedIndex(typeof(TypeDefOrRef))]
        public uint Extends;

        [SimpleIndex(TableID.Field)]
        public uint FieldList;

        [SimpleIndex(TableID.MethodDef)]
        public uint MethodList;

        public void Read(BinaryReader reader, TableStream stream)
        {
            Flags = (TypeAttributes)reader.ReadUInt32();
            if (stream.StringHeapIsWide)
            {
                TypeName = reader.ReadUInt32();
                TypeNameSpace = reader.ReadUInt32();
            }
            else
            {
                TypeName = reader.ReadUInt16();
                TypeNameSpace = reader.ReadUInt16();
            }
            var c = stream.GetRowCount(TableID.TypeRef);
            Extends = reader.ReadUInt16();
            FieldList = reader.ReadUInt16();
            MethodList = reader.ReadUInt16();
        }
    }
}