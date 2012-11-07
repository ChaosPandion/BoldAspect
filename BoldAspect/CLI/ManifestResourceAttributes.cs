﻿using System;

namespace BoldAspect.CLI
{
    [Flags]
    public enum ManifestResourceAttributes : uint
    {
        VisibilityMask = 0x0007,
        Public = 0x0001,
        Private = 0x0002,
    }
}