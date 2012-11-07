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

    struct ClassLayoutRecord
    {
        [ConstantColumn(typeof(ushort))]
        public ushort PackingSize;

        [ConstantColumn(typeof(uint))]
        public uint ClassSize;

        [SimpleIndex(TableID.TypeDef)]
        public uint Parent;
    }
}