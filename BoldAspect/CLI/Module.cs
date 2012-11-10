using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.InteropServices;
using BoldAspect.CLI.Metadata;
using BoldAspect.CLI.Metadata.MetadataStreams;
using BoldAspect.PE;

namespace BoldAspect.CLI.Metadata
{
    public interface IModule
    {
        string Name { get; set; }
        Guid Guid { get; set; }
        TypeCollection DefinedTypes { get; } 
        IAssembly Assembly { get; set; }
        
    }

    public sealed class CLIModule : IModule
    {
        private readonly TypeCollection _definedTypes = new TypeCollection();

        public string Name { get; set; }
        public Guid Guid { get; set; }

        public TypeCollection DefinedTypes 
        {
            get { return _definedTypes; }
        }

        public IAssembly Assembly { get; set; }

        public override string ToString()
        {
            return Name ?? "";
        }
    }

    public interface IModuleRef
    {
        string Name { get; set; }
    }

    public sealed class CLIModuleRef : IModuleRef
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return Name ?? "";
        }
    }

    public sealed class ModuleRefCollection : Collection<IModuleRef>
    {

    }
}