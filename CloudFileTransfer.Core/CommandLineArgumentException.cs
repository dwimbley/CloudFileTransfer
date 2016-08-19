using System;
using System.Runtime.Serialization;

namespace CloudFileTransfer.Core
{
    public class CommandLineArgumentException : ApplicationException
    {
         public CommandLineArgumentException(string message) : base(message) { }

        public CommandLineArgumentException(string message, Exception innerException) : base(message, innerException) { }

        public CommandLineArgumentException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
