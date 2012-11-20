using System.Runtime.InteropServices;

namespace BoldAspect.CLI
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct DataDirectoryHeader
    {
        public readonly uint RVA;
        public readonly uint Size;
    }
}