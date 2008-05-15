using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Tools.Common.Logging
{
    public static class Log
    {
        private static TraceSource traceSource =
            new TraceSource((typeof(Log).Assembly.GetName().Name));

        public static TraceSource Source { get { return traceSource; } }

        public static void TraceData(this TraceSource source, TraceEventType eventType,
            Enum eventId, object data)
        {
            source.TraceData(eventType, Convert.ToInt32(eventId), data);
        }

    }
}
