using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI.Metadata
{
    sealed class CodedIndexAttribute : ColumnAttribute
    {
        private readonly Type _enumType;
        private readonly int _tagWidth;

        public CodedIndexAttribute(Type enumType)
        {
            _enumType = enumType;
            var y = (long)Enum.GetValues(enumType).Cast<object>().Select(o => Convert.ToUInt64(o)).Max();
            var x = Math.Log((y / 2) + y) / Math.Log(2);
            _tagWidth = (int)Math.Ceiling(x);
        }

        public override ulong GetIndex(BinaryReader reader, TableStream stream)
        {
            var val = reader.ReadUInt16();
            var tag = (int)(val & _tagWidth);
            var tb = (TableID)Enum.Parse(typeof(TableID), Enum.GetName(_enumType, tag));
            var index = stream.TableIndex(tb);
            var rowSum = 0;
            for (int i = 0; i < index; i++)
            {
                rowSum += (int)stream.Rows[i];
            }
            if (rowSum > Math.Pow(2, 16 - Math.Log(index)))
            {
                return (uint)val + (uint)(reader.ReadUInt16() >> 2);
            }
            return (uint)val;
        }

    }
}
