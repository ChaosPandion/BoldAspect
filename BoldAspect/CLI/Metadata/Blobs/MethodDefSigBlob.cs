using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI.Metadata.Blobs
{
    [Flags]
    public enum MethodDefSigFlags : byte
    {
        Default = 0x00,
        Vararg = 0x05,
        Generic = 0x10,
        HasThis = 0x20,
        ExplicitThis = 0x40
    }

    sealed class MethodDefSigBlob
    {
        public readonly MethodDefSigFlags Flags;
        public readonly uint GenParamCount;
        public readonly uint ParamCount;
        public readonly RetTypeBlob RetType;
        public readonly ParamBlob[] Params;

        public MethodDefSigBlob(BlobHeap.Slice slice)
        {
            Flags = (MethodDefSigFlags)slice[0];
            var pcIndex = 1;
            if (Flags.HasFlag(MethodDefSigFlags.Generic))
            {
                var bg = slice[1];
                if ((bg & 0xC0) == 0xC0)
                {
                    GenParamCount = (uint)(bg << 24) + (uint)(slice[2] << 16) + (uint)(slice[3] << 8) + slice[4];
                    pcIndex = 5;
                }
                else if ((bg & 0x80) == 0x80)
                {
                    GenParamCount = (uint)(bg << 8) + slice[2];
                    pcIndex = 3;
                }
                else
                {
                    GenParamCount = bg;
                    pcIndex = 2;
                }
            }
            var b = slice[pcIndex];
            if ((b & 0xC0) == 0xC0)
            {
                ParamCount = (uint)(b << 24) + (uint)(slice[pcIndex + 1] << 16) + (uint)(slice[pcIndex + 2] << 8) + slice[pcIndex + 3];
            }
            else if ((b & 0x80) == 0x80)
            {
                ParamCount = (uint)(b << 8) + slice[pcIndex + 1];
            }
            else
            {
                ParamCount = b;
            }
        }

    }
}