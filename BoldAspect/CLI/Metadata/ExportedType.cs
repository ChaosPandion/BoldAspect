using System;
using System.IO;

namespace BoldAspect.CLI.Metadata
{
    class ExportedTypeTable : Table<ExportedTypeRecord>
    {
        public ExportedTypeTable()
            : base(TableID.ExportedType)
        {

        }
    }

    struct ExportedTypeRecord : IMetadataRecord
    {
        [ConstantColumn(typeof(uint))]
        public uint Flags;

        [ConstantColumn(typeof(uint))]
        public uint TypeDefId;

        [StringHeapIndex]
        public uint TypeName;

        [StringHeapIndex]
        public uint TypeNameSpace;

        [CodedIndex(typeof(Implementation))]
        public uint Implementation;


        void IMetadataRecord.Read(BinaryReader reader, TableStream stream)
        {

        }
    }
}