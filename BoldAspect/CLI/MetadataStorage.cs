using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoldAspect.CLI.Metadata;
using BoldAspect.PE;

namespace BoldAspect.CLI
{
    public static class MetadataStorage
    {
        public static IModule LoadModule(string fileName)
        {
            var pe = new PortableExecutable(fileName);
            using (var tr = pe.MetadataRoot.CreateTableReader(TableID.Module))
            {
                if (!tr.NextRow())
                    throw new Exception();
                var module = new CLIModule();
                tr.ReadColumn<ushort>();
                module.Name = tr.ReadColumn<string>();
                module.Guid = tr.ReadColumn<Guid>();
                ReadDefinedTypes(pe, module);
                module.Assembly = LoadAssembly(pe, module);
                return module;
            }
        }

        public static IAssembly LoadAssembly(string fileName)
        {
            return LoadModule(fileName).Assembly;
        }

        private static IAssembly LoadAssembly(PortableExecutable pe, IModule manifestModule)
        {
            using (var tr = pe.MetadataRoot.CreateTableReader(TableID.Assembly))
            {
                if (!tr.NextRow())
                    throw new Exception();
                var assembly = new CLIAssembly();
                assembly.HashAlgorithm = tr.ReadColumn<AssemblyHashAlgorithm>();
                assembly.Version = new Version(tr.ReadColumn<ushort>(), tr.ReadColumn<ushort>(), tr.ReadColumn<ushort>(), tr.ReadColumn<ushort>());
                assembly.Flags = tr.ReadColumn<AssemblyFlags>();
                assembly.PublicKey = tr.ReadColumn<Slice>();
                assembly.Name = tr.ReadColumn<string>();
                assembly.Culture = CultureInfo.CreateSpecificCulture(tr.ReadColumn<string>());
                assembly.ManifestModule = manifestModule;
                ReadAssemblyRefs(pe, assembly);
                ReadModuleRefs(pe, assembly);
                return assembly;
            }
        }

        private static void ReadAssemblyRefs(PortableExecutable pe, IAssembly assembly)
        {
            using (var tr = pe.MetadataRoot.CreateTableReader(TableID.AssemblyRef))
            {
                while (tr.NextRow())
                {
                    var assemblyRef = new CLIAssemblyRef();
                    assemblyRef.Version = new Version(tr.ReadColumn<ushort>(), tr.ReadColumn<ushort>(), tr.ReadColumn<ushort>(), tr.ReadColumn<ushort>());
                    assemblyRef.Flags = tr.ReadColumn<AssemblyFlags>();
                    assemblyRef.PublicKeyOrToken = tr.ReadColumn<Slice>();
                    assemblyRef.Name = tr.ReadColumn<string>();
                    assemblyRef.Culture = CultureInfo.CreateSpecificCulture(tr.ReadColumn<string>());
                    assemblyRef.HashValue = tr.ReadColumn<Slice>();
                    assembly.AssemblyReferences.Add(assemblyRef);
                }
            }
        }

        private static void ReadModuleRefs(PortableExecutable pe, IAssembly assembly)
        {
            using (var tr = pe.MetadataRoot.CreateTableReader(TableID.ModuleRef))
            {
                while (tr.NextRow())
                {
                    var moduleRef = new CLIModuleRef();
                    moduleRef.Name = tr.ReadColumn<string>();
                    assembly.ModuleReferences.Add(moduleRef);
                }
            }
        }

        private static void ReadDefinedTypes(PortableExecutable pe, IModule module)
        {
            using (var tr = pe.MetadataRoot.CreateTableReader(TableID.TypeDef))
            {
                while (tr.NextRow())
                {
                    var t = new CLIType();
                    t.Attributes = tr.ReadColumn<TypeAttributes>();
                    t.Name = tr.ReadColumn<string>();
                    t.NameSpace = tr.ReadColumn<string>();
                    tr.ReadColumn<uint>();
                    tr.ReadColumn<uint>();
                    tr.ReadColumn<uint>();
                    t.DeclaringModule = module;
                    module.DefinedTypes.Add(t);
                }
            }
        }
    }
}
