﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI
{
    sealed class SimpleIndexAttribute : ColumnAttribute
    {
        private readonly TableID _tableID;

        public SimpleIndexAttribute(TableID tableID)
        {
            _tableID = tableID;
        }

        //public TableID TableID
        //{
        //    get { return _tableID; }
        //}

        //public override ulong GetIndex(BinaryReader reader, BoldAspect.CLI.Metadata.MetadataStreams.TableStream stream)
        //{
        //    var c = stream.GetRowCount(_tableID);
        //    if (c < Math.Pow(2, 16))
        //        return reader.ReadUInt16();
        //    return reader.ReadUInt32();
        //}

    }
}
