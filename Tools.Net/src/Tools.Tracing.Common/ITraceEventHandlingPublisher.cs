namespace Tools.Tracing.Common
{
    /// <summary>
    /// Summary description for ITraceEventHandlingPublisher.
    /// </summary>
    public interface ITraceEventHandlingPublisher
    {
        event TraceEventDelegate EventHandled;
    }
}