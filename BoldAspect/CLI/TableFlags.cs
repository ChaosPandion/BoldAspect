using System;

namespace BoldAspect.CLI
{
    [Flags]
    public enum TableFlags : ulong
    {
        Module = 0x00000001,
        TypeRef = 0x00000002,
        TypeDef = 0x00000004,
        Field = 0x00000008,
        MethodDef = 0x00000010,
        Param = 0x00000020,
        InterfaceImpl = 0x00000040,
        MemberRef = 0x00000080,
        Constant = 0x00000100,
        CustomAttribute = 0x00000200,
        FieldMarshal = 0x00000400,
        DeclSecurity = 0x00000800,
        ClassLayout = 0x00001000,
        FieldLayout = 0x00002000,
        StandAloneSig = 0x00004000,
        EventMap = 0x00008000,
        Event = 0x00010000,
        PropertyMap = 0x00020000,
        Property = 0x00040000,
        MethodSemantics = 0x00080000,
        MethodImpl = 0x00100000,
        ModuleRef = 0x00200000,
        TypeSpec = 0x00400000,
        ImplMap = 0x00800000,
        FieldRVA = 0x01000000,
        Assembly = 0x02000000,
        AssemblyProcessor = 0x04000000,
        AssemblyOS = 0x08000000,
        AssemblyRef = 0x10000000,
        AssemblyRefProcessor = 0x20000000,
        AssemblyRefOS = 0x40000000,
        File = 0x80000000,
        ExportedType = 0x100000000,
        ManifestResource = 0x200000000,
        NestedClass = 0x400000000,
        GenericParam = 0x800000000,
        MethodSpec = 0x1000000000,
        GenericParamConstraint = 0x2000000000,
    }
}