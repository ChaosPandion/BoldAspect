using System;
using System.IO;

namespace BoldAspect.CLI.Metadata
{
    class AssemblyProcessorTable : Table<AssemblyProcessorRecord>
    {
        public AssemblyProcessorTable()
            : base(TableID.AssemblyProcessor)
        {

        }
    }

    struct AssemblyProcessorRecord : IMetadataRecord
    {
        [ConstantColumn(typeof(uint))]
        public uint Processor;

        void IMetadataRecord.Read(BinaryReader reader, TableStream stream)
        {

        }
    }
}