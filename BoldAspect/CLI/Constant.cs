using System;
using System.IO;
using BoldAspect.CLI.Metadata;

namespace BoldAspect.CLI.Metadata
{
    class ConstantTable : Table<ConstantRecord>
    {
        public ConstantTable()
            : base(TableID.Constant)
        {

        }
    }

    struct ConstantRecord 
    {
        [ConstantColumn(typeof(ushort))]
        public ushort Type;

        [CodedIndex(typeof(HasConstant))]
        public uint Parent;

        [BlobHeapIndex]
        public uint Value;
    }
}