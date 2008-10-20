using System;
using System.Diagnostics;
using Tools.Core.Utils;
using Tools.Tracing.Common;

namespace Tools.Tracing.ClientHandler
{
    /// <summary>
    /// Summary description for RemotableApplicationEventHandler.
    /// </summary>
    public class TraceEventHandlerWrapper :
        MarshalByRefObject,
        ITraceEventHandler,
        ITraceEventHandlingPublisher,
        ITraceEventFilterContainer
    {
        //public event TraceEventDelegate EventHandled = null;
        // TODO: it was separated to the property due to the logging requirements (SD)

        #region ITraceEventHandler Members

        public void HandleEvent(TraceEvent traceEvent)
        {
            //***TraceEventHandler.Instance.HandleEvent(traceEvent);

            OnEventHandled(traceEvent);
        }

        #endregion

        #region ITraceEventHandlingPublisher Members

        public event TraceEventDelegate EventHandled
        {
            add { _eventHandled += value; }
            remove { _eventHandled -= value; }
        }

        #endregion

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

        private event TraceEventDelegate _eventHandled = null;

        private void OnEventHandled(TraceEvent e)
        {
            if (_eventHandled != null)
            {
                try
                {
                    Delegate[] delegates = _eventHandled.GetInvocationList();
                    for (int i = 0; i < delegates.Length; i++)
                    {
                        var dlg = delegates[i] as TraceEventDelegate;
                        try
                        {
                            dlg(new TraceEventArgs {Event = e});
                        }
                        catch (Exception ex)
                        {
                            Log.Source.TraceData(
                                TraceEventType.Error, 0,
                                "Unable to deliver the event to the subscriber." + Environment.NewLine +
                                "Original Event: " +
                                SerializationUtility.Serialize2String(e) +
                                "Exception text: " + ex +
                                Environment.NewLine +
                                "Delegate: " + dlg.GetType().FullName,
                                e.ContextIdentifier
                                );
                            EventHandled -= dlg;
                        }
                    }
                    //EventHandled(new TraceEventArgs(e));

#warning This is not the best way how to do this, delegates collection might be much better (SD)
                }
                catch (Exception ex)
                {
#warning Handle exception! (SD)
                    int i = 0;
                }
            }
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public void AddHandler(ITraceEventHandler handler)
        {
            //***TraceEventHandlerManager.Instance.AddHandler(handler);
        }

        public void RemoveHandler(ITraceEventHandler handler)
        {
            //***TraceEventHandlerManager.Instance.RemoveHandler(handler);
        }

        // TODO: for test purposes only
        public void Check()
        {
        }
    }
}