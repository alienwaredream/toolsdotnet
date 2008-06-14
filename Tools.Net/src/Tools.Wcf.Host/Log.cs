﻿using System;
using System.Collections.Generic;

using System.Text;
using System.Diagnostics;

namespace Tools.Wcf.Host
{
    internal static class Log
    {
        private static TraceSource traceSource =
            new TraceSource((typeof(Log).Assembly.GetName().Name));

        internal static TraceSource Source { get { return traceSource; } }
    }
}
