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

    struct ModuleRefRecord
    {
        [StringHeapIndex]
        public uint Name;
    }
}