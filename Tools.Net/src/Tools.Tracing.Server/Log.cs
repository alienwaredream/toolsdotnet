using System.Diagnostics;

namespace Tools.Tracing.Server
{
    internal static class Log
    {
        private static readonly TraceSource traceSource =
            new TraceSource((typeof (Log).Assembly.GetName().Name));

        internal static TraceSource Source
        {
            get { return traceSource; }
        }
    }
}