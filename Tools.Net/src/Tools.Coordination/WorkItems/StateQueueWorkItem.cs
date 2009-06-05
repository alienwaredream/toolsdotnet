using System;
using Tools.Core.Context;
using Tools.Coordination.WorkItems;

namespace Tools.Coordination.Core
{

    #region StateQueueWorkItem class

    /// <summary>
    /// Summary description for WorkItem.
    /// </summary>
    [Serializable]
    public class StateQueueWorkItem : QueueWorkItem
    {
        #region Fields

        private readonly object _syncRoot = new object();
        private WorkItemProcessStatus _status;
        private DateTime _submittedToExternalRecipientAt;

        #endregion

        #region Properties

        public WorkItemProcessStatus SyncStatus
        {
            get
            {
                lock (_syncRoot)
                {
                    return _status;
                }
            }

            set
            {
                lock (_syncRoot)
                {
                    _status = value;
                }
            }
        }

        public DateTime SubmittedToExternalRecipientAt
        {
            get { return _submittedToExternalRecipientAt; }
        }

        #endregion

        #region Constructors

        public StateQueueWorkItem()
        {
            _status = WorkItemProcessStatus.None;
        }

        public StateQueueWorkItem
            (
            WorkItem workItem,
            IContextIdentifierHolder messageBody
            )
            : this
                (
                workItem,
                messageBody,
                WorkItemProcessStatus.None
                )
        {
        }

        public StateQueueWorkItem
            (
            WorkItem workItem,
            IContextIdentifierHolder messageBody,
            WorkItemProcessStatus status
            ) : base
                (
                workItem //,
                //null //TODO: **(SD) Extra attention here. I forgot what was the intent
                )
        {
            _status = status;
        }

        #endregion

        #region  Methods

        /// <summary>
        /// Not synchronized!
        /// </summary>
        public void SetToSubmittedToExternalRecipientState()
        {
            // TODO: We might not need synchronization here (SD) **
            lock (_syncRoot)
            {
                _status = WorkItemProcessStatus.SubmittedToExternalRecipient;
                _submittedToExternalRecipientAt = DateTime.UtcNow;
            }
        }

        #endregion  Methods
    }

    #endregion
}