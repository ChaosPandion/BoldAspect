
using System;
using System.IO;

namespace BoldAspect.CLI.Metadata
{

    class AssemblyOSTable : Table<AssemblyOSRecord>
    {
        public AssemblyOSTable()
            : base(TableID.AssemblyOS)
        {

        }
    }

    struct AssemblyOSRecord : IMetadataRecord
    {
        [ConstantColumn(typeof(uint))]
        public uint OSPlatformID;

        [ConstantColumn(typeof(uint))]
        public uint OSMajorVersion;

        [ConstantColumn(typeof(uint))]
        public uint OSMinorVersion;

        void IMetadataRecord.Read(BinaryReader reader, TableStream stream)
        {

        }
    }
}