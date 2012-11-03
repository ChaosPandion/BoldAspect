using System;
using System.IO;

namespace BoldAspect.CLI.Metadata
{
    class MethodSemanticsTable : Table<MethodSemanticsRecord>
    {
        public MethodSemanticsTable()
            : base(TableID.MethodSemantics)
        {

        }
    }

    struct MethodSemanticsRecord : IMetadataRecord
    {
        [ConstantColumn(typeof(MethodSemanticsAttributes))]
        public MethodSemanticsAttributes Semantics;

        [SimpleIndex(TableID.MethodDef)]
        public uint Method;

        [CodedIndex(typeof(HasSemantics))]
        public uint Association;

        void IMetadataRecord.Read(BinaryReader reader, TableStream stream)
        {

        }
    }
}