using System;
using System.Threading;
using System.Transactions;
using Tools.Core.Context;
using Tools.Core.Utils;

namespace Tools.Coordination.WorkItems
{
    /// <summary>
    /// Unit for work processing.
    /// </summary>
    [Serializable]
    public abstract class WorkItem : IContextIdentifierHolder
    {
        #region Fields

        private readonly object _messageBodyLock = new object();
        //private object _bodyObject = null;

        private readonly object _syncRoot = new object();
        private readonly long lastNoteTicks = DateTime.UtcNow.Ticks;

        private ContextIdentifier _contextIdentifier =
            new ContextIdentifier();

        private byte[] _messageBody;

        private string _note;
        private SubmissionPriority _originalPriority = SubmissionPriority.Unassigned;
        private SubmissionPriority _submissionPriority = SubmissionPriority.Normal;
        private DateTime _submittedToExternalRecipientAt;
        private WorkItemState _workItemState = WorkItemState.Unknown;
        //private string 

        #endregion Fields

        #region Public properties

        public WorkItemState SyncStatus
        {
            get
            {
                lock (_syncRoot)
                {
                    return _workItemState;
                }
            }

            set
            {
                lock (_syncRoot)
                {
                    _workItemState = value;
                }
            }
        }


        public string Note
        {
            get { return _note; }
            set { _note = value; }
        }

        public string IdHash
        {
            get { return "CH" + ContextIdentifier.ContextHolderId + "WID" + ContextIdentifier.ContextGuid; }
            set { }
        }

        /// <summary>
        /// Unique item identifier - identity
        /// </summary>
        public decimal Id { get; set; }

        /// <summary>
        /// Points to the external entity to which work item should be sent
        /// (like an job for job, or NewSkies for response) 
        /// </summary>
        public int ExternalEntityId { get; set; }

        /// <summary>
        /// Provides the type of the work item.
        /// </summary>
        public abstract WorkItemType Type { get; }

        /// <summary>
        /// One of the possible states from the <see cref="WorkItemState"/>
        /// </summary>
        public WorkItemState WorkItemState
        {
            get { return _workItemState; }
            set { _workItemState = value; }
        }

        /// <summary>
        /// Identifies the workflow details for the work item.
        /// TODO: For future iteration that property will wrap an integer by
        /// some structure or class, so value in db can be wrapped by some
        /// bitwise operations to it.
        /// </summary>
        public int ProcessCode { get; set; }

        /// <summary>
        /// One of the possible states from the <see cref="SubmissionPriority"/>
        /// </summary>
        public SubmissionPriority SubmissionPriority
        {
            get { return _submissionPriority; }
            set { _submissionPriority = value; }
        }

        /// <summary>
        /// One of the possible states from the <see cref="SubmissionPriority"/>.
        /// Provides the one level deep back track on the submission priority, so
        /// appriopriate actions can be taken if change of the item's priority
        /// is identified.
        /// </summary>
        public SubmissionPriority OriginalPriority
        {
            get { return _originalPriority; }
            set { _originalPriority = value; }
        }

        /// <summary>
        /// Stores the serialized message to be processed.
        /// </summary>
//		public string Message
//		{
//			get
//			{
//				return _message;
//			}
//			set
//			{
//				_message = value;
//			}
//		}
        /// <summary>
        /// Stores the serialized message to be processed.
        /// </summary>
        public byte[] MessageBody
        {
            get { return _messageBody; }
            set { _messageBody = value; }
        }

        /// <summary>
        /// True is item is being processed. False if it is available to be assigned.
        /// </summary>
        public bool Assigned { get; set; }

        /// <summary>
        /// If false, work item is processed for the first time.
        /// If true, represents second or subsequent attempts to process
        /// the item.
        /// </summary>
        public bool IsRetry { get; set; }

        /// <summary>
        /// Owner that reserved the work item for processing
        /// </summary>
        public string OwnerName { get; set; }

        /// <summary>
        /// Token assigned for the processing
        /// </summary>
        public long AssignmentToken { get; set; }

        /// <summary>
        /// DateTime when submitted to the external recipient, used in order to 
        /// validate if item submission can be considered expired.
        /// </summary>
        public DateTime SubmittedToExternalRecipientAt
        {
            get
            {
                lock (_syncRoot)
                {
                    return _submittedToExternalRecipientAt;
                }
            }
            set
            {
                lock (_syncRoot)
                {
                    _submittedToExternalRecipientAt = value;
                }
            }
        }

        /// <summary>
        /// Transaction to pass through boundaries of work item processing.
        /// </summary>
        public Transaction Transaction { get; set; }

        /// <summary>
        /// The DateTime when item was retrieved for processing
        /// </summary>
        public DateTime RetrievedAt { get; set; }

        /// <summary>
        /// The DateTime when work on the work item completed
        /// </summary>
        public DateTime CompletedAt { get; set; }

        public ContextIdentifier ContextIdentifier
        {
            get { return _contextIdentifier; }
            set { _contextIdentifier = value; }
        }

        #endregion Public properties

        #region Constructors

        /// <summary>
        /// Default ctor.
        /// </summary>
        protected WorkItem()
        {
        }

        protected WorkItem
            (
            decimal id,
            int externalEntityId,
            WorkItemState workItemState,
            SubmissionPriority submissionPriority,
            byte[] messageBody,
            bool assigned,
            bool isRetry,
            string ownerName,
            ContextIdentifier contextIdentifier
            )
        {
            Id = id;
            ExternalEntityId = externalEntityId;
            _workItemState = workItemState;
            _submissionPriority = submissionPriority;
            _messageBody = messageBody;
            Assigned = assigned;
            IsRetry = isRetry;
            OwnerName = ownerName;
            _contextIdentifier = contextIdentifier;
//			_processCode = processCode;
//			_assignmentToken = assignmentToken;
        }

        #endregion Constructors

        // TODO: subject to apply formatter (SD)
        public void AttachNote(string note)
        {
            long currentTicks = DateTime.UtcNow.Ticks;
            double msDiff = TimeSpan.FromTicks(currentTicks - lastNoteTicks).TotalMilliseconds;
            _note +=
                DateTime.UtcNow.ToString("dd-MMM-yy HH:mm:ss (fff)") +
                "[+" + msDiff + "]:" + note + " by " + Thread.CurrentThread.Name + Environment.NewLine;
        }

        public override string ToString()
        {
            return "<WorkItem IdHash=\"" + IdHash + "\" >" +
                   "<Note>" + Environment.NewLine
                   + _note +
                   Environment.NewLine +
                   "</Note></WorkItem>";
        }

        public static WorkItem CreateWorkItem(WorkItemType type)
        {
            if (type == WorkItemType.Request) return new RequestWorkItem();
            if (type == WorkItemType.Response) return new ResponseWorkItem();

            throw new ArgumentException
                (
                "Creation of work item type " + type + " is not currently supported."
                );
        }

        public object GetObjectFromMessageBody()
        {
            lock (_messageBodyLock)
            {
                //if (_bodyObject!=null) return _bodyObject;

                //return _bodyObject = 
                return
                    SerializationUtility.DeserializeFromByteArray
                        (
                        _messageBody
                        );
            }
        }

        public void SetMessageFromObject(object source)
        {
            lock (_messageBodyLock)
            {
                //_bodyObject = source;
                _messageBody =
                    SerializationUtility.Serialize2ByteArray
                        (
                        source
                        );
            }
        }
    }
}