using System;
using System.IO;

namespace BoldAspect.CLI.Metadata
{
    class FileTable : Table<FileRecord>
    {
        public FileTable()
            : base(TableID.File)
        {

        }
    }

    struct FileRecord : IMetadataRecord
    {
        [ConstantColumn(typeof(uint))]
        public uint Flags;

        [StringHeapIndex]
        public uint Name;

        [BlobHeapIndex]
        public uint HashValue;

        void IMetadataRecord.Read(BinaryReader reader, TableStream stream)
        {

        }
    }
}