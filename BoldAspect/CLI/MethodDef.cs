using System;
using System.IO;
using BoldAspect.CLI.Metadata;


namespace BoldAspect.CLI.Metadata
{

    public class MethodDefTable : Table<MethodDefRecord>
    {
        public MethodDefTable()
            : base(TableID.MethodDef)
        {

        }
    }

    public struct MethodDefRecord
    {
        [ConstantColumn(typeof(uint))]
        public uint RVA;

        [ConstantColumn(typeof(MethodImplAttributes))]
        public MethodImplAttributes ImplFlags;

        [ConstantColumn(typeof(MethodAttributes))]
        public MethodAttributes Flags;

        [StringHeapIndex]
        public uint Name;

        [BlobHeapIndex]
        public uint Signature;

        [SimpleIndex(TableID.Param)]
        public uint ParamList;
    }
}