using System;
using System.Threading;

namespace Tools.Failover
{
    public class DelayedFailureExceptionHandler : IFailureExceptionHandler
    {
        private int defaultDelay = 5000;
        private int currentDelay;
        private int delayFactor = 2;
        private int maxDelay = 30000;

        public DelayedFailureExceptionHandler()
        {
        }

        public DelayedFailureExceptionHandler(int defaultDelay, int delayFactor, int maxDelay)
        {
            this.defaultDelay = defaultDelay;
            this.currentDelay = defaultDelay;
            this.delayFactor = delayFactor;
            this.maxDelay = maxDelay;
        }

        internal int UpdateDelay()
        {
            if (currentDelay == 0)
                currentDelay = defaultDelay;
            else
                currentDelay *= delayFactor;

            if (maxDelay < currentDelay) currentDelay = maxDelay;

            return currentDelay;
        }

        #region IFailureExceptionHandler Members

        public virtual FailureExceptionType HandleFailure(Exception ex)
        {
            Thread.Sleep(this.UpdateDelay());

            return FailureExceptionType.Recoverable;
        }

        public void ResetState()
        {
            this.currentDelay = 0;
        }

        #endregion
    }
}