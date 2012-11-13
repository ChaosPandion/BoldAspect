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
        private static readonly int _indexCount = Enum.GetValues(typeof(Index)).Length;
        private readonly int[] _indexWidths = new int[_indexCount];
        private readonly bool[] _setIndexes = new bool[_indexCount];

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

    public abstract class Table<TRecord> : Collection<TRecord>
    {
        internal Indexes Indexes { get; private set; }
        public TableID TableID { get; private set; }
        internal ColumnSchema[] Schema { get; private set; }

        protected Table(Indexes indexes, TableID tableID, ColumnSchema[] schema)
        {
            Indexes = indexes;
            TableID = tableID;
            Schema = schema;
        }

        internal void Populate(int rowCount, BlobReader reader)
        {
            for (int i = 0; i < rowCount; i++)
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
            : base(indexes, TableID.Module, new[] { new ColumnSchema(typeof(ushort), Index.None), new ColumnSchema(typeof(uint), Index.String), new ColumnSchema(typeof(uint), Index.Guid), new ColumnSchema(typeof(uint), Index.Guid), new ColumnSchema(typeof(uint), Index.Guid) })
        {

        }

        protected override ModuleRecord Read(BlobReader reader)
        {
            var record = new ModuleRecord();
            record.Column0 = reader.ReadUInt16();
            record.Column1 = ReadIndex(Index.String, reader);
            record.Column2 = ReadIndex(Index.Guid, reader);
            record.Column3 = ReadIndex(Index.Guid, reader);
            record.Column4 = ReadIndex(Index.Guid, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct ModuleRecord
        {
            public ushort Column0;
            public uint Column1;
            public uint Column2;
            public uint Column3;
            public uint Column4;
        }
    }

    public sealed class TypeRefTable : Table<TypeRefTable.TypeRefRecord>
    {
        public TypeRefTable(Indexes indexes)
            : base(indexes, TableID.TypeRef, new[] { new ColumnSchema(typeof(uint), Index.ResolutionScope), new ColumnSchema(typeof(uint), Index.String), new ColumnSchema(typeof(uint), Index.String) })
        {

        }

        protected override TypeRefRecord Read(BlobReader reader)
        {
            var record = new TypeRefRecord();
            record.Column0 = ReadIndex(Index.ResolutionScope, reader);
            record.Column1 = ReadIndex(Index.String, reader);
            record.Column2 = ReadIndex(Index.String, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TypeRefRecord
        {
            public uint Column0;
            public uint Column1;
            public uint Column2;
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
            record.Column0 = reader.ReadUInt32();
            record.Column1 = ReadIndex(Index.String, reader);
            record.Column2 = ReadIndex(Index.String, reader);
            record.Column3 = ReadIndex(Index.TypeDefOrRef, reader);
            record.Column4 = ReadIndex(Index.Field, reader);
            record.Column5 = ReadIndex(Index.MethodDef, reader);
            return record;
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
            record.Column0 = reader.ReadUInt16();
            record.Column1 = ReadIndex(Index.String, reader);
            record.Column2 = ReadIndex(Index.Blob, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct FieldRecord
        {
            public ushort Column0;
            public uint Column1;
            public uint Column2;
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
            record.Column0 = reader.ReadUInt32();
            record.Column1 = reader.ReadUInt16();
            record.Column2 = reader.ReadUInt16();
            record.Column3 = ReadIndex(Index.String, reader);
            record.Column4 = ReadIndex(Index.Blob, reader);
            record.Column5 = ReadIndex(Index.Param, reader);
            return record;
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
            record.Column0 = reader.ReadUInt16();
            record.Column1 = reader.ReadUInt16();
            record.Column2 = ReadIndex(Index.String, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct ParamRecord
        {
            public ushort Column0;
            public ushort Column1;
            public uint Column2;
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
            record.Column0 = ReadIndex(Index.TypeDef, reader);
            record.Column1 = ReadIndex(Index.TypeDefOrRef, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct InterfaceImplRecord
        {
            public uint Column0;
            public uint Column1;
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
            record.Column0 = ReadIndex(Index.MemberRefParent, reader);
            record.Column1 = ReadIndex(Index.String, reader);
            record.Column2 = ReadIndex(Index.Blob, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct MemberRefRecord
        {
            public uint Column0;
            public uint Column1;
            public uint Column2;
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
            record.Column0 = reader.ReadUInt16();
            record.Column1 = ReadIndex(Index.HasConstant, reader);
            record.Column2 = ReadIndex(Index.Blob, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct ConstantRecord
        {
            public ushort Column0;
            public uint Column1;
            public uint Column2;
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
            record.Column0 = ReadIndex(Index.HasCustomAttribute, reader);
            record.Column1 = ReadIndex(Index.CustomAttributeType, reader);
            record.Column2 = ReadIndex(Index.Blob, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct CustomAttributeRecord
        {
            public uint Column0;
            public uint Column1;
            public uint Column2;
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
            record.Column0 = ReadIndex(Index.HasFieldMarshal, reader);
            record.Column1 = ReadIndex(Index.Blob, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct FieldMarshalRecord
        {
            public uint Column0;
            public uint Column1;
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
            record.Column0 = reader.ReadUInt16();
            record.Column1 = ReadIndex(Index.HasDeclSecurity, reader);
            record.Column2 = ReadIndex(Index.Blob, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct DeclSecurityRecord
        {
            public ushort Column0;
            public uint Column1;
            public uint Column2;
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
            record.Column0 = reader.ReadUInt16();
            record.Column1 = reader.ReadUInt32();
            record.Column2 = ReadIndex(Index.TypeDef, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct ClassLayoutRecord
        {
            public ushort Column0;
            public uint Column1;
            public uint Column2;
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
            record.Column0 = reader.ReadUInt32();
            record.Column1 = ReadIndex(Index.Field, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct FieldLayoutRecord
        {
            public uint Column0;
            public uint Column1;
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
            record.Column0 = ReadIndex(Index.Blob, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct StandAloneSigRecord
        {
            public uint Column0;
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
            record.Column0 = ReadIndex(Index.TypeDef, reader);
            record.Column1 = ReadIndex(Index.Event, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct EventMapRecord
        {
            public uint Column0;
            public uint Column1;
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
            record.Column0 = reader.ReadUInt16();
            record.Column1 = ReadIndex(Index.String, reader);
            record.Column2 = ReadIndex(Index.TypeDefOrRef, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct EventRecord
        {
            public ushort Column0;
            public uint Column1;
            public uint Column2;
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
            record.Column0 = ReadIndex(Index.TypeDef, reader);
            record.Column1 = ReadIndex(Index.Property, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PropertyMapRecord
        {
            public uint Column0;
            public uint Column1;
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
            record.Column0 = reader.ReadUInt16();
            record.Column1 = ReadIndex(Index.String, reader);
            record.Column2 = ReadIndex(Index.Blob, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PropertyRecord
        {
            public ushort Column0;
            public uint Column1;
            public uint Column2;
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
            record.Column0 = reader.ReadUInt16();
            record.Column1 = ReadIndex(Index.MethodDef, reader);
            record.Column2 = ReadIndex(Index.HasSemantics, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct MethodSemanticsRecord
        {
            public ushort Column0;
            public uint Column1;
            public uint Column2;
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
            record.Column0 = ReadIndex(Index.TypeDef, reader);
            record.Column1 = ReadIndex(Index.MethodDefOrRef, reader);
            record.Column2 = ReadIndex(Index.MethodDefOrRef, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct MethodImplRecord
        {
            public uint Column0;
            public uint Column1;
            public uint Column2;
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
            record.Column0 = ReadIndex(Index.String, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct ModuleRefRecord
        {
            public uint Column0;
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
            record.Column0 = ReadIndex(Index.Blob, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TypeSpecRecord
        {
            public uint Column0;
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
            record.Column0 = reader.ReadUInt16();
            record.Column1 = ReadIndex(Index.MemberForwarded, reader);
            record.Column2 = ReadIndex(Index.String, reader);
            record.Column3 = ReadIndex(Index.ModuleRef, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct ImplMapRecord
        {
            public ushort Column0;
            public uint Column1;
            public uint Column2;
            public uint Column3;
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
            record.Column0 = reader.ReadUInt32();
            record.Column1 = ReadIndex(Index.Field, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct FieldRVARecord
        {
            public uint Column0;
            public uint Column1;
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
            record.Column0 = reader.ReadUInt32();
            record.Column1 = reader.ReadUInt16();
            record.Column2 = reader.ReadUInt16();
            record.Column3 = reader.ReadUInt16();
            record.Column4 = reader.ReadUInt16();
            record.Column5 = reader.ReadUInt32();
            record.Column6 = ReadIndex(Index.Blob, reader);
            record.Column7 = ReadIndex(Index.String, reader);
            record.Column8 = ReadIndex(Index.String, reader);
            return record;
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
            record.Column0 = reader.ReadUInt32();
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct AssemblyProcessorRecord
        {
            public uint Column0;
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
            record.Column0 = reader.ReadUInt32();
            record.Column1 = reader.ReadUInt32();
            record.Column2 = reader.ReadUInt32();
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct AssemblyOSRecord
        {
            public uint Column0;
            public uint Column1;
            public uint Column2;
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
            record.Column0 = reader.ReadUInt16();
            record.Column1 = reader.ReadUInt16();
            record.Column2 = reader.ReadUInt16();
            record.Column3 = reader.ReadUInt16();
            record.Column4 = reader.ReadUInt32();
            record.Column5 = ReadIndex(Index.Blob, reader);
            record.Column6 = ReadIndex(Index.String, reader);
            record.Column7 = ReadIndex(Index.String, reader);
            record.Column8 = ReadIndex(Index.Blob, reader);
            return record;
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
            record.Column0 = reader.ReadUInt32();
            record.Column1 = ReadIndex(Index.AssemblyRef, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct AssemblyRefProcessorRecord
        {
            public uint Column0;
            public uint Column1;
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
            record.Column0 = reader.ReadUInt32();
            record.Column1 = reader.ReadUInt32();
            record.Column2 = reader.ReadUInt32();
            record.Column3 = ReadIndex(Index.AssemblyRef, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct AssemblyRefOSRecord
        {
            public uint Column0;
            public uint Column1;
            public uint Column2;
            public uint Column3;
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
            record.Column0 = reader.ReadUInt32();
            record.Column1 = ReadIndex(Index.String, reader);
            record.Column2 = ReadIndex(Index.Blob, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct FileRecord
        {
            public uint Column0;
            public uint Column1;
            public uint Column2;
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
            record.Column0 = reader.ReadUInt32();
            record.Column1 = reader.ReadUInt32();
            record.Column2 = ReadIndex(Index.String, reader);
            record.Column3 = ReadIndex(Index.String, reader);
            record.Column4 = ReadIndex(Index.Implementation, reader);
            return record;
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
            record.Column0 = reader.ReadUInt32();
            record.Column1 = reader.ReadUInt32();
            record.Column2 = ReadIndex(Index.String, reader);
            record.Column3 = ReadIndex(Index.Implementation, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct ManifestResourceRecord
        {
            public uint Column0;
            public uint Column1;
            public uint Column2;
            public uint Column3;
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
            record.Column0 = ReadIndex(Index.TypeDef, reader);
            record.Column1 = ReadIndex(Index.TypeDef, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct NestedClassRecord
        {
            public uint Column0;
            public uint Column1;
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
            record.Column0 = reader.ReadUInt16();
            record.Column1 = reader.ReadUInt16();
            record.Column2 = ReadIndex(Index.TypeOrMethodDef, reader);
            record.Column3 = ReadIndex(Index.String, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct GenericParamRecord
        {
            public ushort Column0;
            public ushort Column1;
            public uint Column2;
            public uint Column3;
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
            record.Column0 = ReadIndex(Index.MethodDefOrRef, reader);
            record.Column1 = ReadIndex(Index.Blob, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct MethodSpecRecord
        {
            public uint Column0;
            public uint Column1;
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
            record.Column0 = ReadIndex(Index.GenericParam, reader);
            record.Column1 = ReadIndex(Index.TypeDefOrRef, reader);
            return record;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct GenericParamConstraintRecord
        {
            public uint Column0;
            public uint Column1;
        }
    }
}