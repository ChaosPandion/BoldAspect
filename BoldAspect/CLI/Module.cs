using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.InteropServices;




namespace BoldAspect.CLI
{
    public interface IModule
    {
        string Name { get; set; }
        Guid Guid { get; set; }
        ParamCollection DefinedParams { get; } 
        TypeCollection DefinedTypes { get; }
        MethodCollection DefinedMethods { get; }
        IAssembly Assembly { get; set; }
        
    }

    public sealed class CLIModule : IModule
    {
        private readonly ParamCollection _definedParams = new ParamCollection();
        private readonly TypeCollection _definedTypes = new TypeCollection();
        private readonly MethodCollection _definedMethods = new MethodCollection();

        public string Name { get; set; }
        public Guid Guid { get; set; }

        public ParamCollection DefinedParams
        {
            get { return _definedParams; }
        }

        public TypeCollection DefinedTypes 
        {
            get { return _definedTypes; }
        }

        public MethodCollection DefinedMethods
        {
            get { return _definedMethods; }
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