using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI.Metadata
{
    public abstract class MetadataStream
    {
        private readonly MetadataStreamHeader _header;
        private readonly byte[] _data;

        protected MetadataStream(ref MetadataStreamHeader header, byte[] data)
        {
            _header = header;
            _data = data;
        }

        public static MetadataStream Create(ref MetadataStreamHeader header, byte[] data)
        {
            return null;
        }

    }
}