using System;
using Tools.Tracing.Common;

namespace Tools.Tracing.UI
{
    /// <summary>
    /// Summary description for RemoteEventHandlerManagerConfiguration.
    /// </summary>
    [Serializable]
    public class RemoteEventHandlerManagerConfiguration
    {
        public RemoteEventHandlerManagerConfiguration()
        {
        }

        public RemoteEventHandlerManagerConfiguration
            (
            RemoteConnectionConfiguration remoteConnectionConfiguration,
            TraceEventHandlerManagerConfiguration traceEventHandlerManagerConfiguration
            )
        {
            RemoteConnectionConfiguration = remoteConnectionConfiguration;
            traceEventHandlerManagerConfiguration = traceEventHandlerManagerConfiguration;
        }

        public RemoteConnectionConfiguration RemoteConnectionConfiguration { get; set; }

        public TraceEventHandlerManagerConfiguration TraceEventHandlerManagerConfiguration { get; set; }
    }
}