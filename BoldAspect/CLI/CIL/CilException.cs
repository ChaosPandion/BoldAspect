using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI.CIL
{
    public class CilException : CliException
    {
        public CilException()
            : base()
        {

        }

        public CilException(string message)
            : base(message)
        {

        }

        public CilException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        public CilException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }
}
