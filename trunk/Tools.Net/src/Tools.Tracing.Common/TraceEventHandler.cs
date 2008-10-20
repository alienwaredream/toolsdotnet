using System;

namespace Tools.Tracing.Common
{
    /// <summary>
    /// Summary description for TraceEventHandler.
    /// Double check implemented already onto the initializing the Singleton instance,
    /// expected to have loading of the handlers chain.
    /// </summary>
    public class TraceEventHandler :
        ITraceEventHandler
    {
        private static readonly object syncRoot = new object();
        private static TraceEventHandler _instance;

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

        //private ITraceEventHandler			fallbackHandler		= null;

        #region ITraceEventHandler Members

        /// <summary>
        /// Calls through the handlers chain.
        /// Very simplified approach for 0 iteration of the 
        /// application event handling.
        /// Calls expected to be configurably asynch, fire and forget, etc. (SD)
        /// "configurably asynch, fire and forget, etc." - can also be a responsibility
        /// of the handler itself for the iteration 1 (SD).
        /// </summary>
        /// <param name="traceEvent"></param>
        public virtual void HandleEvent(TraceEvent traceEvent)
        {
            // Skip handling if handling is globally disabled or event has
            // already been handled.
            if (!Enabled || traceEvent.Handled) return;

            // The rest of the code is taken away as step of the bigger recode.

        }

        #endregion

        public virtual void HandleEvent(object sender, TraceEventArgs e)
        {
            // TODO: This is temporary. To be changed to fully fledged version (SD)
            HandleEvent(e.Event);
        }
    }
}