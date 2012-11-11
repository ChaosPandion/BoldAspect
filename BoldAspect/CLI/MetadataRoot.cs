using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI
{
    public sealed class MetadataRoot
    {
        private const int _validSignature = 0x424A5342;
        private const short _validMajorVersion = 1;
        private const short _validMinorVersion = 1;
        private const int _validReserved = 0;
        private const short _validFlags = 0;

        private static readonly int[] _tableIDValues = Enum.GetValues(typeof(TableID)).Cast<object>().Select(o => (int)o).ToArray();

        private readonly Slice _data;
        public readonly int _signature;
        public readonly short _majorVersion;
        public readonly short _minorVersion;
        public readonly int _reserved;
        public readonly int _length;
        public readonly string _version;
        public readonly short _flags;
        public readonly short _streams;
        public readonly StreamHeader[] _headers;
        public readonly uint _reserved1;
        public readonly byte _tableMajorVersion;
        public readonly byte _tableMinorVersion;
        public readonly HeapSizeFlags _heapSizeFlags;
        public readonly byte _reserved2;
        public readonly ulong _valid;
        public readonly ulong _sorted;
        public readonly uint[] _rows;
        public readonly Dictionary<TableID, Slice> _tableMap = new Dictionary<TableID, Slice>();
        public int _stringsHeapIndexWidth = 2;
        public int _guidHeapIndexWidth = 2;
        public int _blobHeapIndexWidth = 2;
        public readonly SimpleIndex[] SimpleIndexes;
        public readonly CodedIndex[] CodedIndexes;
        public readonly TableSchema[] Tables;
        public readonly Slice _stringHeap;
        public readonly Slice _userStringHeap;
        public readonly Slice _blobHeap;
        public readonly Slice _guidHeap;

        public MetadataRoot(Slice data)
        {
            _data = data;
            using (var br = _data.CreateReader())
            {
                _signature = br.ReadAssert(_validSignature);
                _majorVersion = br.ReadAssert(_validMajorVersion);
                _minorVersion = br.ReadAssert(_validMinorVersion);
                _reserved = br.ReadAssert(_validReserved);
                _length = br.Read<int>();
                _version = br.ReadUTF8String(_length);
                _flags = br.ReadAssert(_validFlags);
                _streams = br.Read<short>();
                _headers = new StreamHeader[_streams];
                for (int i = 0; i < _streams; i++)
                {
                    var header = _headers[i] = new StreamHeader(br);
                    var slice = data.GetSlice(header.Offset, header.Size);
                    switch (header.Name)
                    {
                        case "#Strings":
                            _stringHeap = slice;
                            break;
                        case "#US":
                            _userStringHeap = slice;
                            break;
                        case "#Blob":
                            _blobHeap = slice;
                            break;
                        case "#GUID":
                            _guidHeap = slice;
                            break;
                        case "#~":
                            SimpleIndexes = GetSimpleIndexes();
                            CodedIndexes = GetCodedIndexes();
                            Tables = GetTables();
                            using (var reader = slice.CreateReader())
                            {
                                _reserved1 = reader.Read<uint>();
                                _tableMajorVersion = reader.Read<byte>();
                                _tableMinorVersion = reader.Read<byte>();
                                _heapSizeFlags = reader.Read<HeapSizeFlags>();
                                _reserved2 = reader.Read<byte>();
                                _valid = reader.Read<ulong>();
                                _sorted = reader.Read<ulong>();


                                var vb = ValidTableCount;
                                _rows = new uint[vb];
                                for (int r = 0; r < vb; r++)
                                {
                                    _rows[r] = reader.Read<uint>();
                                }


                                for (int t = 0; t < 64; t++)
                                {
                                    if (!Enum.IsDefined(typeof(TableID), i))
                                        continue;
                                    Console.WriteLine("{0} = {1} -> {2}", (TableID)t, t, TableExists((TableID)t));
                                }

                                if (_heapSizeFlags.HasFlag(HeapSizeFlags.StringHeapIsWide))
                                {
                                    _stringsHeapIndexWidth = 4;
                                }
                                if (_heapSizeFlags.HasFlag(HeapSizeFlags.GuidHeapIsWide))
                                {
                                    _guidHeapIndexWidth = 4;
                                }
                                if (_heapSizeFlags.HasFlag(HeapSizeFlags.BlobHeapIsWide))
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
                                                t.RowCount = (int)_rows[index];
                                                break;
                                            }
                                        }
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
                                }

                                foreach (var table in Tables)
                                {
                                    if (!TableExists(table.TableID))
                                        continue;
                                    var tableData = reader.ReadSlice((table.RowCount * table.RowWidth));
                                    //Debug.WriteLine(BitConverter.ToString(tableData.Data, tableData.Offset, tableData.Length));
                                    _tableMap.Add(table.TableID, tableData);
                                }
                            }
                            break;
                        default:
                            throw new MetadataException();
                    }
                }
            }
        }

        public TableReader CreateTableReader(TableID table)
        {
            return new TableReader(this, table);
        }


        public Blob GetBlob(uint index)
        {
            using (var br = _blobHeap.CreateReader())
            {
                br.Seek((int)index);
                var length = br.ReadBigEndianCompressedInteger();
                var data = br.ReadBytes(length);
                return new Blob(data, 0, length);
            }
        }

        public Guid GetGuid(uint index)
        {
            using (var br = _guidHeap.CreateReader())
                return br.Read<Guid>(((int)index - 1) * 16);
        }

        public string GetString(uint index)
        {
            using (var br = _stringHeap.CreateReader())
            {
                br.Seek((int)index);
                return br.ReadNullTerminatedUTF8String();
            }
        }


        public string GetUserString(uint index)
        {
            using (var br = _userStringHeap.CreateReader())
            {
                br.Seek((int)index);
                var length = br.ReadBigEndianCompressedInteger();
                return br.ReadUTF16String(length);
            }
        }


        public Slice GetTableData(TableID table)
        {
            Slice result;
            _tableMap.TryGetValue(table, out result);
            return result;
        }

        public TableSchema GetTableSchema(TableID id)
        {
            return Tables.Single(t => t.TableID == id);
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
            return (int)_rows[index];
        }
    }
}