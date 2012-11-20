using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI
{
    public sealed class FieldSignature : Signature
    {
        public CustomModSignature FirstMod { get; set; }
        public TypeSignature Type { get; set; }
    }
}