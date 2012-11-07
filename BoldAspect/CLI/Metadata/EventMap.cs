using System;
using System.IO;

namespace BoldAspect.CLI.Metadata
{
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