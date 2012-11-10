using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI.Metadata.MetadataStreams
{
    public sealed class StringHeap
    {
        private readonly Slice _data;

        public StringHeap(Slice data)
        {
            _data = data;
        }

        public string Get(uint index)
        {
            using (var br = _data.CreateReader())
            {
                br.Seek((int)index);
                return br.ReadNullTerminatedUTF8String();
            }
        }
    }
}
