using System;

namespace BoldAspect.CLI
{
    public sealed class HasFieldMarshalCodedIndex : CodedIndex
    {
        public HasFieldMarshalCodedIndex()
            : base(1, TableID.Field, TableID.Param)
        {

        }

        public override MetadataToken Decode(uint codedIndex)
        {
            var key = codedIndex >> Width;
            switch ((Value)(codedIndex & Mask))
            {
                case Value.Field:
                    return new MetadataToken(TableID.Field, key);
                case Value.Param:
                    return new MetadataToken(TableID.Param, key);
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
            Param
        }
    }
}