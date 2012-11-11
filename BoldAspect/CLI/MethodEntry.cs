using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI
{
    [Flags]
    public enum MethodHeaderFlags : byte
    {
        TinyFormat = 0x02,
        FatFormat = 0x03,
        MoreSections = 0x08,
        InitLocals = 0x10
    }

    [Flags]
    public enum MethodDataFlags : byte
    {
        ExceptionHandlingData = 0x01,
        OptILTable = 0x02,
        FatFormat = 0x40,
        MoreSections = 0x80
    }

    public sealed class MethodData
    {
        public readonly MethodDataFlags Flags;
        public readonly byte Size;
        public readonly byte[] Data;

        public MethodData(BlobReader reader)
        {
            Flags = (MethodDataFlags)reader.ReadByte();
            Size = reader.ReadByte();
            Data = reader.ReadBytes(Size);
        }
    }

    public sealed class MethodEntry
    {
        public readonly int RVA;
        public readonly MethodHeaderFlags Flags;
        public readonly int Size;
        public readonly ushort MaxStack;
        public readonly uint CodeSize;
        public readonly MetadataToken LocalVarSigTok;
        public readonly byte[] MethodBody;
        public readonly MethodData[] Data;

        public MethodEntry(int rva, BlobReader reader)
        {
            RVA = rva;
            Flags = (MethodHeaderFlags)reader.ReadByte();
            if (!Flags.HasFlag(MethodHeaderFlags.FatFormat))
            {
                Size = (int)Flags & 0x3F;
                Flags = MethodHeaderFlags.TinyFormat;
                MethodBody = reader.ReadBytes((int)Size);
            }
            else
            {
                Size = (reader.ReadByte() & 0xF0) >> 4;
                MaxStack = reader.Read<ushort>();
                CodeSize = reader.Read<uint>();
                LocalVarSigTok = new MetadataToken(reader.Read<uint>());
                MethodBody = reader.ReadBytes((int)CodeSize);

                //var end = reader.BaseStream.Position + Size;
                //var data = new List<MethodData>();
                //while (reader.BaseStream.Position < end)
                //{
                //    data.Add(new MethodData(reader));
                //}
                //Data = data.ToArray();
            }
        }
    }
}