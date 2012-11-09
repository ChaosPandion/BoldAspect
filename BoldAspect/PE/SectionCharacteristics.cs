using System;

namespace BoldAspect.PE
{
    /// <summary>
    /// Describes the characteristics of a section. 
    /// </summary>
    [Flags]
    public enum SectionCharacteristics : uint
    {
        ///<summary>Reserved for future use.</summary>
        Reserved0 = 0x00000000,
        ///<summary>Reserved for future use.</summary>
        Reserved1 = 0x00000001,
        ///<summary>Reserved for future use.</summary>
        Reserved2 = 0x00000002,
        ///<summary>Reserved for future use.</summary>
        Reserved3 = 0x00000004,
        ///<summary>The section should not be padded to the next boundary. This flag is obsolete and is replaced by IMAGE_SCN_ALIGN_1BYTES. This is valid only for object files.</summary>
        NoPadding = 0x00000008,
        ///<summary>Reserved for future use.</summary>
        Reserved4 = 0x00000010,
        ///<summary>The section contains executable code.</summary>
        ContainsCode = 0x00000020,
        ///<summary>The section contains initialized data.</summary>
        ContainsInitializedData = 0x00000040,
        ///<summary>The section contains uninitialized data.</summary>
        ContainsUninitializedData = 0x00000080,
        ///<summary>Reserved for future use.</summary>
        LinkerOther = 0x00000100,
        ///<summary>The section contains comments or other information. The .drectve section has this type. This is valid for object files only.</summary>
        LinkerInfo = 0x00000200,
        ///<summary>Reserved for future use.</summary>
        Reserved5 = 0x00000400,
        ///<summary>The section will not become part of the image. This is valid only for object files.</summary>
        LinkerRemove = 0x00000800,
        ///<summary>The section contains COMDAT data. For more information, see section 5.5.6, “COMDAT Sections (Object Only).” This is valid only for object files.</summary>
        LinkerComdat = 0x00001000,
        ///<summary>The section contains data referenced through the global pointer (GP).</summary>
        GlobalPointerReferencedData = 0x00008000,
        ///<summary>Reserved for future use.</summary>
        MemoryPurgeable = 0x00020000,
        ///<summary>For ARM machine types, the section contains Thumb code.  Reserved for future use with other machine types.</summary>
        Memory16Bit = 0x00020000,
        ///<summary>Reserved for future use.</summary>
        MemoryLocked = 0x00040000,
        ///<summary>Reserved for future use.</summary>
        MemoryPreload = 0x00080000,
        ///<summary>Align data on a 1-byte boundary. Valid only for object files.</summary>
        AlignBy1 = 0x00100000,
        ///<summary>Align data on a 2-byte boundary. Valid only for object files.</summary>
        AlignBy2 = 0x00200000,
        ///<summary>Align data on a 4-byte boundary. Valid only for object files.</summary>
        AlignBy4 = 0x00300000,
        ///<summary>Align data on an 8-byte boundary. Valid only for object files.</summary>
        AlignBy8 = 0x00400000,
        ///<summary>Align data on a 16-byte boundary. Valid only for object files.</summary>
        AlignBy16 = 0x00500000,
        ///<summary>Align data on a 32-byte boundary. Valid only for object files.</summary>
        AlignBy32 = 0x00600000,
        ///<summary>Align data on a 64-byte boundary. Valid only for object files.</summary>
        AlignBy64 = 0x00700000,
        ///<summary>Align data on a 128-byte boundary. Valid only for object files.</summary>
        AlignBy128 = 0x00800000,
        ///<summary>Align data on a 256-byte boundary. Valid only for object files.</summary>
        AlignBy256 = 0x00900000,
        ///<summary>Align data on a 512-byte boundary. Valid only for object files. </summary>
        AlignBy512 = 0x00A00000,
        ///<summary>Align data on a 1024-byte boundary. Valid only for object files.</summary>
        AlignBy1024 = 0x00B00000,
        ///<summary>Align data on a 2048-byte boundary. Valid only for object files.</summary>
        AlignBy2048 = 0x00C00000,
        ///<summary>Align data on a 4096-byte boundary. Valid only for object files.</summary>
        AlignBy4096 = 0x00D00000,
        ///<summary>Align data on an 8192-byte boundary. Valid only for object files.</summary>
        AlignBy8192 = 0x00E00000,
        ///<summary>The section contains extended relocations.</summary>
        ExtendedRelocations = 0x01000000,
        ///<summary>The section can be discarded as needed.</summary>
        CanDiscard = 0x02000000,
        ///<summary>The section cannot be cached.</summary>
        NoCache = 0x04000000,
        ///<summary>The section is not pageable.</summary>
        NotPageable = 0x08000000,
        ///<summary>The section can be shared in memory.</summary>
        SharedMemory = 0x10000000,
        ///<summary>The section can be executed as code.</summary>
        ExecutableCode = 0x20000000,
        ///<summary>The section can be read.</summary>
        CanRead = 0x40000000,
        ///<summary>The section can be written to.</summary>
        CanWrite = 0x80000000,
     }
}