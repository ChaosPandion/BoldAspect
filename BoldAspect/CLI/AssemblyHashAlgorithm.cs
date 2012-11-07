using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI
{
    public enum AssemblyHashAlgorithm : uint
    {
        None = 0x0000,
        MD5 = 0x8003,
        SHA1 = 0x8004
    }
}
