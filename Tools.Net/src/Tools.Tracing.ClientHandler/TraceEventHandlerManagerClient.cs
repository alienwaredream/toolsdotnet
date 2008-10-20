using System;
using Tools.Remoting.Client.Common;
using Tools.Tracing.Common;

namespace Tools.Tracing.ClientHandler
{
    /// <summary>
    /// Summary description for RemotableApplicationEventHandler.
    /// </summary>
    public class TraceEventHandlerManagerClient :
        RemotingClient
    {
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

        public event EventHandler EnabledChanged = null;

        protected virtual void OnEnabledChanged()
        {
            if (EnabledChanged != null)
            {
                EnabledChanged(this, EventArgs.Empty);
            }
        }

        #endregion

        public TraceEventHandlerManagerClient
            (
            string serviceHost,
            string servicePort,
            string objectUriPath
            )
            : base
                (
                serviceHost,
                Convert.ToInt32(servicePort),
                objectUriPath
                )
        {
        }

        #region ITraceEventHandlerManager Members



        #endregion
    }
}