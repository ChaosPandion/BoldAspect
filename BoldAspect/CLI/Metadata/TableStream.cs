using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
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
        public readonly uint _reserved1;
        public readonly byte _majorVersion;
        public readonly byte _minorVersion;
        public readonly HeapSizeFlags HeapSizeFlags;
        public readonly byte _reserved2;
        public readonly TableFlags _valid;
        public readonly TableFlags _sorted;
        public readonly uint[] Rows;
        public readonly Dictionary<TableID, List<byte[]>> _tableMap = new Dictionary<TableID, List<byte[]>>();

        public sealed class TableSchema
        {
            public TableID TableID;
            public int RowCount;
            public int RowWidth;
            public int Offset;
            public int ByteLength;
            public object[] Columns;

            public TableSchema(TableID tableID, params object[] columns)
            {
                TableID = tableID;
                Columns = columns;
            }

            public override string ToString()
            {
                return string.Format("TableID={0};RowCount={1};RowWidth={2}", TableID, RowCount, RowWidth);
            }
        }
        public sealed class CodedIndex
        {
            public Type EnumType;
            public int TagWidth;
            public int ByteWidth;
            public TableID[] Tables;

            public CodedIndex(Type enumType, TableID[] tables)
            {
                EnumType = enumType;
                var y = (double)Enum.GetValues(enumType).Cast<object>().Select(o => Convert.ToUInt64(o)).Max();
                var c = y / 2.0;
                var x = Math.Log(c + y) / Math.Log(2);
                TagWidth = (int)Math.Ceiling(x);
                ByteWidth = 2;
                Tables = tables;
            }

            public override string ToString()
            {
                return string.Format("EnumType={0};TagWidth={1};ByteWidth={2}", EnumType.Name, TagWidth, ByteWidth);
            }
        }

        public sealed class SimpleIndex
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

        public int _stringsHeapIndexWidth = 2;
        public int _guidHeapIndexWidth = 2;
        public int _blobHeapIndexWidth = 2;

        public readonly SimpleIndex[] SimpleIndexes;
        public readonly CodedIndex[] CodedIndexes;
        public readonly TableSchema[] Tables;



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


                for (int i = 0; i < 64; i++)
                {
                    if (!Enum.IsDefined(typeof(TableID), i))
                        continue;
                    Console.WriteLine("{0} = {1} -> {2}", (TableID)i, i, TableExists((TableID)i));
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
                    else
                    {
                        Debug.WriteLine(v);
                    }
                }

                foreach (var t in Tables.Zip(SimpleIndexes, (t, s) => new { t, s }))
                {
                    if (t.t.RowCount >= ushort.MaxValue)
                    {
                        t.s.ByteWidth = 4;
                    }
                }

                foreach (var ci in CodedIndexes)
                {
                    var limit = Math.Pow(2, 16 - Math.Log(ci.Tables.Length));
                    var overLimit = false;

                    var max = 0;
                    foreach (var t in Tables.Where(tb => ci.Tables.Contains(tb.TableID)))
                    {
                        max = Math.Max(max, t.RowCount);
                    }
                    overLimit = max > (1 << (16 - ci.TagWidth));
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
                    table.RowWidth = w;
                    Console.WriteLine(table);
                }

                foreach (var table in Tables)
                {
                    if (!TableExists(table.TableID))
                        continue;
                    List<byte[]> list;
                    if (!_tableMap.TryGetValue(table.TableID, out list))
                        continue;
                    for (int i = 0; i < table.RowCount; i++)
                        list.Add(reader.ReadBytes(table.RowWidth));
                }
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
                new CodedIndex(typeof(TypeDefOrRef), new [] { TableID.TypeDef, TableID.TypeRef, TableID.TypeSpec}),
                new CodedIndex(typeof(HasConstant), new [] { TableID.Field, TableID.Param, TableID.Property}),
                new CodedIndex(typeof(HasCustomAttribute), new [] { TableID.MethodDef, TableID.Field, TableID.TypeRef, TableID.TypeDef, TableID.Param, TableID.InterfaceImpl, TableID.MemberRef, TableID.Module, TableID.Property, TableID.Event, TableID.StandAloneSig, TableID.ModuleRef, TableID.TypeSpec, TableID.Assembly, TableID.AssemblyRef, TableID.File, TableID.ExportedType, TableID.ManifestResource, TableID.GenericParam, TableID.GenericParamConstraint, TableID.MethodSpec}),
                new CodedIndex(typeof(HasFieldMarshal), new [] { TableID.Field, TableID.Param}),
                new CodedIndex(typeof(HasDeclSecurity), new [] { TableID.TypeDef, TableID.MethodDef, TableID.Assembly}),
                new CodedIndex(typeof(MemberRefParent), new [] { TableID.TypeDef, TableID.TypeRef, TableID.ModuleRef, TableID.MethodDef, TableID.TypeSpec}),
                new CodedIndex(typeof(HasSemantics), new [] { TableID.Event, TableID.Property}),
                new CodedIndex(typeof(MethodDefOrRef), new [] { TableID.MethodDef, TableID.MemberRef}),
                new CodedIndex(typeof(MemberForwarded), new [] { TableID.Field, TableID.MethodDef}),
                new CodedIndex(typeof(Implementation), new [] { TableID.File, TableID.AssemblyRef, TableID.ExportedType}),
                new CodedIndex(typeof(CustomAttributeType), new [] { TableID.MethodDef, TableID.MemberRef}),
                new CodedIndex(typeof(ResolutionScope), new [] { TableID.Module, TableID.ModuleRef, TableID.AssemblyRef, TableID.TypeRef}),
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

            return ((long)_valid & (1L << (int)id)) != 0;
            //var bitIndex = (int)id;
            //return (((ulong)_valid >> bitIndex) & 1) != 0;
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
