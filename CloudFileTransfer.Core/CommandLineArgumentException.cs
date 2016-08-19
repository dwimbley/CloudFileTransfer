using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CloudFileTransfer.Core
{
    public class CommandLineArgumentException : ApplicationException
    {
         public CommandLineArgumentException(string message) : base(message) { }

        public CommandLineArgumentException(string message, Exception innerException) : base(message, innerException) { }

        public CommandLineArgumentException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
