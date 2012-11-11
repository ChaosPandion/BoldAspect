using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI
{
    enum HasCustomAttribute : byte
    {
        MethodDef,
        Field,
        TypeRef,
        TypeDef,
        Param,
        InterfaceImpl,
        MemberRef,
        Module,
        Permission,
        Property,
        Event,
        StandAloneSig,
        ModuleRef,
        TypeSpec,
        Assembly,
        AssemblyRef,
        File,
        ExportedType,
        ManifestResource,
        GenericParam,
        GenericParamConstraint,
        MethodSpec,
    }
}