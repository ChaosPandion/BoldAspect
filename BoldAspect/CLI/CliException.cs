using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI
{
    public class CliException : BoldAspectException
    {
        public CliException()
            : base()
        {

        }

        public CliException(string message)
            : base(message)
        {

        }

        public CliException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        public CliException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }
}
