using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI.Metadata
{
    public class TableReader : BlobReader
    {
        public TableReader(byte[] data, int offset, int length)
            : base(data, offset, length)
        {

        }
    }
}