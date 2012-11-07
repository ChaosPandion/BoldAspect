using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using BoldAspect.CLI.Metadata;

namespace BoldAspect.CLI.Metadata.MetadataStreams
{
    public sealed class TableStream : MetadataStream
    {
        readonly uint _reserved1;
        readonly byte _majorVersion;
        readonly byte _minorVersion;
        public readonly HeapSizeFlags HeapSizeFlags;
        readonly byte _reserved2;
        readonly TableFlags _valid;
        readonly TableFlags _sorted;
        public readonly uint[] Rows;
        private readonly Dictionary<TableID, List<byte[]>> _tableMap = new Dictionary<TableID, List<byte[]>>();
        //readonly ModuleTable ModuleTable = new ModuleTable();
        //readonly TypeRefTable TypeRefTable = new TypeRefTable();
        //readonly TypeDefTable TypeDefTable = new TypeDefTable();
        //readonly FieldTable FieldTable = new FieldTable();
        //readonly MethodDefTable MethodDefTable = new MethodDefTable();
        //readonly ParamTable ParamTable = new ParamTable();
        //readonly InterfaceImplTable InterfaceImplTable = new InterfaceImplTable();
        //readonly MemberRefTable MemberRefTable = new MemberRefTable();
        //readonly ConstantTable ConstantTable = new ConstantTable();
        //readonly CustomAttributeTable CustomAttributeTable = new CustomAttributeTable();
        //readonly FieldMarshalTable FieldMarshalTable = new FieldMarshalTable();
        //readonly DeclSecurityTable DeclSecurityTable = new DeclSecurityTable();
        //readonly ClassLayoutTable ClassLayoutTable = new ClassLayoutTable();
        //readonly FieldLayoutTable FieldLayoutTable = new FieldLayoutTable();
        //readonly StandAloneSigTable StandAloneSigTable = new StandAloneSigTable();
        //readonly EventMapTable EventMapTable = new EventMapTable();
        //readonly EventTable EventTable = new EventTable();
        //readonly PropertyMapTable PropertyMapTable = new PropertyMapTable();
        //readonly PropertyTable PropertyTable = new PropertyTable();
        //readonly MethodSemanticsTable MethodSemanticsTable = new MethodSemanticsTable();
        //readonly MethodImplTable MethodImplTable = new MethodImplTable();
        //readonly ModuleRefTable ModuleRefTable = new ModuleRefTable();
        //readonly TypeSpecTable TypeSpecTable = new TypeSpecTable();
        //readonly ImplMapTable ImplMapTable = new ImplMapTable();
        //readonly FieldRVATable FieldRVATable = new FieldRVATable();
        //readonly AssemblyTable AssemblyTable = new AssemblyTable();
        //readonly AssemblyProcessorTable AssemblyProcessorTable = new AssemblyProcessorTable();
        //readonly AssemblyOSTable AssemblyOSTable = new AssemblyOSTable();
        //readonly AssemblyRefTable AssemblyRefTable = new AssemblyRefTable();
        //readonly AssemblyRefProcessorTable AssemblyRefProcessorTable = new AssemblyRefProcessorTable();
        //readonly AssemblyRefOSTable AssemblyRefOSTable = new AssemblyRefOSTable();
        //readonly FileTable FileTable = new FileTable();
        //readonly ExportedTypeTable ExportedTypeTable = new ExportedTypeTable();
        //readonly ManifestResourceTable ManifestResourceTable = new ManifestResourceTable();
        //readonly NestedClassTable NestedClassTable = new NestedClassTable();
        //readonly GenericParamTable GenericParamTable = new GenericParamTable();
        //readonly MethodSpecTable MethodSpecTable = new MethodSpecTable();
        //readonly GenericParamConstraintTable GenericParamConstraintTable = new GenericParamConstraintTable();

        private sealed class TableSchema
        {
            public TableID TableID;
            public int RowCount;
            public int ByteWidth;
            public object[] Columns;

            public TableSchema(TableID tableID, params object[] columns)
            {
                TableID = tableID;
                Columns = columns;
            }
        }
        private sealed class CodedIndex
        {
            public Type EnumType;
            public int TagWidth;
            public int ByteWidth;
            public TableID[] Tables;

