using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.PE
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct DataDirectories
    {
        public readonly uint ExportRVA;
        public readonly uint ExportSize;
        public readonly uint ImportRVA;
        public readonly uint ImportSize;
        public readonly uint ResourceRVA;
        public readonly uint ResourceSize;
        public readonly uint ExceptionRVA;
        public readonly uint ExceptionSize;
        public readonly uint CertificateRVA;
        public readonly uint CertificateSize;
        public readonly uint BaseRelocationRVA;
        public readonly uint BaseRelocationSize;
        public readonly uint DebugRVA;
        public readonly uint DebugSize;
        public readonly uint CopyrightRVA;
        public readonly uint CopyrightSize;
        public readonly uint GlobalPtrRVA;
        public readonly uint GlobalPtrSize;
        public readonly uint TlsTableRVA;
        public readonly uint TlsTableSize;
        public readonly uint LoadConfigTableRVA;
        public readonly uint LoadConfigTableSize;
        public readonly uint BoundImportRVA;
        public readonly uint BoundImportSize;
        public readonly uint IatRVA;
        public readonly uint IatSize;
        public readonly uint DelayImportDescriptorRVA;
        public readonly uint DelayImportDescriptorSize;
        public readonly uint CliHeaderRVA;
        public readonly uint CliHeaderSize;
        public readonly ulong Reserved;
    }
}