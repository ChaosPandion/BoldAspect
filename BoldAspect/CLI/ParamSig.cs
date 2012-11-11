using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI
{
    public sealed class ParamSig
    {
        public readonly CustomModCollection CustomMods;
        public readonly ITypeRef _type;

        public ParamSig(CustomModCollection customMods, ITypeRef type)
        {
            CustomMods = customMods;
            _type = type;
        }
    }
}
