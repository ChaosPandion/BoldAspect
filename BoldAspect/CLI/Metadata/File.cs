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

    struct FileRecord
    {
        [ConstantColumn(typeof(uint))]
        public uint Flags;

        [StringHeapIndex]
        public uint Name;

        [BlobHeapIndex]
        public uint HashValue;
    }
}