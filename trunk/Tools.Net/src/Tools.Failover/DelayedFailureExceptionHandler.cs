using System;
using System.Threading;

namespace Tools.Failover
{
    public class DelayedFailureExceptionHandler : IFailureExceptionHandler
    {
        private int defaultDelay;
        private int currentDelay;
        private int delayFactor;

        public DelayedFailureExceptionHandler(int defaultDelay, int delayFactor)
        {
            this.defaultDelay = defaultDelay;
            this.currentDelay = defaultDelay;
            this.delayFactor = delayFactor;
        }

        internal int UpdateDelay()
        {
            if (this.currentDelay == 0)
                this.currentDelay = this.defaultDelay;
            else
                this.currentDelay *= this.delayFactor;
            return this.currentDelay;
        }

        #region IFailureExceptionHandler Members

        public FailureExceptionType HandleFailure(Exception ex)
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