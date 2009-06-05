using System;
using Tools.Coordination.WorkItems;

namespace Tools.Coordination.Core
{
    /// <summary>
    /// Summary description for SubmittingRequestArgs.
    /// </summary>
    public class SubmittingJobEventArgs
    {

        public bool Cancel
        {
            get;
            set;
        }

        public bool Retry
        {
            get;
            set;
        }
        public WorkItem WorkItem
        {
            get;
            set;
        }
    }

}