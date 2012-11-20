using System;

namespace BoldAspect.CLI
{
    public sealed class HasSemanticsCodedIndex : CodedIndex
    {
        public HasSemanticsCodedIndex()
            : base(1, TableID.Event, TableID.Property)
        {

        }

        public override MetadataToken Decode(uint codedIndex)
        {
            var key = codedIndex >> Width;
            switch ((Value)(codedIndex & Mask))
            {
                case Value.Event:
                    return new MetadataToken(TableID.Event, key);
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
            Event,
            Property
        }
    }
}