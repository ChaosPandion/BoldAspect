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
        public CLIMetadata Owner { get; private set; }
        public AssemblyFlags Flags { get; private set; }
        public string Name { get; private set; }
        public Version Version { get; private set; }
        public CultureInfo Culture { get; private set; }
        public AssemblyHashAlgorithm HashAlgorithm { get; private set; }
        public ReadOnlyCollection<byte> PublicKey { get; private set; }
        public ReadOnlyCollection<CLIModule> Modules { get; private set; }
        public CLIModule ManifestModule { get; private set; }

        private CLIAssembly(
            CLIMetadata owner,
            AssemblyFlags flags,
            string name,
            Version version,
            CultureInfo culture,
            AssemblyHashAlgorithm hashAlgorithm,
            ReadOnlyCollection<byte> publicKey,
            ReadOnlyCollection<CLIModule> modules,
            CLIModule manifestModule)
        {
            Owner = owner;
            Flags = flags;
            Name = name;
            Version = version;
            Culture = culture;
            HashAlgorithm = hashAlgorithm;
            PublicKey = publicKey;
            Modules = modules;
            ManifestModule = manifestModule;
        }

        public sealed class Builder
        {
            private CLIAssembly _assembly;

            internal Builder(CLIMetadata owner)
            {
                _assembly = new CLIAssembly(

            }

            public Builder WithName(string name)
            {
                _name = name;
                return this;
            }

            public Builder WithVersion(Version version)
            {
                _version = version;
                return this;
            }

            public Builder WithCulture(CultureInfo culture)
            {
                _culture = culture;
                return this;
            }

            public Builder WithFlags(AssemblyFlags flags)
            {
                _flags = flags;
                return this;
            }

            public Builder WithHashAlgorithm(AssemblyHashAlgorithm hashAlgorithm)
            {
                _hashAlgorithm = hashAlgorithm;
                return this;
            }

            public Builder WithModule(CLIModule module)
            {
                _modules.Add(module);
                return this;
            }

            public Builder WithManifestModule(CLIModule manifestModule)
            {
                _manifestModule = manifestModule;
                _modules.Add(manifestModule);
                return this;
            }
        }
    }

    class CLIAssemblyReference
    {

    }
}
