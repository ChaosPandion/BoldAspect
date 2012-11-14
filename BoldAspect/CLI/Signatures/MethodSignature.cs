using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI.Signatures
{
    public sealed class MethodSignature
    {
        public CallingConventions CallingConventions { get; set; }
        public uint GenericParamCount { get; set; }
        public uint ParamCount { get; set; }
        public ReturnTypeSignature ReturnType { get; set; }
        public ParamSignature FirstParam { get; set; }
        public ParamSignature FirstVarargParam { get; set; }
    }
}