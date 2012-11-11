using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;



namespace BoldAspect.CLI
{
    public struct MetadataToken : IEquatable<MetadataToken>, IComparable, IComparable<MetadataToken>
    {
        public readonly uint Value;

        public MetadataToken(uint value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return string.Format("0x{0:X8}", Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj is MetadataToken && Equals((MetadataToken)obj);
        }

        public bool Equals(MetadataToken other)
        {
            return this.Value == other.Value;
        }

        public int CompareTo(object obj)
        {
            return CompareTo((obj as MetadataToken?) ?? default(MetadataToken));
        }

        public int CompareTo(MetadataToken other)
        {
            return this.Value.CompareTo(other.Value);
        }

        public static bool operator ==(MetadataToken left, MetadataToken right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(MetadataToken left, MetadataToken right)
        {
            return !left.Equals(right);
        }
    }


    public static class MetadataStorage
    {
        public static ITypeRef GetTypeRef(Type type)
        {
            var tr = new CLITypeRef();
            tr.Token = new MetadataToken((uint)type.MetadataToken);
            tr.Name = type.Name;
            tr.NameSpace = type.Namespace;
            return tr;
        }

        public static IModule ReadModule(string fileName)
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
                ReadDefinedParams(pe, module);
                ReadDefinedMethods(pe, module);
                ReadDefinedFields(pe, module);


                var start = module.DefinedMethods.Count - 1;
                var stopIndex = module.DefinedParams.Count - 1;
                for (int i = start; i >= 0; --i)
                {
                    var method = (CLIMethod)module.DefinedMethods[i];
                    if (method.ParamListIndex <= 0)
                        continue;
                    var paramStart = (int)method.ParamListIndex - 1;
                    for (int j = paramStart; j < stopIndex; j++)
                    {
                        var param = module.DefinedParams[j];
                        try
                        {
                            param.Type = method.Signature.Params[j - paramStart].TypeSignature.TypeReference;
                        }
                        catch (Exception)
                        {
                            // I hate myself
                        }
                        method.Parameters.Add(param);
                    }
                    stopIndex = paramStart;
                }

                CompleteDefinedTypes(pe, module);

                module.Assembly = ReadAssembly(pe, module);
                return module;
            }
        }

        private static void CompleteDefinedTypes(PortableExecutable pe, IModule module)
        {
            {
                var start = module.DefinedTypes.Count - 1;
                var stopIndex = module.DefinedFields.Count - 1;
                for (int i = start; i >= 0; --i)
                {
                    var type = (CLIType)module.DefinedTypes[i];
                    if (type.FieldListIndex <= 0)
                        continue;
                    var fieldStart = (int)type.FieldListIndex - 1;
                    for (int j = fieldStart; j < stopIndex; j++)
                    {
                        var field = module.DefinedFields[j];
                        type.Fields.Add(field);
                    }
                    stopIndex = fieldStart;
                }
            }
            {
                var start = module.DefinedTypes.Count - 1;
                var stopIndex = module.DefinedMethods.Count - 1;
                for (int i = start; i >= 0; --i)
                {
                    var type = (CLIType)module.DefinedTypes[i];
                    if (type.FieldListIndex <= 0)
                        continue;
                    var methodStart = (int)type.MethodListIndex - 1;
                    for (int j = methodStart; j < stopIndex; j++)
                    {
                        var method = module.DefinedMethods[j];
                        type.Methods.Add(method);
                    }
                    stopIndex = methodStart;
                }
            }
        }

        public static IAssembly ReadAssembly(string fileName)
        {
            return ReadModule(fileName).Assembly;
        }

