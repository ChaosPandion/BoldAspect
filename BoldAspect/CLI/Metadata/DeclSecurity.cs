using System;
using System.IO;

namespace BoldAspect.CLI.Metadata
{
    class DeclSecurityTable : Table<DeclSecurityRecord>
    {
        public DeclSecurityTable()
            : base(TableID.DeclSecurity)
        {

        }
    }

    struct DeclSecurityRecord : IMetadataRecord
    {
        [ConstantColumn(typeof(ushort))]
        public ushort Action;

        [CodedIndex(typeof(HasDeclSecurity))]
        public uint Parent;

        [BlobHeapIndex]
        public uint PermissionSet;

        void IMetadataRecord.Read(BinaryReader reader, TableStream stream)
        {

        }
    }
}