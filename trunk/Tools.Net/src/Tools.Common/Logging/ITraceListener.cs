using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Tools.Common.Logging
{
    public interface ITraceListener
    {
        void TraceData(TraceEventCache eventCache, string source,
            TraceEventType eventType, int id, object data);
    }
}
