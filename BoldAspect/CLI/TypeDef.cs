
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







 

    struct TypeDefRecord 
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
    }
}