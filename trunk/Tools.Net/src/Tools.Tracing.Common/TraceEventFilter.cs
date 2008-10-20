using System;

namespace Tools.Tracing.Common
{
    /// <summary>
    /// Default/Base ApplicationEventTypeFilter.
    /// </summary>
    [Serializable]
    public abstract class TraceEventFilter : ITraceEventFilter
    {
        #region IEnabled Implementation

        private bool _enabled = true;

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

        #region ITraceEventFilter Members

        public virtual bool AcceptEvent(TraceEvent e)
        {
            return true;
        }

        #endregion
    }
}