using System.Runtime.InteropServices;

namespace BoldAspect.PE
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct FileHeader
    {
        public readonly MachineType Machine;

        public readonly ushort SectionCount;

        public readonly uint TimeStamp;

        public readonly uint SymbolTablePointer;

        public readonly uint SymbolCount;

        public readonly ushort OptionalHeaderSize;

        public readonly FileCharacteristics Characteristics;
    }
}