using System;
using System.IO;

namespace BoldAspect.CLI.Metadata
{
    class ClassLayoutTable : Table<ClassLayoutRecord>
    {
        public ClassLayoutTable()
            : base(TableID.ClassLayout)
        {

        }
    }

    struct ClassLayoutRecord : IMetadataRecord
    {
        [ConstantColumn(typeof(ushort))]
        public ushort PackingSize;

        [ConstantColumn(typeof(uint))]
        public uint ClassSize;

        [SimpleIndex(TableID.TypeDef)]
        public uint Parent;

        void IMetadataRecord.Read(BinaryReader reader, TableStream stream)
        {

        }
    }
}