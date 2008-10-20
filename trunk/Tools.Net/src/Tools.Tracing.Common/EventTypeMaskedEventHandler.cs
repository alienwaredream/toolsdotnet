using System;

namespace Tools.Tracing.Common
{
    /// <summary>
    /// Summary description for EventLogEventHandler.
    /// </summary>
    public abstract class EventTypeMaskedEventHandler : ITraceEventHandler
    {
        private readonly ITraceEventHandlerCollection _handlersChain = new ITraceEventHandlerCollection();

        private readonly TraceEventTypeMask mask = TraceEventTypeMask.All;

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

        public EventTypeMaskedEventHandler(string mask)
            : this()
        {
            this.mask =
                (TraceEventTypeMask) Enum.Parse(typeof (TraceEventTypeMask), mask, true);
        }

        public EventTypeMaskedEventHandler()
        {
        }

        public ITraceEventHandlerCollection HandlersChain
        {
            get { return _handlersChain; }
        }

        #region ITraceEventHandler Members

        public abstract void HandleEvent(TraceEvent traceEvent);

        #endregion

        protected bool ShouldHandleEvent(TraceEvent traceEvent)
        {
            // Check for negative cases
            if (!Enabled) return false;
            if (!Convert.ToBoolean((((short) traceEvent.Type) & (short) mask))) return false;
            // If none of negative cases applies, return true
            return true;
        }
    }
}