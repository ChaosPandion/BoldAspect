using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI
{
    public abstract class CodedIndex
    {
        public static readonly CodedIndex TypeDefOrRef = new TypeDefOrRefCodedIndex();
        public static readonly CodedIndex HasConstant = new HasConstantCodedIndex();
        public static readonly CodedIndex HasCustomAttribute = new HasCustomAttributeCodedIndex();
        public static readonly CodedIndex HasFieldMarshal = new HasFieldMarshalCodedIndex();
        public static readonly CodedIndex HasDeclSecurity = new HasDeclSecurityCodedIndex();
        public static readonly CodedIndex MemberRefParent = new MemberRefParentCodedIndex();
        public static readonly CodedIndex HasSemantics = new HasSemanticsCodedIndex();
        public static readonly CodedIndex MethodDefOrRef = new MethodDefOrRefCodedIndex();
        public static readonly CodedIndex MemberForwarded = new MemberForwardedCodedIndex();
        public static readonly CodedIndex Implementation = new ImplementationCodedIndex();
        public static readonly CodedIndex CustomAttributeType = new CustomAttributeTypeCodedIndex();
        public static readonly CodedIndex ResolutionScope = new ResolutionScopeCodedIndex();
        public static readonly CodedIndex TypeOrMethodDef = new TypeOrMethodDefCodedIndex();

        private readonly int _width;
        private readonly int _mask;
        private readonly ReadOnlyCollection<TableID> _tables;

        protected internal CodedIndex(int width, params TableID[] tables)
        {
            _width = width;
            _mask = Enumerable.Range(1, _width).Aggregate((acc, val) => acc | (acc << 1));
            _tables = Array.AsReadOnly(tables);
        }

        public int Width
        {
            get { return _width; }
        }

        public int Mask
        {
            get { return _mask; }
        }

        public ReadOnlyCollection<TableID> Tables
        {
            get { return _tables; }
        }

        public abstract MetadataToken Decode(uint codedIndex);

        public abstract uint Encode(MetadataToken token);

        public abstract bool Validate(uint codedIndex);

        public abstract bool Validate(MetadataToken token);
    }
}