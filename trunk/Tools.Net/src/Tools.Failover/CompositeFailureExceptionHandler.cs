using System;
using System.Collections.Generic;

namespace Tools.Failover
{
    public sealed class CompositeFailureExceptionHandler : IFailureExceptionHandler
    {
        private IList<IFailureExceptionHandler> handlers;

        public CompositeFailureExceptionHandler(IList<IFailureExceptionHandler> handlers)
        {
            this.handlers = handlers;
        }

        #region IFailureExceptionHandler Members

        public FailureExceptionType HandleFailure(Exception ex)
        {
            FailureExceptionType type = FailureExceptionType.Unknown;

            foreach(IFailureExceptionHandler handler in handlers)
            {
                FailureExceptionType t = handler.HandleFailure(ex);
                if ((byte)t < (byte)type)
                    type = t;
            }

            return type;
        }

        public void ResetState()
        {
            foreach (IFailureExceptionHandler handler in handlers)
                handler.ResetState();
        }

        #endregion
    }
}