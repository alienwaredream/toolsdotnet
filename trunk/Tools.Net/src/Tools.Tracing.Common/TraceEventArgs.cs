using System;

namespace Tools.Tracing.Common
{
    /// <summary>
    /// Summary description for TraceEventArgs.
    /// </summary>
    [Serializable]
    public class TraceEventArgs
    {
        public TraceEvent Event { get; set; }
    }
}