            public CodedIndex(Type enumType, TableID[] tables)
            {
                EnumType = enumType;
                var y = (long)Enum.GetValues(enumType).Cast<object>().Select(o => Convert.ToUInt64(o)).Max();
                var x = Math.Log((y / 2) + y) / Math.Log(2);
                TagWidth = (int)Math.Ceiling(x);
                ByteWidth = 2;
                Tables = tables;
            }

            public override string ToString()
            {
                return string.Format("EnumType={0};TagWidth={1};ByteWidth={2}", EnumType.Name, TagWidth, ByteWidth);
            }
        }

        private sealed class SimpleIndex
        {
            public TableID TableID;
            public int ByteWidth;

            public SimpleIndex(TableID tableID)
            {
                TableID = tableID;
                ByteWidth = 2;
            }

            public override string ToString()
            {
                return string.Format("{0}({1})", TableID, ByteWidth);
            }
        }

        private sealed class StringsHeapIndex
        {

        }

        private sealed class GuidHeapIndex
        {

        }

        private sealed class BlobHeapIndex
        {

        }

        private int _stringsHeapIndexWidth = 2;
        private int _guidHeapIndexWidth = 2;
        private int _blobHeapIndexWidth = 2;

        private readonly SimpleIndex[] SimpleIndexes;
        private readonly CodedIndex[] CodedIndexes;
        private readonly TableSchema[] Tables;



