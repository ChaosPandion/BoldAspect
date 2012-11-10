using System.Runtime.InteropServices;

namespace BoldAspect.PE
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Signature
    {
        public const uint Constant = 0x4550;

        public uint Value;
    }
}