using System;
using System.Xml.Serialization;

namespace Tools.Coordination.WorkItems
{
    /// <summary>
    /// Summary description for WorkItemSlot.
    /// </summary>
    [Serializable]
    public class WorkItemSlot
    {
        private readonly object workItemSyncRoot = new object();
        private SubmissionPriority _submissionPriority = SubmissionPriority.Unassigned;
        private WorkItem _workItem;

        public WorkItemSlot()
        {
        }

        protected WorkItemSlot(SubmissionPriority priority)
        {
            _submissionPriority = priority;
        }

        public bool IsEmpty
        {
            get
            {
                lock (workItemSyncRoot)
                {
                    return _workItem == null;
                }
            }
        }

        [XmlAttribute]
        public SubmissionPriority SubmissionPriority
        {
            get { return _submissionPriority; }
            set { _submissionPriority = value; }
        }


        /// <summary>
        /// Returns slot's work item and clears the slot.
        /// </summary>
        /// <returns></returns>
        public WorkItem RetrieveWorkItem()
        {
            lock (workItemSyncRoot)
            {
                WorkItem retWorkItem = _workItem;
                _workItem = null;
                return retWorkItem;
            }
        }

        /// <summary>
        /// Assigns the work item to the slot.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// When workItem has different priority than the slot it is being assigned to.
        /// </exception>
        /// <param name="workItem"></param>
        public void AssignWorkItem(WorkItem workItem)
        {
            if (workItem.SubmissionPriority != SubmissionPriority)
            {
                throw new ArgumentException
                    (
                    "WorkItem with submission type " + workItem.SubmissionPriority +
                    " can't be assigned to the WorkItemSlot with different priority (" + SubmissionPriority +
                    ").",
                    "workItem"
                    );
            }

            lock (workItemSyncRoot)
            {
                _workItem = workItem;
            }
        }

        /// <summary>
        /// Clears the slot.
        /// </summary>
        public void Clear()
        {
            lock (workItemSyncRoot)
            {
                _workItem = null;
            }
        }

        public static WorkItemSlot Create(SubmissionPriority priority)
        {
            return new WorkItemSlot(priority);
        }
    }
}