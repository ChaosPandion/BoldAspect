using System;

namespace BoldAspect.CLI.Metadata
{
    [Flags]
    public enum AssemblyFlags : uint
    {
        PublicKey = 0x0001,
        Retargetable = 0x0100,
        DisableJITcompileOptimizer = 0x4000,
        EnableJITcompileTracking = 0x8000,
    }
}