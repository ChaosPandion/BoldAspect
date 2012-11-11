using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BoldAspect.CLI
{
    public class SignatureReader : IDisposable
    {
        private readonly Blob _blob;
        private readonly BlobReader _reader;
        private readonly IModule _module;

        public SignatureReader(Blob blob, IModule module)
        {
            _blob = blob;
            _reader = blob.ToReader();
            _module = module;
        }

        public object ReadSignature()
        {
            var cc = _reader.Read<CallingConventions>();
            switch (cc)
            {
                case CallingConventions.Default:
                    break;
                case CallingConventions.Unmanaged_cdecl:
                    break;
                case CallingConventions.Unmanaged_sdtcall:
                    break;
                case CallingConventions.Unmanaged_thiscall:
                    break;
                case CallingConventions.Unmanaged_fastcall:
                    break;
                case CallingConventions.VarArg:
                    break;
                case CallingConventions.Field:
                    break;
                case CallingConventions.LocalVar:
                    break;
                case CallingConventions.Property:
                    break;
                case CallingConventions.Unmanaged:
                    break;
                case CallingConventions.Mask:
                    break;
                case CallingConventions.Generic:
                    break;
                case CallingConventions.HasThis:
                    break;
                case CallingConventions.ExplicitThis:
                    break;
                case CallingConventions.Sentinel:
                    break;
                default:
                    break;
            }
            return null;
        }

        public ITypeRef ReadType()
        {
            var type = (ElementType)_reader.ReadByte();
            switch (type)
            {
                case ElementType.Void:
                    return MetadataStorage.GetTypeRef(typeof(void));
                case ElementType.Boolean:
                    return MetadataStorage.GetTypeRef(typeof(bool));
                case ElementType.Char:
                    return MetadataStorage.GetTypeRef(typeof(char));
                case ElementType.I1:
                    return MetadataStorage.GetTypeRef(typeof(sbyte));
                case ElementType.U1:
                    return MetadataStorage.GetTypeRef(typeof(byte));
                case ElementType.I2:
                    return MetadataStorage.GetTypeRef(typeof(short));
                case ElementType.U2:
                    return MetadataStorage.GetTypeRef(typeof(ushort));
                case ElementType.I4:
                    return MetadataStorage.GetTypeRef(typeof(int));
                case ElementType.U4:
                    return MetadataStorage.GetTypeRef(typeof(uint));
                case ElementType.I8:
                    return MetadataStorage.GetTypeRef(typeof(long));
                case ElementType.U8:
                    return MetadataStorage.GetTypeRef(typeof(ulong));
                case ElementType.R4:
                    return MetadataStorage.GetTypeRef(typeof(float));
                case ElementType.R8:
                    return MetadataStorage.GetTypeRef(typeof(double));
                case ElementType.String:
                    return MetadataStorage.GetTypeRef(typeof(string));
                case ElementType.Ptr:
                    return ReadType();
                case ElementType.Byref:
                    return ReadType();
                case ElementType.Valuetype:
                case ElementType.Class:
                    var token = ReadCompressedToken();
                    switch ((token.Value & 0xFF000000) >> 24)
                    {
                        case 0:
                            {
                                var d = _module.DefinedTypes[(int)(token.Value & 0x00FFFFFF)];
                                var r = new CLITypeRef();
                                r.Token = new MetadataToken(1);
                                r.Name = d.Name;
                                r.NameSpace = d.NameSpace;
                                return r;
                            }
                        case 1:
                            throw new Exception();
                        case 2:
                            throw new Exception();
                        default:
                            throw new Exception();
                    }
                case ElementType.Var:
                    throw new Exception();
                case ElementType.Array:
                    throw new Exception();
                case ElementType.Genericinst:
                    throw new Exception();
                case ElementType.Typedbyref:
                    return MetadataStorage.GetTypeRef(typeof(TypedReference));
                case ElementType.I:
                    return MetadataStorage.GetTypeRef(typeof(IntPtr));
                case ElementType.U:
                    return MetadataStorage.GetTypeRef(typeof(UIntPtr));
                case ElementType.Fnptr:
                    throw new Exception();
                case ElementType.Object:
                    return MetadataStorage.GetTypeRef(typeof(object));
                case ElementType.Szarray:
                    throw new Exception();
                case ElementType.Mvar:
                    throw new Exception();
                default:
                    throw new Exception();
            }
        }

        public CallingConventions ReadCallingConventions()
        {
            return _reader.Read<CallingConventions>();
        }

        public int ReadCompressedInteger()
        {
            return _reader.ReadBigEndianCompressedInteger();
        }

        public RetType ReadRetType()
        {
            var cms = ReadCustomMods();
            var type = ReadType();
            return new RetType(cms, type);
        }

        public IReadOnlyCollection<ParamSig> ReadParamSigs(int count)
        {
            var list = new List<ParamSig>();
            for (int i = 0; i < count; i++)
            {
                var cms = ReadCustomMods();
                var t = ReadType();
                list.Add(new ParamSig(cms, t));
            }
            return list.AsReadOnly();
        }

        public CustomModCollection ReadCustomMods()
        {
            var list = new CustomModCollection();
            var cm = default(CustomMod);
            while (TryReadCustomMod(out cm))
                list.Add(cm);
            return list;
        }

        public bool TryReadCustomMod(out CustomMod result)
        {
            result = null;
            if (_reader.Index == _reader.Length)
                return false;
            var type = (ElementType)_reader.ReadByte();
            var match = type == ElementType.Cmod_opt || type == ElementType.Cmod_reqd;
            if (!match)
            {
                _reader.Offset(-1);
                return false;
            }
            var token = ReadCompressedToken();
            result = new CustomMod(type == ElementType.Cmod_reqd, null);
            return true;
        }

        public uint ReadCompressedUInt32()
        {
            byte first = _reader.ReadByte();
            if ((first & 0x80) == 0)
                return first;

            if ((first & 0x40) == 0)
                return ((uint)(first & ~0x80) << 8)
                    | _reader.ReadByte();

            return ((uint)(first & ~0xc0) << 24)
                | (uint)_reader.ReadByte() << 16
                | (uint)_reader.ReadByte() << 8
                | _reader.ReadByte();
        }

        public MetadataToken ReadCompressedToken()
        {
            var val = ReadCompressedUInt32();
            var tag = (val & 0x00000003);
            var index = (val & 0xFFFFFFFC) >> 2;
            return new MetadataToken((tag << 24) | index);
        }

        public ArrayShape ReadArrayShape()
        {
            return _reader.Read<ArrayShape>();
        }

        public void Dispose()
        {
            _reader.Dispose();
        }
    }
}