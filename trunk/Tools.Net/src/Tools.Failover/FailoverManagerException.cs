using System;
using Tools.Core;
using Tools.Core.Context;

namespace Tools.Failover
{
    /// <summary>
    /// Summary description for FailoverManagerException.
    /// </summary>
    [Serializable]
    public class FailoverManagerException : BaseException
    {
        #region Constructors

        public FailoverManagerException(
            object eventId,
            ContextIdentifier contextIdentifier
            )
            : this
                (
                null,
                eventId,
                contextIdentifier,
                null
                )
        {
        }

        public FailoverManagerException(
            string details,
            object eventId,
            ContextIdentifier contextIdentifier
            )
            : this
                (
                details,
                eventId,
                contextIdentifier,
                null
                )
        {
        }

        public FailoverManagerException
            (
            string details,
            object eventId,
            ContextIdentifier contextIdentifier,
            Exception innerException
            )
            : base
                (
                Convert.ToInt32(eventId),
                contextIdentifier,
                details,
                innerException
                )
        {
        }

        #endregion
    }
}