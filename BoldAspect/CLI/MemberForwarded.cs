using System;

namespace BoldAspect.CLI
{
    public sealed class MemberForwardedCodedIndex : CodedIndex
    {
        public MemberForwardedCodedIndex()
            : base(1, TableID.Field, TableID.MethodDef)
        {

        }

        public override MetadataToken Decode(uint codedIndex)
        {
            var key = codedIndex >> Width;
            switch ((Value)(codedIndex & Mask))
            {
                case Value.Field:
                    return new MetadataToken(TableID.Field, key);
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
            Field,
            MethodDef
        }
    }
}