        public TableStream(byte[] data)
            : base(data)
        {

            SimpleIndexes = GetSimpleIndexes();
            CodedIndexes = GetCodedIndexes();
            Tables = GetTables();

            using (var ms = new MemoryStream(data))
            using (var reader = new BinaryReader(ms))
            {
                _reserved1 = reader.ReadUInt32();
                _majorVersion = reader.ReadByte();
                _minorVersion = reader.ReadByte();
                HeapSizeFlags = (HeapSizeFlags)reader.ReadByte();
                _reserved2 = reader.ReadByte();
                _valid = (TableFlags)reader.ReadUInt64();
                _sorted = (TableFlags)reader.ReadUInt64();


                var vb = ValidTableCount;
                Rows = new uint[vb];
                for (int i = 0; i < vb; i++)
                {
                    Rows[i] = reader.ReadUInt32();
                }

                if (HeapSizeFlags.HasFlag(HeapSizeFlags.StringHeapIsWide))
                {
                    _stringsHeapIndexWidth = 4;
                }
                if (HeapSizeFlags.HasFlag(HeapSizeFlags.GuidHeapIsWide))
                {
                    _guidHeapIndexWidth = 4;
                }
                if (HeapSizeFlags.HasFlag(HeapSizeFlags.BlobHeapIsWide))
                {
                    _blobHeapIndexWidth = 4;
                }


                foreach (var v in Enum.GetValues(typeof(TableID)).Cast<TableID>())
                {
                    var index = TableIndex(v);
                    if (index >= 0)
                    {
                        foreach (var t in Tables)
                        {
                            if (t.TableID == v)
                            {
                                t.RowCount = (int)Rows[index];
                                break;
                            }
                        }
                        _tableMap.Add(v, new List<byte[]>());
                    }
                }

                foreach (var t in Tables.Zip(SimpleIndexes, (t, s) => new { t, s }))
                {
                    if (t.t.RowCount > ushort.MaxValue)
                    {
                        t.s.ByteWidth = 4;
                    }
                }

                foreach (var ci in CodedIndexes)
                {
                    var limit = Math.Pow(2, 16 - Math.Log(ci.Tables.Length));
                    var overLimit = false;
                    foreach (var t in Tables.Where(tb => ci.Tables.Contains(tb.TableID)))
                    {
                        if (t.RowCount > limit)
                        {
                            overLimit = true;
                            break;
                        }
                    }
                    if (overLimit)
                    {
                        ci.ByteWidth = 4;
                    }
                }

                foreach (var table in Tables)
                {
                    var w = 0;
                    foreach (var column in table.Columns)
                    {
                        var t = column as Type;
                        if (t != null)
                        {
                            if (t == typeof(byte))
                            {
                                w += 1;
                            }
                            else if (t == typeof(ushort))
                            {
                                w += 2;
                            }
                            else if (t == typeof(uint))
                            {
                                w += 4;
                            }
                            else if (t == typeof(ulong))
                            {
                                w += 8;
                            }
                            else if (t == typeof(StringsHeapIndex))
                            {
                                w += _stringsHeapIndexWidth;
                            }
                            else if (t == typeof(GuidHeapIndex))
                            {
                                w += _guidHeapIndexWidth;
                            }
                            else if (t == typeof(BlobHeapIndex))
                            {
                                w += _blobHeapIndexWidth;
                            }
                            else
                            {
                                throw new Exception();
                            }
                        }
                        var si = column as SimpleIndex;
                        if (si != null)
                        {
                            w += si.ByteWidth;
                        }
                        var ci = column as CodedIndex;
                        if (ci != null)
                        {
                            w += ci.ByteWidth;
                        }
                    }
                    table.ByteWidth = w;
                }

                foreach (var table in Tables)
                {
                    List<byte[]> list;
                    if (!_tableMap.TryGetValue(table.TableID, out list))
                        continue;
                    for (int i = 0; i < table.RowCount; i++)
                        list.Add(reader.ReadBytes(table.ByteWidth));
                }

                //ReadTable(reader, ModuleTable);
                //ReadTable(reader, TypeRefTable);
                //ReadTable(reader, TypeDefTable);
                //ReadTable(reader, FieldTable);
                //ReadTable(reader, MethodDefTable);
                //ReadTable(reader, ParamTable);
                //ReadTable(reader, InterfaceImplTable);
                //ReadTable(reader, MemberRefTable);
                //ReadTable(reader, ConstantTable);
                //ReadTable(reader, CustomAttributeTable);
                //ReadTable(reader, FieldMarshalTable);
                //ReadTable(reader, DeclSecurityTable);
                //ReadTable(reader, ClassLayoutTable);
                //ReadTable(reader, FieldLayoutTable);
                //ReadTable(reader, StandAloneSigTable);
                //ReadTable(reader, EventMapTable);
                //ReadTable(reader, EventTable);
                //ReadTable(reader, PropertyMapTable);
                //ReadTable(reader, PropertyTable);
                //ReadTable(reader, MethodSemanticsTable);
                //ReadTable(reader, MethodImplTable);
                //ReadTable(reader, ModuleRefTable);
                //ReadTable(reader, TypeSpecTable);
                //ReadTable(reader, ImplMapTable);
                //ReadTable(reader, FieldRVATable);
                //ReadTable(reader, AssemblyTable);
                //ReadTable(reader, AssemblyProcessorTable);
                //ReadTable(reader, AssemblyOSTable);
                //ReadTable(reader, AssemblyRefTable);
                //ReadTable(reader, AssemblyRefProcessorTable);
                //ReadTable(reader, AssemblyRefOSTable);
                //ReadTable(reader, FileTable);
                //ReadTable(reader, ExportedTypeTable);
                //ReadTable(reader, ManifestResourceTable);
                //ReadTable(reader, NestedClassTable);
                //ReadTable(reader, GenericParamTable);
                //ReadTable(reader, MethodSpecTable);
                //ReadTable(reader, GenericParamConstraintTable);
            }
        }


        private SimpleIndex[] GetSimpleIndexes()
        {
            return new[] {
                new SimpleIndex(TableID.Module),
                new SimpleIndex(TableID.TypeRef),
                new SimpleIndex(TableID.TypeDef),
                new SimpleIndex(TableID.Field),
                new SimpleIndex(TableID.MethodDef),
                new SimpleIndex(TableID.Param),
                new SimpleIndex(TableID.InterfaceImpl),
                new SimpleIndex(TableID.MemberRef),
                new SimpleIndex(TableID.Constant),
                new SimpleIndex(TableID.CustomAttribute),
                new SimpleIndex(TableID.FieldMarshal),
                new SimpleIndex(TableID.DeclSecurity),
                new SimpleIndex(TableID.ClassLayout),
                new SimpleIndex(TableID.FieldLayout),
                new SimpleIndex(TableID.StandAloneSig),
                new SimpleIndex(TableID.EventMap),
                new SimpleIndex(TableID.Event),
                new SimpleIndex(TableID.PropertyMap),
                new SimpleIndex(TableID.Property),
                new SimpleIndex(TableID.MethodSemantics),
                new SimpleIndex(TableID.MethodImpl),
                new SimpleIndex(TableID.ModuleRef),
                new SimpleIndex(TableID.TypeSpec),
                new SimpleIndex(TableID.ImplMap),
                new SimpleIndex(TableID.FieldRVA),
                new SimpleIndex(TableID.Assembly),
                new SimpleIndex(TableID.AssemblyProcessor),
                new SimpleIndex(TableID.AssemblyOS),
                new SimpleIndex(TableID.AssemblyRef),
                new SimpleIndex(TableID.AssemblyRefProcessor),
                new SimpleIndex(TableID.AssemblyRefOS),
                new SimpleIndex(TableID.File),
                new SimpleIndex(TableID.ExportedType),
                new SimpleIndex(TableID.ManifestResource),
                new SimpleIndex(TableID.NestedClass),
                new SimpleIndex(TableID.GenericParam),
                new SimpleIndex(TableID.MethodSpec),
                new SimpleIndex(TableID.GenericParamConstraint)
            };
        }

