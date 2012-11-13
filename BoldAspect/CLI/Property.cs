using System;
using System.IO;

namespace BoldAspect.CLI
{
    [Flags]
    public enum PropertyAttributes : ushort
    {
        SpecialName = 0x0200,
        RTSpecialName = 0x0400,
        HasDefault = 0x1000,
        Unused = 0xe9ff,
    }

    //class PropertyTable : Table<PropertyRecord>
    //{
    //    public PropertyTable()
    //        : base(TableID.Property)
    //    {

    //    }
    //}

    //struct PropertyRecord
    //{
    //    [ConstantColumn(typeof(PropertyAttributes))]
    //    public PropertyAttributes Flags;

    //    [StringHeapIndex]
    //    public uint Name;

    //    [BlobHeapIndex]
    //    public uint Type;
    //}

    //class PropertyMapTable : Table<PropertyMapRecord>
    //{
    //    public PropertyMapTable()
    //        : base(TableID.PropertyMap)
    //    {

    //    }
    //}

    //struct PropertyMapRecord
    //{
    //    [SimpleIndex(TableID.TypeDef)]
    //    public uint Parent;

    //    [SimpleIndex(TableID.Property)]
    //    public uint PropertyList;
    //}
}