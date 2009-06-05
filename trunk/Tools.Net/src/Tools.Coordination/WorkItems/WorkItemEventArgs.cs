using System;

namespace Tools.Coordination.WorkItems
{
    /// <summary>
    /// Summary description for WorkItemEventArgs.
    /// </summary>
    [Serializable]
    public class WorkItemEventArgs : EventArgs
    {
        private readonly WorkItem _workItem;

        public WorkItemEventArgs
            (
            WorkItem workItem
            )
        {
            _workItem = workItem;
        }

        public WorkItem WorkItem
        {
            get { return _workItem; }
        }
    }
}