using System;
using Tools.Tracing.Common;

namespace Tools.Tracing.Server
{
    /// <summary>
    /// Summary description for ApplicationEventHandlerManagerClient.
    /// </summary>
    public class TraceEventHandlerManagerWrapper
        : MarshalByRefObject,
          ITraceEventHandlerManager
    {
        #region ITraceEventHandlerManager Members

        public void LoadConfiguration(TraceEventHandlerManagerConfiguration configuration)
        {
            TraceEventHandlerManager.Instance.LoadConfiguration(configuration);
        }

        public TraceEventHandlerManagerConfiguration GetConfiguration()
        {
            return TraceEventHandlerManager.Instance.GetConfiguration();
        }

        public void AddHandler(ITraceEventHandler handler)
        {
            TraceEventHandlerManager.Instance.AddHandler
                (
                handler
                );
        }

        public void RemoveHandler(ITraceEventHandler handler)
        {
            TraceEventHandlerManager.Instance.RemoveHandler
                (
                handler
                );
        }

        #endregion
    }
}