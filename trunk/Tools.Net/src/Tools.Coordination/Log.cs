using System;
using System.Diagnostics;

namespace Tools.Coordination
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
            try
            {
                source.TraceData(eventType, Convert.ToInt32(eventId), data);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString()); // will get into standard output then
                // this is the lowest fallback possible (SD)
            }
            
        }
        internal static void TraceData(TraceSource traceSource, TraceEventType traceEventType, int p, string data)
        {
            traceSource.TraceData(traceEventType, p, data);
        }
    }
}