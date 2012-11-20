using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoldAspect.CLI;
using BoldAspect.CLI;

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
            return string.Format("{0}", Name);
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
            if (string.IsNullOrEmpty(Namespace))
                return Name ?? "";
            if (string.IsNullOrEmpty(Name))
                return "";
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
        public CLITypeDef EnclosingType { get; set; }


        public override string ToString()
        {
            if (string.IsNullOrEmpty(Namespace))
                return Name ?? "";
            if (string.IsNullOrEmpty(Name))
                return "";
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

        public MetadataToken Token { get; set; }
        public FieldAttributes Flags { get; set; }
        public string Name { get; set; }
        public FieldSignature Signature { get; set; }

        public override string ToString()
        {
            return Name ?? "";
        }
    }

    public sealed class CLIMethodDef
    {
        private readonly CLIMetadata _metadata;

        internal CLIMethodDef(CLIMetadata metadata)
        {
            _metadata = metadata;
        }

        public MetadataToken Token { get; set; }
        public MethodImplAttributes ImplFlags { get; set; }
        public MethodAttributes Flags { get; set; }
        public string Name { get; set; }
        public MethodSignature Signature { get; set; }

        public override string ToString()
        {
            if (Signature == null)
                return Name ?? "";
            return string.Format("{0} {1}()", Signature.ReturnType.Type, Name);
        }
    }

    public sealed class CLIParam
    {
        private readonly CLIMetadata _metadata;

        internal CLIParam(CLIMetadata metadata)
        {
            _metadata = metadata;
        }

        public MetadataToken Token { get; set; }
        public ParamAttributes Flags { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return Name ?? "";
        }
    }

    public sealed class CLIInterfaceImpl
    {
        private readonly CLIMetadata _metadata;

        internal CLIInterfaceImpl(CLIMetadata metadata)
        {
            _metadata = metadata;
        }

        public MetadataToken Token { get; set; }
        public MetadataToken Class { get; set; }
        public MetadataToken Interface { get; set; }
    }

    public sealed class CLIMemberRef
    {
        private readonly CLIMetadata _metadata;

        internal CLIMemberRef(CLIMetadata metadata)
        {
            _metadata = metadata;
        }

        public MetadataToken Token { get; set; }
        public MetadataToken Class { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return Name ?? "";
        }
    }

    public sealed class CLIConstant
    {
        private readonly CLIMetadata _metadata;

        internal CLIConstant(CLIMetadata metadata)
        {
            _metadata = metadata;
        }

        public MetadataToken Token { get; set; }
        public ElementType Type { get; set; }
        public MetadataToken Parent { get; set; }
        public Blob Value { get; set; }
    }

    public sealed class CLICustomAttribute
    {
        private readonly CLIMetadata _metadata;

        internal CLICustomAttribute(CLIMetadata metadata)
        {
            _metadata = metadata;
        }

        public MetadataToken Token { get; set; }
        public MetadataToken Parent { get; set; }
        public MetadataToken Type { get; set; }
        public Blob Value { get; set; }
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

    public sealed class CLIAssemblyRef
    {
        private readonly CLIMetadata _metadata;

        internal CLIAssemblyRef(CLIMetadata metadata)
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
        internal readonly ConcurrentDictionary<MetadataToken, ConcurrentBag<CLIInterfaceImpl>> InterfaceImplList = new ConcurrentDictionary<MetadataToken, ConcurrentBag<CLIInterfaceImpl>>();
        internal readonly List<CLIMemberRef> MemberRefList = new List<CLIMemberRef>();
        internal readonly ConcurrentDictionary<MetadataToken, ConcurrentBag<CLIConstant>> ConstantList = new ConcurrentDictionary<MetadataToken, ConcurrentBag<CLIConstant>>();
        internal readonly ConcurrentDictionary<MetadataToken, ConcurrentBag<CLICustomAttribute>> CustomAttributeList = new ConcurrentDictionary<MetadataToken, ConcurrentBag<CLICustomAttribute>>();
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
            ReadModuleList(pe);
            ReadTypeRefList(pe);
            ReadTypeDefList(pe);
            ReadFieldList(pe);
            ReadMethodDefList(pe);
            ReadParamList(pe);
            ReadInterfaceImplList(pe);
            ReadMemberRefList(pe);
            ReadConstantList(pe);
            ReadCustomAttributeList(pe);
            ReadFieldMarshalList(pe);
            ReadDeclSecurityList(pe);
            ReadClassLayoutList(pe);
            ReadFieldLayoutList(pe);
            ReadStandAloneSigList(pe);
            ReadEventMapList(pe);
            ReadEventList(pe);
            ReadPropertyMapList(pe);
            ReadPropertyList(pe);
            ReadMethodSemanticsList(pe);
            ReadMethodImplList(pe);
            ReadModuleRefList(pe);
            ReadTypeSpecList(pe);
            ReadImplMapList(pe);
            ReadFieldRVAList(pe);
            ReadAssemblyList(pe);
            ReadAssemblyRefList(pe);
            ReadFileList(pe);
            ReadExportedTypeList(pe);
            ReadManifestResourceList(pe);
            ReadNestedClassList(pe);
            ReadGenericParamList(pe);
            ReadMethodSpecList(pe);
            ReadGenericParamConstraintList(pe);
        }

        void ReadModuleList(PortableExecutable pe)
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
        }

        void ReadTypeRefList(PortableExecutable pe)
        {
            uint index = 0;
            foreach (var record in pe.MetadataRoot.GetTable<TypeRefTable>(TableID.TypeRef))
            {
                index++;
                var item = new CLITypeRef(this);
                item.Token = new MetadataToken(TableID.TypeRef, index);
                item.ResolutionScope = CodedIndex.ResolutionScope.Decode(record.ResolutionScope);
                item.Name = pe.MetadataRoot.GetString(record.TypeName);
                item.Namespace = pe.MetadataRoot.GetString(record.TypeNamespace);
                TypeRefList.Add(item);
            }
        }

        void ReadTypeDefList(PortableExecutable pe)
        {
            uint index = 0;
            foreach (var record in pe.MetadataRoot.GetTable<TypeDefTable>(TableID.TypeDef))
            {
                index++;
                var item = new CLITypeDef(this);
                item.Token = new MetadataToken(TableID.TypeDef, index);
                item.Flags = (TypeAttributes)record.Flags;
                item.Name = pe.MetadataRoot.GetString(record.TypeName);
                item.Namespace = pe.MetadataRoot.GetString(record.TypeNamespace);
                TypeDefList.Add(item);
            }
        }

        void ReadFieldList(PortableExecutable pe)
        {
            uint index = 0;
            foreach (var record in pe.MetadataRoot.GetTable<FieldTable>(TableID.Field))
            {
                index++;
                var item = new CLIField(this);
                item.Token = new MetadataToken(TableID.Field, index);
                item.Flags = (FieldAttributes)record.Flags;
                item.Name = pe.MetadataRoot.GetString(record.Name);
                item.Signature = Signature.ParseFieldSignature(pe.MetadataRoot.GetBlob(record.Signature));
                FieldList.Add(item);
            }
        }

        void ReadMethodDefList(PortableExecutable pe)
        {
            uint index = 0;
            foreach (var record in pe.MetadataRoot.GetTable<MethodDefTable>(TableID.MethodDef))
            {
                index++;
                var item = new CLIMethodDef(this);
                item.Token = new MetadataToken(TableID.MethodDef, index);
                item.ImplFlags = (MethodImplAttributes)record.ImplFlags;
                item.Flags = (MethodAttributes)record.Flags;
                item.Name = pe.MetadataRoot.GetString(record.Name);
                item.Signature = Signature.ParseMethodSignature(pe.MetadataRoot.GetBlob(record.Signature));
                MethodDefList.Add(item);
            }
        }

        void ReadParamList(PortableExecutable pe)
        {
            uint index = 0;
            foreach (var record in pe.MetadataRoot.GetTable<ParamTable>(TableID.Param))
            {
                index++;
                var item = new CLIParam(this);
                item.Token = new MetadataToken(TableID.Param, index);
                item.Flags = (ParamAttributes)record.Flags;
                item.Name = pe.MetadataRoot.GetString(record.Name);
                ParamList.Add(item);
            }
        }

        void ReadInterfaceImplList(PortableExecutable pe)
        {
            uint index = 0;
            foreach (var record in pe.MetadataRoot.GetTable<InterfaceImplTable>(TableID.InterfaceImpl))
            {
                index++;
                var item = new CLIInterfaceImpl(this);
                item.Token = new MetadataToken(TableID.InterfaceImpl, index);
                item.Class = new MetadataToken(TableID.TypeDef, record.Class);
                item.Interface = CodedIndex.TypeDefOrRef.Decode(record.Interface);
                InterfaceImplList.AddOrUpdate(
                    item.Class,
                    token => {
                        var bag = new ConcurrentBag<CLIInterfaceImpl>();
                        bag.Add(item);
                        return bag;
                    },
                    (token, bag) => {
                        bag.Add(item);
                        return bag;
                    }
                );
            }
        }

        void ReadMemberRefList(PortableExecutable pe)
        {
            uint index = 0;
            foreach (var record in pe.MetadataRoot.GetTable<MemberRefTable>(TableID.MemberRef))
            {
                index++;
                var item = new CLIMemberRef(this);
                item.Token = new MetadataToken(TableID.MemberRef, index);
                item.Class = CodedIndex.MemberRefParent.Decode(record.Class);
                item.Name = pe.MetadataRoot.GetString(record.Name);
                MemberRefList.Add(item);
            }
        }

        void ReadConstantList(PortableExecutable pe)
        {
            uint index = 0;
            foreach (var record in pe.MetadataRoot.GetTable<ConstantTable>(TableID.Constant))
            {
                index++;
                var item = new CLIConstant(this);
                item.Token = new MetadataToken(TableID.Constant, index);
                item.Type = (ElementType)record.Type;
                item.Parent = CodedIndex.HasConstant.Decode(record.Parent);
                item.Value = pe.MetadataRoot.GetBlob(record.Value);
                ConstantList.AddOrUpdate(
                    item.Parent,
                    token =>
                    {
                        var bag = new ConcurrentBag<CLIConstant>();
                        bag.Add(item);
                        return bag;
                    },
                    (token, bag) =>
                    {
                        bag.Add(item);
                        return bag;
                    }
                );
            }
        }

        void ReadCustomAttributeList(PortableExecutable pe)
        {
            uint index = 0;
            foreach (var record in pe.MetadataRoot.GetTable<CustomAttributeTable>(TableID.CustomAttribute))
            {
                index++;
                var item = new CLICustomAttribute(this);
                item.Token = new MetadataToken(TableID.CustomAttribute, index);
                item.Parent = CodedIndex.HasCustomAttribute.Decode(record.Parent);
                item.Type = CodedIndex.CustomAttributeType.Decode(record.Type);
                item.Value = pe.MetadataRoot.GetBlob(record.Value);
                CustomAttributeList.AddOrUpdate(
                    item.Parent,
                    token =>
                    {
                        var bag = new ConcurrentBag<CLICustomAttribute>();
                        bag.Add(item);
                        return bag;
                    },
                    (token, bag) =>
                    {
                        bag.Add(item);
                        return bag;
                    }
                );
            }
        }

        void ReadFieldMarshalList(PortableExecutable pe)
        {

        }

        void ReadDeclSecurityList(PortableExecutable pe)
        {

        }

        void ReadClassLayoutList(PortableExecutable pe)
        {

        }

        void ReadFieldLayoutList(PortableExecutable pe)
        {

        }

        void ReadStandAloneSigList(PortableExecutable pe)
        {

        }

        void ReadEventMapList(PortableExecutable pe)
        {

        }

        void ReadEventList(PortableExecutable pe)
        {

        }

        void ReadPropertyMapList(PortableExecutable pe)
        {

        }

        void ReadPropertyList(PortableExecutable pe)
        {

        }

        void ReadMethodSemanticsList(PortableExecutable pe)
        {

        }

        void ReadMethodImplList(PortableExecutable pe)
        {

        }

        void ReadModuleRefList(PortableExecutable pe)
        {

        }

        void ReadTypeSpecList(PortableExecutable pe)
        {

        }

        void ReadImplMapList(PortableExecutable pe)
        {

        }

        void ReadFieldRVAList(PortableExecutable pe)
        {

        }

        void ReadAssemblyList(PortableExecutable pe)
        {

        }

        void ReadAssemblyRefList(PortableExecutable pe)
        {

        }

        void ReadFileList(PortableExecutable pe)
        {

        }

        void ReadExportedTypeList(PortableExecutable pe)
        {

        }

        void ReadManifestResourceList(PortableExecutable pe)
        {

        }

        void ReadNestedClassList(PortableExecutable pe)
        {            
            foreach (var record in pe.MetadataRoot.GetTable<NestedClassTable>(TableID.NestedClass))
            {

            }
        }

        void ReadGenericParamList(PortableExecutable pe)
        {

        }

        void ReadMethodSpecList(PortableExecutable pe)
        {

        }

        void ReadGenericParamConstraintList(PortableExecutable pe)
        {

        }
    }
}