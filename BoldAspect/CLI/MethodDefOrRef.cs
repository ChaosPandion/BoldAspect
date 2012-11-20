using System;

namespace BoldAspect.CLI
{
    public sealed class MethodDefOrRefCodedIndex : CodedIndex
    {
        public MethodDefOrRefCodedIndex()
            : base(1, TableID.MethodDef, TableID.MemberRef)
        {

        }

        public override MetadataToken Decode(uint codedIndex)
        {
            var key = codedIndex >> Width;
            switch ((Value)(codedIndex & Mask))
            {
                case Value.MethodDef:
                    return new MetadataToken(TableID.MethodDef, key);
                case Value.MemberRef:
                    return new MetadataToken(TableID.MemberRef, key);
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
            MemberRef
        }
    }
}