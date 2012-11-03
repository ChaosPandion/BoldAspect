using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI.Metadata
{
    /// <summary>
    /// A stream header gives the names, and the position and length of a particular table or heap. Note that the 
    /// length of a Stream header structure is not fixed, but depends on the length of its name field (a variable 
    /// length null-terminated string).  
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct MetadataStreamHeader
    {
        /// <summary>
        /// Memory offset to start of this stream from start of the metadata root
        /// </summary>
        public uint Offset;

        /// <summary>
        /// Size of this stream in bytes, shall be a multiple of 4. 
        /// </summary>
        public uint Size;

        /// <summary>
        /// Name of the stream as null-terminated variable length array of ASCII characters, padded to the next 4-byte boundary with \0 characters. 
        /// The name is limited to 32 characters. 
        /// </summary>
        public string Name;

        public void Read(System.IO.BinaryReader reader)
        {
            Offset = reader.ReadUInt32();
            Size = reader.ReadUInt32();
            if (Size % 4 != 0)
                throw new MetadataException();
            var i = 0;
            var list = new List<char>(32);
            var c = reader.PeekChar();
            while (c > 0 && c != '\0' && i < 32)
            {
                list.Add((char)reader.ReadByte());
                c = reader.PeekChar();
            }
            list.Add((char)reader.ReadByte());
            while (list.Count % 4 != 0)
                list.Add((char)reader.ReadByte());
            Name = new string(list.ToArray()).TrimEnd('\0');

        }

        public override string ToString()
        {
            return Name;
        }
    }
}