using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DifferentDevices
{
    /// <summary>
    /// Represents errors that occur during parsing source file
    /// </summary>
    internal class ParseException : Exception
    {
        public ParseException() : base("Wrong format of file lines")
        {
        }

        public ParseException(string? message) : base(message)
        {
        }

        public ParseException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ParseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
