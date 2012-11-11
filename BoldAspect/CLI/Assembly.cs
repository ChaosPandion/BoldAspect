using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;


namespace BoldAspect.CLI
{


    [Flags]
    public enum AssemblyFlags : uint
    {
        None = 0x0000,
        PublicKey = 0x0001,
        Retargetable = 0x0100,
        DisableJITcompileOptimizer = 0x4000,
        EnableJITcompileTracking = 0x8000,
    }

    public enum AssemblyHashAlgorithm : uint
    {
        None = 0x0000,
        MD5 = 0x8003,
        SHA1 = 0x8004
    }

    public interface IAssembly
    {
        string Name { get; set; }
        AssemblyFlags Flags { get; set; }
        Version Version { get; set; }
        CultureInfo Culture { get; set; }
        AssemblyHashAlgorithm HashAlgorithm { get; set; }
        Blob PublicKey { get; set; }
        IModule ManifestModule { get; set; }
        AssemblyRefCollection AssemblyReferences { get; }
        ModuleRefCollection ModuleReferences { get; }
        TypeRefCollection TypeReferences { get; }
    }

    public sealed class CLIAssembly : IAssembly
    {
        private readonly AssemblyRefCollection _assemblyRefs = new AssemblyRefCollection();
        private readonly ModuleRefCollection _moduleRefs = new ModuleRefCollection();
        private readonly TypeRefCollection _typeRefs = new TypeRefCollection();

        public string Name { get; set; }
        public AssemblyFlags Flags { get; set; }
        public Version Version { get; set; }
        public CultureInfo Culture { get; set; }
        public AssemblyHashAlgorithm HashAlgorithm { get; set; }
        public Blob PublicKey { get; set; }
        public IModule ManifestModule { get; set; }
        public AssemblyRefCollection AssemblyReferences { get { return _assemblyRefs; } }
        public ModuleRefCollection ModuleReferences { get { return _moduleRefs; } }
        public TypeRefCollection TypeReferences { get { return _typeRefs; } }

        public override string ToString()
        {
            return Name;
        }
    }

    public interface IAssemblyRef
    {
        Version Version { get; set; }
        AssemblyFlags Flags { get; set; }
        Blob PublicKeyOrToken { get; set; }
        string Name { get; set; }
        CultureInfo Culture { get; set; }
        Blob HashValue { get; set; }
    }

    public sealed class CLIAssemblyRef : IAssemblyRef
    {
        public Version Version { get; set; }
        public AssemblyFlags Flags { get; set; }
        public Blob PublicKeyOrToken { get; set; }
        public string Name { get; set; }
        public CultureInfo Culture { get; set; }
        public Blob HashValue { get; set; }

        public override string ToString()
        {
            return string.Format(
                "{0}, Version={1}, Culture={2}, PublicKeyToken={3}",
                Name,
                Version,
                Culture == null || Culture.Name == "" 
                    ? "neutral"
                    : Culture.IetfLanguageTag, 
                PublicKeyOrToken.Length > 0
                    ? BitConverter.ToString(PublicKeyOrToken.ToArray()).Replace("-", "").ToLower()
                    : "null");
        }
    }

    public sealed class AssemblyRefCollection : Collection<IAssemblyRef>
    {

    }
}