using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.InteropServices;




namespace BoldAspect.CLI
{
    //public interface IModule
    //{
    //    string Name { get; set; }
    //    Guid Guid { get; set; }
    //    ParamCollection DefinedParams { get; }
    //    MethodCollection DefinedMethods { get; }
    //    FieldCollection DefinedFields { get; }
    //    TypeCollection DefinedTypes { get; }
    //    IAssembly Assembly { get; set; }
        
    //}

    //public sealed class CLIModule : IModule
    //{
    //    private readonly ParamCollection _definedParams = new ParamCollection();
    //    private readonly MethodCollection _definedMethods = new MethodCollection();
    //    private readonly FieldCollection _definedFields = new FieldCollection();
    //    private readonly TypeCollection _definedTypes = new TypeCollection();

    //    public string Name { get; set; }
    //    public Guid Guid { get; set; }

    //    public ParamCollection DefinedParams
    //    {
    //        get { return _definedParams; }
    //    }

    //    public TypeCollection DefinedTypes 
    //    {
    //        get { return _definedTypes; }
    //    }

    //    public FieldCollection DefinedFields
    //    {
    //        get { return _definedFields; }
    //    }

    //    public MethodCollection DefinedMethods
    //    {
    //        get { return _definedMethods; }
    //    }

    //    public IAssembly Assembly { get; set; }

    //    public override string ToString()
    //    {
    //        return Name ?? "";
    //    }
    //}

    //public interface IModuleRef
    //{
    //    string Name { get; set; }
    //}

    //public sealed class CLIModuleRef : IModuleRef
    //{
    //    public string Name { get; set; }

    //    public override string ToString()
    //    {
    //        return Name ?? "";
    //    }
    //}

    //public sealed class ModuleRefCollection : Collection<IModuleRef>
    //{

    //}
}