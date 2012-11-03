using System;
using System.IO;

namespace BoldAspect.CLI.Metadata
{
    class ModuleRefTable : Table<ModuleRefRecord>
    {
        public ModuleRefTable()
            : base(TableID.ModuleRef)
        {

        }
    }

    struct ModuleRefRecord : IMetadataRecord
    {
        [StringHeapIndex]
        public uint Name;

        void IMetadataRecord.Read(BinaryReader reader, TableStream stream)
        {

        }
    }
}