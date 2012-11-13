using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BoldAspect.CLI
{

    //public sealed class ParamSignature
    //{
    //    private readonly TypeSignatureCollection _customMods = new TypeSignatureCollection();

    //    public TypeSignature TypeSignature { get; set; }

    //    public TypeSignatureCollection CustomMods
    //    {
    //        get { return _customMods; }
    //    }

    //    public override string ToString()
    //    {
    //        return TypeSignature == null ? "null" : TypeSignature.ToString();
    //    }
    //}


    //public sealed class ParamSignatureCollection : Collection<ParamSignature>
    //{

    //}

    //public sealed class ReturnTypeSignature
    //{
    //    private readonly TypeSignatureCollection _customMods = new TypeSignatureCollection();

    //    public TypeSignature TypeSignature { get; set; }

    //    public TypeSignatureCollection CustomMods
    //    {
    //        get { return _customMods; }
    //    }

    //    public override string ToString()
    //    {
    //        return TypeSignature == null ? "null" : TypeSignature.ToString();
    //    }
    //}

    //public class SignatureReader : IDisposable
    //{
    //    private readonly Blob _blob;
    //    private readonly BlobReader _reader;
    //    private readonly IModule _module;

    //    public SignatureReader(Blob blob, IModule module)
    //    {
    //        _blob = blob;
    //        _reader = blob.ToReader();
    //        _module = module;
    //    }
        
    //    public object ReadSignature()
    //    {
    //        var cc = _reader.Read<CallingConventions>();
    //        if (((int)cc & 0x06) == 0x06)
    //        {
    //            var sig = new FieldSignature();
    //            var tsig = default(TypeSignature);
    //            do
    //            {
    //                if (tsig != null)
    //                    sig.CustomMods.Add(tsig);
    //                tsig = ReadType();
    //            } while (tsig.ElementType == ElementType.Cmod_opt || tsig.ElementType == ElementType.Cmod_reqd);
    //            sig.TypeSignature = tsig;
    //            return sig;
    //        }
    //        else if (((int)cc & 0x07) == 0x07)
    //        {
    //            return null;
    //        }
    //        else if (((int)cc & 0x08) == 0x08)
    //        {
    //            return null;
    //        }
    //        else
    //        {
    //            var sig = new MethodSignature();
    //            sig.CallingConventions = cc;
    //            if ((cc & CallingConventions.Generic) != 0)
    //                sig.GenParamCount = _reader.ReadCompressedUInt32();
    //            var paramCount = _reader.ReadCompressedUInt32();
    //            sig.ReturnType = ReadReturnType();
    //            for (int i = 0; i < paramCount; i++)
    //                sig.Params.Add(ReadParam());
    //            return sig;
    //        }
    //    }

    //    public ReturnTypeSignature ReadReturnType()
    //    {
    //        var sig = new ReturnTypeSignature();
    //        var tsig = default(TypeSignature);
    //        do
    //        {
    //            if (tsig != null)
    //                sig.CustomMods.Add(tsig);
    //            tsig = ReadType();
    //        } while (tsig != null && (tsig.ElementType == ElementType.Cmod_opt || tsig.ElementType == ElementType.Cmod_reqd));
    //        sig.TypeSignature = tsig;
    //        return sig;
    //    }

    //    public ParamSignature ReadParam()
    //    {
    //        var sig = new ParamSignature();
    //        var tsig = default(TypeSignature);
    //        do
    //        {
    //            if (tsig != null)
    //                sig.CustomMods.Add(tsig);
    //            tsig = ReadType();
    //        } while (tsig != null && (tsig.ElementType == ElementType.Cmod_opt || tsig.ElementType == ElementType.Cmod_reqd));
    //        sig.TypeSignature = tsig;
    //        return sig;
    //    }

    //    public TypeSignature ReadType()
    //    {
    //        var type = (ElementType)_reader.ReadByte();
    //        switch (type)
    //        {
    //            case ElementType.Void:
    //                return new TypeSignature(type, MetadataStorage.GetTypeRef(typeof(void)));
    //            case ElementType.Boolean:
    //                return new TypeSignature(type, MetadataStorage.GetTypeRef(typeof(bool)));
    //            case ElementType.Char:
    //                return new TypeSignature(type, MetadataStorage.GetTypeRef(typeof(char)));
    //            case ElementType.I1:
    //                return new TypeSignature(type, MetadataStorage.GetTypeRef(typeof(sbyte)));
    //            case ElementType.U1:
    //                return new TypeSignature(type, MetadataStorage.GetTypeRef(typeof(byte)));
    //            case ElementType.I2:
    //                return new TypeSignature(type, MetadataStorage.GetTypeRef(typeof(short)));
    //            case ElementType.U2:
    //                return new TypeSignature(type, MetadataStorage.GetTypeRef(typeof(ushort)));
    //            case ElementType.I4:
    //                return new TypeSignature(type, MetadataStorage.GetTypeRef(typeof(int)));
    //            case ElementType.U4:
    //                return new TypeSignature(type, MetadataStorage.GetTypeRef(typeof(uint)));
    //            case ElementType.I8:
    //                return new TypeSignature(type, MetadataStorage.GetTypeRef(typeof(long)));
    //            case ElementType.U8:
    //                return new TypeSignature(type, MetadataStorage.GetTypeRef(typeof(ulong)));
    //            case ElementType.R4:
    //                return new TypeSignature(type, MetadataStorage.GetTypeRef(typeof(float)));
    //            case ElementType.R8:
    //                return new TypeSignature(type, MetadataStorage.GetTypeRef(typeof(double)));
    //            case ElementType.String:
    //                return new TypeSignature(type, MetadataStorage.GetTypeRef(typeof(string)));
    //            case ElementType.Ptr:
    //                return ReadType();
    //            case ElementType.Byref:
    //                return ReadType();
    //            case ElementType.Valuetype:
    //            case ElementType.Class:
    //            case ElementType.Cmod_opt:
    //            case ElementType.Cmod_reqd:
    //                {
    //                    var token = ReadCompressedToken();
    //                    switch ((token.Value & 0xFF000000) >> 24)
    //                    {
    //                        case 0:
    //                            {
    //                                var d = _module.DefinedTypes[(int)(token.Value & 0x00FFFFFF)];
    //                                var r = new CLITypeRef();
    //                                r.Token = new MetadataToken(1);
    //                                r.Name = d.Name;
    //                                r.NameSpace = d.NameSpace;
    //                                return new TypeSignature() { ElementType = type, TypeReference = r };
    //                            }
    //                        case 1:
    //                            throw new Exception();
    //                        case 2:
    //                            throw new Exception();
    //                        default:
    //                            throw new Exception();
    //                    }
    //                }
    //            case ElementType.Var:
    //                return new TypeSignature();
    //            case ElementType.Array:
    //                return new TypeSignature();
    //            case ElementType.Genericinst:
    //                return new TypeSignature();
    //            case ElementType.Typedbyref:
    //                return new TypeSignature(type, MetadataStorage.GetTypeRef(typeof(TypedReference)));
    //            case ElementType.I:
    //                return new TypeSignature(type, MetadataStorage.GetTypeRef(typeof(IntPtr)));
    //            case ElementType.U:
    //                return new TypeSignature(type, MetadataStorage.GetTypeRef(typeof(UIntPtr)));
    //            case ElementType.Fnptr:
    //                return new TypeSignature();
    //            case ElementType.Object:
    //                return new TypeSignature(type, MetadataStorage.GetTypeRef(typeof(object)));
    //            case ElementType.Szarray:
    //                return new TypeSignature();
    //            case ElementType.Mvar:
    //                return new TypeSignature();
    //            default:
    //                return new TypeSignature();
    //        }
    //    }
        
    //    public MetadataToken ReadCompressedToken()
    //    {
    //        var val = _reader.ReadCompressedUInt32();
    //        var tag = (val & 0x00000003);
    //        var index = (val & 0xFFFFFFFC) >> 2;
    //        return new MetadataToken((tag << 24) | index);
    //    }

    //    public void Dispose()
    //    {
    //        _reader.Dispose();
    //    }
    //}
}