        private CodedIndex[] GetCodedIndexes()
        {
            return new[] {
                new CodedIndex(typeof(CustomAttributeType), new [] { TableID.MethodDef, TableID.MemberRef}),
                new CodedIndex(typeof(HasConstant), new [] { TableID.Field, TableID.Param, TableID.Property}),
                new CodedIndex(typeof(HasCustomAttribute), new [] { TableID.MethodDef, TableID.Field, TableID.TypeRef, TableID.TypeDef, TableID.Param, TableID.InterfaceImpl, TableID.MemberRef, TableID.Module, TableID.Property, TableID.Event, TableID.StandAloneSig, TableID.ModuleRef, TableID.TypeSpec, TableID.Assembly, TableID.AssemblyRef, TableID.File, TableID.ExportedType, TableID.ManifestResource, TableID.GenericParam, TableID.GenericParamConstraint, TableID.MethodSpec}),
                new CodedIndex(typeof(HasDeclSecurity), new [] { TableID.TypeDef, TableID.MethodDef, TableID.Assembly}),
                new CodedIndex(typeof(HasFieldMarshal), new [] { TableID.Field, TableID.Param}),
                new CodedIndex(typeof(HasSemantics), new [] { TableID.Event, TableID.Property}),
                new CodedIndex(typeof(Implementation), new [] { TableID.File, TableID.AssemblyRef, TableID.ExportedType}),
                new CodedIndex(typeof(MemberForwarded), new [] { TableID.Field, TableID.MethodDef}),
                new CodedIndex(typeof(MemberRefParent), new [] { TableID.TypeDef, TableID.TypeRef, TableID.ModuleRef, TableID.MethodDef, TableID.TypeSpec}),
                new CodedIndex(typeof(MethodDefOrRef), new [] { TableID.MethodDef, TableID.MemberRef}),
                new CodedIndex(typeof(ResolutionScope), new [] { TableID.Module, TableID.ModuleRef, TableID.AssemblyRef, TableID.TypeRef}),
                new CodedIndex(typeof(TypeDefOrRef), new [] { TableID.TypeDef, TableID.TypeRef, TableID.TypeSpec}),
                new CodedIndex(typeof(TypeOrMethodDef), new [] { TableID.TypeDef, TableID.MethodDef})
            };
        }

