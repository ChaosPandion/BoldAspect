using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect
{//DebuggerStepThrough]
    public struct Blob
    {
        readonly byte[] _data;
        readonly int _offset;
        readonly int _length;

        public Blob(byte[] data, int offset, int length)
        {
            _data = data;
            _offset = offset;
            _length = length;
        }

        public int Length
        {
            get { return _length; }
        }

        public byte this[int index]
        {
            get { return _data[index]; }
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

        public string ToString(int numericBase)
        {
            return string.Join("-", _data.Skip(_offset).Take(_length).Select(b => Convert.ToString(b, numericBase).PadLeft(numericBase == 2 ? 8 : numericBase / 8, '0')));
        }
    }
}