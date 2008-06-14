using System;
using System.Collections.Generic;

using System.Text;
using System.Diagnostics;

namespace Tools.Processes.Host
{
    internal static class Log
    {
        private static TraceSource traceSource =
            new TraceSource((typeof(Log).Assembly.GetName().Name));

        internal static TraceSource Source { get { return traceSource; } }

        //internal static void TraceData(this TraceSource source, TraceEventType eventType,
        //    Enum eventId, object data)
        //{
        //    source.TraceData(eventType, Convert.ToInt32(eventId), data);
        //}
    }
}
