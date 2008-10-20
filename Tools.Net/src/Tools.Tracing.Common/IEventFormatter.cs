namespace Tools.Tracing.Common
{
    /// <summary>
    /// Summary description for IEventFormatter.
    /// </summary>
    public interface IEventFormatter
    {
        string Format(TraceEvent traceEvent);
    }
}