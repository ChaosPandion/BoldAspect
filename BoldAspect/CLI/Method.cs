using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BoldAspect.CLI
{



    [Flags]
    public enum MethodAttributes : ushort
    {
        MemberAccessMask = 0x0007,
        CompilerControlled = 0x0000,
        Private = 0x0001,
        FamANDAssem = 0x0002,
        Assem = 0x0003,
        Family = 0x0004,
        FamORAssem = 0x0005,
        Public = 0x0006,
        Static = 0x0010,
        Final = 0x0020,
        Virtual = 0x0040,
        HideBySig = 0x0080,
        VtableLayoutMask = 0x0100,
        ReuseSlot = 0x0000,
        NewSlot = 0x0100,
        Strict = 0x0200,
        Abstract = 0x0400,
        SpecialName = 0x0800,
        PInvokeImpl = 0x2000,
        UnmanagedExport = 0x0008,
        RTSpecialName = 0x1000,
        HasSecurity = 0x4000,
        RequireSecObject = 0x8000,
    }

    [Flags]
    public enum MethodImplAttributes : ushort
    {
        CodeTypeMask = 0x0003,
        IL = 0x0000,
        Native = 0x0001,
        OPTIL = 0x0002,
        Runtime = 0x0003,
        ManagedMask = 0x0004,
        Unmanaged = 0x0004,
        Managed = 0x0000,
        ForwardRef = 0x0010,
        PreserveSig = 0x0080,
        InternalCall = 0x1000,
        Synchronized = 0x0020,
        NoInlining = 0x0008,
        MaxMethodImplVal = 0xffff,
        NoOptimization = 0x0040,
    }

    [Flags]
    public enum MethodSemanticsAttributes : ushort
    {
        Setter = 0x0001,
        Getter = 0x0002,
        Other = 0x0004,
        AddOn = 0x0008,
        RemoveOn = 0x0010,
        Fire = 0x0020,
    }


    [Flags]
    public enum CallingConventions : byte
    {        
        Default = 0x00,
        //Unmanaged_cdecl = 0x01,
        //Unmanaged_sdtcall = 0x02,
        //Unmanaged_thiscall = 0x03,
        //Unmanaged_fastcall = 0x04,
        VarArg = 0x05,
        //Field = 0x06,
        //LocalVar = 0x07,
        //Property = 0x08,
        //Unmanaged = 0x09,
        //Mask = 0x0f,
        Generic = 0x10,
        HasThis = 0x20,
        ExplicitThis = 0x40,
        //Sentinel = 0x41,
    }

    public sealed class MethodCollection : Collection<IMethod>
    {

    }

    public interface IMethod
    {
        MethodSignature Signature { get; set; }
        MethodImplAttributes ImplFlags { get; set; }
        MethodAttributes Flags { get; set; }
        string Name { get; set; }
        ParamCollection Parameters { get; }
        IModule DeclaringModule { get; set; }
        MethodEntry MethodEntry { get; set; }
    }

    public sealed class CLIMethod : IMethod
    {
        private readonly ParamCollection _params = new ParamCollection();


        public MethodSignature Signature { get; set; }
        public MethodEntry MethodEntry { get; set; }
        public CallingConventions CallingConventions { get; set; }
        public MethodImplAttributes ImplFlags { get; set; }
        public MethodAttributes Flags { get; set; }
        public string Name { get; set; }
        public ParamCollection Parameters { get { return _params; } }
        internal uint ParamListIndex { get; set; }
        public IModule DeclaringModule { get; set; }

        public override string ToString()
        {
            return string.Format("{0}({1})", Name, string.Join(", ", _params.Select(p => p.Name)));
        }
    }


    public struct MethodDefRecord
    {
        [ConstantColumn(typeof(uint))]
        public uint RVA;

        [ConstantColumn(typeof(MethodImplAttributes))]
        public MethodImplAttributes ImplFlags;

        [ConstantColumn(typeof(MethodAttributes))]
        public MethodAttributes Flags;

        [StringHeapIndex]
        public uint Name;

        [BlobHeapIndex]
        public uint Signature;

        [SimpleIndex(TableID.Param)]
        public uint ParamList;
    }
    class MethodImplTable : Table<MethodImplRecord>
    {
        public MethodImplTable()
            : base(TableID.MethodImpl)
        {

        }
    }

    struct MethodImplRecord
    {
        [SimpleIndex(TableID.TypeDef)]
        public uint Class;

        [CodedIndex(typeof(MethodDefOrRef))]
        public uint MethodBody;

        [CodedIndex(typeof(MethodDefOrRef))]
        public uint MethodDeclaration;
    }
    class MethodSemanticsTable : Table<MethodSemanticsRecord>
    {
        public MethodSemanticsTable()
            : base(TableID.MethodSemantics)
        {

        }
    }

    struct MethodSemanticsRecord
    {
        [ConstantColumn(typeof(MethodSemanticsAttributes))]
        public MethodSemanticsAttributes Semantics;

        [SimpleIndex(TableID.MethodDef)]
        public uint Method;

        [CodedIndex(typeof(HasSemantics))]
        public uint Association;
    }
    class MethodSpecTable : Table<MethodSpecRecord>
    {
        public MethodSpecTable()
            : base(TableID.MethodSpec)
        {

        }
    }

    struct MethodSpecRecord
    {
        [CodedIndex(typeof(MethodDefOrRef))]
        public uint Method;

        [BlobHeapIndex]
        public uint Instantiation;
    }
    [Flags]
    public enum PInvokeAttributes : ushort
    {
        NoMangle = 0x0001,
        CharSetMask = 0x0006,
        CharSetNotSpec = 0x0000,
        CharSetAnsi = 0x0002,
        CharSetUnicode = 0x0004,
        CharSetAuto = 0x0006,
        SupportsLastError = 0x0040,
        CallConvMask = 0x0700,
        CallConvPlatformapi = 0x0100,
        CallConvCdecl = 0x0200,
        CallConvStdcall = 0x0300,
        CallConvThiscall = 0x0400,
        CallConvFastcall = 0x0500,
    }

    class ImplMapTable : Table<ImplMapRecord>
    {
        public ImplMapTable()
            : base(TableID.ImplMap)
        {

        }
    }

    struct ImplMapRecord
    {
        [ConstantColumn(typeof(PInvokeAttributes))]
        public PInvokeAttributes MappingFlags;

        [CodedIndex(typeof(MemberForwarded))]
        public uint MemberForwarded;

        [StringHeapIndex]
        public uint ImportName;

        [SimpleIndex(TableID.ModuleRef)]
        public uint ImportScope;
    }
}
