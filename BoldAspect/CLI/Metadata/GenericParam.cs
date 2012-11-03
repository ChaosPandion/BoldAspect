using System;
using System.IO;

namespace BoldAspect.CLI.Metadata
{
    class GenericParamTable : Table<GenericParamRecord>
    {
        public GenericParamTable()
            : base(TableID.GenericParam)
        {

        }
    }

    struct GenericParamRecord : IMetadataRecord
    {
        [ConstantColumn(typeof(ushort))]
        public ushort Number;

        [ConstantColumn(typeof(GenericParamAttributes))]
        public GenericParamAttributes Flags;

        [CodedIndex(typeof(TypeOrMethodDef))]
        public uint Owner;

        [StringHeapIndex]
        public uint Name;

        void IMetadataRecord.Read(BinaryReader reader, TableStream stream)
        {

        }
    }
}