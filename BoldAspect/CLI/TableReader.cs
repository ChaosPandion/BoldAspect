using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BoldAspect.CLI
{


    public sealed class TableReader : BlobReader
    {
        private readonly MetadataRoot _root;
        private readonly TableSchema _schema;
        private int _rowIndex;
        private int _columnIndex;

        public TableReader(MetadataRoot root, TableID table)
            : base(root.GetTableData(table))
        {
            _root = root;
            _schema = _root.GetTableSchema(table);
        }

        public void SeekRow(int index)
        {
            Seek((index - 1) * _schema.RowWidth);
        }

        public bool NextRow()
        {
            var hasRow = _rowIndex != _schema.RowCount;
            if (hasRow)
            {
                if (_columnIndex < _schema.Columns.Length)
                {

                }
                _rowIndex++;
                _columnIndex = 0;
            }
            return hasRow;
        }

        public T ReadColumn<T>()
        {
            var col = _schema.Columns[_columnIndex];
            _columnIndex++;
            var type = col as Type;
            if (type != null)
            {
                switch (Type.GetTypeCode(type))
                {
                    case TypeCode.Object:
                        if (type == typeof(StringsHeapIndex))
                            return (T)(object)ReadString();
                        if (type == typeof(GuidHeapIndex))
                            return (T)(object)ReadGuid();
                        if (type == typeof(BlobHeapIndex))
                            return (T)(object)ReadBlob();
                        break;
                    case TypeCode.UInt16:
                        return (T)(object)Read<ushort>();
                    case TypeCode.UInt32:
                        return (T)(object)Read<uint>();
                    case TypeCode.UInt64:
                        return (T)(object)Read<ulong>();
                }
            }
            var si = col as SimpleIndex;
            if (si != null)
            {
                if (si.ByteWidth == 2)
                {
                    return (T)Convert.ChangeType(Read<ushort>(), typeof(T));
                }
                return (T)Convert.ChangeType(Read<uint>(), typeof(T));
            }
            var ci = col as CodedIndex;
            if (ci != null)
            {
                uint token = 0;
                if (ci.ByteWidth == 2)
                {
                    token = Read<ushort>();
                }
                else
                {
                    token = Read<uint>();
                }
                return (T)(object)new MetadataToken(token);
            }
            return default(T);
        }

        public Blob ReadBlob()
        {
            uint index = 0;
            if (_root._blobHeapIndexWidth == 2)
            {
                index = Read<ushort>();
            }
            else
            {
                index = (uint)ReadInt32();
            }
            return _root.GetBlob(index);
        }

        public string ReadString()
        {
            uint index = 0;
            if (_root._stringsHeapIndexWidth == 2)
            {
                index = Read<ushort>();
            }
            else
            {
                index = (uint)ReadInt32();
            }
            return _root.GetString(index);
        }

        public Guid ReadGuid()
        {
            uint index = 0;
            if (_root._guidHeapIndexWidth == 2)
            {
                index = Read<ushort>();
            }
            else
            {
                index = (uint)ReadInt32();
            }
            return _root.GetGuid(index);
        }
    }
}