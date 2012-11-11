using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI
{
    public sealed class TypeSignature
    {
        public ElementType ElementType { get; set; }
        public ITypeRef TypeReference { get; set; }

        public TypeSignature()
        {

        }

        public TypeSignature(ElementType elementType, ITypeRef typeReference)
        {
            ElementType = elementType;
            TypeReference = typeReference;
        }

        public override string ToString()
        {
            return TypeReference == null ? "null" : TypeReference.Name;
        }
    }


    public sealed class TypeSignatureCollection : Collection<TypeSignature>
    {

    }
}