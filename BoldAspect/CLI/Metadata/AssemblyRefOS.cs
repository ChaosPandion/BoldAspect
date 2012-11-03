using System;
using System.IO;

namespace BoldAspect.CLI.Metadata
{
    class AssemblyRefOSTable : Table<AssemblyRefOSRecord>
    {
        public AssemblyRefOSTable()
            : base(TableID.AssemblyRefOS)
        {

        }
    }

    struct AssemblyRefOSRecord : IMetadataRecord
    {
        [ConstantColumn(typeof(uint))]
        public uint OSPlatformID;

        [ConstantColumn(typeof(uint))]
        public uint OSMajorVersion;

        [ConstantColumn(typeof(uint))]
        public uint OSMinorVersion;

        [SimpleIndex(TableID.AssemblyRef)]
        public uint AssemblyRef;


        void IMetadataRecord.Read(BinaryReader reader, TableStream stream)
        {

        }
    }
}