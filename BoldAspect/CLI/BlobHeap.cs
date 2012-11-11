using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BoldAspect.CLI
{
    public sealed class BlobHeap
    {
        private readonly Slice _data;

        public BlobHeap(Slice data)
        {
            _data = data;
        }

        public Slice Get(uint index)
        {
            using (var br = _data.CreateReader())
            {
                br.Seek((int)index);
                var length = br.ReadBigEndianCompressedInteger();
                return br.ReadSlice(length);
            }
        }
    }
}