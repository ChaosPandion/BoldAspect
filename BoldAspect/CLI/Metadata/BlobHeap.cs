using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI.Metadata
{
    public sealed class BlobHeap
    {
        private readonly byte[] _data;

        public BlobHeap(byte[] data)
        {
            _data = data;
        }

        public Slice Get(uint index)
        {
            var length = 0;
            var lengthOffset = 0;
            {
                var b1 = _data[(int)index];
                if ((b1 & 0x80) == 0x00)
                {
                    length += b1;
                    lengthOffset = 1;
                }
                else if ((b1 & 0xC0) == 0xC0)
                {
                    length += _data[(int)index + 1];
                    length += b1 << 8;
                    lengthOffset = 2;
                }
                else 
                {
                    length += b1 << 24;
                    length += _data[(int)index + 1] << 16;
                    length += _data[(int)index + 2] << 8;
                    length += _data[(int)index + 3];
                    lengthOffset = 4;
                }
            }
            return new Slice(_data, (int)index + lengthOffset, length);
        }

        public sealed class Slice : IEnumerable<byte>
        {
            private readonly byte[] _data;
            private readonly int _startIndex;
            private readonly int _count;

            public Slice(byte[] data, int startIndex, int count)
            {
                _data = data;
                _startIndex = startIndex;
                _count = count;
            }

            public int Count
            {
                get { return _count; }
            }

            public byte this[int index]
            {
                get { return _data[_startIndex + index]; }
            }

            public IEnumerator<byte> GetEnumerator()
            {
                var limit = _startIndex + _count;
                for (int i = _startIndex; i < limit; i++)
                    yield return _data[i];
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}
