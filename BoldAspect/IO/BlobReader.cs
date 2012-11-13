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

        public BlobReader(Blob blob)
        {

        }

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

        public int Index
        {
            get { return (int)((long)_current - (long)_first); }
        }

        public int Length
        {
            get { return _length; }
        }

        public void Seek(int index)
        {
            _current = _start + index;
        }

        public void Offset(int offset)
        {
            _current += offset;
        }

        public byte ReadByte()
        {
            ValidateIndex(1);
            var result = Marshal.ReadByte(_current);
            _current += 1;
            return result;
        }

        public ushort ReadUInt16()
        {
            ValidateIndex(2);
            var result = (ushort)Marshal.ReadInt16(_current);
            _current += 2;
            return result;
        }

        public uint ReadUInt32()
        {
            ValidateIndex(4);
            var result = (uint)Marshal.ReadInt32(_current);
            _current += 4;
            return result;
        }

        public ulong ReadUInt64()
        {
            ValidateIndex(8);
            var result = (ulong)Marshal.ReadInt64(_current);
            _current += 8;
            return result;
        }

        public byte[] ReadBytes(int count)
        {
            ValidateIndex(count);
            var result = new byte[count];
            Array.Copy(_data, Index, result, 0, count);
            _current += count;
            return result;
        }

        public uint ReadCompressedUInt32()
        {
            int offset;
            return ReadCompressedUInt32(out offset);
        }

        public uint ReadCompressedUInt32(out int offset)
        {
            uint result = 0;
            offset = 1;
            var index = Index;
            var b1 = _data[index];
            if ((b1 & 0x80) == 0)
            {
                result = (uint)b1;
            }
            else
            {
                offset = 2;
                if ((b1 & 0x40) == 0)
                {
                    result = ((uint)(b1 & ~0x80) << 8)
                        | (uint)_data[index + 1];
                }
                else
                {
                    offset = 4;
                    result = ((uint)(b1 & ~0xC0) << 24)
                        | ((uint)_data[index + 1] << 16)
                        | ((uint)_data[index + 2] << 8)
                        | (uint)_data[index + 3];
                }
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