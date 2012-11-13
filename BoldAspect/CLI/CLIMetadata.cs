using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI
{
    public sealed class CLIModule
    {
        private readonly CLIMetadata _metadata;

        internal CLIModule(CLIMetadata metadata)
        {
            _metadata = metadata;
        }

        public MetadataToken Token { get; set; }
        public string Name { get; set; }
        public Guid Guid { get; set; }

        public override string ToString()
        {
            return string.Format("{0}({1})", Name, Guid);
        }

        public static CLIModule Load(string fileName)
        {
            return new CLIMetadata(new PortableExecutable(fileName)).ModuleList[0];
        }
    }

    public sealed class CLITypeRef
    {
        private readonly CLIMetadata _metadata;

        internal CLITypeRef(CLIMetadata metadata)
        {
            _metadata = metadata;
        }

        public MetadataToken Token { get; set; }
        public MetadataToken ResolutionScope { get; set; }
        public string Name { get; set; }
        public string Namespace { get; set; }

        public override string ToString()
        {
            return string.Format("{0}.{1}", Namespace, Name);
        }
    }

    public sealed class CLITypeDef
    {
        private readonly CLIMetadata _metadata;

        internal CLITypeDef(CLIMetadata metadata)
        {
            _metadata = metadata;
        }

        public MetadataToken Token { get; set; }
        public TypeAttributes Flags { get; set; }
        public string Name { get; set; }
        public string Namespace { get; set; }
        public MetadataToken ExtendsToken { get; set; }
        public MetadataToken FieldListToken { get; set; }
        public MetadataToken MethodListToken { get; set; }

        public override string ToString()
        {
            return string.Format("{0}.{1}", Namespace, Name);
        }
    }

    public sealed class CLIField
    {
        private readonly CLIMetadata _metadata;

        internal CLIField(CLIMetadata metadata)
        {
            _metadata = metadata;
        }

        public FieldAttributes Flags { get; set; }
        public string Name { get; set; }
    }

    public sealed class CLIMethodDef
    {
        private readonly CLIMetadata _metadata;

        internal CLIMethodDef(CLIMetadata metadata)
        {
            _metadata = metadata;
        }
    }

    public sealed class CLIParam
    {
        private readonly CLIMetadata _metadata;

        internal CLIParam(CLIMetadata metadata)
        {
            _metadata = metadata;
        }
    }

    public sealed class CLIInterfaceImpl
    {
        private readonly CLIMetadata _metadata;

        internal CLIInterfaceImpl(CLIMetadata metadata)
        {
            _metadata = metadata;
        }
    }

    public sealed class CLIMemberRef
    {
        private readonly CLIMetadata _metadata;

        internal CLIMemberRef(CLIMetadata metadata)
        {
            _metadata = metadata;
        }
    }

    public sealed class CLIConstant
    {
        private readonly CLIMetadata _metadata;

        internal CLIConstant(CLIMetadata metadata)
        {
            _metadata = metadata;
        }
    }

    public sealed class CLICustomAttribute
    {
        private readonly CLIMetadata _metadata;

        internal CLICustomAttribute(CLIMetadata metadata)
        {
            _metadata = metadata;
        }
    }

    public sealed class CLIFieldMarshal
    {
        private readonly CLIMetadata _metadata;

        internal CLIFieldMarshal(CLIMetadata metadata)
        {
            _metadata = metadata;
        }
    }

    public sealed class CLIDeclSecurity
    {
        private readonly CLIMetadata _metadata;

        internal CLIDeclSecurity(CLIMetadata metadata)
        {
            _metadata = metadata;
        }
    }

    public sealed class CLIClassLayout
    {
        private readonly CLIMetadata _metadata;

        internal CLIClassLayout(CLIMetadata metadata)
        {
            _metadata = metadata;
        }
    }

    public sealed class CLIFieldLayout
    {
        private readonly CLIMetadata _metadata;

        internal CLIFieldLayout(CLIMetadata metadata)
        {
            _metadata = metadata;
        }
    }

    public sealed class CLIStandAloneSig
    {
        private readonly CLIMetadata _metadata;

        internal CLIStandAloneSig(CLIMetadata metadata)
        {
            _metadata = metadata;
        }
    }

    public sealed class CLIEventMap
    {
        private readonly CLIMetadata _metadata;

        internal CLIEventMap(CLIMetadata metadata)
        {
            _metadata = metadata;
        }
    }

    public sealed class CLIEvent
    {
        private readonly CLIMetadata _metadata;

        internal CLIEvent(CLIMetadata metadata)
        {
            _metadata = metadata;
        }
    }

    public sealed class CLIPropertyMap
    {
        private readonly CLIMetadata _metadata;

        internal CLIPropertyMap(CLIMetadata metadata)
        {
            _metadata = metadata;
        }
    }

    public sealed class CLIProperty
    {
        private readonly CLIMetadata _metadata;

        internal CLIProperty(CLIMetadata metadata)
        {
            _metadata = metadata;
        }
    }

    public sealed class CLIMethodSemantics
    {
        private readonly CLIMetadata _metadata;

        internal CLIMethodSemantics(CLIMetadata metadata)
        {
            _metadata = metadata;
        }
    }

    public sealed class CLIMethodImpl
    {
        private readonly CLIMetadata _metadata;

        internal CLIMethodImpl(CLIMetadata metadata)
        {
            _metadata = metadata;
        }
    }

    public sealed class CLIModuleRef
    {
        private readonly CLIMetadata _metadata;

        internal CLIModuleRef(CLIMetadata metadata)
        {
            _metadata = metadata;
        }
    }

    public sealed class CLITypeSpec
    {
        private readonly CLIMetadata _metadata;

        internal CLITypeSpec(CLIMetadata metadata)
        {
            _metadata = metadata;
        }
    }

    public sealed class CLIImplMap
    {
        private readonly CLIMetadata _metadata;

        internal CLIImplMap(CLIMetadata metadata)
        {
            _metadata = metadata;
        }
    }

    public sealed class CLIFieldRVA
    {
        private readonly CLIMetadata _metadata;

        internal CLIFieldRVA(CLIMetadata metadata)
        {
            _metadata = metadata;
        }
    }

    public sealed class CLIAssembly
    {
        private readonly CLIMetadata _metadata;

        internal CLIAssembly(CLIMetadata metadata)
        {
            _metadata = metadata;
        }
    }

    public sealed class CLIAssemblyProcessor
    {
        private readonly CLIMetadata _metadata;

        internal CLIAssemblyProcessor(CLIMetadata metadata)
        {
            _metadata = metadata;
        }
    }

    public sealed class CLIAssemblyOS
    {
        private readonly CLIMetadata _metadata;

        internal CLIAssemblyOS(CLIMetadata metadata)
        {
            _metadata = metadata;
        }
    }

    public sealed class CLIAssemblyRef
    {
        private readonly CLIMetadata _metadata;

        internal CLIAssemblyRef(CLIMetadata metadata)
        {
            _metadata = metadata;
        }
    }

    public sealed class CLIAssemblyRefProcessor
    {
        private readonly CLIMetadata _metadata;

        internal CLIAssemblyRefProcessor(CLIMetadata metadata)
        {
            _metadata = metadata;
        }
    }

    public sealed class CLIAssemblyRefOS
    {
        private readonly CLIMetadata _metadata;

        internal CLIAssemblyRefOS(CLIMetadata metadata)
        {
            _metadata = metadata;
        }
    }

    public sealed class CLIFile
    {
        private readonly CLIMetadata _metadata;

        internal CLIFile(CLIMetadata metadata)
        {
            _metadata = metadata;
        }
    }

    public sealed class CLIExportedType
    {
        private readonly CLIMetadata _metadata;

        internal CLIExportedType(CLIMetadata metadata)
        {
            _metadata = metadata;
        }
    }

    public sealed class CLIManifestResource
    {
        private readonly CLIMetadata _metadata;

        internal CLIManifestResource(CLIMetadata metadata)
        {
            _metadata = metadata;
        }
    }

    public sealed class CLINestedClass
    {
        private readonly CLIMetadata _metadata;

        internal CLINestedClass(CLIMetadata metadata)
        {
            _metadata = metadata;
        }
    }

    public sealed class CLIGenericParam
    {
        private readonly CLIMetadata _metadata;

        internal CLIGenericParam(CLIMetadata metadata)
        {
            _metadata = metadata;
        }
    }

    public sealed class CLIMethodSpec
    {
        private readonly CLIMetadata _metadata;

        internal CLIMethodSpec(CLIMetadata metadata)
        {
            _metadata = metadata;
        }
    }

    public sealed class CLIGenericParamConstraint
    {
        private readonly CLIMetadata _metadata;

        internal CLIGenericParamConstraint(CLIMetadata metadata)
        {
            _metadata = metadata;
        }
    }

    public sealed class CLIMetadata
    {
        internal readonly List<CLIModule> ModuleList = new List<CLIModule>();
        internal readonly List<CLITypeRef> TypeRefList = new List<CLITypeRef>();
        internal readonly List<CLITypeDef> TypeDefList = new List<CLITypeDef>();
        internal readonly List<CLIField> FieldList = new List<CLIField>();
        internal readonly List<CLIMethodDef> MethodDefList = new List<CLIMethodDef>();
        internal readonly List<CLIParam> ParamList = new List<CLIParam>();
        internal readonly List<CLIInterfaceImpl> InterfaceImplList = new List<CLIInterfaceImpl>();
        internal readonly List<CLIMemberRef> MemberRefList = new List<CLIMemberRef>();
        internal readonly List<CLIConstant> ConstantList = new List<CLIConstant>();
        internal readonly List<CLICustomAttribute> CustomAttributeList = new List<CLICustomAttribute>();
        internal readonly List<CLIFieldMarshal> FieldMarshalList = new List<CLIFieldMarshal>();
        internal readonly List<CLIDeclSecurity> DeclSecurityList = new List<CLIDeclSecurity>();
        internal readonly List<CLIClassLayout> ClassLayoutList = new List<CLIClassLayout>();
        internal readonly List<CLIFieldLayout> FieldLayoutList = new List<CLIFieldLayout>();
        internal readonly List<CLIStandAloneSig> StandAloneSigList = new List<CLIStandAloneSig>();
        internal readonly List<CLIEventMap> EventMapList = new List<CLIEventMap>();
        internal readonly List<CLIEvent> EventList = new List<CLIEvent>();
        internal readonly List<CLIPropertyMap> PropertyMapList = new List<CLIPropertyMap>();
        internal readonly List<CLIProperty> PropertyList = new List<CLIProperty>();
        internal readonly List<CLIMethodSemantics> MethodSemanticsList = new List<CLIMethodSemantics>();
        internal readonly List<CLIMethodImpl> MethodImplList = new List<CLIMethodImpl>();
        internal readonly List<CLIModuleRef> ModuleRefList = new List<CLIModuleRef>();
        internal readonly List<CLITypeSpec> TypeSpecList = new List<CLITypeSpec>();
        internal readonly List<CLIImplMap> ImplMapList = new List<CLIImplMap>();
        internal readonly List<CLIFieldRVA> FieldRVAList = new List<CLIFieldRVA>();
        internal readonly List<CLIAssembly> AssemblyList = new List<CLIAssembly>();
        internal readonly List<CLIAssemblyRef> AssemblyRefList = new List<CLIAssemblyRef>();
        internal readonly List<CLIFile> FileList = new List<CLIFile>();
        internal readonly List<CLIExportedType> ExportedTypeList = new List<CLIExportedType>();
        internal readonly List<CLIManifestResource> ManifestResourceList = new List<CLIManifestResource>();
        internal readonly List<CLINestedClass> NestedClassList = new List<CLINestedClass>();
        internal readonly List<CLIGenericParam> GenericParamList = new List<CLIGenericParam>();
        internal readonly List<CLIMethodSpec> MethodSpecList = new List<CLIMethodSpec>();
        internal readonly List<CLIGenericParamConstraint> GenericParamConstraintList = new List<CLIGenericParamConstraint>();

        internal CLIMetadata(PortableExecutable pe)
        {
            uint index = 0;
            foreach (var record in pe.MetadataRoot.GetTable<ModuleTable>(TableID.Module))
            {
                index++;
                var item = new CLIModule(this);
                item.Token = new MetadataToken(TableID.Module, index);
                item.Name = pe.MetadataRoot.GetString(record.Name);
                item.Guid = pe.MetadataRoot.GetGuid(record.Mvid);
                ModuleList.Add(item);
            }

            index = 0;
            foreach (var record in pe.MetadataRoot.GetTable<TypeRefTable>(TableID.TypeRef))
            {
                index++;
                var item = new CLITypeRef(this);
                item.Token = new MetadataToken(TableID.TypeRef, index);
                item.ResolutionScope = new MetadataToken((TableID)Enum.Parse(typeof(TableID), ((ResolutionScope)(record.ResolutionScope & 0x3)).ToString()), record.ResolutionScope >> 2);
                item.Name = pe.MetadataRoot.GetString(record.TypeName);
                item.Namespace = pe.MetadataRoot.GetString(record.TypeNamespace);
                TypeRefList.Add(item);
            }

            index = 0;
            foreach (var record in pe.MetadataRoot.GetTable<TypeDefTable>(TableID.TypeDef))
            {
                index++;
                var item = new CLITypeDef(this);
                item.Token = new MetadataToken(TableID.TypeDef, index);
                item.Flags = (TypeAttributes)record.Flags;
                item.Name = pe.MetadataRoot.GetString(record.TypeName);
                item.Namespace = pe.MetadataRoot.GetString(record.TypeNamespace);
                item.ExtendsToken = new MetadataToken((TableID)Enum.Parse(typeof(TableID), ((TypeDefOrRef)(record.Extends & 0x3)).ToString()), record.Extends >> 2);
                item.FieldListToken = new MetadataToken(TableID.Field, record.FieldList);
                item.MethodListToken = new MetadataToken(TableID.MethodDef, record.MethodList);
                TypeDefList.Add(item);
            }
        }
    }
}
