using System;
using System.Diagnostics;
using Tools.Core.Context;
using Tools.Processes.Core;
using Tools.Coordination.WorkItems;


namespace Tools.Coordination.ProducerConsumer
{
    /// <summary>
    /// Summary description for Consumer.
    /// </summary>
    public abstract class Consumer : ThreadedProcess
    {
        #region Fields

        private ContextIdentifier _contextIdentifier = new ContextIdentifier();
        protected DateTime _lastActivityDateTime = DateTime.UtcNow;

        #endregion

        #region Properties

        protected ConsumerConfiguration Configuration { get; set; }

        #endregion

        #region Events

        public event WorkItemEventHandler WorkItemRetrieved;

        #endregion Events

        #region Properties

        protected bool Waiting { get; set; }

        protected ContextIdentifier ContextIdentifier
        {
            get { return _contextIdentifier; }
        }

        #endregion

        #region Methods

        protected virtual void OnWorkItemRetrieved(WorkItem workItem)
        {
            #region Log

            Log.Source.TraceData(TraceEventType.Verbose,
                                 ConsumerMessage.WorkItemRetrieved,
                                 new ContextualLogEntry
                                     {
                                         Message =
                                             "Work item retrieved by the " + Name + " comsumer.",
                                         ContextIdentifier = workItem.ContextIdentifier
                                     });

            #endregion Log

            if (WorkItemRetrieved != null)
            {
                WorkItemRetrieved(this, workItem);
            }
        }

        protected static void ReturnToRetrievalQueue
            (
            WorkItem queueWorkItem
            )
        {
            
        }

        #endregion Methods

        protected override void OnStopped()
        {
            // TODO: Think about placement of base Process class,
            // it can be prefferable to have it lower as it gets in
            // the architecture, on the other side it can provide default logging;
            // can represent the need for delegates use then. Or logging can be located in the utility (SD)
            Log.Source.TraceData(TraceEventType.Stop,
                                 ConsumerMessage.QueueWorkItemsConsumerStopped,
                                 new ContextualLogEntry
                                     {
                                         Message =
                                             string.Format
                                             (
                                             "{0} process is stopped. Stopped event is about to be raised.",
                                             Name
                                             ),
                                         ContextIdentifier = _contextIdentifier
                                     });

            base.OnStopped();
        }

        public override void Start()
        {
            _contextIdentifier = new ContextIdentifier();

            Log.Source.TraceData(TraceEventType.Start,
                                 ConsumerMessage.QueueWorkItemsConsumerStartRequested,
                                 new ContextualLogEntry
                                     {
                                         Message =
                                             string.Format
                                             (
                                             "{0} process is starting.",
                                             Name
                                             ),
                                         ContextIdentifier = ContextIdentifier
                                     });

            base.Start();
        }
    }
}