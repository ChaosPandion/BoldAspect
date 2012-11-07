using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoldAspect.CLI.Metadata.Blobs;

namespace BoldAspect.CLI.Metadata.MetadataStreams
{
    public sealed class BlobHeapStream : MetadataStream
    {
        public BlobHeapStream(byte[] data)
            : base(data)
        {

        }

        public Slice Get(uint index)
        {
            var data = Data;
            var length = 0;
            var lengthOffset = 0;
            {
                var b1 = data[(int)index];
                if ((b1 & 0x80) == 0x00)
                {
                    length += b1;
                    lengthOffset = 1;
                }
                else if ((b1 & 0xC0) == 0x00)
                {
                    length += data[(int)index + 1];
                    length += b1 << 8;
                    lengthOffset = 2;
                }
                else
                {
                    length += b1 << 24;
                    length += data[(int)index + 1] << 16;
                    length += data[(int)index + 2] << 8;
                    length += data[(int)index + 3];
                    lengthOffset = 4;
                }
            }
            return new Slice(data, (int)index + lengthOffset, length);
        }
    }
}