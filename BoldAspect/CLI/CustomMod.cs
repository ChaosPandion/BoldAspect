using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI
{
    public sealed class CustomMod
    {
        public bool Required { get; set; }
        public CLITypeRef Type { get; set; }

        public CustomMod(bool required, CLITypeRef type)
        {
            Required = required;
            Type = type;
        }
    }

    public sealed class CustomModCollection : Collection<CustomMod>
    {

    }
}
