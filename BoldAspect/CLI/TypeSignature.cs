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
        public CLITypeRef Type { get; set; }

        public TypeSignature()
        {

        }

        public TypeSignature(ElementType elementType, CLITypeRef type)
        {
            ElementType = elementType;
            Type = type;
        }

        public override string ToString()
        {
            return Type == null ? "null" : Type.Name;
        }
    }


    public sealed class TypeSignatureCollection : Collection<TypeSignature>
    {

    }
}