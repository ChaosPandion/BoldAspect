using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI
{
    public abstract class MetadataStream
    {
        private readonly byte[] _data;

        protected MetadataStream(byte[] data)
        {
            _data = data;
        }

        protected byte[] Data
        {
            get { return _data; }
        }
    }
}