        private TableSchema[] GetTables()
        {
            return new[] {
                new TableSchema(TableID.Module,
                        typeof(UInt16),
                        typeof(StringsHeapIndex),
                        typeof(GuidHeapIndex),
                        typeof(GuidHeapIndex),
                        typeof(GuidHeapIndex)),
                new TableSchema(TableID.TypeRef,
                        CodedIndexes.Single(c => c.EnumType == typeof(ResolutionScope)),
                        typeof(StringsHeapIndex),
                        typeof(StringsHeapIndex)),
                new TableSchema(TableID.TypeDef,
                        typeof(UInt32),
                        typeof(StringsHeapIndex),
                        typeof(StringsHeapIndex),
                        CodedIndexes.Single(c => c.EnumType == typeof(TypeDefOrRef)),
                        SimpleIndexes[3],
                        SimpleIndexes[4]),
                new TableSchema(TableID.Field,
                        typeof(UInt16),
                        typeof(StringsHeapIndex),
                        typeof(BlobHeapIndex)),
                new TableSchema(TableID.MethodDef,
                        typeof(UInt32),
                        typeof(UInt16),
                        typeof(UInt16),
                        typeof(StringsHeapIndex),
                        typeof(BlobHeapIndex),
                        SimpleIndexes[5]),
                new TableSchema(TableID.Param,
                        typeof(UInt16),
                        typeof(UInt16),
                        typeof(StringsHeapIndex)),
                new TableSchema(TableID.InterfaceImpl,
                        SimpleIndexes[2],
                        CodedIndexes.Single(c => c.EnumType == typeof(TypeDefOrRef))),
                new TableSchema(TableID.MemberRef,
                        CodedIndexes.Single(c => c.EnumType == typeof(MemberRefParent)),
                        typeof(StringsHeapIndex),
                        typeof(BlobHeapIndex)),
                new TableSchema(TableID.Constant,
                        typeof(UInt16),
                        CodedIndexes.Single(c => c.EnumType == typeof(HasConstant)),
                        typeof(BlobHeapIndex)),
                new TableSchema(TableID.CustomAttribute,
                        CodedIndexes.Single(c => c.EnumType == typeof(HasCustomAttribute)),
                        CodedIndexes.Single(c => c.EnumType == typeof(CustomAttributeType)),
                        typeof(BlobHeapIndex)),
                new TableSchema(TableID.FieldMarshal,
                        CodedIndexes.Single(c => c.EnumType == typeof(HasFieldMarshal)),
                        typeof(BlobHeapIndex)),
                new TableSchema(TableID.DeclSecurity,
                        typeof(UInt16),
                        CodedIndexes.Single(c => c.EnumType == typeof(HasDeclSecurity)),
                        typeof(BlobHeapIndex)),
                new TableSchema(TableID.ClassLayout,
                        typeof(UInt16),
                        typeof(UInt32),
                        SimpleIndexes[2]),
                new TableSchema(TableID.FieldLayout,
                        typeof(UInt32),
                        SimpleIndexes[3]),
                new TableSchema(TableID.StandAloneSig,
                        typeof(BlobHeapIndex)),
                new TableSchema(TableID.EventMap,
                        SimpleIndexes[2],
                        SimpleIndexes[16]),
                new TableSchema(TableID.Event,
                        typeof(UInt16),
                        typeof(StringsHeapIndex),
                        CodedIndexes.Single(c => c.EnumType == typeof(TypeDefOrRef))),
                new TableSchema(TableID.PropertyMap,
                        SimpleIndexes[2],
                        SimpleIndexes[18]),
                new TableSchema(TableID.Property,
                        typeof(UInt16),
                        typeof(StringsHeapIndex),
                        typeof(BlobHeapIndex)),
                new TableSchema(TableID.MethodSemantics,
                        typeof(UInt16),
                        SimpleIndexes[4],
                        CodedIndexes.Single(c => c.EnumType == typeof(HasSemantics))),
                new TableSchema(TableID.MethodImpl,
                        SimpleIndexes[2],
                        CodedIndexes.Single(c => c.EnumType == typeof(MethodDefOrRef)),
                        CodedIndexes.Single(c => c.EnumType == typeof(MethodDefOrRef))),
                new TableSchema(TableID.ModuleRef,
                        typeof(StringsHeapIndex)),
                new TableSchema(TableID.TypeSpec,
                        typeof(BlobHeapIndex)),
                new TableSchema(TableID.ImplMap,
                        typeof(UInt16),
                        CodedIndexes.Single(c => c.EnumType == typeof(MemberForwarded)),
                        typeof(StringsHeapIndex),
                        SimpleIndexes[21]),
                new TableSchema(TableID.FieldRVA,
                        typeof(UInt32),
                        SimpleIndexes[3]),
                new TableSchema(TableID.Assembly,
                        typeof(UInt32),
                        typeof(UInt16),
                        typeof(UInt16),
                        typeof(UInt16),
                        typeof(UInt16),
                        typeof(UInt32),
                        typeof(BlobHeapIndex),
                        typeof(StringsHeapIndex),
                        typeof(StringsHeapIndex)),
                new TableSchema(TableID.AssemblyProcessor,
                        typeof(UInt32)),
                new TableSchema(TableID.AssemblyOS,
                        typeof(UInt32),
                        typeof(UInt32),
                        typeof(UInt32)),
                new TableSchema(TableID.AssemblyRef,
                        typeof(UInt16),
                        typeof(UInt16),
                        typeof(UInt16),
                        typeof(UInt16),
                        typeof(UInt32),
                        typeof(BlobHeapIndex),
                        typeof(StringsHeapIndex),
                        typeof(StringsHeapIndex),
                        typeof(BlobHeapIndex)),
                new TableSchema(TableID.AssemblyRefProcessor,
                        typeof(UInt32),
                        SimpleIndexes[28]),
                new TableSchema(TableID.AssemblyRefOS,
                        typeof(UInt32),
                        typeof(UInt32),
                        typeof(UInt32),
                        SimpleIndexes[28]),
                new TableSchema(TableID.File,
                        typeof(UInt32),
                        typeof(StringsHeapIndex),
                        typeof(BlobHeapIndex)),
                new TableSchema(TableID.ExportedType,
                        typeof(UInt32),
                        typeof(UInt32),
                        typeof(StringsHeapIndex),
                        typeof(StringsHeapIndex),
                        CodedIndexes.Single(c => c.EnumType == typeof(Implementation))),
                new TableSchema(TableID.ManifestResource,
                        typeof(UInt32),
                        typeof(UInt32),
                        typeof(StringsHeapIndex),
                        CodedIndexes.Single(c => c.EnumType == typeof(Implementation))),
                new TableSchema(TableID.NestedClass,
                        SimpleIndexes[2],
                        SimpleIndexes[2]),
                new TableSchema(TableID.GenericParam,
                        typeof(UInt16),
                        typeof(UInt16),
                        CodedIndexes.Single(c => c.EnumType == typeof(TypeOrMethodDef)),
                        typeof(StringsHeapIndex)),
                new TableSchema(TableID.MethodSpec,
                        CodedIndexes.Single(c => c.EnumType == typeof(MethodDefOrRef)),
                        typeof(BlobHeapIndex)),
                new TableSchema(TableID.GenericParamConstraint,
                        SimpleIndexes[35],
                        CodedIndexes.Single(c => c.EnumType == typeof(TypeDefOrRef)))
            };
        }




