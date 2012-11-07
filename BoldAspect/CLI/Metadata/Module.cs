using System;
using System.IO;
using System.Runtime.InteropServices;
using BoldAspect.CLI.Metadata;

namespace BoldAspect.CLI.Metadata
{
    class ModuleTable : Table<ModuleRecord>
    {
        public ModuleTable()
            : base(TableID.Module)
        {

        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct ModuleRecord
    {
        [ConstantColumn(typeof(ushort))]
        public ushort Generation;

        [StringHeapIndex]
        public uint Name;

        [GuidHeapIndex]
        public uint Mvid;

        [GuidHeapIndex]
        public uint EncId;

        [GuidHeapIndex]
        public uint EncBaseId;
    }
}