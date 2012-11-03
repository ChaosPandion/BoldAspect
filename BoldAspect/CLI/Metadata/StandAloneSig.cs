using System;
using System.IO;

namespace BoldAspect.CLI.Metadata
{
    class StandAloneSigTable : Table<StandAloneSigRecord>
    {
        public StandAloneSigTable()
            : base(TableID.StandAloneSig)
        {

        }
    }

    struct StandAloneSigRecord
    {
        [BlobHeapIndex]
        public uint Signature;
    }
}