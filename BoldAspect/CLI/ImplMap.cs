using System;
using System.IO;

namespace BoldAspect.CLI.Metadata
{
    class ImplMapTable : Table<ImplMapRecord>
    {
        public ImplMapTable()
            : base(TableID.ImplMap)
        {

        }
    }

    struct ImplMapRecord
    {
        [ConstantColumn(typeof(PInvokeAttributes))]
        public PInvokeAttributes MappingFlags;

        [CodedIndex(typeof(MemberForwarded))]
        public uint MemberForwarded;

        [StringHeapIndex]
        public uint ImportName;

        [SimpleIndex(TableID.ModuleRef)]
        public uint ImportScope;
    }
}