using System;
using System.IO;

namespace BoldAspect.CLI.Metadata
{
    class EventTable : Table<EventRecord>
    {
        public EventTable()
            : base(TableID.Event)
        {

        }
    }

    struct EventRecord : IMetadataRecord
    {
        [ConstantColumn(typeof(ushort))]
        public ushort EventFlags;

        [StringHeapIndex]
        public uint Name;

        [CodedIndex(typeof(TypeDefOrRef))]
        public uint EventType;

        void IMetadataRecord.Read(BinaryReader reader, TableStream stream)
        {

        }
    }
}