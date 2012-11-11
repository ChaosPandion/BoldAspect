using System;
using System.Collections.ObjectModel;
using System.IO;



namespace BoldAspect.CLI
{
    [Flags]
    public enum FieldAttributes : ushort
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


    public sealed class FieldCollection : Collection<IField>
    {

    }

    public interface IField
    {
        FieldAttributes Flags { get; set; }
        string Name { get; set; }
        ITypeRef DeclaringType { get; set; }
        FieldSignature Signature { get; set; }
        IModule DeclaringModule { get; set; }
    }

    public sealed class CLIField : IField
    {
        public FieldAttributes Flags { get; set; }
        public string Name { get; set; }
        public ITypeRef DeclaringType { get; set; }
        public FieldSignature Signature { get; set; }
        public IModule DeclaringModule { get; set; }

        public override string ToString()
        {
            return Name ?? "";
        }
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