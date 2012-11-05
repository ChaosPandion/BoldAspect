using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI
{
    [Flags]
    public enum AssemblyFlags : uint
    {
        PublicKey = 0x0001,
        Retargetable = 0x0100,
        DisableJITcompileOptimizer = 0x4000,
        EnableJITcompileTracking = 0x8000,
    }

    public enum AssemblyHashAlgorithm : uint
    {
        None = 0x0000,
        MD5 = 0x8003,
        SHA1 = 0x8004
    }

    public sealed class CLIAssembly
    {
        public CLIAssembly(CLIMetadata metadata)
        {
            Metadata = metadata;
            PublicKey = new List<byte>();
            ModuleReferences = new List<CLIModuleReference>();
            AssemblyReferences = new List<CLIAssemblyReference>();
        }

        public CLIMetadata Metadata { get; private set; }

        public string Name { get; set; }

        public AssemblyFlags Flags { get; set; }

        public Version Version { get; set; }

        public CultureInfo Culture { get; set; }

        public AssemblyHashAlgorithm HashAlgorithm { get; set; }

        public IList<byte> PublicKey { get; private set; }

        public CLIModule ManifestModule { get; set; }

        public IList<CLIModuleReference> ModuleReferences { get; private set; }

        public IList<CLIAssemblyReference> AssemblyReferences { get; private set; }
    }

    public sealed class CLIAssemblyReference
    {
        public CLIAssemblyReference(CLIMetadata metadata)
        {
            Metadata = metadata;
            PublicKeyOrToken = new List<byte>();
            HashValue = new List<byte>();
        }

        public CLIMetadata Metadata { get; private set; }

        public string Name { get; set; }

        public AssemblyFlags Flags { get; set; }

        public Version Version { get; set; }

        public CultureInfo Culture { get; set; }

        public IList<byte> PublicKeyOrToken { get; private set; }

        public IList<byte> HashValue { get; private set; }

    }
}