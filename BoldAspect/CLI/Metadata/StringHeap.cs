using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI.Metadata
{
    public sealed class StringHeap
    {
        private readonly byte[] _data;

        public StringHeap(byte[] data)
        {
            _data = data;
        }

        public string Get(uint index)
        {
            var end = index;
            while (end < _data.Length)
            {
                var b = _data[end++];
                if (b == 0)
                    break;
            }
            return Encoding.UTF8.GetString(_data, (int)index, (int)end - (int)index - 1);
        }
    }
}
