using System;
using System.Collections.Generic;

using System.Text;
using System.Diagnostics;

namespace Tools.Core
{
    internal static class Log
    {
        private static TraceSource traceSource =
            new TraceSource("Test");

        internal static TraceSource Source { get { return traceSource; } }

    }
}
