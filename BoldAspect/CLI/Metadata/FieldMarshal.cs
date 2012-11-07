using System;
using System.IO;
using BoldAspect.CLI.Metadata;

namespace BoldAspect.CLI.Metadata
{
    class FieldMarshalTable : Table<FieldMarshalRecord>
    {
        public FieldMarshalTable()
            : base(TableID.FieldMarshal)
        {

        }
    }

    struct FieldMarshalRecord 
    {
        [CodedIndex(typeof(HasFieldMarshal))] 
        public uint Parent;

        [BlobHeapIndex] 
        public uint NativeType;
    }
}