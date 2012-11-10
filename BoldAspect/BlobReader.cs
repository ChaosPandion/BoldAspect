using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect
{
    public class BlobReader : IDisposable
    {
        private readonly byte[] _data;
        private readonly int _offset;
        private readonly int _length;
        private readonly int _endIndex;
        private readonly GCHandle _handle;
        private readonly IntPtr _first;
        private readonly IntPtr _start;
        private IntPtr _current;

        public BlobReader(Slice slice)
            : this(slice.Data, slice.Offset, slice.Length)
        {

        }

        public BlobReader(byte[] data, int offset, int length)
        {
            _data = data;
            _offset = offset;
            _length = length;
            _handle = GCHandle.Alloc(_data, GCHandleType.Pinned);
            _first = _handle.AddrOfPinnedObject();
            _endIndex = (int)_first + _offset + length;
            _start = _current = _first + offset;
        }

        int Index
        {
            get { return (int)((long)_current - (long)_first); }
        }

        public void Seek(int index)
        {
            _current = _start + index;
        }

        public void Offset(int offset)
        {
            _current += offset;
        }

        public int ReadBigEndianCompressedInteger()
        {
            var result = 0;
            var offset = 0;
            var index = Index;
            var data = _data;
            var b1 = data[(int)index];
            if ((b1 & 0xC0) == 0xC0)
            {
                result += b1 << 24;
                result += data[(int)index + 1] << 16;
                result += data[(int)index + 2] << 8;
                result += data[(int)index + 3];
                offset = 4;
            }
            else if ((b1 & 0x80) == 0x80)
            {
                result += data[(int)index + 1];
                result += b1 << 8;
                offset = 2;
            }
            else
            {
                result += b1;
                offset = 1;
            }
            _current += offset;
            return result;
        }

        public string ReadNullTerminatedASCIIString(int maxLength, int boundarySize)
        {
            var startIndex = Index;
            var j = 0;
            for (int i = startIndex; i < _endIndex; i++)
            {
                if (_data[i] == 0 && (i + 1) % boundarySize == 0)
                {
                    Offset(i - startIndex + 1);
                    j = i;
                    break;
                }
            }
            while (_data[j] == 0)
            {
                j--;
            }
            return Encoding.ASCII.GetString(_data, startIndex, j - startIndex + 1);
        }

        public string ReadNullTerminatedUTF8String()
        {
            var start = Index;
            for (int j = 0, i = start; i < _endIndex; i++, j++)
            {
                if (_data[i] == 0)
                {
                    _current += (j + 1);
                    return Encoding.UTF8.GetString(_data, start, j); 
                }
            }
            throw new Exception();
        }

        public string ReadUTF8String(int length)
        {
            ValidateIndex(length);
            var result = Encoding.UTF8.GetString(_data, Index, length);
            _current += length;
            return result;
        }

        public string ReadUTF16String(int length)
        {
            ValidateIndex(length);
            var result = Encoding.Unicode.GetString(_data, Index, length);
            _current += length;
            return result;
        }

        public Slice ReadSlice(int length)
        {
            ValidateIndex(length);
            var result = new Slice(_data, Index, length);
            _current += length;
            return result;
        }

        public int ReadInt32()
        {
            ValidateIndex(4);
            var result = Marshal.ReadInt32(_current);
            _current += 4;
            return result;
        }

        public T Read<T>() where T : struct
        {
            var t = typeof(T);
            if (t.IsEnum)
                t = Enum.GetUnderlyingType(t);
            var size = Marshal.SizeOf(t);
            ValidateIndex(size);
            var result = (T)Marshal.PtrToStructure(_current, t);
            _current += size;
            return result;
        }

        public T Read<T>(int startIndex) where T : struct
        {
            var t = typeof(T);
            if (t.IsEnum)
                t = Enum.GetUnderlyingType(t);
            Seek(startIndex);
            var size = Marshal.SizeOf(t);
            ValidateIndex(size);
            var result = (T)Marshal.PtrToStructure(_current, t);
            _current += size;
            return result;
        }

        public int Read<T>(ref T result) where T : struct
        {
            var t = typeof(T);
            if (t.IsEnum)
                t = Enum.GetUnderlyingType(t);
            var size = Marshal.SizeOf(t);
            ValidateIndex(size);
            result = (T)Marshal.PtrToStructure(_current, t);
            _current += size;
            return size;
        }

        public int Read<T>(ref T[] result, int length) where T : struct
        {
            var t = typeof(T);
            if (t.IsEnum)
                t = Enum.GetUnderlyingType(t);
            var size = Marshal.SizeOf(t);
            var totalLength = size * length;
            ValidateIndex(totalLength);
            result = new T[length];
            for (int i = 0; i < length; i++)
            {
                Read(ref result[i]);
            }
            return totalLength;
        }

        public T ReadAssert<T>(T expected) where T : struct
        {
            var t = typeof(T);
            if (t.IsEnum)
                t = Enum.GetUnderlyingType(t);
            var size = Marshal.SizeOf(t);
            ValidateIndex(size);
            var result = (T)Marshal.PtrToStructure(_current, t);
            if (!object.Equals(result, expected))
                throw new Exception();
            _current += size;
            return result;
        }

        public T? TryRead<T>() where T : struct
        {
            var size = Marshal.SizeOf(typeof(T));
            if (!CanRead(size))
                return null;
            return (T)Marshal.PtrToStructure(_current, typeof(T));
        }

        public void Dispose()
        {
            _handle.Free();
        }

        private void ValidateIndex(int length)
        {
            if (!CanRead(length))
                throw new ArgumentException();
        }

        private bool CanRead(int length)
        {
            return (int)_current + length <= (int)_start + _length;
        }
    }
}