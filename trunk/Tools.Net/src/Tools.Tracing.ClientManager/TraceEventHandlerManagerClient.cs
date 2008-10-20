using System;
using Tools.Remoting.Client.Common;
using Tools.Tracing.Common;

namespace Tools.Tracing.ClientManager
{
    /// <summary>
    /// Summary description for RemotableApplicationEventHandler.
    /// </summary>
    public class TraceEventHandlerManagerClient :
        RemotingClient,
        ITraceEventHandlerManager
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

        public void LoadConfiguration(TraceEventHandlerManagerConfiguration configuration)
        {
            (getTransparentProxy
                 (
                 typeof (ITraceEventHandlerManager)
                 ) as ITraceEventHandlerManager).LoadConfiguration(configuration);
        }

        public TraceEventHandlerManagerConfiguration GetConfiguration()
        {
            return (getTransparentProxy
                        (
                        typeof (ITraceEventHandlerManager)
                        ) as ITraceEventHandlerManager).GetConfiguration();
        }

        public void AddHandler(ITraceEventHandler handler)
        {
            throw new NotImplementedException
                (
                "Method TraceEventHandlerManagerClient.AddHandler is not yet implemented!"
                );
        }

        public void RemoveHandler(ITraceEventHandler handler)
        {
            throw new NotImplementedException
                (
                "Method TraceEventHandlerManagerClient.RemoveHandler is not yet implemented!"
                );
        }

        #endregion
    }
}