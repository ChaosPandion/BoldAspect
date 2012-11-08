using System;
using System.IO;
using System.Runtime.InteropServices;
using BoldAspect.CLI.Metadata;

namespace BoldAspect.CLI.Metadata
{
    public interface IModule
    {
        string Name { get; set; }
        Guid Guid { get; set; }
        IAssembly Assembly { get; set; }
    }

    public sealed class CLIModule : IModule
    {
        public string Name { get; set; }
        public Guid Guid { get; set; }
        public IAssembly Assembly { get; set; }

        public override string ToString()
        {
            return Name ?? "";
        }
    }
}