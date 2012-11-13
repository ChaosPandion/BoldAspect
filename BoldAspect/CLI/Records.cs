using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI
{

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ModuleRecord
    {
        public ushort Column0;
        public uint Column1;
        public uint Column2;
        public uint Column3;
        public uint Column4;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TypeRefRecord
    {
        public uint Column0;
        public uint Column1;
        public uint Column2;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TypeDefRecord
    {
        public uint Column0;
        public uint Column1;
        public uint Column2;
        public uint Column3;
        public uint Column4;
        public uint Column5;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct FieldRecord
    {
        public ushort Column0;
        public uint Column1;
        public uint Column2;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct MethodDefRecord
    {
        public uint Column0;
        public ushort Column1;
        public ushort Column2;
        public uint Column3;
        public uint Column4;
        public uint Column5;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ParamRecord
    {
        public ushort Column0;
        public ushort Column1;
        public uint Column2;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct InterfaceImplRecord
    {
        public uint Column0;
        public uint Column1;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct MemberRefRecord
    {
        public uint Column0;
        public uint Column1;
        public uint Column2;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ConstantRecord
    {
        public ushort Column0;
        public uint Column1;
        public uint Column2;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CustomAttributeRecord
    {
        public uint Column0;
        public uint Column1;
        public uint Column2;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct FieldMarshalRecord
    {
        public uint Column0;
        public uint Column1;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct DeclSecurityRecord
    {
        public ushort Column0;
        public uint Column1;
        public uint Column2;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ClassLayoutRecord
    {
        public ushort Column0;
        public uint Column1;
        public uint Column2;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct FieldLayoutRecord
    {
        public uint Column0;
        public uint Column1;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct StandAloneSigRecord
    {
        public uint Column0;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct EventMapRecord
    {
        public uint Column0;
        public uint Column1;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct EventRecord
    {
        public ushort Column0;
        public uint Column1;
        public uint Column2;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PropertyMapRecord
    {
        public uint Column0;
        public uint Column1;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PropertyRecord
    {
        public ushort Column0;
        public uint Column1;
        public uint Column2;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct MethodSemanticsRecord
    {
        public ushort Column0;
        public uint Column1;
        public uint Column2;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct MethodImplRecord
    {
        public uint Column0;
        public uint Column1;
        public uint Column2;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ModuleRefRecord
    {
        public uint Column0;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TypeSpecRecord
    {
        public uint Column0;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ImplMapRecord
    {
        public ushort Column0;
        public uint Column1;
        public uint Column2;
        public uint Column3;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct FieldRVARecord
    {
        public uint Column0;
        public uint Column1;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct AssemblyRecord
    {
        public uint Column0;
        public ushort Column1;
        public ushort Column2;
        public ushort Column3;
        public ushort Column4;
        public uint Column5;
        public uint Column6;
        public uint Column7;
        public uint Column8;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct AssemblyProcessorRecord
    {
        public uint Column0;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct AssemblyOSRecord
    {
        public uint Column0;
        public uint Column1;
        public uint Column2;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct AssemblyRefRecord
    {
        public ushort Column0;
        public ushort Column1;
        public ushort Column2;
        public ushort Column3;
        public uint Column4;
        public uint Column5;
        public uint Column6;
        public uint Column7;
        public uint Column8;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct AssemblyRefProcessorRecord
    {
        public uint Column0;
        public uint Column1;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct AssemblyRefOSRecord
    {
        public uint Column0;
        public uint Column1;
        public uint Column2;
        public uint Column3;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct FileRecord
    {
        public uint Column0;
        public uint Column1;
        public uint Column2;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ExportedTypeRecord
    {
        public uint Column0;
        public uint Column1;
        public uint Column2;
        public uint Column3;
        public uint Column4;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ManifestResourceRecord
    {
        public uint Column0;
        public uint Column1;
        public uint Column2;
        public uint Column3;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct NestedClassRecord
    {
        public uint Column0;
        public uint Column1;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct GenericParamRecord
    {
        public ushort Column0;
        public ushort Column1;
        public uint Column2;
        public uint Column3;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct MethodSpecRecord
    {
        public uint Column0;
        public uint Column1;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct GenericParamConstraintRecord
    {
        public uint Column0;
        public uint Column1;
    }
}