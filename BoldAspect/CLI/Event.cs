using System;
using System.IO;

namespace BoldAspect.CLI
{
    [Flags]
    public enum EventAttributes : ushort
    {
        SpecialName = 0x0200,
        RTSpecialName = 0x0400
    }

    class EventTable : Table<EventRecord>
    {
        public EventTable()
            : base(TableID.Event)
        {

        }
    }

    struct EventRecord
    {
        [ConstantColumn(typeof(ushort))]
        public ushort EventFlags;

        [StringHeapIndex]
        public uint Name;

        [CodedIndex(typeof(TypeDefOrRef))]
        public uint EventType;
    }

    class EventMapTable : Table<EventMapRecord>
    {
        public EventMapTable()
            : base(TableID.EventMap)
        {

        }
    }

    struct EventMapRecord
    {
        [SimpleIndex(TableID.TypeDef)]
        public uint Parent;

        [SimpleIndex(TableID.Event)]
        public uint EventList;
    }
}