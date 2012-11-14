using System;

namespace BoldAspect.CLI.CodedIndexes
{
    public sealed class TypeDefOrRefCodedIndex : CodedIndex
    {
        public TypeDefOrRefCodedIndex()
            : base(2, TableID.TypeDef, TableID.TypeRef, TableID.TypeSpec)
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
            TypeSpec
        }
    }
}