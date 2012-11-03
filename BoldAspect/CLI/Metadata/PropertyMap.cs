using System;
using System.IO;

namespace BoldAspect.CLI.Metadata
{
    class PropertyMapTable : Table<PropertyMapRecord>
    {
        public PropertyMapTable()
            : base(TableID.PropertyMap)
        {

        }
    }

    struct PropertyMapRecord : IMetadataRecord
    {
        [SimpleIndex(TableID.TypeDef)]
        public uint Parent;

        [SimpleIndex(TableID.Property)]
        public uint PropertyList;

        void IMetadataRecord.Read(BinaryReader reader, TableStream stream)
        {

        }
    }
}