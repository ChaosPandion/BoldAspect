using System;
using System.IO;

namespace BoldAspect.CLI
{
    [Flags]
    public enum FileAttributes : uint
    {
        ContainsMetadata = 0x0000,
        ContainsNoMetadata = 0x0001
    }

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