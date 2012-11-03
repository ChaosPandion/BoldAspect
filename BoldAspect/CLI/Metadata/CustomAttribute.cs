using System;
using System.IO;
using BoldAspect.CLI.Metadata;

namespace BoldAspect.CLI.Metadata
{
    class CustomAttributeTable : Table<CustomAttributeRecord>
    {
        public CustomAttributeTable()
            : base(TableID.CustomAttribute)
        {

        }
    }

    struct CustomAttributeRecord : IMetadataRecord
    {
        [CodedIndex(typeof(HasCustomAttribute))]
        public uint Parent;

        [CodedIndex(typeof(CustomAttributeType))]
        public uint Type;

        [BlobHeapIndex]
        public uint Value;

        void IMetadataRecord.Read(BinaryReader reader, TableStream stream)
        {

        }
    }
}