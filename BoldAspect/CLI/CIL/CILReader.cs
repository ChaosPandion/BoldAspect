using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI.CIL
{
    public sealed class CILReader : IDisposable
    {
        private readonly Stream _stream;

        public CILReader(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");
            if (!stream.CanRead)
                throw new ArgumentException("Cannot read from stream.", "stream");
            _stream = stream;
        }

        public void Dispose()
        {
            _stream.Dispose();
        }
    }
}