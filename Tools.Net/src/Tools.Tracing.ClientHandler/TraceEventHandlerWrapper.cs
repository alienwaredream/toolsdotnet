using System;
using Tools.Tracing.Common;

namespace Tools.Tracing.ClientHandler
{
    /// <summary>
    /// Summary description for RemotableApplicationEventHandler.
    /// </summary>
    public class TraceEventHandlerWrapper :
        MarshalByRefObject /*RemotingClient*/,
        ITraceEventHandler,
        ITraceEventFilterContainer
    {
        #region ITraceEventHandler Members

        public void HandleEvent(TraceEvent traceEvent)
        {
            throw new NotImplementedException
                (
                "Client method of the TraceEventHandlerWrapper called!" +
                "Setup remoting logging correctly and try again!"
                );
        }

        #endregion

        public event TraceEventDelegate EventHandled = null;

        private void OnEventHandled(TraceEvent e)
        {
            if (EventHandled != null)
            {
                EventHandled(new TraceEventArgs {Event = e});
            }
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public void AddHandler(ITraceEventHandler handler)
        {
            throw new NotImplementedException
                (
                "Client method of the TraceEventHandlerWrapper called!" +
                "Setup remoting logging correctly and try again!"
                );
        }

        public void RemoveHandler(ITraceEventHandler handler)
        {
            throw new NotImplementedException
                (
                "Client method of the TraceEventHandlerWrapper called!" +
                "Setup remoting logging correctly and try again!"
                );
        }

        public void Check()
        {
            throw new NotImplementedException
                (
                "Client method of the TraceEventHandlerWrapper called!" +
                "Setup remoting logging correctly and try again!"
                );
        }

        #region ITraceEventFilterContainer Implementation

        private readonly ITraceEventFilterCollection _filtersChain = new ITraceEventFilterCollection();

        public ITraceEventFilterCollection Filters
        {
            get { return _filtersChain; }
        }


        public void AddFilter(ITraceEventFilter filter)
        {
            lock (_filtersChain)
            {
                _filtersChain.Add(filter);
            }
        }

        public void RemoveFilter(ITraceEventFilter filter)
        {
            lock (_filtersChain)
            {
                _filtersChain.Remove(filter);
            }
        }

        #endregion

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
    }
}