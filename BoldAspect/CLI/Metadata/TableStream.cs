using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using BoldAspect.CLI.Metadata;

namespace BoldAspect.CLI.Metadata
{
    public sealed class TableStream
    {
        public uint Reserved1;
        public byte MajorVersion;
        public byte MinorVersion;
        public byte HeapSizes;
        public byte Reserved2;
        public ulong Valid;
        public ulong Sorted;
        public uint[] Rows;
        
        readonly ModuleTable ModuleTable = new ModuleTable();
        readonly TypeRefTable TypeRefTable = new TypeRefTable();
        readonly TypeDefTable TypeDefTable = new TypeDefTable();
        readonly FieldTable FieldTable = new FieldTable();
        public readonly MethodDefTable MethodDefTable = new MethodDefTable();
        readonly ParamTable ParamTable = new ParamTable();
        readonly InterfaceImplTable InterfaceImplTable = new InterfaceImplTable();
        readonly MemberRefTable MemberRefTable = new MemberRefTable();
        readonly ConstantTable ConstantTable = new ConstantTable();
        readonly CustomAttributeTable CustomAttributeTable = new CustomAttributeTable();
        readonly FieldMarshalTable FieldMarshalTable = new FieldMarshalTable();
        readonly DeclSecurityTable DeclSecurityTable = new DeclSecurityTable();
        readonly ClassLayoutTable ClassLayoutTable = new ClassLayoutTable();
        readonly FieldLayoutTable FieldLayoutTable = new FieldLayoutTable();
        readonly StandAloneSigTable StandAloneSigTable = new StandAloneSigTable();
        readonly EventMapTable EventMapTable = new EventMapTable();
        readonly EventTable EventTable = new EventTable();
        readonly PropertyMapTable PropertyMapTable = new PropertyMapTable();
        readonly PropertyTable PropertyTable = new PropertyTable();
        readonly MethodSemanticsTable MethodSemanticsTable = new MethodSemanticsTable();
        readonly MethodImplTable MethodImplTable = new MethodImplTable();
        readonly ModuleRefTable ModuleRefTable = new ModuleRefTable();
        readonly TypeSpecTable TypeSpecTable = new TypeSpecTable();
        readonly ImplMapTable ImplMapTable = new ImplMapTable();
        readonly FieldRVATable FieldRVATable = new FieldRVATable();
        readonly AssemblyTable AssemblyTable = new AssemblyTable();
        readonly AssemblyProcessorTable AssemblyProcessorTable = new AssemblyProcessorTable();
        readonly AssemblyOSTable AssemblyOSTable = new AssemblyOSTable();
        readonly AssemblyRefTable AssemblyRefTable = new AssemblyRefTable();
        readonly AssemblyRefProcessorTable AssemblyRefProcessorTable = new AssemblyRefProcessorTable();
        readonly AssemblyRefOSTable AssemblyRefOSTable = new AssemblyRefOSTable();
        readonly FileTable FileTable = new FileTable();
        readonly ExportedTypeTable ExportedTypeTable = new ExportedTypeTable();
        readonly ManifestResourceTable ManifestResourceTable = new ManifestResourceTable();
        readonly NestedClassTable NestedClassTable = new NestedClassTable();
        readonly GenericParamTable GenericParamTable = new GenericParamTable();
        readonly MethodSpecTable MethodSpecTable = new MethodSpecTable();
        readonly GenericParamConstraintTable GenericParamConstraintTable = new GenericParamConstraintTable();
        
        public bool StringHeapIsWide
        {
            get { return (HeapSizes & 0x80) == 0x80; }
        }

        public bool GuidHeapIsWide
        {
            get { return (HeapSizes & 0x40) == 0x40; }
        }

        public bool BlobHeapIsWide
        {
            get { return (HeapSizes & 0x20) == 0x20; }
        }

        public int ValidBits
        {
            get
            {
                var c = 0;
                var x = Valid;
                while (x > 0)
                {
                    if ((x & 0x1) == 0x1)
                        c++;
                    x = x >> 1;
                }
                return c;
            }
        }

        private static readonly int[] TableIDValues = (int[])Enum.GetValues(typeof(TableID));
        public bool TableExists(TableID id)
        {
            var bit = (int)id;
            return ((Valid >> bit) & 0x1) == 0x1;
        }

        public int TableIndex(TableID id)
        {
            var vals = TableIDValues.Where(i => TableExists((TableID)i)).ToArray();
            var lim = (int)ValidBits;
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
                var fields = type.GetFields().Select(f => new { 
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


        public void Read(BinaryReader reader, MetadataStreamHeader streamHeader)
        {
            Reserved1 = reader.ReadUInt32();
            MajorVersion = reader.ReadByte();
            MinorVersion = reader.ReadByte();
            HeapSizes = reader.ReadByte();
            Reserved2 = reader.ReadByte();
            Valid = reader.ReadUInt64();
            Sorted = reader.ReadUInt64();
            
            var vb = ValidBits;
            Rows = new uint[vb];
            for (int i = 0; i < vb; i++)
            {
                Rows[i] = reader.ReadUInt32();
            }

            ReadTable(reader, ModuleTable);
            ReadTable(reader, TypeRefTable);
            ReadTable(reader, TypeDefTable);
            ReadTable(reader, FieldTable);
            ReadTable(reader, MethodDefTable);
            ReadTable(reader, ParamTable);
            ReadTable(reader, InterfaceImplTable);
            ReadTable(reader, MemberRefTable);
            ReadTable(reader, ConstantTable);
            ReadTable(reader, CustomAttributeTable);
            ReadTable(reader, FieldMarshalTable);
            ReadTable(reader, DeclSecurityTable);
            ReadTable(reader, ClassLayoutTable);
            ReadTable(reader, FieldLayoutTable);
            ReadTable(reader, StandAloneSigTable);
            ReadTable(reader, EventMapTable);
            ReadTable(reader, EventTable);
            ReadTable(reader, PropertyMapTable);
            ReadTable(reader, PropertyTable);
            ReadTable(reader, MethodSemanticsTable);
            ReadTable(reader, MethodImplTable);
            ReadTable(reader, ModuleRefTable);
            ReadTable(reader, TypeSpecTable);
            ReadTable(reader, ImplMapTable);
            ReadTable(reader, FieldRVATable);
            ReadTable(reader, AssemblyTable);
            ReadTable(reader, AssemblyProcessorTable);
            ReadTable(reader, AssemblyOSTable);
            ReadTable(reader, AssemblyRefTable);
            ReadTable(reader, AssemblyRefProcessorTable);
            ReadTable(reader, AssemblyRefOSTable);
            ReadTable(reader, FileTable);
            ReadTable(reader, ExportedTypeTable);
            ReadTable(reader, ManifestResourceTable);
            ReadTable(reader, NestedClassTable);
            ReadTable(reader, GenericParamTable);
            ReadTable(reader, MethodSpecTable);
            ReadTable(reader, GenericParamConstraintTable);
        }
    }
}
