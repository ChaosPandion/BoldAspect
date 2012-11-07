using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI.Metadata
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    abstract class ColumnAttribute : Attribute
    {
        protected ColumnAttribute()
        {

        }

        public abstract ulong GetIndex(BinaryReader reader, BoldAspect.CLI.Metadata.MetadataStreams.TableStream stream);
    }
}
