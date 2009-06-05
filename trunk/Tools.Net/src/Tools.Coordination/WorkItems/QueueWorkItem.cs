using System;
using Tools.Core.Context;
using Tools.Coordination.WorkItems;

namespace Tools.Coordination.WorkItems
{
    /// <summary>
    /// Summary description for WorkItem.
    /// </summary>
    [Serializable]
    public class QueueWorkItem : IContextIdentifierHolder
    {
        #region Fields

        private WorkItem _workItem;

        #endregion

        #region Properties

        public string MessageId { get; set; }

        public long Id { get; set; }

        public int ExternalEntityId { get; set; }

        public WorkItem WorkItem
        {
            get { return _workItem; }

            set { _workItem = value; }
        }

        public ContextIdentifier ContextIdentifier
        {
            get
            {
                return _workItem.ContextIdentifier;
                // TODO: Handle null operation context shortcut (SD)
            }
        }

        #endregion

        #region Constructors

        protected QueueWorkItem()
        {
        }

        public QueueWorkItem
            (
            WorkItem workItem
            ) : this()
        {
            _workItem = workItem;

        }

        #endregion
    }
}