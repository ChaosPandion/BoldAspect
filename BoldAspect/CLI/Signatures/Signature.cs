using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI.Signatures
{
    public abstract class Signature
    {
        public static MethodSignature ParseMethodSignature(Blob blob)
        {
            using (var br = blob.ToReader())
            {
                var ms = new MethodSignature();
                ms.CallingConventions = br.Read<CallingConventions>();
                if (ms.CallingConventions.HasFlag(CallingConventions.Generic))
                    ms.GenericParamCount = br.ReadCompressedUInt32();
                ms.ParamCount = br.ReadCompressedUInt32();
                ms.ReturnType = ReadReturnTypeSignature(br);
                if (ms.ParamCount > 0)
                    ms.FirstParam = ReadParams(br, ms.ParamCount);
                return ms;
            }
        }

        public static FieldSignature ParseFieldSignature(Blob blob)
        {
            using (var br = blob.ToReader())
            {
                if (br.ReadByte() != 6)
                    throw new Exception();
                var fs = new FieldSignature();
                fs.FirstMod = ReadCustomModSignatures(br);
                fs.Type = ParseTypeSignature(br);
                return fs;
            }
        }

        public static PropertySignature ParsePropertySignature(Blob blob)
        {
            using (var br = blob.ToReader())
            {
                if (br.ReadByte() != 7)
                    throw new Exception();
                var ps = new PropertySignature();
                ps.FirstMod = ReadCustomModSignatures(br);
                ps.Type = ParseTypeSignature(br);
                return ps;
            }
        }

        public static LocalVariableSignature ParseLocalVariableSignature(Blob blob)
        {
            return null;
        }

        private static MethodSignature ReadMethodSignature(BlobReader br)
        {
            return null;
        }

        private static ParamSignature ReadParams(BlobReader br, uint count)
        {
            var ps = new ParamSignature();
            ps.FirstMod = ReadCustomModSignatures(br);
            ps.Type = ParseTypeSignature(br);
            if (count > 1)
                ps.Next = ReadParams(br, count - 1);
            return ps;
        }

        private static ReturnTypeSignature ReadReturnTypeSignature(BlobReader br)
        {
            var rt = new ReturnTypeSignature();
            rt.FirstMod = ReadCustomModSignatures(br);
            rt.Type = ParseTypeSignature(br);
            return rt;
        }

        private static CustomModSignature ReadCustomModSignatures(BlobReader br)
        {
            var b = br.TryRead<ElementType>();
            if (b == null)
                return null;
            if (b.Value != ElementType.Cmod_opt && b.Value != ElementType.Cmod_reqd)
            {
                br.Offset(-1);
                return null;
            }
            var cm = new CustomModSignature(b.Value == ElementType.Cmod_reqd, ReadCompressedToken(br));
            cm.Next = ReadCustomModSignatures(br);
            return cm;
        }

        private static TypeSignature ParseTypeSignature(BlobReader br)
        {
            var t = br.Read<ElementType>();
            if (PrimitiveTypeSignature.IsPrimitive(t))
                return new PrimitiveTypeSignature(t);
            switch (t)
            {
                case ElementType.Ptr:
                    break;
                case ElementType.Byref:
                    break;
                case ElementType.Valuetype:
                    return new ValueTypeSignature(ReadCompressedToken(br));
                case ElementType.Class:
                    return new ReferenceTypeSignature(ReadCompressedToken(br));
                case ElementType.Var:
                    break;
                case ElementType.Array:
                    break;
                case ElementType.Genericinst:
                    break;
                case ElementType.Fnptr:
                    return new FunctionPointerTypeSignature(ReadMethodSignature(br));
                case ElementType.Szarray:
                    return new SZArrayTypeSignature(ParseTypeSignature(br));
                case ElementType.Mvar:
                    break;
                default:
                    break;
            }
            return null;
        }

        private static MetadataToken ReadCompressedToken(BlobReader br)
        {
            var val = br.ReadCompressedUInt32();
            var tag = (val & 0x00000003);
            var index = (val & 0xFFFFFFFC) >> 2;
            return new MetadataToken((tag << 24) | index);
        }
    }
}