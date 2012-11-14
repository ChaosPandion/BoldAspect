using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI.Signatures
{
    public abstract class TypeSignature
    {
        protected TypeSignature(ElementType type)
        {
            Type = type;
        }

        public ElementType Type { get; set; }

        public override string ToString()
        {
            return Type.ToString();
        }
    }

    public sealed class PrimitiveTypeSignature : TypeSignature
    {
        public PrimitiveTypeSignature(ElementType type)
            : base(type)
        {
            if (!IsPrimitive(type))
                throw new Exception();
        }

        public static bool IsPrimitive(ElementType type)
        {
            switch (type)
            {
                case ElementType.Void:
                case ElementType.Boolean:
                case ElementType.Char:
                case ElementType.I1:
                case ElementType.U1:
                case ElementType.I2:
                case ElementType.U2:
                case ElementType.I4:
                case ElementType.U4:
                case ElementType.I8:
                case ElementType.U8:
                case ElementType.R4:
                case ElementType.R8:
                case ElementType.String:
                case ElementType.Typedbyref:
                case ElementType.I:
                case ElementType.U:
                case ElementType.Object:
                    return true;
                default:
                    return false;
            }
        }
    }

    public sealed class GeneralArrayTypeSignature : TypeSignature
    {
        public GeneralArrayTypeSignature(TypeSignature innerType, ArrayShape shape)
            : base(ElementType.Array)
        {
            InnerType = innerType;
            Shape = shape;
        }

        public TypeSignature InnerType { get; set; }
        public ArrayShape Shape { get; set; }
    }

    public sealed class ReferenceTypeSignature : TypeSignature
    {
        public ReferenceTypeSignature(MetadataToken token)
            : base(ElementType.Class)
        {
            Token = token;
        }

        public MetadataToken Token { get; set; }
    }

    public sealed class FunctionPointerTypeSignature : TypeSignature
    {
        public FunctionPointerTypeSignature(MethodSignature signature)
            : base(ElementType.Fnptr)
        {
            Signature = signature;
        }

        public MethodSignature Signature { get; set; }
    }

    public sealed class SZArrayTypeSignature : TypeSignature
    {
        public SZArrayTypeSignature(TypeSignature innerType)
            : base(ElementType.Szarray)
        {
            InnerType = innerType;
        }

        public TypeSignature InnerType { get; set; }
    }

    public sealed class ValueTypeSignature : TypeSignature
    {
        public ValueTypeSignature(MetadataToken token)
            : base(ElementType.Valuetype)
        {
            Token = token;
        }

        public MetadataToken Token { get; set; }
    }
}