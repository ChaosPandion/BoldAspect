using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI
{
    enum MemberRefParent : byte
    {
        TypeDef,
        TypeRef,
        ModuleRef,
        MethodDef,
        TypeSpec,
    }
}
