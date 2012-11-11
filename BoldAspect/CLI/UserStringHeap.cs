using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI
{
    public sealed class UserStringHeap
    {
        private readonly Slice _data;

        public UserStringHeap(Slice data)
        {
            _data = data;
        }

        public string Get(uint index)
        {
            using (var br = _data.CreateReader())
            {
                br.Seek((int)index);
                var length = br.ReadBigEndianCompressedInteger();
                return br.ReadUTF16String(length);
            }
        }
    }
}