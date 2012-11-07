using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect
{
    public sealed class SliceReader
    {
        private readonly Slice _slice;
        private int _offset;

        public SliceReader(Slice slice)
        {
            _slice = slice;
        }

        public void Reset()
        {
            _offset = 0;
        }

        public void Seek(int offset)
        {
            _offset += offset;
        }

        public sbyte ReadSByte()
        {
            ThrowIfEnd(1);
            var r = (sbyte)_slice[_offset];
            _offset += 1;
            return r;
        }

        public short ReadInt16()
        {
            ThrowIfEnd(2);
            var r = BitConverter.ToInt16(_slice.Data, _slice.Offset + _offset);
            _offset += 2;
            return r;
        }

        public int ReadInt32()
        {
            ThrowIfEnd(4);
            var r = BitConverter.ToInt32(_slice.Data, _slice.Offset + _offset);
            _offset += 4;
            return r;
        }

        public long ReadInt64()
        {
            ThrowIfEnd(8);
            var r = BitConverter.ToInt64(_slice.Data, _slice.Offset + _offset);
            _offset += 8;
            return r;
        }

        public byte? PeekByte()
        {
            byte? r = null;
            if (_offset < _slice.Length)
                r = _slice[_offset];
            return r;
        }

        public byte ReadByte()
        {
            ThrowIfEnd(1);
            var r = _slice[_offset];
            _offset += 1;
            return r;
        }

        public string ReadASCIIString(int length)
        {
            ThrowIfEnd(length);
            var r = Encoding.ASCII.GetString(_slice.Data, _slice.Offset + _offset, length);
            _offset += length;
            return r;
        }

        public string ReadUTF8String(int length)
        {
            ThrowIfEnd(length);
            var r = Encoding.UTF8.GetString(_slice.Data, _slice.Offset + _offset, length);
            _offset += length;
            return r;
        }

        public Slice ReadSlice(int length)
        {
            ThrowIfEnd(length);
            var r = _slice.Subslice(_offset, length);
            _offset += length;
            return r;
        }

        private void ThrowIfEnd(int expectedLength)
        {
            if (_offset >= _slice.Length)
                throw new Exception();
            if (_offset + expectedLength >= _slice.Length)
                throw new Exception();
        }
    }
}
