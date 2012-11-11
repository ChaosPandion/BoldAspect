using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI
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

    public sealed class MethodDefSig
    {
        public readonly CallingConventions CallingConventions;
        public readonly uint GenParamCount;
        public readonly uint ParamCount;
        public readonly RetType RetType;
        public readonly IReadOnlyCollection<ParamSig> Params;

        public MethodDefSig(CallingConventions callingConventions, uint genParamCount, uint paramCount, RetType retType, IReadOnlyCollection<ParamSig> @params)
        {
            CallingConventions = callingConventions;
            GenParamCount = genParamCount;
            ParamCount = paramCount;
            Params = @params;
        }

    }
}