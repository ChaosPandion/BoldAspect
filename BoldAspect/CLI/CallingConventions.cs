using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI
{
    [Flags]
    public enum CallingConventions : byte
    {
        Default = 0x00,
        VarArg = 0x05,
        Field = 0x06,
        LocalVar = 0x07,
        Property = 0x08,
        Generic = 0x10,
        HasThis = 0x20,
        ExplicitThis = 0x40,
        Sentinel = 0x41,
        Cdecl = 0x01,
        Sdtcall = 0x02,
        Thiscall = 0x03,
        Fastcall = 0x04,
    }
}