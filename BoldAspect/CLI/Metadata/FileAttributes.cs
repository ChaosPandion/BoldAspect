using System;

namespace BoldAspect.CLI.Metadata
{
    [Flags]
    public enum FileAttributes : uint
    {
        ContainsMetadata = 0x0000,
        ContainsNoMetadata = 0x0001
    }
}