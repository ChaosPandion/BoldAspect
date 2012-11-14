using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI
{



    public sealed class TableSchema
    {
        public TableID TableID;
        public int RowCount;
        public int RowWidth;
        public int Offset;
        public int ByteLength;
        public object[] Columns;

        public TableSchema(TableID tableID, params object[] columns)
        {
            TableID = tableID;
            Columns = columns;
        }

        public override string ToString()
        {
            return string.Format("TableID={0};RowCount={1};RowWidth={2}", TableID, RowCount, RowWidth);
        }
    }

    public sealed class CodedIndex2
    {
        public Type EnumType;
        public int TagWidth;
        public int ByteWidth;
        public TableID[] Tables;

        public CodedIndex2(Type enumType, TableID[] tables)
        {
            EnumType = enumType;
            var y = (double)Enum.GetValues(enumType).Cast<object>().Select(o => Convert.ToUInt64(o)).Max();
            var c = y / 2.0;
            var x = Math.Log(c + y) / Math.Log(2);
            TagWidth = (int)Math.Ceiling(x);
            ByteWidth = 2;
            Tables = tables;
        }

        public override string ToString()
        {
            return string.Format("EnumType={0};TagWidth={1};ByteWidth={2}", EnumType.Name, TagWidth, ByteWidth);
        }
    }

    public sealed class SimpleIndex
    {
        public TableID TableID;
        public int ByteWidth;

        public SimpleIndex(TableID tableID)
        {
            TableID = tableID;
            ByteWidth = 2;
        }

        public override string ToString()
        {
            return string.Format("{0}({1})", TableID, ByteWidth);
        }
    }

    public sealed class StringsHeapIndex
    {

    }

    public sealed class GuidHeapIndex
    {

    }

    public sealed class BlobHeapIndex
    {

    }
}
