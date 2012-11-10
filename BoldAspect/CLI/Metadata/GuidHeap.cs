using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI.Metadata.MetadataStreams
{
    public sealed class GuidHeap
    {
        private readonly Slice _data;

        public GuidHeap(Slice data)
        {
            _data = data;
        }

        public Guid Get(uint index)
        {
            using (var br = new BlobReader(_data))
                return br.Read<Guid>(((int)index - 1) * 16);
        }
    }
}