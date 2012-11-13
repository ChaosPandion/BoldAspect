using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI
{
    
    public enum Index
    {
        None = -1,
        // Simple Indexes
        Module,
        TypeRef,
        TypeDef,
        Field,
        MethodDef,
        Param,
        InterfaceImpl,
        MemberRef,
        Constant,
        CustomAttribute,
        FieldMarshal,
        DeclSecurity,
        ClassLayout,
        FieldLayout,
        StandAloneSig,
        EventMap,
        Event,
        PropertyMap,
        Property,
        MethodSemantics,
        MethodImpl,
        ModuleRef,
        TypeSpec,
        ImplMap,
        FieldRVA,
        Assembly,
        AssemblyProcessor,
        AssemblyOS,
        AssemblyRef,
        AssemblyRefProcessor,
        AssemblyRefOS,
        File,
        ExportedType,
        ManifestResource,
        NestedClass,
        GenericParam,
        MethodSpec,
        GenericParamConstraint,

        // Coded Indexes
        TypeDefOrRef,
        HasConstant,
        HasCustomAttribute,
        HasFieldMarshal,
        HasDeclSecurity,
        MemberRefParent,
        HasSemantics,
        MethodDefOrRef,
        MemberForwarded,
        Implementation,
        CustomAttributeType,
        ResolutionScope,
        TypeOrMethodDef,

        // Heap Indexes
        String,
        Guid,
        Blob
    }

    public sealed class Indexes
    {
        private static readonly int _indexCount = Enum.GetValues(typeof(Index)).Length - 1;
        private readonly int[] _indexWidths = new int[_indexCount];
        private readonly bool[] _setIndexes = new bool[_indexCount];

        public Indexes()
        {
            for (int i = 0; i < _indexWidths.Length; i++)
            {
                _indexWidths[i] = 2;
            }
        }

        public void SetWidth(TableID table, int width)
        {
            switch (table)
            {
                case TableID.Module:
                    SetWidth(Index.Module, width);
                    break;
                case TableID.TypeRef:
                    SetWidth(Index.TypeRef, width);
                    break;
                case TableID.TypeDef:
                    SetWidth(Index.TypeDef, width);
                    break;
                case TableID.Field:
                    SetWidth(Index.Field, width);
                    break;
                case TableID.MethodDef:
                    SetWidth(Index.MethodDef, width);
                    break;
                case TableID.Param:
                    SetWidth(Index.Param, width);
                    break;
                case TableID.InterfaceImpl:
                    SetWidth(Index.InterfaceImpl, width);
                    break;
                case TableID.MemberRef:
                    SetWidth(Index.MemberRef, width);
                    break;
                case TableID.Constant:
                    SetWidth(Index.Constant, width);
                    break;
                case TableID.CustomAttribute:
                    SetWidth(Index.CustomAttribute, width);
                    break;
                case TableID.FieldMarshal:
                    SetWidth(Index.FieldMarshal, width);
                    break;
                case TableID.DeclSecurity:
                    SetWidth(Index.DeclSecurity, width);
                    break;
                case TableID.ClassLayout:
                    SetWidth(Index.ClassLayout, width);
                    break;
                case TableID.FieldLayout:
                    SetWidth(Index.FieldLayout, width);
                    break;
                case TableID.StandAloneSig:
                    SetWidth(Index.StandAloneSig, width);
                    break;
                case TableID.EventMap:
                    SetWidth(Index.EventMap, width);
                    break;
                case TableID.Event:
                    SetWidth(Index.Event, width);
                    break;
                case TableID.PropertyMap:
                    SetWidth(Index.PropertyMap, width);
                    break;
                case TableID.Property:
                    SetWidth(Index.Property, width);
                    break;
                case TableID.MethodSemantics:
                    SetWidth(Index.MethodSemantics, width);
                    break;
                case TableID.MethodImpl:
                    SetWidth(Index.MethodImpl, width);
                    break;
                case TableID.ModuleRef:
                    SetWidth(Index.ModuleRef, width);
                    break;
                case TableID.TypeSpec:
                    SetWidth(Index.TypeSpec, width);
                    break;
                case TableID.ImplMap:
                    SetWidth(Index.ImplMap, width);
                    break;
                case TableID.FieldRVA:
                    SetWidth(Index.FieldRVA, width);
                    break;
                case TableID.Assembly:
                    SetWidth(Index.Assembly, width);
                    break;
                case TableID.AssemblyProcessor:
                    SetWidth(Index.AssemblyProcessor, width);
                    break;
                case TableID.AssemblyOS:
                    SetWidth(Index.AssemblyOS, width);
                    break;
                case TableID.AssemblyRef:
                    SetWidth(Index.AssemblyRef, width);
                    break;
                case TableID.AssemblyRefProcessor:
                    SetWidth(Index.AssemblyRefProcessor, width);
                    break;
                case TableID.AssemblyRefOS:
                    SetWidth(Index.AssemblyRefOS, width);
                    break;
                case TableID.File:
                    SetWidth(Index.File, width);
                    break;
                case TableID.ExportedType:
                    SetWidth(Index.ExportedType, width);
                    break;
                case TableID.ManifestResource:
                    SetWidth(Index.ManifestResource, width);
                    break;
                case TableID.NestedClass:
                    SetWidth(Index.NestedClass, width);
                    break;
                case TableID.GenericParam:
                    SetWidth(Index.GenericParam, width);
                    break;
                case TableID.MethodSpec:
                    SetWidth(Index.MethodSpec, width);
                    break;
                case TableID.GenericParamConstraint:
                    SetWidth(Index.GenericParamConstraint, width);
                    break;
            }
        }

        public void SetWidth(Index index, int width)
        {
            _indexWidths[(int)index] = width;
            _setIndexes[(int)index] = true;
        }

        public int GetWidth(Index index)
        {
            return _indexWidths[(int)index];
        }
        
    }

    public sealed class ColumnSchema
    {
        public readonly Type Type;
        public readonly Index Index;

        public ColumnSchema(Type type, Index index)
        {
            Type = type;
            Index = index;
        }

        public override string ToString()
        {
            return string.Format("{0} As {1}", Index, Type);
        }
    }

    public interface ITable
    {
        int RowCount { get; set; }
        int FileIndex { get; set; }
        int RowWidth { get; }
        void Populate(BlobReader reader);
    }

    public abstract class Table<TRecord> : Collection<TRecord>, ITable
    {
        internal Indexes Indexes { get; private set; }
        public TableID TableID { get; private set; }
        internal ColumnSchema[] Schema { get; private set; }
        public int RowCount { get; set; }
        public int FileIndex { get; set; }

        public int RowWidth
        {
            get
            {
                var r = 0;
                foreach (var c in Schema)
                {
                    if (c.Index != Index.None)
                    {
                        r += Indexes.GetWidth(c.Index);
                    }
                    else
                    {
                        r += Marshal.SizeOf(c.Type);
                    }
                }
                return r;
            }
        }

        protected Table(Indexes indexes, TableID tableID, ColumnSchema[] schema)
        {
            Indexes = indexes;
            TableID = tableID;
            Schema = schema;
        }

        void ITable.Populate(BlobReader reader)
        {
            for (int i = 0; i < RowCount; i++)
            {
                Add(Read(reader));
            }
        }

        protected uint ReadIndex(Index index, BlobReader reader)
        {
            switch (Indexes.GetWidth(index))
            {
                case 2:
                    return reader.ReadUInt16();
                case 4:
                    return reader.ReadUInt32();
                default:
                    throw new Exception();
            }
        }

        protected abstract TRecord Read(BlobReader reader);
    }

    public sealed class ModuleTable : Table<ModuleTable.ModuleRecord>
    {
        public ModuleTable(Indexes indexes)
            : base(indexes, TableID.Module, new[] { 
                new ColumnSchema(typeof(ushort), Index.None), 
                new ColumnSchema(typeof(uint), Index.String), 
                new ColumnSchema(typeof(uint), Index.Guid), 
                new ColumnSchema(typeof(uint), Index.Guid), 
                new ColumnSchema(typeof(uint), Index.Guid) })
        {

        }

        protected override ModuleRecord Read(BlobReader reader)
        {
            var record = new ModuleRecord();
            record.Generation = reader.ReadUInt16();
            record.Name = ReadIndex(Index.String, reader);
            record.Mvid = ReadIndex(Index.Guid, reader);
            record.EncId = ReadIndex(Index.Guid, reader);
            record.EncBaseId = ReadIndex(Index.Guid, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct ModuleRecord
        {
            public ushort Generation;
            public uint Name;
            public uint Mvid;
            public uint EncId;
            public uint EncBaseId;
        }
    }

    public sealed class TypeRefTable : Table<TypeRefTable.TypeRefRecord>
    {
        public TypeRefTable(Indexes indexes)
            : base(indexes, TableID.TypeRef, new[] { 
                new ColumnSchema(typeof(uint), Index.ResolutionScope), 
                new ColumnSchema(typeof(uint), Index.String), 
                new ColumnSchema(typeof(uint), Index.String) })
        {

        }

        protected override TypeRefRecord Read(BlobReader reader)
        {
            var record = new TypeRefRecord();
            record.ResolutionScope = ReadIndex(Index.ResolutionScope, reader);
            record.TypeName = ReadIndex(Index.String, reader);
            record.TypeNamespace = ReadIndex(Index.String, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TypeRefRecord
        {
            public uint ResolutionScope;
            public uint TypeName;
            public uint TypeNamespace;
        }
    }

    public sealed class TypeDefTable : Table<TypeDefTable.TypeDefRecord>
    {
        public TypeDefTable(Indexes indexes)
            : base(indexes, TableID.TypeDef, new[] { new ColumnSchema(typeof(uint), Index.None), new ColumnSchema(typeof(uint), Index.String), new ColumnSchema(typeof(uint), Index.String), new ColumnSchema(typeof(uint), Index.TypeDefOrRef), new ColumnSchema(typeof(uint), Index.Field), new ColumnSchema(typeof(uint), Index.MethodDef) })
        {

        }

        protected override TypeDefRecord Read(BlobReader reader)
        {
            var record = new TypeDefRecord();
            record.Flags = reader.ReadUInt32();
            record.TypeName = ReadIndex(Index.String, reader);
            record.TypeNamespace = ReadIndex(Index.String, reader);
            record.Extends = ReadIndex(Index.TypeDefOrRef, reader);
            record.FieldList = ReadIndex(Index.Field, reader);
            record.MethodList = ReadIndex(Index.MethodDef, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TypeDefRecord
        {
            public uint Flags;
            public uint TypeName;
            public uint TypeNamespace;
            public uint Extends;
            public uint FieldList;
            public uint MethodList;
        }
    }

    public sealed class FieldTable : Table<FieldTable.FieldRecord>
    {
        public FieldTable(Indexes indexes)
            : base(indexes, TableID.Field, new[] { new ColumnSchema(typeof(ushort), Index.None), new ColumnSchema(typeof(uint), Index.String), new ColumnSchema(typeof(uint), Index.Blob) })
        {

        }

        protected override FieldRecord Read(BlobReader reader)
        {
            var record = new FieldRecord();
            record.Flags = reader.ReadUInt16();
            record.Name = ReadIndex(Index.String, reader);
            record.Signature = ReadIndex(Index.Blob, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct FieldRecord
        {
            public ushort Flags;
            public uint Name;
            public uint Signature;
        }
    }

    public sealed class MethodDefTable : Table<MethodDefTable.MethodDefRecord>
    {
        public MethodDefTable(Indexes indexes)
            : base(indexes, TableID.MethodDef, new[] { new ColumnSchema(typeof(uint), Index.None), new ColumnSchema(typeof(ushort), Index.None), new ColumnSchema(typeof(ushort), Index.None), new ColumnSchema(typeof(uint), Index.String), new ColumnSchema(typeof(uint), Index.Blob), new ColumnSchema(typeof(uint), Index.Param) })
        {

        }

        protected override MethodDefRecord Read(BlobReader reader)
        {
            var record = new MethodDefRecord();
            record.RVA = reader.ReadUInt32();
            record.ImplFlags = reader.ReadUInt16();
            record.Flags = reader.ReadUInt16();
            record.Name = ReadIndex(Index.String, reader);
            record.Signature = ReadIndex(Index.Blob, reader);
            record.ParamList = ReadIndex(Index.Param, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct MethodDefRecord
        {
            public uint RVA;
            public ushort ImplFlags;
            public ushort Flags;
            public uint Name;
            public uint Signature;
            public uint ParamList;
        }
    }

    public sealed class ParamTable : Table<ParamTable.ParamRecord>
    {
        public ParamTable(Indexes indexes)
            : base(indexes, TableID.Param, new[] { new ColumnSchema(typeof(ushort), Index.None), new ColumnSchema(typeof(ushort), Index.None), new ColumnSchema(typeof(uint), Index.String) })
        {

        }

        protected override ParamRecord Read(BlobReader reader)
        {
            var record = new ParamRecord();
            record.Flags = reader.ReadUInt16();
            record.Sequence = reader.ReadUInt16();
            record.Name = ReadIndex(Index.String, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct ParamRecord
        {
            public ushort Flags;
            public ushort Sequence;
            public uint Name;
        }
    }

    public sealed class InterfaceImplTable : Table<InterfaceImplTable.InterfaceImplRecord>
    {
        public InterfaceImplTable(Indexes indexes)
            : base(indexes, TableID.InterfaceImpl, new[] { new ColumnSchema(typeof(uint), Index.TypeDef), new ColumnSchema(typeof(uint), Index.TypeDefOrRef) })
        {

        }



        protected override InterfaceImplRecord Read(BlobReader reader)
        {
            var record = new InterfaceImplRecord();
            record.Class = ReadIndex(Index.TypeDef, reader);
            record.Interface = ReadIndex(Index.TypeDefOrRef, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct InterfaceImplRecord
        {
            public uint Class;
            public uint Interface;
        }
    }

    public sealed class MemberRefTable : Table<MemberRefTable.MemberRefRecord>
    {
        public MemberRefTable(Indexes indexes)
            : base(indexes, TableID.MemberRef, new[] { new ColumnSchema(typeof(uint), Index.MemberRefParent), new ColumnSchema(typeof(uint), Index.String), new ColumnSchema(typeof(uint), Index.Blob) })
        {

        }

        protected override MemberRefRecord Read(BlobReader reader)
        {
            var record = new MemberRefRecord();
            record.Class = ReadIndex(Index.MemberRefParent, reader);
            record.Name = ReadIndex(Index.String, reader);
            record.Signature = ReadIndex(Index.Blob, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct MemberRefRecord
        {
            public uint Class;
            public uint Name;
            public uint Signature;
        }
    }

    public sealed class ConstantTable : Table<ConstantTable.ConstantRecord>
    {
        public ConstantTable(Indexes indexes)
            : base(indexes, TableID.Constant, new[] { new ColumnSchema(typeof(ushort), Index.None), new ColumnSchema(typeof(uint), Index.HasConstant), new ColumnSchema(typeof(uint), Index.Blob) })
        {

        }

        protected override ConstantRecord Read(BlobReader reader)
        {
            var record = new ConstantRecord();
            record.Type = reader.ReadUInt16();
            record.Parent = ReadIndex(Index.HasConstant, reader);
            record.Value = ReadIndex(Index.Blob, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct ConstantRecord
        {
            public ushort Type;
            public uint Parent;
            public uint Value;
        }
    }

    public sealed class CustomAttributeTable : Table<CustomAttributeTable.CustomAttributeRecord>
    {
        public CustomAttributeTable(Indexes indexes)
            : base(indexes, TableID.CustomAttribute, new[] { new ColumnSchema(typeof(uint), Index.HasCustomAttribute), new ColumnSchema(typeof(uint), Index.CustomAttributeType), new ColumnSchema(typeof(uint), Index.Blob) })
        {

        }

        protected override CustomAttributeRecord Read(BlobReader reader)
        {
            var record = new CustomAttributeRecord();
            record.Parent = ReadIndex(Index.HasCustomAttribute, reader);
            record.Type = ReadIndex(Index.CustomAttributeType, reader);
            record.Value = ReadIndex(Index.Blob, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct CustomAttributeRecord
        {
            public uint Parent;
            public uint Type;
            public uint Value;
        }
    }

    public sealed class FieldMarshalTable : Table<FieldMarshalTable.FieldMarshalRecord>
    {
        public FieldMarshalTable(Indexes indexes)
            : base(indexes, TableID.FieldMarshal, new[] { new ColumnSchema(typeof(uint), Index.HasFieldMarshal), new ColumnSchema(typeof(uint), Index.Blob) })
        {

        }

        protected override FieldMarshalRecord Read(BlobReader reader)
        {
            var record = new FieldMarshalRecord();
            record.Parent = ReadIndex(Index.HasFieldMarshal, reader);
            record.NativeType = ReadIndex(Index.Blob, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct FieldMarshalRecord
        {
            public uint Parent;
            public uint NativeType;
        }
    }

    public sealed class DeclSecurityTable : Table<DeclSecurityTable.DeclSecurityRecord>
    {
        public DeclSecurityTable(Indexes indexes)
            : base(indexes, TableID.DeclSecurity, new[] { new ColumnSchema(typeof(ushort), Index.None), new ColumnSchema(typeof(uint), Index.HasDeclSecurity), new ColumnSchema(typeof(uint), Index.Blob) })
        {

        }

        protected override DeclSecurityRecord Read(BlobReader reader)
        {
            var record = new DeclSecurityRecord();
            record.Action = reader.ReadUInt16();
            record.Parent = ReadIndex(Index.HasDeclSecurity, reader);
            record.PermissionSet = ReadIndex(Index.Blob, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct DeclSecurityRecord
        {
            public ushort Action;
            public uint Parent;
            public uint PermissionSet;
        }
    }

    public sealed class ClassLayoutTable : Table<ClassLayoutTable.ClassLayoutRecord>
    {
        public ClassLayoutTable(Indexes indexes)
            : base(indexes, TableID.ClassLayout, new[] { new ColumnSchema(typeof(ushort), Index.None), new ColumnSchema(typeof(uint), Index.None), new ColumnSchema(typeof(uint), Index.TypeDef) })
        {

        }

        protected override ClassLayoutRecord Read(BlobReader reader)
        {
            var record = new ClassLayoutRecord();
            record.PackingSize = reader.ReadUInt16();
            record.ClassSize = reader.ReadUInt32();
            record.Parent = ReadIndex(Index.TypeDef, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct ClassLayoutRecord
        {
            public ushort PackingSize;
            public uint ClassSize;
            public uint Parent;
        }
    }

    public sealed class FieldLayoutTable : Table<FieldLayoutTable.FieldLayoutRecord>
    {
        public FieldLayoutTable(Indexes indexes)
            : base(indexes, TableID.FieldLayout, new[] { new ColumnSchema(typeof(uint), Index.None), new ColumnSchema(typeof(uint), Index.Field) })
        {

        }

        protected override FieldLayoutRecord Read(BlobReader reader)
        {
            var record = new FieldLayoutRecord();
            record.Offset = reader.ReadUInt32();
            record.Field = ReadIndex(Index.Field, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct FieldLayoutRecord
        {
            public uint Offset;
            public uint Field;
        }
    }

    public sealed class StandAloneSigTable : Table<StandAloneSigTable.StandAloneSigRecord>
    {
        public StandAloneSigTable(Indexes indexes)
            : base(indexes, TableID.StandAloneSig, new[] { new ColumnSchema(typeof(uint), Index.Blob) })
        {

        }

        protected override StandAloneSigRecord Read(BlobReader reader)
        {
            var record = new StandAloneSigRecord();
            record.Signature = ReadIndex(Index.Blob, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct StandAloneSigRecord
        {
            public uint Signature;
        }
    }

    public sealed class EventMapTable : Table<EventMapTable.EventMapRecord>
    {
        public EventMapTable(Indexes indexes)
            : base(indexes, TableID.EventMap, new[] { new ColumnSchema(typeof(uint), Index.TypeDef), new ColumnSchema(typeof(uint), Index.Event) })
        {

        }

        protected override EventMapRecord Read(BlobReader reader)
        {
            var record = new EventMapRecord();
            record.Parent = ReadIndex(Index.TypeDef, reader);
            record.EventList = ReadIndex(Index.Event, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct EventMapRecord
        {
            public uint Parent;
            public uint EventList;
        }
    }

    public sealed class EventTable : Table<EventTable.EventRecord>
    {
        public EventTable(Indexes indexes)
            : base(indexes, TableID.Event, new[] { new ColumnSchema(typeof(ushort), Index.None), new ColumnSchema(typeof(uint), Index.String), new ColumnSchema(typeof(uint), Index.TypeDefOrRef) })
        {

        }

        protected override EventRecord Read(BlobReader reader)
        {
            var record = new EventRecord();
            record.EventFlags = reader.ReadUInt16();
            record.Name = ReadIndex(Index.String, reader);
            record.EventType = ReadIndex(Index.TypeDefOrRef, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct EventRecord
        {
            public ushort EventFlags;
            public uint Name;
            public uint EventType;
        }
    }

    public sealed class PropertyMapTable : Table<PropertyMapTable.PropertyMapRecord>
    {
        public PropertyMapTable(Indexes indexes)
            : base(indexes, TableID.PropertyMap, new[] { new ColumnSchema(typeof(uint), Index.TypeDef), new ColumnSchema(typeof(uint), Index.Property) })
        {

        }

        protected override PropertyMapRecord Read(BlobReader reader)
        {
            var record = new PropertyMapRecord();
            record.Parent = ReadIndex(Index.TypeDef, reader);
            record.PropertyList = ReadIndex(Index.Property, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PropertyMapRecord
        {
            public uint Parent;
            public uint PropertyList;
        }
    }

    public sealed class PropertyTable : Table<PropertyTable.PropertyRecord>
    {
        public PropertyTable(Indexes indexes)
            : base(indexes, TableID.Property, new[] { new ColumnSchema(typeof(ushort), Index.None), new ColumnSchema(typeof(uint), Index.String), new ColumnSchema(typeof(uint), Index.Blob) })
        {

        }

        protected override PropertyRecord Read(BlobReader reader)
        {
            var record = new PropertyRecord();
            record.Flags = reader.ReadUInt16();
            record.Name = ReadIndex(Index.String, reader);
            record.Type = ReadIndex(Index.Blob, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PropertyRecord
        {
            public ushort Flags;
            public uint Name;
            public uint Type;
        }
    }

    public sealed class MethodSemanticsTable : Table<MethodSemanticsTable.MethodSemanticsRecord>
    {
        public MethodSemanticsTable(Indexes indexes)
            : base(indexes, TableID.MethodSemantics, new[] { new ColumnSchema(typeof(ushort), Index.None), new ColumnSchema(typeof(uint), Index.MethodDef), new ColumnSchema(typeof(uint), Index.HasSemantics) })
        {

        }

        protected override MethodSemanticsRecord Read(BlobReader reader)
        {
            var record = new MethodSemanticsRecord();
            record.Semantics = reader.ReadUInt16();
            record.Method = ReadIndex(Index.MethodDef, reader);
            record.Association = ReadIndex(Index.HasSemantics, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct MethodSemanticsRecord
        {
            public ushort Semantics;
            public uint Method;
            public uint Association;
        }
    }

    public sealed class MethodImplTable : Table<MethodImplTable.MethodImplRecord>
    {
        public MethodImplTable(Indexes indexes)
            : base(indexes, TableID.MethodImpl, new[] { new ColumnSchema(typeof(uint), Index.TypeDef), new ColumnSchema(typeof(uint), Index.MethodDefOrRef), new ColumnSchema(typeof(uint), Index.MethodDefOrRef) })
        {

        }

        protected override MethodImplRecord Read(BlobReader reader)
        {
            var record = new MethodImplRecord();
            record.Class = ReadIndex(Index.TypeDef, reader);
            record.MethodBody = ReadIndex(Index.MethodDefOrRef, reader);
            record.MethodDeclaration = ReadIndex(Index.MethodDefOrRef, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct MethodImplRecord
        {
            public uint Class;
            public uint MethodBody;
            public uint MethodDeclaration;
        }
    }

    public sealed class ModuleRefTable : Table<ModuleRefTable.ModuleRefRecord>
    {
        public ModuleRefTable(Indexes indexes)
            : base(indexes, TableID.ModuleRef, new[] { new ColumnSchema(typeof(uint), Index.String) })
        {

        }

        protected override ModuleRefRecord Read(BlobReader reader)
        {
            var record = new ModuleRefRecord();
            record.Name = ReadIndex(Index.String, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct ModuleRefRecord
        {
            public uint Name;
        }
    }

    public sealed class TypeSpecTable : Table<TypeSpecTable.TypeSpecRecord>
    {
        public TypeSpecTable(Indexes indexes)
            : base(indexes, TableID.TypeSpec, new[] { new ColumnSchema(typeof(uint), Index.Blob) })
        {

        }

        protected override TypeSpecRecord Read(BlobReader reader)
        {
            var record = new TypeSpecRecord();
            record.Signature = ReadIndex(Index.Blob, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TypeSpecRecord
        {
            public uint Signature;
        }
    }

    public sealed class ImplMapTable : Table<ImplMapTable.ImplMapRecord>
    {
        public ImplMapTable(Indexes indexes)
            : base(indexes, TableID.ImplMap, new[] { new ColumnSchema(typeof(ushort), Index.None), new ColumnSchema(typeof(uint), Index.MemberForwarded), new ColumnSchema(typeof(uint), Index.String), new ColumnSchema(typeof(uint), Index.ModuleRef) })
        {

        }

        protected override ImplMapRecord Read(BlobReader reader)
        {
            var record = new ImplMapRecord();
            record.MappingFlags = reader.ReadUInt16();
            record.MemberForwarded = ReadIndex(Index.MemberForwarded, reader);
            record.ImportName = ReadIndex(Index.String, reader);
            record.ImportScope = ReadIndex(Index.ModuleRef, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct ImplMapRecord
        {
            public ushort MappingFlags;
            public uint MemberForwarded;
            public uint ImportName;
            public uint ImportScope;
        }
    }

    public sealed class FieldRVATable : Table<FieldRVATable.FieldRVARecord>
    {
        public FieldRVATable(Indexes indexes)
            : base(indexes, TableID.FieldRVA, new[] { new ColumnSchema(typeof(uint), Index.None), new ColumnSchema(typeof(uint), Index.Field) })
        {

        }

        protected override FieldRVARecord Read(BlobReader reader)
        {
            var record = new FieldRVARecord();
            record.RVA = reader.ReadUInt32();
            record.Field = ReadIndex(Index.Field, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct FieldRVARecord
        {
            public uint RVA;
            public uint Field;
        }
    }

    public sealed class AssemblyTable : Table<AssemblyTable.AssemblyRecord>
    {
        public AssemblyTable(Indexes indexes)
            : base(indexes, TableID.Assembly, new[] { new ColumnSchema(typeof(uint), Index.None), new ColumnSchema(typeof(ushort), Index.None), new ColumnSchema(typeof(ushort), Index.None), new ColumnSchema(typeof(ushort), Index.None), new ColumnSchema(typeof(ushort), Index.None), new ColumnSchema(typeof(uint), Index.None), new ColumnSchema(typeof(uint), Index.Blob), new ColumnSchema(typeof(uint), Index.String), new ColumnSchema(typeof(uint), Index.String) })
        {

        }

        protected override AssemblyRecord Read(BlobReader reader)
        {
            var record = new AssemblyRecord();
            record.HashAlgId = reader.ReadUInt32();
            record.MajorVersion = reader.ReadUInt16();
            record.MinorVersion = reader.ReadUInt16();
            record.BuildNumber = reader.ReadUInt16();
            record.RevisionNumber = reader.ReadUInt16();
            record.Flags = reader.ReadUInt32();
            record.PublicKey = ReadIndex(Index.Blob, reader);
            record.Name = ReadIndex(Index.String, reader);
            record.Culture = ReadIndex(Index.String, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct AssemblyRecord
        {
            public uint HashAlgId;
            public ushort MajorVersion;
            public ushort MinorVersion;
            public ushort BuildNumber;
            public ushort RevisionNumber;
            public uint Flags;
            public uint PublicKey;
            public uint Name;
            public uint Culture;
        }
    }

    public sealed class AssemblyProcessorTable : Table<AssemblyProcessorTable.AssemblyProcessorRecord>
    {
        public AssemblyProcessorTable(Indexes indexes)
            : base(indexes, TableID.AssemblyProcessor, new[] { new ColumnSchema(typeof(uint), Index.None) })
        {

        }

        protected override AssemblyProcessorRecord Read(BlobReader reader)
        {
            var record = new AssemblyProcessorRecord();
            record.Processor = reader.ReadUInt32();
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct AssemblyProcessorRecord
        {
            public uint Processor;
        }
    }

    public sealed class AssemblyOSTable : Table<AssemblyOSTable.AssemblyOSRecord>
    {
        public AssemblyOSTable(Indexes indexes)
            : base(indexes, TableID.AssemblyOS, new[] { new ColumnSchema(typeof(uint), Index.None), new ColumnSchema(typeof(uint), Index.None), new ColumnSchema(typeof(uint), Index.None) })
        {

        }

        protected override AssemblyOSRecord Read(BlobReader reader)
        {
            var record = new AssemblyOSRecord();
            record.OSPlatformID = reader.ReadUInt32();
            record.OSMajorVersion = reader.ReadUInt32();
            record.OSMinorVersion = reader.ReadUInt32();
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct AssemblyOSRecord
        {
            public uint OSPlatformID;
            public uint OSMajorVersion;
            public uint OSMinorVersion;
        }
    }

    public sealed class AssemblyRefTable : Table<AssemblyRefTable.AssemblyRefRecord>
    {
        public AssemblyRefTable(Indexes indexes)
            : base(indexes, TableID.AssemblyRef, new[] { new ColumnSchema(typeof(ushort), Index.None), new ColumnSchema(typeof(ushort), Index.None), new ColumnSchema(typeof(ushort), Index.None), new ColumnSchema(typeof(ushort), Index.None), new ColumnSchema(typeof(uint), Index.None), new ColumnSchema(typeof(uint), Index.Blob), new ColumnSchema(typeof(uint), Index.String), new ColumnSchema(typeof(uint), Index.String), new ColumnSchema(typeof(uint), Index.Blob) })
        {

        }

        protected override AssemblyRefRecord Read(BlobReader reader)
        {
            var record = new AssemblyRefRecord();
            record.MajorVersion = reader.ReadUInt16();
            record.MinorVersion = reader.ReadUInt16();
            record.BuildNumber = reader.ReadUInt16();
            record.RevisionNumber = reader.ReadUInt16();
            record.Flags = reader.ReadUInt32();
            record.PublicKeyOrToken = ReadIndex(Index.Blob, reader);
            record.Name = ReadIndex(Index.String, reader);
            record.Culture = ReadIndex(Index.String, reader);
            record.HashValue = ReadIndex(Index.Blob, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct AssemblyRefRecord
        {
            public ushort MajorVersion;
            public ushort MinorVersion;
            public ushort BuildNumber;
            public ushort RevisionNumber;
            public uint Flags;
            public uint PublicKeyOrToken;
            public uint Name;
            public uint Culture;
            public uint HashValue;
        }
    }

    public sealed class AssemblyRefProcessorTable : Table<AssemblyRefProcessorTable.AssemblyRefProcessorRecord>
    {
        public AssemblyRefProcessorTable(Indexes indexes)
            : base(indexes, TableID.AssemblyRefProcessor, new[] { new ColumnSchema(typeof(uint), Index.None), new ColumnSchema(typeof(uint), Index.AssemblyRef) })
        {

        }

        protected override AssemblyRefProcessorRecord Read(BlobReader reader)
        {
            var record = new AssemblyRefProcessorRecord();
            record.Processor = reader.ReadUInt32();
            record.AssemblyRef = ReadIndex(Index.AssemblyRef, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct AssemblyRefProcessorRecord
        {
            public uint Processor;
            public uint AssemblyRef;
        }
    }

    public sealed class AssemblyRefOSTable : Table<AssemblyRefOSTable.AssemblyRefOSRecord>
    {
        public AssemblyRefOSTable(Indexes indexes)
            : base(indexes, TableID.AssemblyRefOS, new[] { new ColumnSchema(typeof(uint), Index.None), new ColumnSchema(typeof(uint), Index.None), new ColumnSchema(typeof(uint), Index.None), new ColumnSchema(typeof(uint), Index.AssemblyRef) })
        {

        }

        protected override AssemblyRefOSRecord Read(BlobReader reader)
        {
            var record = new AssemblyRefOSRecord();
            record.OSPlatformID = reader.ReadUInt32();
            record.OSMajorVersion = reader.ReadUInt32();
            record.OSMinorVersion = reader.ReadUInt32();
            record.AssemblyRef = ReadIndex(Index.AssemblyRef, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct AssemblyRefOSRecord
        {
            public uint OSPlatformID;
            public uint OSMajorVersion;
            public uint OSMinorVersion;
            public uint AssemblyRef;
        }
    }

    public sealed class FileTable : Table<FileTable.FileRecord>
    {
        public FileTable(Indexes indexes)
            : base(indexes, TableID.File, new[] { new ColumnSchema(typeof(uint), Index.None), new ColumnSchema(typeof(uint), Index.String), new ColumnSchema(typeof(uint), Index.Blob) })
        {

        }

        protected override FileRecord Read(BlobReader reader)
        {
            var record = new FileRecord();
            record.Flags = reader.ReadUInt32();
            record.Name = ReadIndex(Index.String, reader);
            record.HashValue = ReadIndex(Index.Blob, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct FileRecord
        {
            public uint Flags;
            public uint Name;
            public uint HashValue;
        }
    }

    public sealed class ExportedTypeTable : Table<ExportedTypeTable.ExportedTypeRecord>
    {
        public ExportedTypeTable(Indexes indexes)
            : base(indexes, TableID.ExportedType, new[] { new ColumnSchema(typeof(uint), Index.None), new ColumnSchema(typeof(uint), Index.None), new ColumnSchema(typeof(uint), Index.String), new ColumnSchema(typeof(uint), Index.String), new ColumnSchema(typeof(uint), Index.Implementation) })
        {

        }

        protected override ExportedTypeRecord Read(BlobReader reader)
        {
            var record = new ExportedTypeRecord();
            record.Flags = reader.ReadUInt32();
            record.TypeDefId = reader.ReadUInt32();
            record.TypeName = ReadIndex(Index.String, reader);
            record.TypeNamespace = ReadIndex(Index.String, reader);
            record.Implementation = ReadIndex(Index.Implementation, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct ExportedTypeRecord
        {
            public uint Flags;
            public uint TypeDefId;
            public uint TypeName;
            public uint TypeNamespace;
            public uint Implementation;
        }
    }

    public sealed class ManifestResourceTable : Table<ManifestResourceTable.ManifestResourceRecord>
    {
        public ManifestResourceTable(Indexes indexes)
            : base(indexes, TableID.ManifestResource, new[] { new ColumnSchema(typeof(uint), Index.None), new ColumnSchema(typeof(uint), Index.None), new ColumnSchema(typeof(uint), Index.String), new ColumnSchema(typeof(uint), Index.Implementation) })
        {

        }

        protected override ManifestResourceRecord Read(BlobReader reader)
        {
            var record = new ManifestResourceRecord();
            record.Offset = reader.ReadUInt32();
            record.Flags = reader.ReadUInt32();
            record.Name = ReadIndex(Index.String, reader);
            record.Implementation = ReadIndex(Index.Implementation, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct ManifestResourceRecord
        {
            public uint Offset;
            public uint Flags;
            public uint Name;
            public uint Implementation;
        }
    }

    public sealed class NestedClassTable : Table<NestedClassTable.NestedClassRecord>
    {
        public NestedClassTable(Indexes indexes)
            : base(indexes, TableID.NestedClass, new[] { new ColumnSchema(typeof(uint), Index.TypeDef), new ColumnSchema(typeof(uint), Index.TypeDef) })
        {

        }

        protected override NestedClassRecord Read(BlobReader reader)
        {
            var record = new NestedClassRecord();
            record.NestedClass = ReadIndex(Index.TypeDef, reader);
            record.EnclosingClass = ReadIndex(Index.TypeDef, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct NestedClassRecord
        {
            public uint NestedClass;
            public uint EnclosingClass;
        }
    }

    public sealed class GenericParamTable : Table<GenericParamTable.GenericParamRecord>
    {
        public GenericParamTable(Indexes indexes)
            : base(indexes, TableID.GenericParam, new[] { new ColumnSchema(typeof(ushort), Index.None), new ColumnSchema(typeof(ushort), Index.None), new ColumnSchema(typeof(uint), Index.TypeOrMethodDef), new ColumnSchema(typeof(uint), Index.String) })
        {

        }

        protected override GenericParamRecord Read(BlobReader reader)
        {
            var record = new GenericParamRecord();
            record.Number = reader.ReadUInt16();
            record.Flags = reader.ReadUInt16();
            record.Owner = ReadIndex(Index.TypeOrMethodDef, reader);
            record.Name = ReadIndex(Index.String, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct GenericParamRecord
        {
            public ushort Number;
            public ushort Flags;
            public uint Owner;
            public uint Name;
        }
    }

    public sealed class MethodSpecTable : Table<MethodSpecTable.MethodSpecRecord>
    {
        public MethodSpecTable(Indexes indexes)
            : base(indexes, TableID.MethodSpec, new[] { new ColumnSchema(typeof(uint), Index.MethodDefOrRef), new ColumnSchema(typeof(uint), Index.Blob) })
        {

        }

        protected override MethodSpecRecord Read(BlobReader reader)
        {
            var record = new MethodSpecRecord();
            record.Method = ReadIndex(Index.MethodDefOrRef, reader);
            record.Instantiation = ReadIndex(Index.Blob, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct MethodSpecRecord
        {
            public uint Method;
            public uint Instantiation;
        }
    }

    public sealed class GenericParamConstraintTable : Table<GenericParamConstraintTable.GenericParamConstraintRecord>
    {
        public GenericParamConstraintTable(Indexes indexes)
            : base(indexes, TableID.GenericParamConstraint, new[] { new ColumnSchema(typeof(uint), Index.GenericParam), new ColumnSchema(typeof(uint), Index.TypeDefOrRef) })
        {

        }

        protected override GenericParamConstraintRecord Read(BlobReader reader)
        {
            var record = new GenericParamConstraintRecord();
            record.Owner = ReadIndex(Index.GenericParam, reader);
            record.Constraint = ReadIndex(Index.TypeDefOrRef, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct GenericParamConstraintRecord
        {
            public uint Owner;
            public uint Constraint;
        }
    }
}