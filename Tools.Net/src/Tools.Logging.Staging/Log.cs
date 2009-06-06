using System;
using System.Diagnostics;

namespace Tools.Logging
{
    internal static class Log
    {
        private static readonly TraceSource traceSource =
            new TraceSource((typeof (Log).Assembly.GetName().Name));

        internal static TraceSource Source
        {
            get { return traceSource; }
        }

        internal static void TraceData(TraceSource source, TraceEventType eventType,
                                       Enum eventId, object data)
        {
            source.TraceData(eventType, Convert.ToInt32(eventId), data);
        }
        internal static void TraceData(TraceSource traceSource, TraceEventType traceEventType, int p, string data)
        {
            traceSource.TraceData(traceEventType, p, data);
        }

        internal static void TraceData(TraceSource traceSource, TraceEventType traceEventType, int p, Exception ex)
        {
            traceSource.TraceData(traceEventType, p, ex.ToString());
        }
    }
}