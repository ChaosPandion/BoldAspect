using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace BoldAspect
{   
    [DebuggerStepThrough]
    public struct Blob : IList<byte>
    {
        readonly byte[] _data;
        readonly int _offset;
        readonly int _length;

        public Blob(byte[] data, int offset, int length)
        {
            if (data == null)
                throw new ArgumentNullException("data");
            if (offset < 0 || offset >= data.Length || length < 0 || offset + length >= data.Length)
                throw new ArgumentException("The supplied offset and length are not within range.");
            _data = data;
            _offset = offset;
            _length = length;
        }

        public int Length
        {
            get { return _length; }
        }

        int ICollection<byte>.Count
        {
            get { return _length; }
        }

        bool ICollection<byte>.IsReadOnly
        {
            get { return true; }
        }

        public byte this[int index]
        {
            get { return _data[index]; }
            set { throw new NotSupportedException(); }
        }

        public BlobReader ToReader()
        {
            return new BlobReader(_data, _offset, _length);
        }

        public byte[] ToArray()
        {
            var result = new byte[Length];
            Array.Copy(_data, _offset, result, 0, Length);
            return result;
        }

        public override string ToString()
        {
            return BitConverter.ToString(_data, _offset, _length).ToLower();
        }

        public int IndexOf(byte item)
        {
            for (int i = 0; i < _length; i++)
            {
                if (_data[i + _offset] == item)
                    return i;
            }
            return -1;
        }

        void IList<byte>.Insert(int index, byte item)
        {
            throw new NotSupportedException();
        }

        void IList<byte>.RemoveAt(int index)
        {
            throw new NotSupportedException();
        }

        void ICollection<byte>.Add(byte item)
        {
            throw new NotSupportedException();
        }

        void ICollection<byte>.Clear()
        {
            throw new NotSupportedException();
        }

        public bool Contains(byte item)
        {
            for (int i = 0; i < _length; i++)
            {
                if (_data[i + _offset] == item)
                    return true;
            }
            return false;
        }

        public void CopyTo(byte[] array, int arrayIndex)
        {
            for (int i = 0; i < _length; i++)
            {
                array[arrayIndex + i] = _data[_offset + i];
            }
        }

        bool ICollection<byte>.Remove(byte item)
        {
            throw new NotSupportedException();
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