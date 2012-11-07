using System;
using System.IO;

namespace BoldAspect.CLI.Metadata
{
    class InterfaceImplTable : Table<InterfaceImplRecord>
    {
        public InterfaceImplTable()
            : base(TableID.InterfaceImpl)
        {

        }
    }

    struct InterfaceImplRecord 
    {
        [SimpleIndex(TableID.TypeDef)]
        public uint Class;

        [CodedIndex(typeof(TypeDefOrRef))]
        public uint Interface;
    }
}