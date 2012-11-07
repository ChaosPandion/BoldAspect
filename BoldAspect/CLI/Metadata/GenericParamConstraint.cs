using System;
using System.IO;

namespace BoldAspect.CLI.Metadata
{
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