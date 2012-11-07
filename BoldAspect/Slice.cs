using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect
{
    public struct Slice : IEnumerable<byte>
    {
        private readonly byte[] _data;
        private readonly int _offset;
        private readonly int _length;

        public Slice(byte[] data, int offset, int length)
        {
            _data = data;
            _offset = offset;
            _length = length;
        }

        public byte[] Data
        {
            get { return _data; }
        }

        public int Offset
        {
            get { return _offset; }
        }

        public int Length
        {
            get { return _length; }
        }

        public byte this[int index]
        {
            get { return _data[_offset + index]; }
        }

        public Slice Subslice(int offset)
        {
            return new Slice(_data, _offset + offset, _length - offset);
        }

        public Slice Subslice(int offset, int length)
        {
            return new Slice(_data, _offset + offset, length);
        }

        public short GetInt16(int offset = 0)
        {
            return BitConverter.ToInt16(_data, _offset + offset);
        }

        public int GetInt32(int offset = 0)
        {
            return BitConverter.ToInt32(_data, _offset + offset);
        }

        public long GetInt64(int offset = 0)
        {
            return BitConverter.ToInt64(_data, _offset + offset);
        }

        public string GetUTF8String(int length, int offset = 0)
        {
            return Encoding.UTF8.GetString(_data, offset, length);
        }

        public IEnumerator<byte> GetEnumerator()
        {
            var limit = _offset + _length;
            for (int i = _offset; i < limit; i++)
                yield return _data[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
