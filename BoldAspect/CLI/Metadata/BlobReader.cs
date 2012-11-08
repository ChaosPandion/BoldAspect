using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI.Metadata
{
    public sealed class BlobReader : IDisposable
    {
        private readonly byte[] _data;
        private readonly int _offset;
        private readonly int _length;
        private readonly GCHandle _handle;
        private readonly IntPtr _start;
        private IntPtr _current;

        public BlobReader(byte[] data, int offset, int length)
        {
            _data = data;
            _offset = offset;
            _length = length;
            _handle = GCHandle.Alloc(_data, GCHandleType.Pinned);
            _start = _current = _handle.AddrOfPinnedObject() + offset;
        }

        public T Read<T>() where T : struct
        {
            var size = Marshal.SizeOf(typeof(T));
            ValidateIndex(size);
            var result = (T)Marshal.PtrToStructure(_current, typeof(T));
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

        public string ReadAsciiString(int maxLength)
        {
            var start = (int)((long)_current - (long)_start);
            var index = start;
            var limit = _offset + _length;
            for (; index < limit; index++)
                if (_data[index] == 0 && (index + 1) % 4 == 0)
                    break;
            var nullLimit = index;
            while (_data[nullLimit] == 0)
                nullLimit--;
            return Encoding.ASCII.GetString(_data, start, nullLimit - start);
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
            return (long)_current + length <= (long)_start + _length;
        }
    }
}