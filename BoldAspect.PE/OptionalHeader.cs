using System.Runtime.InteropServices;

namespace BoldAspect.PE
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct OptionalHeader32
    {
        public readonly byte LMajor;
        public readonly byte LMinor;
        public readonly uint CodeSize;
        public readonly uint InitializedDataSize;
        public readonly uint UninitializedDataSize;
        public readonly uint EntryPointRVA;
        public readonly uint BaseOfCode;
        public readonly uint BaseOfData;
        public readonly uint ImageBase;
        public readonly uint SectionAlignment;
        public readonly uint FileAlignment;
        public readonly ushort OSMajor;
        public readonly ushort OSMinor;
        public readonly ushort UserMajor;
        public readonly ushort UserMinor;
        public readonly ushort SubSysMajor;
        public readonly ushort SubSysMinor;
        public readonly uint Reserved;
        public readonly uint ImageSize;
        public readonly uint HeaderSize;
        public readonly uint FileChecksum;
        public readonly ushort SubSystem;
        public readonly ushort DLLFlags;
        public readonly uint StackReserveSize;
        public readonly uint StackCommitSize;
        public readonly uint HeapReserveSize;
        public readonly uint HeapCommitSize;
        public readonly uint LoaderFlags;
        public readonly uint DataDirectoryCount;
        public readonly DataDirectories DataDirectories;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct OptionalHeader64
    {
        public readonly byte LMajor;
        public readonly byte LMinor;
        public readonly uint CodeSize;
        public readonly uint InitializedDataSize;
        public readonly uint UninitializedDataSize;
        public readonly uint EntryPointRVA;
        public readonly uint BaseOfCode;
        public readonly ulong ImageBase;
        public readonly uint SectionAlignment;
        public readonly uint FileAlignment;
        public readonly ushort OSMajor;
        public readonly ushort OSMinor;
        public readonly ushort UserMajor;
        public readonly ushort UserMinor;
        public readonly ushort SubSysMajor;
        public readonly ushort SubSysMinor;
        public readonly uint Reserved;
        public readonly uint ImageSize;
        public readonly uint HeaderSize;
        public readonly uint FileChecksum;
        public readonly ushort SubSystem;
        public readonly ushort DLLFlags;
        public readonly ulong StackReserveSize;
        public readonly ulong StackCommitSize;
        public readonly ulong HeapReserveSize;
        public readonly ulong HeapCommitSize;
        public readonly uint LoaderFlags;
        public readonly uint DataDirectoryCount;
        public readonly DataDirectories DataDirectories;
    }
}