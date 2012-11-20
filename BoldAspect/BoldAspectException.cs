using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect
{
    public class BoldAspectException : Exception
    {
        public BoldAspectException()
            : base()
        {

        }

        public BoldAspectException(string message)
            : base(message)
        {

        }

        public BoldAspectException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        public BoldAspectException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }
}