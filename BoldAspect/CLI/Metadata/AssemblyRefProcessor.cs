using System;
using System.IO;

namespace BoldAspect.CLI.Metadata
{
    class AssemblyRefProcessorTable : Table<AssemblyRefProcessorRecord>
    {
        public AssemblyRefProcessorTable()
            : base(TableID.AssemblyRefProcessor)
        {

        }
    }

    struct AssemblyRefProcessorRecord : IMetadataRecord
    {
        [ConstantColumn(typeof(uint))]
        public uint Processor;

        [SimpleIndex(TableID.AssemblyRef)]
        public uint AssemblyRef;

        void IMetadataRecord.Read(BinaryReader reader, TableStream stream)
        {

        }
    }
}