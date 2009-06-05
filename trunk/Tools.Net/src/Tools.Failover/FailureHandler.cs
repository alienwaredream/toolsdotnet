using System;
using System.Threading;
using Tools.Core;

namespace Tools.Failover
{
    public interface IFailureHandler
    {
        void HandleNormal
            (
            );

        bool HandleFailure
            (
            );
    }

    /// <summary>
    /// First iteration implementation of very simple handler.
    /// This handler only initiates the delay when operation failed.
    /// </summary>
    public class FailureHandler : Descriptor, IEnabled, IFailureHandler
    {
        private readonly FailureConfiguration _configuration;

        private readonly FailuresCounter _failCounter =
            new FailuresCounter(0, 0);

        private DateTime failureStartDateTime;

        #region IEnabled Implementation

        private bool _enabled;

        public event EventHandler EnabledChanged = null;

        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                if (_enabled != value)
                {
                    _enabled = value;
                    OnEnabledChanged();
                }
            }
        }

        protected virtual void OnEnabledChanged()
        {
            if (EnabledChanged != null)
            {
                EnabledChanged(this, EventArgs.Empty);
            }
        }

        #endregion

        public FailureHandler() { }

        public FailureHandler (FailureConfiguration configuration)
        {
            _configuration = configuration;
            Enabled = configuration.Enabled;
        }

        public virtual void HandleNormal()
        {
            _failCounter.Reset();
        }

        public virtual bool HandleFailure()
        {
            if (!Enabled || _failCounter.FailedCount == 0)
            {
                _failCounter.FailedCount++;
                failureStartDateTime = DateTime.UtcNow;
                return false;
            }

            Thread.Sleep(_configuration.FailureRetryRule.RetryInterval);
            if (_configuration.FailureRetryRule.ExitOnRetryTimeExceeded)
            {
                if (DateTime.UtcNow > failureStartDateTime.AddMilliseconds(_configuration.FailureRetryRule.RetryTime))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }
    }
}
