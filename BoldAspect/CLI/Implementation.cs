using System;

namespace BoldAspect.CLI
{
    public sealed class ImplementationCodedIndex : CodedIndex
    {
        public ImplementationCodedIndex()
            : base(2, TableID.File, TableID.AssemblyRef, TableID.ExportedType)
        {

        }

        public override MetadataToken Decode(uint codedIndex)
        {
            var key = codedIndex >> Width;
            switch ((Value)(codedIndex & Mask))
            {
                case Value.File:
                    return new MetadataToken(TableID.File, key);
                case Value.AssemblyRef:
                    return new MetadataToken(TableID.AssemblyRef, key);
                case Value.ExportedType:
                    return new MetadataToken(TableID.ExportedType, key);
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
            File,
            AssemblyRef,
            ExportedType
        }
    }
}