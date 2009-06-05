using System;
using Tools.Core.Context;
using Tools.Coordination.WorkItems;

namespace Tools.Coordination.Core
{
    /// <summary>
    /// Summary description for JobProcessedEventArgs.
    /// </summary>
    public class JobProcessedEventArgs : EventArgs
    {
        #region Properties

        public ContextIdentifier OperationContextShortcut { get; set; }
        public WorkItem WorkItem { get; set; }
        public bool? Success { get; set; }
        public bool? Retry { get; set; }

        #endregion
    }
}