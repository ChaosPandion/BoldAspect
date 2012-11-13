using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoldAspect.CLI;

namespace BoldAspect.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            BuildAssembly();
        }

        static void ReadPE()
        {
            const string fileName = @"C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll";
            var pe = new PortableExecutable(fileName);
        }

        static void BuildAssembly()
        {
            const string fileName = @"C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll";
            var assembly = MetadataStorage.ReadAssembly(fileName);
        }

        static void CodedIndexes()
        {

            var sb = new StringBuilder();
            var r = new MetadataRoot();
            foreach (var ci in r.CodedIndexes)
            {
                sb.AppendFormat("CalculateCodedIndexWidth(Index.{0}, {1}, ", ci.EnumType.Name, ci.TagWidth);
                sb.Append("new [] { ");
                sb.Append(string.Join(", ", ci.Tables.Select(t => "TableID." + t)));
                sb.Append("});\r\n");
            }
            Debug.Write(sb.ToString());
        }

        static void BuildRowCount2()
        {

            var sb = new StringBuilder();
            var r = new MetadataRoot();
            foreach (var t in r.Tables)
            {
                sb.AppendFormat("case TableID.{0}:\r\n", t.TableID);
                sb.AppendFormat("SetWidth(Index.{0}, width);\r\n", t.TableID);
                sb.AppendLine("break;");
            }
            Debug.Write(sb.ToString());
        }

        static void BuildRowCount()
        {

            var sb = new StringBuilder();
            var r = new MetadataRoot();
            foreach (var t in r.Tables)
            {
                sb.AppendFormat("case TableID.{0}:\r\n", t.TableID);
                sb.AppendFormat("{0}Table.RowCount = rowCount;\r\n", t.TableID);
                sb.AppendFormat("{0}Table.FileIndex = fileIndex;\r\n", t.TableID);
                sb.AppendFormat("Indexes.SetWidth(Index.{0}, rowCount >= ushort.MaxValue ? 4 : 2);\r\n", t.TableID);
                sb.AppendLine("break;");
            }
            Debug.Write(sb.ToString());
        }

        static void BuildTableConstruct()
        {
            
            var sb = new StringBuilder();
            var r = new MetadataRoot();
            foreach (var t in r.Tables)
            {
                sb.AppendFormat("public readonly {0}Table {0}Table;\r\n", t.TableID);
            }
            foreach (var t in r.Tables)
            {
                sb.AppendFormat("_tables.Add(TableID.{0}, new {0}Table(Indexes));\r\n", t.TableID);
            }
            Debug.Write(sb.ToString());
        }

        static void BuildTableReaders()
        {
            var colIndex = 0;
            var sb = new StringBuilder();
            var r = new MetadataRoot();
            foreach (var t in r.Tables)
            {
                sb.AppendFormat("public sealed class {0}Table : Table<{0}Table.{0}Record>\r\n", t.TableID);
                sb.AppendLine("{");
                sb.AppendFormat("public {0}Table(Indexes indexes) : base(indexes, TableID.{0}, new [] {{ ", t.TableID);
                var firstCol = true;
                foreach (var c in t.Columns)
                {
                    if (!firstCol)
                    {
                        sb.Append(", ");
                    }
                    firstCol = false;
                    sb.Append("new ColumnSchema(");

                    var type = c as Type;
                    if (type != null)
                    {
                        switch (Type.GetTypeCode(type))
                        {
                            case TypeCode.Object:
                                if (type == typeof(StringsHeapIndex))
                                {
                                    sb.AppendFormat("typeof(uint), Index.String)");
                                }
                                if (type == typeof(GuidHeapIndex))
                                {
                                    sb.AppendFormat("typeof(uint), Index.Guid)");
                                }
                                if (type == typeof(BlobHeapIndex))
                                {
                                    sb.AppendFormat("typeof(uint), Index.Blob)");
                                }
                                break;
                            case TypeCode.UInt16:
                                sb.AppendFormat("typeof(ushort), Index.None)");
                                break;
                            case TypeCode.UInt32:
                                sb.AppendFormat("typeof(uint), Index.None)");
                                break;
                            case TypeCode.UInt64:
                                sb.AppendFormat("typeof(ulong), Index.None)");
                                break;

                        }
                    }
                    var si = c as SimpleIndex;
                    if (si != null)
                    {
                        sb.AppendFormat("typeof(uint), Index.{0})", si.TableID);
                    }
                    var ci = c as CodedIndex;
                    if (ci != null)
                    {
                        sb.AppendFormat("typeof(uint), Index.{0})", ci.EnumType.Name);
                    }
                }
                sb.AppendLine("})");
                sb.AppendLine("{");
                sb.AppendLine();
                sb.AppendLine("}");
                sb.AppendLine();
                sb.AppendFormat("protected override {0}Record Read(BlobReader reader)\r\n", t.TableID);
                sb.AppendLine("{");
                sb.AppendFormat("var record = new {0}Record();\r\n", t.TableID); 
                colIndex = 0;
                foreach (var c in t.Columns)
                {
                    var type = c as Type;
                    if (type != null)
                    {
                        switch (Type.GetTypeCode(type))
                        {
                            case TypeCode.Object:
                                if (type == typeof(StringsHeapIndex))
                                {
                                    sb.AppendFormat("record.Column{0} = ReadIndex(Index.String, reader);\r\n", colIndex);
                                }
                                if (type == typeof(GuidHeapIndex))
                                {
                                    sb.AppendFormat("record.Column{0} = ReadIndex(Index.Guid, reader);\r\n", colIndex);
                                }
                                if (type == typeof(BlobHeapIndex))
                                {
                                    sb.AppendFormat("record.Column{0} = ReadIndex(Index.Blob, reader);\r\n", colIndex);
                                }
                                break;
                            case TypeCode.UInt16:
                                sb.AppendFormat("record.Column{0} = reader.ReadUInt16();\r\n", colIndex);
                                break;
                            case TypeCode.UInt32:
                                sb.AppendFormat("record.Column{0} = reader.ReadUInt32();\r\n", colIndex);
                                break;
                            case TypeCode.UInt64:
                                sb.AppendFormat("record.Column{0} = reader.ReadUInt64();\r\n", colIndex);
                                break;

                        }
                    }
                    var si = c as SimpleIndex;
                    if (si != null)
                    {
                        sb.AppendFormat("record.Column{0} = ReadIndex(Index.{1}, reader);\r\n", colIndex, si.TableID);
                    }
                    var ci = c as CodedIndex;
                    if (ci != null)
                    {
                        sb.AppendFormat("record.Column{0} = ReadIndex(Index.{1}, reader);\r\n", colIndex, ci.EnumType.Name);
                    }
                    colIndex++;
                }
                sb.AppendLine("return record;");
                sb.AppendLine("}");
                sb.AppendLine();

                sb.AppendLine("[StructLayout(LayoutKind.Sequential, Pack = 1)]");
                sb.AppendFormat("public struct {0}Record\r\n", t.TableID);
                sb.AppendLine("{"); 
                colIndex = 0;
                foreach (var c in t.Columns)
                {
                    sb.Append("    ");
                    var type = c as Type;
                    if (type != null)
                    {
                        switch (Type.GetTypeCode(type))
                        {
                            case TypeCode.Object:
                                sb.AppendFormat("public uint Column{0};\r\n", colIndex);
                                break;
                            case TypeCode.UInt16:
                                sb.AppendFormat("public ushort Column{0};\r\n", colIndex);
                                break;
                            case TypeCode.UInt32:
                                sb.AppendFormat("public uint Column{0};\r\n", colIndex);
                                break;
                            case TypeCode.UInt64:
                                sb.AppendFormat("public ulong Column{0};\r\n", colIndex);
                                break;

                        }
                    }
                    var si = c as SimpleIndex;
                    if (si != null)
                    {
                        sb.AppendFormat("public uint Column{0};\r\n", colIndex);
                    }
                    var ci = c as CodedIndex;
                    if (ci != null)
                    {
                        sb.AppendFormat("public uint Column{0};\r\n", colIndex);
                    }

                    colIndex++;
                }
                sb.AppendLine("}");
                sb.AppendLine("}");
                sb.AppendLine();
            }

            Debug.Write(sb.ToString());
        }
    }
}