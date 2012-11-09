using System;
using System.IO;
using BoldAspect.CLI.Metadata;

namespace BoldAspect.CLI.Metadata
{
    [Flags]
    public enum ParamAttributes : ushort
    {
        In = 0x0001,
        Out = 0x0002,
        Optional = 0x0010,
        HasDefault = 0x1000,
        HasFieldMarshal = 0x2000,
        Unused = 0xcfe0,
    }

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