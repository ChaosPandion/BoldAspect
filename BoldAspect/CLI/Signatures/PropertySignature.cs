using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI.Signatures
{
    public sealed class PropertySignature : Signature
    {
        public CustomModSignature FirstMod { get; set; }
        public TypeSignature Type { get; set; }
    }
}
