using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace BoldAspect.CLI.Metadata
{
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

    public interface IAssembly
    {
        AssemblyHashAlgorithm HashAlgorithm { get; set; }
        Version Version { get; set; }
        IList<byte> PublicKey { get; }
        string Name { get; set; }
        CultureInfo Culture { get; set; }
        IList<IModule> Modules { get; }
    }
}