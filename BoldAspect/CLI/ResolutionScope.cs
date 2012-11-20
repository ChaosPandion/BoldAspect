using System;

namespace BoldAspect.CLI
{
    public sealed class ResolutionScopeCodedIndex : CodedIndex
    {
        public ResolutionScopeCodedIndex()
            : base(3, TableID.Module, TableID.ModuleRef, TableID.AssemblyRef, TableID.TypeRef)
        {

        }

        public override MetadataToken Decode(uint codedIndex)
        {
            var key = codedIndex >> Width;
            switch ((Value)(codedIndex & Mask))
            {
                case Value.Module:
                    return new MetadataToken(TableID.Module, key);
                case Value.ModuleRef:
                    return new MetadataToken(TableID.ModuleRef, key);
                case Value.AssemblyRef:
                    return new MetadataToken(TableID.AssemblyRef, key);
                case Value.TypeRef:
                    return new MetadataToken(TableID.TypeRef, key);
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
            Module,
            ModuleRef,
            AssemblyRef,
            TypeRef
        }
    }
}