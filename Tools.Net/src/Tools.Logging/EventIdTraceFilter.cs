using System.Collections.Generic;
using System.Diagnostics;

namespace Tools.Logging
{
    public class EventIdTraceFilter : TraceFilter
    {
        private readonly List<int> eventIds = new List<int>();

        public EventIdTraceFilter(IEnumerable<int> eventIds)
        {
            if (eventIds != null /*&& (eventIds.Count() > 0)*/)
            {
                this.eventIds = new List<int>(eventIds);
                this.eventIds.Sort();
            }
        }

        public override bool ShouldTrace(TraceEventCache cache, string source, TraceEventType eventType, int id,
                                         string formatOrMessage, object[] args, object data1, object[] data)
        {
            return eventIds.BinarySearch(id) >= 0;
        }
    }
}