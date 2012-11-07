using System;

namespace BoldAspect.CLI
{
    [Flags]
    public enum FileAttributes : uint
    {
        ContainsMetadata = 0x0000,
        ContainsNoMetadata = 0x0001
    }
}