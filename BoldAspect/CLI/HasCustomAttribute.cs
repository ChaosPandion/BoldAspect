using System;

namespace BoldAspect.CLI
{
    public sealed class HasCustomAttributeCodedIndex : CodedIndex
    {
        public HasCustomAttributeCodedIndex()
            : base(5, TableID.MethodDef, TableID.Field, TableID.TypeRef, TableID.TypeDef, TableID.Param, TableID.InterfaceImpl, TableID.MemberRef, TableID.Module, TableID.Property, TableID.Event, TableID.StandAloneSig, TableID.ModuleRef, TableID.TypeSpec, TableID.Assembly, TableID.AssemblyRef, TableID.File, TableID.ExportedType, TableID.ManifestResource, TableID.GenericParam, TableID.GenericParamConstraint, TableID.MethodSpec)
        {

        }

        public override MetadataToken Decode(uint codedIndex)
        {
            var key = codedIndex >> Width;
            switch ((Value)(codedIndex & Mask))
            {
                case Value.MethodDef:
                    return new MetadataToken(TableID.MethodDef, key);
                case Value.Field:
                    return new MetadataToken(TableID.Field, key);
                case Value.TypeRef:
                    return new MetadataToken(TableID.TypeRef, key);
                case Value.TypeDef:
                    return new MetadataToken(TableID.TypeDef, key);
                case Value.Param:
                    return new MetadataToken(TableID.Param, key);
                case Value.InterfaceImpl:
                    return new MetadataToken(TableID.InterfaceImpl, key);
                case Value.MemberRef:
                    return new MetadataToken(TableID.MemberRef, key);
                case Value.Module:
                    return new MetadataToken(TableID.Module, key);
                case Value.Property:
                    return new MetadataToken(TableID.Property, key);
                case Value.Event:
                    return new MetadataToken(TableID.Event, key);
                case Value.StandAloneSig:
                    return new MetadataToken(TableID.StandAloneSig, key);
                case Value.ModuleRef:
                    return new MetadataToken(TableID.ModuleRef, key);
                case Value.TypeSpec:
                    return new MetadataToken(TableID.TypeSpec, key);
                case Value.Assembly:
                    return new MetadataToken(TableID.Assembly, key);
                case Value.AssemblyRef:
                    return new MetadataToken(TableID.AssemblyRef, key);
                case Value.File:
                    return new MetadataToken(TableID.File, key);
                case Value.ExportedType:
                    return new MetadataToken(TableID.ExportedType, key);
                case Value.ManifestResource:
                    return new MetadataToken(TableID.ManifestResource, key);
                case Value.GenericParam:
                    return new MetadataToken(TableID.GenericParam, key);
                case Value.GenericParamConstraint:
                    return new MetadataToken(TableID.GenericParamConstraint, key);
                case Value.MethodSpec:
                    return new MetadataToken(TableID.MethodSpec, key);
                default:
                    throw new Exception();
            }
        }

        public override uint Encode(MetadataToken token)
        {
            throw new NotImplementedException();
        }

        public override bool Validate(uint codedIndex)
        {
            throw new NotImplementedException();
        }

        public override bool Validate(MetadataToken token)
        {
            throw new NotImplementedException();
        }

        public enum Value : byte
        {
            MethodDef,
            Field,
            TypeRef,
            TypeDef,
            Param,
            InterfaceImpl,
            MemberRef,
            Module,
            Permission,
            Property,
            Event,
            StandAloneSig,
            ModuleRef,
            TypeSpec,
            Assembly,
            AssemblyRef,
            File,
            ExportedType,
            ManifestResource,
            GenericParam,
            GenericParamConstraint,
            MethodSpec,
        }
    }
}