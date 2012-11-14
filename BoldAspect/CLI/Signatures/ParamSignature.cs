using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI.Signatures
{
    public sealed class ParamSignature
    {
        public CustomModSignature FirstMod { get; set; }
        public TypeSignature Type { get; set; }
        public ParamSignature Next { get; set; }
    }
}
