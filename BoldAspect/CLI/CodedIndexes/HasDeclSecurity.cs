using System;

namespace BoldAspect.CLI.CodedIndexes
{
    public sealed class HasDeclSecurityCodedIndex : CodedIndex
    {
        public HasDeclSecurityCodedIndex()
            : base(2, TableID.TypeDef, TableID.MethodDef, TableID.Assembly)
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
                case Value.Assembly:
                    return new MetadataToken(TableID.Assembly, key);
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
            MethodDef,
            Assembly
        }
    }
}