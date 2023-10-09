using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EKRLib
{
    /// <summary>
    /// The exception that is thrown when the model or power of transport is not valid
    /// </summary>
    public class TransportException : Exception
    {
        /// <summary>
        /// Initializes a new instance of TransportException class
        /// </summary>
        public TransportException() : base("Неверный ввод")
        {
        }
        /// <summary>
        /// Initializes a new instance of TransportException class
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception</param>
        public TransportException(string? message) : base(message)
        {
        }

        public TransportException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected TransportException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
