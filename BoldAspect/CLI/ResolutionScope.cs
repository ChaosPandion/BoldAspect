using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI
{
    public enum ResolutionScope : byte
    {
        Module,
        ModuleRef,
        AssemblyRef,
        TypeRef
    }
}
