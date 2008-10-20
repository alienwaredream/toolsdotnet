using System;
using System.Threading;

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

        protected TraceEventHandler()
        {
            //fallbackHandler = new FileLogEventHandler();
        }

        public static TraceEventHandler Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (syncRoot)
                    {
                        if (_instance == null)
                        {
                            _instance = new TraceEventHandler();
                        }
                    }
                }
                return _instance;
            }
        }

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

            ITraceEventHandler eventHandler = null;

            // TODO: Provide a code here to force TraceEventHandlerManager.Instance
            // pre-initialization, so if there is any problem with that log message can be 
            // still added to the fallback log at least with the TraceEventHandlerManager
            // problem description as well (SD)
            try
            {
                lock (TraceEventHandlerManager.Instance.ConfLock)
                {
                    if (TraceEventHandlerManager.Instance != null && TraceEventHandlerManager.Instance.Handlers != null)
                    {
                        for (int i = 0; i < TraceEventHandlerManager.Instance.Handlers.Count; i++)
                        {
                            // TODO: Provide and async version
                            try
                            {
                                eventHandler = TraceEventHandlerManager.Instance.Handlers[i];
                                eventHandler.HandleEvent
                                    (
                                    traceEvent
                                    );
                            }
                            catch (Exception e)
                            {
                                string eText =
                                    "Exception during regular logging attempt: " + e +
                                    Environment.NewLine +
                                    // TODO: Take care if more handlers of the same type are present (SD)
                                    "Thrown by the " + eventHandler.GetType().FullName + " handler.";

                                try
                                {
#warning resolve the case when application event could not be deserialized (SD)
                                    traceEvent.Message +=
                                        Environment.NewLine + eText + Environment.NewLine;
                                    //***TraceEventHandlerManager.Instance.FallbackHandler.HandleEvent(traceEvent);
                                }
                                catch (Exception ex)
                                {
                                    throw new Exception
                                        (
                                        traceEvent.Message +
                                        "Exception during fall back logging attempt:" +
                                        ex
                                        );
                                }
                            }
                        }
                        // Set handled flag to true to avoid duplicate handling by this Instance.
                        traceEvent.Handled = true;
                    }
                }
            }
            catch (ThreadInterruptedException ex)
            {
                try
                {
                    HandleEvent(traceEvent);
                }
                catch (Exception exx)
                {
                    traceEvent.Message += exx.ToString();
                    TraceEventHandlerManager.Instance.FallbackHandler.HandleEvent(traceEvent);
                }
            }
            catch (ThreadAbortException ex)
            {
                traceEvent.Message += ex.ToString();
                TraceEventHandlerManager.Instance.FallbackHandler.HandleEvent(traceEvent);
            }
            catch (Exception ex)
            {
                traceEvent.Message += ex.ToString();
                TraceEventHandlerManager.Instance.FallbackHandler.HandleEvent(traceEvent);
            }
        }

        #endregion

        public virtual void HandleEvent(object sender, TraceEventArgs e)
        {
            // TODO: This is temporary. To be changed to fully fledged version (SD)
            HandleEvent(e.Event);
        }
    }
}