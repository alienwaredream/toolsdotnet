using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Collections;

namespace Tools.Common.Logging
{
    public class EventIdTraceFilter : TraceFilter
    {
        List<int> eventIds = new List<int>();

        public EventIdTraceFilter(IEnumerable<int> eventIds)
        {
            if (eventIds != null && (eventIds.Count<int>() > 0))
            {
                this.eventIds = new List<int>(eventIds);
                this.eventIds.Sort();
            }

        }

        public override bool ShouldTrace(TraceEventCache cache, string source, TraceEventType eventType, int id, string formatOrMessage, object[] args, object data1, object[] data)
        {
            
            return this.eventIds.BinarySearch(id) >= 0;
        }
    }
}