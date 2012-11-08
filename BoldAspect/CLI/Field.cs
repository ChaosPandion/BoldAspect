using System;
using System.IO;
using BoldAspect.CLI.Metadata;


namespace BoldAspect.CLI.Metadata
{
    [Flags]
    enum FieldAttributes : ushort
    {
        FieldAccessMask = 0x0007,
        CompilerControlled = 0x0000,
        Private = 0x0001,
        FamANDAssem = 0x0002,
        Assembly = 0x0003,
        Family = 0x0004,
        FamORAssem = 0x0005,
        Public = 0x0006,

        Static = 0x0010,
        InitOnly = 0x0020,
        Literal = 0x0040,
        NotSerialized = 0x0080,
        SpecialName = 0x0200,
        PInvokeImpl = 0x2000,
        RTSpecialName = 0x0400,
        HasFieldMarshal = 0x1000,
        HasDefault = 0x8000,
        HasFieldRVA = 0x0100,
    }

    class FieldTable : Table<FieldRecord>
    {
        public FieldTable()
            : base(TableID.Field)
        {

        }
    }

    struct FieldRecord 
    {
        [ConstantColumn(typeof(FieldAttributes))]
        public FieldAttributes Flags;

        [StringHeapIndex]
        public uint Name;

        [BlobHeapIndex]
        public uint Signature;
    }
    class FieldLayoutTable : Table<FieldLayoutRecord>
    {
        public FieldLayoutTable()
            : base(TableID.FieldLayout)
        {

        }
    }

    struct FieldLayoutRecord
    {
        [ConstantColumn(typeof(uint))]
        public uint Offset;

        [SimpleIndex(TableID.Field)]
        public uint Field;
    }

    class FieldMarshalTable : Table<FieldMarshalRecord>
    {
        public FieldMarshalTable()
            : base(TableID.FieldMarshal)
        {

        }
    }

    struct FieldMarshalRecord
    {
        [CodedIndex(typeof(HasFieldMarshal))]
        public uint Parent;

        [BlobHeapIndex]
        public uint NativeType;
    }

    class FieldRVATable : Table<FieldRVARecord>
    {
        public FieldRVATable()
            : base(TableID.FieldRVA)
        {

        }
    }

    struct FieldRVARecord
    {
        [ConstantColumn(typeof(uint))]
        public uint RVA;

        [SimpleIndex(TableID.Field)]
        public uint Field;
    }
}