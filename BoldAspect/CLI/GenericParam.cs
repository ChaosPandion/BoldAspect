using System;
using System.IO;

namespace BoldAspect.CLI
{
    [Flags]
    public enum GenericParamAttributes : ushort
    {
        VarianceMask = 0x0003,
        None = 0x0000,
        Covariant = 0x0001,
        Contravariant = 0x0002,
        SpecialConstraintMask = 0x001C,
        ReferenceTypeConstraint = 0x0004,
        NotNullableValueTypeConstraint = 0x0008,
        DefaultConstructorConstraint = 0x0010,
    }

    class GenericParamTable : Table<GenericParamRecord>
    {
        public GenericParamTable()
            : base(TableID.GenericParam)
        {

        }
    }

    struct GenericParamRecord
    {
        [ConstantColumn(typeof(ushort))]
        public ushort Number;

        [ConstantColumn(typeof(GenericParamAttributes))]
        public GenericParamAttributes Flags;

        [CodedIndex(typeof(TypeOrMethodDef))]
        public uint Owner;

        [StringHeapIndex]
        public uint Name;
    }

    class GenericParamConstraintTable : Table<GenericParamConstraintRecord>
    {
        public GenericParamConstraintTable()
            : base(TableID.GenericParamConstraint)
        {

        }
    }

    struct GenericParamConstraintRecord
    {
        [SimpleIndex(TableID.GenericParam)]
        public uint Owner;

        [CodedIndex(typeof(TypeDefOrRef))]
        public uint Constraint;


    }
}