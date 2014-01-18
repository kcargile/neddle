using System;
using System.Runtime.Serialization;

namespace Neddle
{
    /// <summary>
    /// An Neddle specific exception.
    /// </summary>
    public class NeddleException : ApplicationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NeddleException"/> class.
        /// </summary>
        public NeddleException()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NeddleException"/> class.
        /// </summary>
        /// <param name="message">A message that describes the error.</param>
        public NeddleException(string message) : base(message)
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NeddleException" /> class.
        /// </summary>
        /// <param name="message">A message that describes the error.</param>
        /// <param name="formatParams">The format parameters.</param>
        public NeddleException(string message, params object[] formatParams) : base(string.Format(message, formatParams))
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NeddleException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public NeddleException(string message, Exception inner) : base(message, inner)
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NeddleException"/> class.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        protected NeddleException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            
        }
    }
}
