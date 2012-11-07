using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI.Metadata.MetadataStreams
{
    public sealed class GuidHeapStream : MetadataStream
    {
        public GuidHeapStream(byte[] data)
            : base(data)
        {

        }

        public Guid Get(uint index)
        {
            var data = new byte[16];
            var start = ((int)index - 1) * 16;
            var end = start + 16;
            for (int i = start; i < end; i++)
                data[i] = Data[i];
            return new Guid(data);
        }
    }
}