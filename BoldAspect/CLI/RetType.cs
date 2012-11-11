using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI
{
    public sealed class RetType
    {
        public readonly IReadOnlyList<CustomMod> CustomMods;
        public readonly ITypeRef Type;

        public RetType(IReadOnlyList<CustomMod> customMods, ITypeRef type)
        {
            CustomMods = customMods;
            Type = type;
        }
    }
}