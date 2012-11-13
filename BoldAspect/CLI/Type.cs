using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BoldAspect.CLI
{
    [Flags]
    public enum TypeAttributes : uint
    {
        VisibilityMask = 0x00000007,
        NotPublic = 0x00000000,
        Public = 0x00000001,
        NestedPublic = 0x00000002,
        NestedPrivate = 0x00000003,
        NestedFamily = 0x00000004,
        NestedAssembly = 0x00000005,
        NestedFamANDAssem = 0x00000006,
        NestedFamORAssem = 0x00000007,
        LayoutMask = 0x00000018,
        AutoLayout = 0x00000000,
        SequentialLayout = 0x00000008,
        ExplicitLayout = 0x00000010,
        ClassSemanticsMask = 0x00000020,
        Class = 0x00000000,
        Interface = 0x00000020,
        Abstract = 0x00000080,
        Sealed = 0x00000100,
        SpecialName = 0x00000400,
        Import = 0x00001000,
        Serializable = 0x00002000,
        StringFormatMask = 0x00030000,
        AnsiClass = 0x00000000,
        UnicodeClass = 0x00010000,
        AutoClass = 0x00020000,
        CustomFormatClass = 0x00030000,
        CustomStringFormatMask = 0x00C00000,
        BeforeFieldInit = 0x00100000,
        RTSpecialName = 0x00000800,
        HasSecurity = 0x00400000,
        IsTypeForwarder = 0x00200000,
    }

    public interface IType
    {
        IModule DeclaringModule { get; set; }
        TypeAttributes Attributes { get; set; }
        string Name { get; set; }
        string NameSpace { get; set; }
        ITypeRef BaseType { get; set; }
        ITypeRef DeclaringType { get; set; }
        FieldCollection Fields { get; }
        MethodCollection Methods { get; }
    }

    public sealed class TypeCollection : Collection<IType>
    {

    }

    public sealed class CLIType : IType
    {
        private readonly MethodCollection _methods = new MethodCollection();
        private readonly FieldCollection _fields = new FieldCollection();

        public TypeAttributes Attributes { get; set; }
        public string Name { get; set; }
        public string NameSpace { get; set; }
        public IModule DeclaringModule { get; set; }
        public ITypeRef BaseType { get; set; }
        public ITypeRef DeclaringType { get; set; }

        public FieldCollection Fields
        {
            get { return _fields; }
        }

        public MethodCollection Methods 
        {
            get { return _methods; }
        }

        internal MetadataToken ExtendsToken { get; set; }
        internal uint FieldListIndex { get; set; }
        internal uint MethodListIndex { get; set; }

        public override string ToString()
        {
            return Name;
        }


    }

    public interface ITypeRef
    {
        MetadataToken Token { get; set; }
        string Name { get; set; }
        string NameSpace { get; set; }
    }


    public sealed class TypeRefCollection : Collection<ITypeRef>
    {

    }

    public sealed class CLITypeRef : ITypeRef
    {
        public MetadataToken Token { get; set; }
        public string Name { get; set; }
        public string NameSpace { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    public struct TypeRefToken
    {
        public readonly ResolutionScope Scope;
        public readonly uint Index;

        public TypeRefToken(ResolutionScope scope, uint index)
        {
            Scope = scope;
            Index = index;
        }

        public override string ToString()
        {
            return string.Format("{0}(0x{1:X4})", Scope, Index);
        }
    }

    //struct TypeDefRecord
    //{
    //    [ConstantColumn(typeof(TypeAttributes))]
    //    public TypeAttributes Flags;

    //    [StringHeapIndex]
    //    public uint TypeName;

    //    [StringHeapIndex]
    //    public uint TypeNameSpace;

    //    [CodedIndex(typeof(TypeDefOrRef))]
    //    public uint Extends;

    //    [SimpleIndex(TableID.Field)]
    //    public uint FieldList;

    //    [SimpleIndex(TableID.MethodDef)]
    //    public uint MethodList;
    //}
    //class TypeRefTable : Table<TypeRefRecord>
    //{
    //    public TypeRefTable()
    //        : base(TableID.TypeRef)
    //    {

    //    }
    //}

    //struct TypeRefRecord
    //{
    //    [CodedIndex(typeof(ResolutionScope))]
    //    public uint ResolutionScope;

    //    [StringHeapIndex]
    //    public uint TypeName;

    //    [StringHeapIndex]
    //    public uint TypeNameSpace;
    //}
    //class TypeSpecTable : Table<TypeSpecRecord>
    //{
    //    public TypeSpecTable()
    //        : base(TableID.TypeSpec)
    //    {

    //    }
    //}

    //struct TypeSpecRecord
    //{
    //    [BlobHeapIndex]
    //    public uint Signature;
    //}
    //class NestedClassTable : Table<NestedClassRecord>
    //{
    //    public NestedClassTable()
    //        : base(TableID.NestedClass)
    //    {

    //    }
    //}

    //struct NestedClassRecord
    //{
    //    [SimpleIndex(TableID.TypeDef)]
    //    public uint NestedClass;

    //    [SimpleIndex(TableID.TypeDef)]
    //    public uint EnclosingClass;
    //}
    //class InterfaceImplTable : Table<InterfaceImplRecord>
    //{
    //    public InterfaceImplTable()
    //        : base(TableID.InterfaceImpl)
    //    {

    //    }
    //}

    //struct InterfaceImplRecord
    //{
    //    [SimpleIndex(TableID.TypeDef)]
    //    public uint Class;

    //    [CodedIndex(typeof(TypeDefOrRef))]
    //    public uint Interface;
    //}
    //class ClassLayoutTable : Table<ClassLayoutRecord>
    //{
    //    public ClassLayoutTable()
    //        : base(TableID.ClassLayout)
    //    {

    //    }
    //}

    //struct ClassLayoutRecord
    //{
    //    [ConstantColumn(typeof(ushort))]
    //    public ushort PackingSize;

    //    [ConstantColumn(typeof(uint))]
    //    public uint ClassSize;

    //    [SimpleIndex(TableID.TypeDef)]
    //    public uint Parent;
    //}
    //class ExportedTypeTable : Table<ExportedTypeRecord>
    //{
    //    public ExportedTypeTable()
    //        : base(TableID.ExportedType)
    //    {

    //    }
    //}

    //struct ExportedTypeRecord
    //{
    //    [ConstantColumn(typeof(uint))]
    //    public uint Flags;

    //    [ConstantColumn(typeof(uint))]
    //    public uint TypeDefId;

    //    [StringHeapIndex]
    //    public uint TypeName;

    //    [StringHeapIndex]
    //    public uint TypeNameSpace;

    //    [CodedIndex(typeof(Implementation))]
    //    public uint Implementation;
    //}
}
