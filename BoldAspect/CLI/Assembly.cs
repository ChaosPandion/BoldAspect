using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace BoldAspect.CLI.Metadata
{
    [Flags]
    public enum AssemblyFlags : uint
    {
        None = 0x0000,
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

    public interface IAssembly
    {
        string Name { get; set; }
        AssemblyFlags Flags { get; set; }
        Version Version { get; set; }
        CultureInfo Culture { get; set; }
        AssemblyHashAlgorithm HashAlgorithm { get; set; }
        string PublicKey { get; set; }
        IModule ManifestModule { get; set; }
    }

    public sealed class CLIAssembly : IAssembly
    {
        public string Name { get; set; }
        public AssemblyFlags Flags { get; set; }
        public Version Version { get; set; }
        public CultureInfo Culture { get; set; }
        public AssemblyHashAlgorithm HashAlgorithm { get; set; }
        public string PublicKey { get; set; }
        public IModule ManifestModule { get; set; }
    }

    class AssemblyTable : Table<AssemblyRecord>
    {
        public AssemblyTable()
            : base(TableID.Assembly)
        {

        }
    }

    struct AssemblyRecord
    {
        [ConstantColumn(typeof(AssemblyHashAlgorithm))]
        public AssemblyHashAlgorithm HashAlgId;

        [ConstantColumn(typeof(ushort))]
        public ushort MajorVersion;

        [ConstantColumn(typeof(ushort))]
        public ushort MinorVersion;

        [ConstantColumn(typeof(ushort))]
        public ushort BuildNumber;

        [ConstantColumn(typeof(ushort))]
        public ushort RevisionNumber;

        [ConstantColumn(typeof(AssemblyFlags))]
        public AssemblyFlags Flags;

        [BlobHeapIndex]
        public uint PublicKey;

        [StringHeapIndex]
        public uint Name;

        [StringHeapIndex]
        public uint Culture;
    }

    class AssemblyOSTable : Table<AssemblyOSRecord>
    {
        public AssemblyOSTable()
            : base(TableID.AssemblyOS)
        {

        }
    }

    struct AssemblyOSRecord
    {
        [ConstantColumn(typeof(uint))]
        public uint OSPlatformID;

        [ConstantColumn(typeof(uint))]
        public uint OSMajorVersion;

        [ConstantColumn(typeof(uint))]
        public uint OSMinorVersion;
    }


    class AssemblyProcessorTable : Table<AssemblyProcessorRecord>
    {
        public AssemblyProcessorTable()
            : base(TableID.AssemblyProcessor)
        {

        }
    }

    struct AssemblyProcessorRecord
    {
        [ConstantColumn(typeof(uint))]
        public uint Processor;
    }


    class AssemblyRefTable : Table<AssemblyRefRecord>
    {
        public AssemblyRefTable()
            : base(TableID.AssemblyRef)
        {

        }
    }

    struct AssemblyRefRecord
    {
        [ConstantColumn(typeof(ushort))]
        public ushort MajorVersion;

        [ConstantColumn(typeof(ushort))]
        public ushort MinorVersion;

        [ConstantColumn(typeof(ushort))]
        public ushort BuildNumber;

        [ConstantColumn(typeof(ushort))]
        public ushort RevisionNumber;

        [ConstantColumn(typeof(uint))]
        public uint Flags;

        [BlobHeapIndex]
        public uint PublicKeyOrToken;

        [StringHeapIndex]
        public uint Name;

        [StringHeapIndex]
        public uint Culture;

        [BlobHeapIndex]
        public uint HashValue;
    }

    class AssemblyRefOSTable : Table<AssemblyRefOSRecord>
    {
        public AssemblyRefOSTable()
            : base(TableID.AssemblyRefOS)
        {

        }
    }

    struct AssemblyRefOSRecord
    {
        [ConstantColumn(typeof(uint))]
        public uint OSPlatformID;

        [ConstantColumn(typeof(uint))]
        public uint OSMajorVersion;

        [ConstantColumn(typeof(uint))]
        public uint OSMinorVersion;

        [SimpleIndex(TableID.AssemblyRef)]
        public uint AssemblyRef;
    }

    class AssemblyRefProcessorTable : Table<AssemblyRefProcessorRecord>
    {
        public AssemblyRefProcessorTable()
            : base(TableID.AssemblyRefProcessor)
        {

        }
    }

    struct AssemblyRefProcessorRecord
    {
        [ConstantColumn(typeof(uint))]
        public uint Processor;

        [SimpleIndex(TableID.AssemblyRef)]
        public uint AssemblyRef;
    }
}