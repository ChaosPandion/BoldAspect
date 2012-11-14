using System;

namespace BoldAspect.CLI.CodedIndexes
{
    public sealed class TypeOrMethodDefCodedIndex : CodedIndex
    {
        public TypeOrMethodDefCodedIndex()
            : base(1, TableID.TypeDef, TableID.MethodDef)
        {

        }

        public override MetadataToken Decode(uint codedIndex)
        {
            var key = codedIndex >> Width;
            switch ((Value)(codedIndex & Mask))
            {
                case Value.TypeDef:
                    return new MetadataToken(TableID.TypeDef, key);
                case Value.MethodDef:
                    return new MetadataToken(TableID.MethodDef, key);
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
            MethodDef
        }
    }
}