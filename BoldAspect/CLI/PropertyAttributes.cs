using System;

namespace BoldAspect.CLI
{
    [Flags]
    public enum PropertyAttributes : ushort
    {
        SpecialName = 0x0200,
        RTSpecialName = 0x0400,
        HasDefault = 0x1000,
        Unused = 0xe9ff,
    }
}