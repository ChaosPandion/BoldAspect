using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI.Metadata.MetadataStreams
{
    public sealed class StringHeapStream : MetadataStream
    {
        public StringHeapStream(byte[] data)
            : base(data)
        {

        }

        public string Get(uint index)
        {
            var end = index;
            while (end < Data.Length)
            {
                var b = Data[end++];
                if (b == 0)
                    break;
            }
            return Encoding.UTF8.GetString(Data, (int)index, (int)end - (int)index - 1);
        }
    }
}
