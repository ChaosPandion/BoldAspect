using System;
using System.IO;

namespace BoldAspect.CLI.Metadata
{
    class MethodImplTable : Table<MethodImplRecord>
    {
        public MethodImplTable()
            : base(TableID.MethodImpl)
        {

        }
    }

    struct MethodImplRecord 
    {
        [SimpleIndex(TableID.TypeDef)]
        public uint Class;

        [CodedIndex(typeof(MethodDefOrRef))]
        public uint MethodBody;

        [CodedIndex(typeof(MethodDefOrRef))]
        public uint MethodDeclaration;
    }
}