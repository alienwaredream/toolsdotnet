using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace Tools.Coordination.WorkItems
{
    /// <summary>
    /// Summary description for QueueWorkItemsRequestedCounter.
    /// </summary>
    public class PriorityWorkItemsRequestedCounter
    {
        #region Fields

        private readonly object _itemsPresentCountSyncRoot = new object();
        private readonly ManualResetEvent _syncReset = new ManualResetEvent(false);
        private readonly object _syncRoot = new object();
        private int _itemsPresentCount;
        private int _itemsRequestedCount;
        private SubmissionPriority _submissionPriority = SubmissionPriority.Unassigned;

        #endregion Fields

        #region Properties

        public int Value
        {
            get { return _itemsRequestedCount; }
        }

        public ManualResetEvent SyncReset
        {
            get { return _syncReset; }
        }

        public int SyncValue
        {
            get
            {
                lock (_syncRoot)
                {
                    return _itemsRequestedCount;
                }
            }
        }

        public int ItemsPresentCount
        {
            get
            {
                lock (_itemsPresentCountSyncRoot)
                {
                    return _itemsPresentCount;
                }
            }
            set
            {
                lock (_itemsPresentCountSyncRoot)
                {
                    _itemsPresentCount = value;
                }
            }
        }

        public SubmissionPriority SubmissionPriority
        {
            get { return _submissionPriority; }
            set { _submissionPriority = value; }
        }

        #endregion Properties

        #region Constructors

        public PriorityWorkItemsRequestedCounter()
        {
            _itemsRequestedCount = 0;
        }

        public PriorityWorkItemsRequestedCounter(SubmissionPriority submissionPriority)
            : this()
        {
            _submissionPriority = submissionPriority;
        }

        #endregion Constructors

        #region Methods

        public void Increment()
        {
            _itemsRequestedCount++;
        }

        public void SyncIncrement()
        {
            lock (_syncRoot)
            {
                _itemsRequestedCount++;
                Trace.WriteLine
                    (
                    "_itemsRequestedCount = " + _itemsRequestedCount,
                    "**PriorityWorkItemsRequestedCounter"
                    );
            }
        }

        public void Decrement()
        {
            _itemsRequestedCount--;
        }

        public void SyncDecrement()
        {
            lock (_syncRoot)
            {
                _itemsRequestedCount--;
                Trace.WriteLine
                    (
                    "_itemsRequestedCount = " + _itemsRequestedCount +
                    " by " + Assembly.GetCallingAssembly().FullName,
                    "**PriorityWorkItemsRequestedCounter"
                    );
            }
        }

        #endregion Methods
    }
}