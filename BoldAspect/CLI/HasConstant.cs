using System;

namespace BoldAspect.CLI
{
    public sealed class HasConstantCodedIndex : CodedIndex
    {
        public HasConstantCodedIndex()
            : base(2, TableID.Field, TableID.Param, TableID.Property)
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
                case Value.Property:
                    return new MetadataToken(TableID.Property, key);
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
            Param,
            Property
        }
    }
}