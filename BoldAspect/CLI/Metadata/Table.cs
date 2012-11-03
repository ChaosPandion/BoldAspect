using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI.Metadata
{
    public abstract class Table<T> : List<T>
    {
        public TableID TableID { get; private set; }

        protected Table(TableID tableID)
        {
            TableID = tableID;
        }
    }
}