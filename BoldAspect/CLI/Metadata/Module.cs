using System.IO;
using System.Runtime.InteropServices;
using BoldAspect.CLI.Metadata;

namespace BoldAspect.CLI.Metadata
{
    class ModuleTable : Table<ModuleRecord>
    {
        public ModuleTable()
            : base(TableID.Module)
        {

        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct ModuleRecord : IMetadataRecord
    {
        [ConstantColumn(typeof(ushort))]
        public ushort Generation;

        [StringHeapIndex]
        public uint Name;

        [GuidHeapIndex]
        public uint Mvid;

        [GuidHeapIndex]
        public uint EncId;

        [GuidHeapIndex]
        public uint EncBaseId;

        public void Read(BinaryReader reader, TableStream stream)
        {
            reader.ReadUInt16();
            if (stream.StringHeapIsWide)
            {
                Name = reader.ReadUInt32();
            }
            else
            {
                Name = reader.ReadUInt16();
            }
            if (stream.GuidHeapIsWide)
            {
                Mvid = reader.ReadUInt32();
                EncId = reader.ReadUInt32();
                EncBaseId = reader.ReadUInt32();
            }
            else
            {
                Mvid = reader.ReadUInt16();
                EncId = reader.ReadUInt16();
                EncBaseId = reader.ReadUInt16();
            }
            if (EncId != 0)
                throw new MetadataException();
            if (EncBaseId != 0)
                throw new MetadataException();
        }
    }
}