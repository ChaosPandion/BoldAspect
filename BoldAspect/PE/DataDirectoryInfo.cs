using System.Runtime.InteropServices;

namespace BoldAspect.PE
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct DataDirectoryInfo
    {
        public readonly uint RVA;

        public readonly uint Size;

        public bool Exists
        {
            get { return RVA > 0 && Size > 0; }
        }

        public override string ToString()
        {
            return string.Format("Exists={0};RVA={1:X4};Size={2:X4}", Exists, RVA, Size);
        }
    }
}