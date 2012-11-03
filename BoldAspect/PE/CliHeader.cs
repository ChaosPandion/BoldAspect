using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.PE
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CliHeader
    {
        public uint Cb;
        public ushort MajorRuntimeVersion;
        public ushort MinorRuntimeVersion;
        public uint MetadataRVA;
        public uint MetadataSize;
        public RuntimeFlags Flags;
        public uint ResourcesRVA;
        public uint ResourcesSize;
        public uint StrongNameSignatureRVA;
        public uint StrongNameSignatureSize;
        public ulong CodeManagerTable;
        public ulong VTableFixups;
        public ulong ExportAddressTableJumps;
        public ulong ManagedNativeHeader;
    }
}