using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoldAspect.CLI.Metadata.Blobs;

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
                else if ((b1 & 0xC0) == 0x00)
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

        public MethodDefSigBlob GetMethodDefSig(int index)
        {
            var flags = (MethodDefSigFlags)_data[index];
            var genParamCount = 0u;
            var paramCount = 0u;
            if (!flags.HasFlag(MethodDefSigFlags.Generic))
            {
                index++;
            }
            else
            {
                var bg = _data[index + 1];
                if ((bg & 0xC0) == 0xC0)
                {
                    genParamCount = (uint)(bg << 24) + (uint)(_data[index + 2] << 16) + (uint)(_data[index + 3] << 8) + _data[4];
                    index += 4;
                }
                else if ((bg & 0x80) == 0x80)
                {
                    genParamCount = (uint)(bg << 8) + _data[index + 1];
                    index += 2;
                }
                else
                {
                    genParamCount = bg;
                    index += 1;
                }
            }
            var b = _data[index];
            if ((b & 0xC0) == 0xC0)
            {
                paramCount = (uint)(b << 24) + (uint)(_data[index + 1] << 16) + (uint)(_data[index + 2] << 8) + _data[index + 3];
                index += 4;
            }
            else if ((b & 0x80) == 0x80)
            {
                paramCount = (uint)(b << 8) + _data[index + 1];
                index += 2;
            }
            else
            {
                paramCount = b;
                index += 1;
            }
            var retTypeSize = 0;
            var retType = GetRetType(index, out retTypeSize);
            return new MethodDefSigBlob(flags, genParamCount, paramCount, retType, null);
        }

        public RetTypeBlob GetRetType(int index, out int size)
        {
            size = 0;
            var cms = GetCustomMods(index, out size);
            index += size;
            var type = (ElementType)_data[index];
            switch (type)
            {
                case ElementType.Byref:
                    index += 1;
                    size += 1;
                    type = (ElementType)_data[index];
                    return RetTypeBlob.CreateByRefRetType(cms, type);
                case ElementType.Typedbyref:
                    return RetTypeBlob.CreateTypeByRefRetType(cms);
                case ElementType.Void:
                    return RetTypeBlob.CreateVoidRetType(cms);
                default:
                    throw new MetadataException();
            }
        }

        public IReadOnlyCollection<CustomModBlob> GetCustomMods(int index, out int size)
        {
            size = 0;

            var list = new List<CustomModBlob>();
            var i = index;
            CustomModBlob cm;
            int cmSize;
            while (TryGetCustomMod(i, out cm, out cmSize))
            {
                i += cmSize;
                size += cmSize;
                list.Add(cm);
            }
            return list.AsReadOnly();
        }

        public bool TryGetCustomMod(int index, out CustomModBlob result, out int size)
        {
            result = null;
            size = 0;
            if (index < 0 || index >= _data.Length)
                return false;
            var type = (ElementType)_data[index];
            switch (type)
            {
                case ElementType.Cmod_reqd:
                case ElementType.Cmod_opt:
                    var token = GetTypeDefOrRefOrSpecEncodedBlob(index + 1, out size);
                    result = new CustomModBlob(type, token);
                    size++;
                    return true;
                default:
                    return false;
            }
        }

        public TypeDefOrRefOrSpecEncodedBlob GetTypeDefOrRefOrSpecEncodedBlob(int index, out int size)
        {
            var b1 = _data[index];
            var num = 0;
            b1 <<= 2;
            if ((b1 & 0x80) == 0x00)
            {
                num += b1;
                size = 1;
            }
            else if ((b1 & 0xC0) == 0x00)
            {
                num += _data[(int)index + 1];
                num += b1 << 8;
                size = 2;
            }
            else
            {
                num += b1 << 24;
                num += _data[(int)index + 1] << 16;
                num += _data[(int)index + 2] << 8;
                num += _data[(int)index + 3];
                size = 4;
            }
            return new TypeDefOrRefOrSpecEncodedBlob((TypeDefOrRef)(num & 0xC000), (uint)num & 0x3000);
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
