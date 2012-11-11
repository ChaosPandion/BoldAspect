using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI
{
    sealed class ConstantColumnAttribute : ColumnAttribute
    {
        private readonly Type _type;

        public ConstantColumnAttribute(Type type)
            : base()
        {
            _type = type;
            if (_type.IsEnum)
            {
                _type = Enum.GetUnderlyingType(_type);
            }
        }

        //public Type Type
        //{
        //    get { return _type; }
        //}

        //public override ulong GetIndex(BinaryReader reader, BoldAspect.CLI.Metadata.MetadataStreams.TableStream stream)
        //{
        //    var size = Marshal.SizeOf(_type);
        //    switch (size)
        //    {
        //        case 1:
        //            return reader.ReadByte();
        //        case 2:
        //            return reader.ReadUInt16();
        //        case 4:
        //            return reader.ReadUInt32();
        //        case 8:
        //            return reader.ReadUInt64();
        //        default:
        //            throw new Exception();
        //    }

        //}
    }
}
