using System.Runtime.InteropServices;

namespace BoldAspect.PE
{    
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct DosHeader
    {
        public const ushort MagicConstant = 0x5A4D;

        public readonly ushort e_magic;
        public readonly ushort e_cblp;
        public readonly ushort e_cp;
        public readonly ushort e_crlc;
        public readonly ushort e_cparhdr;
        public readonly ushort e_minalloc;
        public readonly ushort e_maxalloc;
        public readonly ushort e_ss;
        public readonly ushort e_sp;
        public readonly ushort e_csum;
        public readonly ushort e_ip;
        public readonly ushort e_cs;
        public readonly ushort e_lfarlc;
        public readonly ushort e_ovno;
        public readonly ulong e_res;
        public readonly ushort e_oemid;
        public readonly ushort e_oeminfo;
        public readonly ulong e_res2a;
        public readonly long e_res2b;
        public readonly uint e_res2c;
        public readonly uint e_lfanew;
    }
}