        private static IAssembly ReadAssembly(PortableExecutable pe, IModule manifestModule)
        {
            using (var tr = pe.MetadataRoot.CreateTableReader(TableID.Assembly))
            {
                if (!tr.NextRow())
                    throw new Exception();
                var assembly = new CLIAssembly();
                assembly.HashAlgorithm = tr.ReadColumn<AssemblyHashAlgorithm>();
                assembly.Version = new Version(tr.ReadColumn<ushort>(), tr.ReadColumn<ushort>(), tr.ReadColumn<ushort>(), tr.ReadColumn<ushort>());
                assembly.Flags = tr.ReadColumn<AssemblyFlags>();
                assembly.PublicKey = tr.ReadColumn<Blob>();
                assembly.Name = tr.ReadColumn<string>();
                assembly.Culture = CultureInfo.CreateSpecificCulture(tr.ReadColumn<string>());
                assembly.ManifestModule = manifestModule;
                ReadAssemblyRefs(pe, assembly);
                ReadModuleRefs(pe, assembly);
                ReadTypeRefs(pe, assembly);
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
                    assemblyRef.PublicKeyOrToken = tr.ReadColumn<Blob>();
                    assemblyRef.Name = tr.ReadColumn<string>();
                    assemblyRef.Culture = CultureInfo.CreateSpecificCulture(tr.ReadColumn<string>());
                    assemblyRef.HashValue = tr.ReadColumn<Blob>();
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
                    t.ExtendsToken = tr.ReadColumn<MetadataToken>();
                    t.FieldListIndex = tr.ReadColumn<uint>();
                    t.MethodListIndex = tr.ReadColumn<uint>();
                    t.DeclaringModule = module;
                    module.DefinedTypes.Add(t);
                }
            }
        }

        private static void ReadDefinedParams(PortableExecutable pe, IModule module)
        {
            using (var tr = pe.MetadataRoot.CreateTableReader(TableID.Param))
            {
                while (tr.NextRow())
                {
                    var p = new CLIParam();
                    p.Flags = tr.ReadColumn<ParamAttributes>();
                    p.Sequence = tr.ReadColumn<ushort>();
                    p.Name = tr.ReadColumn<string>();
                    p.DeclaringModule = module;
                    module.DefinedParams.Add(p);
                }
            }
        }

        private static void ReadDefinedFields(PortableExecutable pe, IModule module)
        {
            using (var tr = pe.MetadataRoot.CreateTableReader(TableID.Field))
            {
                while (tr.NextRow())
                {
                    var f = new CLIField();
                    f.Flags = tr.ReadColumn<FieldAttributes>();
                    f.Name = tr.ReadColumn<string>();
                    var sigData = tr.ReadColumn<Blob>();
                    using (var sr = new SignatureReader(sigData, module))
                        f.Signature = (FieldSignature)sr.ReadSignature();
                    f.DeclaringModule = module;
                    module.DefinedFields.Add(f);
                }
            }
        }

        private static void ReadDefinedMethods(PortableExecutable pe, IModule module)
        {
            using (var tr = pe.MetadataRoot.CreateTableReader(TableID.MethodDef))
            {
                while (tr.NextRow())
                {
                    var method = new CLIMethod();
                    method.DeclaringModule = module;
                    var rva = tr.ReadColumn<uint>();
                    method.ImplFlags = tr.ReadColumn<MethodImplAttributes>();
                    method.Flags = tr.ReadColumn<MethodAttributes>();
                    method.Name = tr.ReadColumn<string>();
                    
                    var sigBlob = tr.ReadColumn<Blob>();
                    using (var sr = new SignatureReader(sigBlob, module))
                        method.Signature = (MethodSignature)sr.ReadSignature();

                    method.ParamListIndex = tr.ReadColumn<uint>();
                    module.DefinedMethods.Add(method);
                    if (rva == 0)
                        continue;
                    var methodBodyData = pe.GetSliceByRVA(rva);
                    using (var br = methodBodyData.CreateReader())
                    {
                        method.MethodEntry = new MethodEntry((int)rva, br);
                    }
                }
            }
        }

        private static void ReadTypeRefs(PortableExecutable pe, IAssembly assembly)
        {
            using (var tr = pe.MetadataRoot.CreateTableReader(TableID.TypeRef))
            {
                while (tr.NextRow())
                {
                    var t = new CLITypeRef();
                    t.Token = tr.ReadColumn<MetadataToken>();
                    t.Name = tr.ReadColumn<string>();
                    t.NameSpace = tr.ReadColumn<string>();
                    assembly.TypeReferences.Add(t);
                }
            }
        }
    }
}
