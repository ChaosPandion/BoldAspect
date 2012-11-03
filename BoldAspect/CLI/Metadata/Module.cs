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

    public interface IModule
    {
        IAssembly Assembly { get; set; }
        string Name { get; set; }
        Guid Mvid { get; set; }
    }

    public class ModuleCLI : IModule
    {
        public IAssembly Assembly { get; set; }
        public string Name { get; set; }
        public Guid Mvid { get; set; }
    }

    public sealed class CliModule
    {
        public CliModule()
        {

        }

        public int Generation { get; set; }
        public string Name { get; set; }
        public Guid Mvid { get; set; }
        public Guid EncId { get; set; }
        public Guid EncBaseId { get; set; }
    }
}