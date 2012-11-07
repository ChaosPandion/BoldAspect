using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI.Metadata.MetadataStreams
{
    public sealed class StreamHeader
    {
        public readonly int Offset;
        public readonly int Size;
        public readonly string Name;

        public StreamHeader(BinaryReader reader)
        {
            Offset = reader.ReadInt32();
            Size = reader.ReadInt32();
            if (Size % 4 != 0)
                throw new MetadataException();

            var s = reader.BaseStream;
            var data = new byte[32];
            var count = s.Read(data, 0, data.Length);
            var j = 0;
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == 0 && (i + 1) % 4 == 0)
                {
                    s.Seek((i + 1) - 32, SeekOrigin.Current);
                    j = i;
                    break;
                }
            }
            while (data[j] == 0)
            {
                j--;
            }
            Name = Encoding.ASCII.GetString(data, 0, j + 1);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}