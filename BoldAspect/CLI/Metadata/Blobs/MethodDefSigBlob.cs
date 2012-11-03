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

    public sealed class MethodDefSigBlob
    {
        public readonly MethodDefSigFlags Flags;
        public readonly uint GenParamCount;
        public readonly uint ParamCount;
        public readonly RetTypeBlob RetType;
        public readonly IReadOnlyCollection<ParamBlob> Params;

        public MethodDefSigBlob(MethodDefSigFlags flags, uint genParamCount, uint paramCount, RetTypeBlob retType, IReadOnlyCollection<ParamBlob> @params)
        {
            Flags = flags;
            GenParamCount = genParamCount;
            ParamCount = paramCount;
            Params = @params;
        }

    }
}