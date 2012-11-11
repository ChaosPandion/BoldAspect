using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI
{
    public sealed class CustomModCollection : Collection<CustomMod>
    {
 
    }

    public sealed class CustomMod
    {
        public readonly bool Required;
        public readonly ITypeRef Type;

        public CustomMod(bool required, ITypeRef type)
        {
            Required = required;
            Type = type;
        }
    }
}
