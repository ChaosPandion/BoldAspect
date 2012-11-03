using System;
using System.IO;
using BoldAspect.CLI.Metadata;

namespace BoldAspect.CLI.Metadata
{

    class ParamTable : Table<ParamRecord>
    {
        public ParamTable()
            : base(TableID.Param)
        {

        }
    }

    struct ParamRecord
    {
        [ConstantColumn(typeof(ParamAttributes))]
        public ParamAttributes Flags;

        [ConstantColumn(typeof(ushort))]
        public ushort Sequence;

        [StringHeapIndex]
        public uint Name;
    }
}