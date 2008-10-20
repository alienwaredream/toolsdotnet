using System;

namespace Tools.Tracing.Common
{
    /// <summary>
    /// TraceEventHandlerDelegate. Keeping for backward compatibility only, should be Action&lt;TraceEventArgs&gt;
    /// </summary>
    [Serializable]
    public delegate void TraceEventDelegate
        (
        TraceEventArgs eventArgs
        );
}