using System;

namespace BoldAspect.CLI.CodedIndexes
{
    public sealed class MemberRefParentCodedIndex : CodedIndex
    {
        public MemberRefParentCodedIndex()
            : base(3, TableID.TypeDef, TableID.TypeRef, TableID.ModuleRef, TableID.MethodDef, TableID.TypeSpec)
        {

        }

        public override MetadataToken Decode(uint codedIndex)
        {
            var key = codedIndex >> Width;
            switch ((Value)(codedIndex & Mask))
            {
                case Value.TypeDef:
                    return new MetadataToken(TableID.TypeDef, key);
                case Value.TypeRef:
                    return new MetadataToken(TableID.TypeRef, key);
                case Value.ModuleRef:
                    return new MetadataToken(TableID.ModuleRef, key);
                case Value.MethodDef:
                    return new MetadataToken(TableID.MethodDef, key);
                case Value.TypeSpec:
                    return new MetadataToken(TableID.TypeSpec, key);
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
            TypeDef,
            TypeRef,
            ModuleRef,
            MethodDef,
            TypeSpec,
        }
    }
}