        private static readonly int[] _tableIDValues = Enum.GetValues(typeof(TableID)).Cast<object>().Select(o => (int)o).ToArray();
        public int ValidTableCount
        {
            get
            {
                var count = 0;
                foreach (var i in _tableIDValues)
                {
                    if (TableExists((TableID)i))
                        count++;
                }
                return count;
            }
        }

        bool TableExists(TableID id)
        {
            return (((ulong)_valid >> (int)id) & 1) == 1;
        }

        public int TableIndex(TableID id)
        {
            var vals = Enum.GetValues(typeof(TableID)).Cast<object>().Select(o => (int)o).Where(i => TableExists((TableID)i)).ToArray();
            var lim = (int)ValidTableCount;
            for (int i = 0, j = 0; i < lim; i++)
            {
                if (vals[j] == (int)id)
                {
                    return i;
                }
                j++;
            }
            return -1;
        }

        public int GetRowCount(TableID id)
        {
            var index = TableIndex(id);
            if (index < 0)
                return 0;
            return (int)Rows[index];
        }

        void ReadTable<T>(BinaryReader reader, Table<T> target)
            where T : new()
        {
            if (TableExists(target.TableID))
            {
                var type = typeof(T);
                var fields = type.GetFields().Select(f => new
                {
                    field = f,
                    type = f.FieldType.IsEnum ? Enum.GetUnderlyingType(f.FieldType) : f.FieldType,
                    attr = (ColumnAttribute)f.GetCustomAttributes(typeof(ColumnAttribute), true).First()
                }).ToArray();
                var c = GetRowCount(target.TableID);
                for (int i = 0; i < c; i++)
                {
                    var v = (object)new T();
                    foreach (var f in fields)
                    {
                        f.field.SetValue(v, Convert.ChangeType(f.attr.GetIndex(reader, this), f.type));
                    }
                    target.Add((T)v);
                }
            }
        }
    }
}
