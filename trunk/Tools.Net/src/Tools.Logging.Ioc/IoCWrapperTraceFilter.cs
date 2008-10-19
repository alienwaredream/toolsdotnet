using System.Diagnostics;
using Spring.Context.Support;

namespace Tools.Logging.Ioc
{
    public class IocWrapperTraceFilter : TraceFilter
    {
        private readonly TraceFilter traceFilter;

        public IocWrapperTraceFilter(string objectName)
        {
            traceFilter = ContextRegistry.GetContext().GetObject(objectName) as TraceFilter;
        }

        public override bool ShouldTrace(TraceEventCache cache, string source, TraceEventType eventType, int id,
                                         string formatOrMessage, object[] args, object data1, object[] data)
        {
            if (traceFilter != null)
            {
                return traceFilter.ShouldTrace(cache, source, eventType, id, formatOrMessage, args, data1, data);
            }
            return true;
        }
    }
}