using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI
{
    public sealed class StreamHeader
    {
        public readonly int Offset;
        public readonly int Size;
        public readonly string Name;

        public StreamHeader(BlobReader reader)
        {
            Offset = reader.Read<int>();
            Size = reader.Read<int>();
            Name = reader.ReadNullTerminatedASCIIString(32, 4);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}