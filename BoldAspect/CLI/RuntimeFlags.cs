using System;

namespace BoldAspect.CLI
{
    /// <summary>
    /// The following flags describe this runtime image and are used by the loader. 
    /// All unspecified bits should be zero. 
    /// </summary>
    [Flags]
    public enum RuntimeFlags : uint
    {
        /// <summary>
        /// Shall be 1.
        /// </summary>
        RuntimeILOnly = 0x00000001,

        /// <summary>
        /// Image can only be loaded into a 32-bit process,
        /// for instance if there are 32-bit vtablefixups, or 
        /// casts from native integers to int32. CLI 
        /// implementations that have 64-bit native integers shall refuse loading binaries with this flag set. 
        /// </summary>
        Runtime32BitRequired = 0x00000002,

        /// <summary>
        /// Image has a strong name signature. 
        /// </summary>
        RuntimeStrongNameSigned = 0x00000008,

        /// <summary>
        /// Shall be 0.
        /// </summary>
        RuntimeEntryPoint = 0x00000010,

        /// <summary>
        /// Shall be 0.
        /// </summary>
        RuntimeTrackDebugData = 0x00010000 
    }
}