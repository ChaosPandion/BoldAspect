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

    struct InterfaceImplRecord : IMetadataRecord
    {
        [SimpleIndex(TableID.TypeDef)]
        public uint Class;

        [CodedIndex(typeof(TypeDefOrRef))]
        public uint Interface;

        void IMetadataRecord.Read(BinaryReader reader, TableStream stream)
        {
            Class = reader.ReadUInt16();
            Interface = reader.ReadUInt16();
        }
    }
}