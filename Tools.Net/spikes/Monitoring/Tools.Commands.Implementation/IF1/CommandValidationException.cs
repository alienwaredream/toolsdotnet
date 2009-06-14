using System;
using Tools.Core;
using Tools.Core.Context;

namespace Tools.Commands.Implementation
{
    public class CommandValidationException: BaseException
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseException"/> class.
        /// </summary>
        public CommandValidationException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public CommandValidationException(string message) : base(message)
        {
        }

        public CommandValidationException(int loggingEventID, string message)
            : base(loggingEventID, message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public CommandValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseException"/> class.
        /// </summary>
        /// <param name="loggingEventID">The logging event ID.</param>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public CommandValidationException(int loggingEventID, string message, Exception innerException)
            : base(loggingEventID, message, innerException)
        {
        }

        public CommandValidationException
            (
            int eventId,
            ContextIdentifier contextIdentifier,
            string details,
            Exception innerException
            )
            : base(eventId, contextIdentifier, details, innerException)
        {
        }

        public CommandValidationException
            (
            Enum eventId,
            ContextIdentifier contextIdentifier,
            string details,
            Exception innerException
            )
            : base(eventId, contextIdentifier, details, innerException)
        {
        }

        #endregion
    }
}