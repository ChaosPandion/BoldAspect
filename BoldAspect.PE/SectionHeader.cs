using System.Runtime.InteropServices;

namespace BoldAspect.PE
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct SectionHeader
    {
        public readonly ulong Name;

        public readonly uint VirtualSize;

        public readonly uint VirtualAddress;

        public readonly uint RawDataSize;

        public readonly uint RawDataPointer;

        public readonly uint RelocationPointer;

        public readonly uint LineNumberPointer;

        public readonly ushort RelocationCount;

        public readonly ushort LineNumberCount;

        public readonly SectionCharacteristics Characteristics;
    }
}