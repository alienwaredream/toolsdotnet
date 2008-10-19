using System;
using System.Collections.Generic;
using Tools.Core.Context;
using Tools.Core.Messaging;

namespace Tools.Core
{
    /// <summary>
    /// The base exception class for the application to throw if there is no any more specific 
    /// exception to use. Contains list of messages, that can be accumulated throughout the 
    /// propagation or failure chain.
    /// </summary>
    [Serializable]
    public class BaseException : Exception
    {
        #region Fields

        //private bool _handled = false;
        private ContextIdentifier _contextIdentifier = new ContextIdentifier();
        private string _ticketId;
        private List<Message> messages = new List<Message>();

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the logging event ID.
        /// </summary>
        /// <value>The logging event ID.</value>
        public int LoggingEventID { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is recoverable.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is recoverable; otherwise, <c>false</c>.
        /// </value>
        public bool IsRecoverable { get; set; }

        /// <summary>
        /// Not thread safe, only access on single thread at a time assumed.
        /// </summary>
        /// <value>The ticket id.</value>
        public string TicketId
        {
            get
            {
                if (_ticketId != null) return _ticketId;

                object test = Data["TicketId"];

                if (test != null)
                {
                    _ticketId = test.ToString();
                }
                return _ticketId;
                // Can still be null, if noone set it during the exception handling chain
            }
            set { _ticketId = value; }
        }

        /// <summary>
        /// Gets or sets the messages.
        /// </summary>
        /// <value>The messages.</value>
        public List<Message> Messages
        {
            get { return messages; }
            set { messages = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseException"/> class.
        /// </summary>
        public BaseException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseException"/> class.
        /// </summary>
        /// <param name="loggingEventID">The logging event ID.</param>
        public BaseException(int loggingEventID)
        {
            LoggingEventID = loggingEventID;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public BaseException(string message) : base(message)
        {
        }

        public BaseException(int loggingEventID, string message)
            : base(message)
        {
            LoggingEventID = loggingEventID;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public BaseException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseException"/> class.
        /// </summary>
        /// <param name="loggingEventID">The logging event ID.</param>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public BaseException(int loggingEventID, string message, Exception innerException)
            : base(message, innerException)
        {
            LoggingEventID = loggingEventID;
        }

        public BaseException
            (
            int eventId,
            ContextIdentifier contextIdentifier,
            string details,
            Exception innerException
            )
            : base(details, innerException)
        {
            LoggingEventID = eventId;
            _contextIdentifier = contextIdentifier;
        }

        public BaseException
            (
            Enum eventId,
            ContextIdentifier contextIdentifier,
            string details,
            Exception innerException
            )
            : base(details, innerException)
        {
            LoggingEventID = Convert.ToInt32(eventId);
            _contextIdentifier = contextIdentifier;
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Probes for ticket.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <returns></returns>
        public static string ProbeForTicket(Exception ex)
        {
            if (ex == null) return null;

            var be = ex as BaseException;

            if (be == null)
                return ex.Data["TicketId"] as string;

            return be.TicketId;
        }

        #endregion

        #region ISerializable implementation

        #endregion

        /// <summary>
        /// Creates and returns a string representation of the current exception.
        /// </summary>
        /// <returns>
        /// A string representation of the current exception.
        /// </returns>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*"/></PermissionSet>
        public override string ToString()
        {
            string messagesDump = null;
            Messages.ForEach(delegate(Message m) { messagesDump += m.Text; });
            return base.ToString() + Environment.NewLine + messagesDump;
        }
    }
}