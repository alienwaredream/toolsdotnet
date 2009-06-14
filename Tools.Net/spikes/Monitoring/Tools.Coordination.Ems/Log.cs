using System;
using System.Diagnostics;

namespace Tools.Coordination.Ems
{
    internal static class Log
    {
        private static readonly TraceSource traceSource =
            new TraceSource((typeof (Log).Assembly.GetName().Name));
        private static readonly TraceSource traceSource2 =
    new TraceSource((typeof(Log).Assembly.GetName().Name + "2"));

        internal static TraceSource Source
        {
            get { return traceSource; }
        }
        internal static TraceSource Source2
        {
            get { return traceSource2; }
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