using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using Tools.Core;
using Tools.Core.Asserts;
using Tools.Core.Context;
using Tools.Core.Threading;
using Tools.Processes.Core;

using Tools.Coordination.WorkItems;
using Process = Tools.Processes.Core.Process;
using Tools.Coordination.Core;

namespace Tools.Coordination.ProducerConsumer
{
    delegate IProcess CreateProcess(string name);

    /// <summary>
    /// Summary description for QueueWorkItemsConsumerManager.
    /// </summary>
    public class ConsumerManager : Process
    {
        #region Fields

        private readonly ManualResetEvent waitForLowPrioritySubmissionDelayResetEvent =
            new ManualResetEvent(false);

        private ContextIdentifier contextIdentifier = new ContextIdentifier();

        private readonly object countersSyncLock = new object();
        //TODO: (SD) use SynchronizedCounter(s)
        private int pendingResponseObtainedCount;
        private int regularResponseObtainedCount;

        //TODO: (SD) Change to the instance field after covering with tests
        private static readonly SynchronizedCounter submittedItemsCounter = new SynchronizedCounter();

        private CreateProcess createConsumerFunction = new CreateProcess(ProcessorFactory.CreateProcess);

        #endregion

        #region Properties
        //TODO: (SD) Change to the instance property after covering with tests
        internal static SynchronizedCounter SubmittedItemsCounter
        {
            get
            {
                return submittedItemsCounter;
            }
        }

        protected TimeOutSubmissionsCollectorConfiguration CollectorConfiguration { get; set; }

        protected ProcessingStateData StateData { get; set; }

        /// <summary>
        /// The ratio between the regular responses and failure or pending responses when
        /// a transition to the failure survival mode should be considered.
        /// </summary>
        public int RegularToPendingThreshold
        {
            get;
            set;
        }

        public ConsumerConfiguration ConsumerConfiguration { get; set; }

        protected int ConsumerThreadsCount { get; set;}
        /// <summary>
        /// Minumum amount of samples to collect before making a decision for
        /// transition into the failure survival mode
        /// </summary>
        protected int ThresholdMinimumSampleCount { get; set;}

        protected List<IProcess> Consumers
        {
            get; set;
        }

        protected TimeoutSubmissionsCollector TimeOutSubmissionsCollector
        {
            get;
            set;
        }
        public int TotalConsumerManagerStopTimeout { get; set; }

        protected ContextIdentifier ContextIdentifier
        {
            get { return contextIdentifier; }
        }

        // TODO: Added in ad-hoc manner only, to be substituted with collection of
        // sync events according to the submission priority (SD)
        protected ManualResetEvent WaitForLowPrioritySubmissionDelayResetEvent
        {
            get { return waitForLowPrioritySubmissionDelayResetEvent; }
        }

        public int RegularResponseObtainedCount
        {
            get { return regularResponseObtainedCount; }
        }

        public object CountersSyncLock
        {
            get { return countersSyncLock; }
        }

        public int PendingResponseObtainedCount
        {
            get { return pendingResponseObtainedCount; }
        }

        #endregion

        #region Methods


        private void ConsumerStoppedCallback(IAsyncResult ar)
        {
            Descriptor ds = null;

            try
            {
                ErrorTrap.AddRaisableAssertion<ArgumentNullException>((ar as AsyncResult) != null,
                                                      "Undelying type of IAsyncResult should be of type AsyncResult");
                ErrorTrap.AddRaisableAssertion<ArgumentNullException>(
                    // ReSharper disable PossibleNullReferenceException - ErrorTrap is used above
                    (ar as AsyncResult).AsyncDelegate as VoidDelegate != null,
                    // ReSharper restore PossibleNullReferenceException
                    "Undelying type of IAsyncResult should be of type AsyncResult");
// ReSharper disable PossibleNullReferenceException - ErrorTrap is used above
                var voidDelegate = (ar as AsyncResult).AsyncDelegate as VoidDelegate;
// ReSharper restore PossibleNullReferenceException

// ReSharper disable PossibleNullReferenceException - ErrorTrap is used above
                voidDelegate.EndInvoke(ar);
// ReSharper restore PossibleNullReferenceException

                ds = ar.AsyncState as Descriptor;
            }
            catch (Exception ex)
            {
                // TODO: Think about placement of base Process class,
                // it can be prefferable to have it lower as it gets in
                // the architecture, on the other side it can provide default logging;
                // can represent the need for delegates use then. Or logging can be located in the utility (SD)
                Log.TraceData(Log.Source,TraceEventType.Error,
                                     ConsumerMessage.ErrorWhileStoppingConsumer,
                                     new ContextualLogEntry
                                         {
                                             Message =
                                                 string.Format
                                                 (
                                                 "Exception happened while trying to StopInternal consumer {0} " +
                                                 ". Exception text: {1}",
                                                 ((ds == null) ? "Unknown" : ds.Name),
                                                 ex
                                                 ),
                                             ContextIdentifier = contextIdentifier
                                         });
            }
        }

        private void Consumer_Stopped(object sender, EventArgs e)
        {
        }

        #endregion

        #region Constructors

        public ConsumerManager()
        {
            Consumers = new List<IProcess>();
            // Give all consumers 10 seconds by default to end.
            TotalConsumerManagerStopTimeout = 10000;
            // 100 by default, will wait for 100 samples before starting to evaluate the 
            // conditions fulfillment for the survival mode.
            ThresholdMinimumSampleCount = 100;
            // 90 percent by default
            RegularToPendingThreshold = 90;

        }

        #endregion

        public override void Start()
        {
            contextIdentifier = new ContextIdentifier();

            Log.TraceData(Log.Source,TraceEventType.Start,
                                 ConsumerManagerMessage.QueueWorkItemsConsumerManagerStartRequested,
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

            CreateConsumers();

            Log.TraceData(Log.Source,TraceEventType.Start,
                                 ConsumerManagerMessage.QueueWorkItemsConsumerManagerStartRequested,
                                 new ContextualLogEntry
                                     {
                                         Message =
                                             string.Format
                                             (
                                             "'{0}': Starting Consuming.",
                                             Name
                                             ),
                                         ContextIdentifier = contextIdentifier
                                     });

            foreach (IProcess consumer in Consumers)
            {
                consumer.Stopped += Consumer_Stopped;
                consumer.Start();
            }
            // If child object setup the collector StartInternal it
            // TODO: reiterate on that, ad hoc fix for response service!! (SD)
            if (TimeOutSubmissionsCollector != null)
            {
                TimeOutSubmissionsCollector.Start();
            }

            SetExecutionState(ProcessExecutionState.Running);

            Log.TraceData(Log.Source,TraceEventType.Start,
                                 ConsumerManagerMessage.QueueWorkItemsConsumerManagerStartRequested,
                                 new ContextualLogEntry
                                     {
                                         Message =
                                             string.Format
                                             (
                                             "{0} process started.",
                                             Name
                                             ),
                                         ContextIdentifier = ContextIdentifier
                                     });
        }

        public override void Abort()
        {
            base.Abort();

            for (int i = 0; i < Consumers.Count; i++)
            {
                Consumers[i].Abort();
            }
            if (TimeOutSubmissionsCollector != null)
            {
                TimeOutSubmissionsCollector.Abort();
            }
        }

        public override void Stop()
        {
            Log.TraceData(Log.Source,TraceEventType.Stop,
                                 ConsumerManagerMessage.QueueWorkItemsConsumerManagerStopRequested,
                                 new ContextualLogEntry
                                     {
                                         Message =
                                             string.Format
                                             (
                                             "{0} process is requested to StopInternal.",
                                             Name
                                             ),
                                         ContextIdentifier = ContextIdentifier
                                     });

            base.Stop();

            if (Consumers == null || Consumers.Count == 0)
            {
                OnStopped();
                return;
            }

            // TODO: This part is implemented differently for prod and cons; they
            // provide two different proof of concepts, the target here will be to
            // hide the complexity of synchronization somewhere else in the future, plus
            // producers SEH part already gone through the intensive testing and proved to be ok. (SD)
            // TODO: Provide intensive SEH testing of this part (SD)
            // TODO: Test for StartInternal/StopInternal/StartInternal/StopInternal with no appdomain unload (SD)
            WaitHandle[] whs = null;

            if (
                ErrorTrap.AddAssertion
                    (
                    Consumers != null,
                    ConsumerManagerMessage.ConsumersNotInstantiated,
                    string.Format
                        (
                        "'{0}': Consumers Not Instantiated",
                        Name
                        ),
                    contextIdentifier
                    )
                )
            {
                whs = new WaitHandle[Consumers.Count];

                for (int i = 0; i < Consumers.Count; i++)
                {
                    IAsyncResult ar =
                        Consumers[i].BeginStop
                            (
                            null,
                            ConsumerStoppedCallback);

                    whs[i] = ar.AsyncWaitHandle;
                }
            }

            bool processesStoppedWithinTimeout = false;

            processesStoppedWithinTimeout =
                WaitHandle.WaitAll
                    (
                    whs,
                    TotalConsumerManagerStopTimeout,
                    false
                    );

            if (!processesStoppedWithinTimeout)
            {
                Log.TraceData(Log.Source,TraceEventType.Error,
                                     ConsumerManagerMessage.ConsumersStoppingTimeoutError,
                                     new ContextualLogEntry
                                         {
                                             Message =
                                                 string.Format
                                                 (
                                                 "{0}: Some of the consumers were not able to StopInternal within timeout of {1} ms." +
                                                 " Process will continue its regular shutdown by calling TimeOutSubmissionsCollector for." +
                                                 "for final collection (if there is one setup).",
                                                 Name,
                                                 TotalConsumerManagerStopTimeout
                                                 ),
                                             ContextIdentifier = ContextIdentifier
                                         });
            }
            // TODO: reiterate on that, ad hoc fix for response service!! (SD)
            if (TimeOutSubmissionsCollector != null)
            {
                TimeOutSubmissionsCollector.Stop();
            }

            Log.TraceData(Log.Source,TraceEventType.Stop,
                                 ConsumerManagerMessage.QueueWorkItemsConsumerManagerStopped,
                                 new ContextualLogEntry
                                     {
                                         Message =
                                             string.Format
                                             (
                                             "{0}: stopped. Stop event is about to be raised.",
                                             Name
                                             ),
                                         ContextIdentifier = ContextIdentifier
                                     });

            OnStopped();
        }


        public void ResetRegularResponseObtainedCount()
        {
            try
            {
                Monitor.Enter(countersSyncLock);
                // TODO: That will be provided as TryEnter with configurable timout (SD)
                regularResponseObtainedCount = 0;
            }
            finally
            {
                Monitor.Exit(countersSyncLock);
            }
        }

        public void ResetPendingResponseObtainedCount()
        {
            try
            {
                Monitor.Enter(countersSyncLock);
                // TODO: That will be provided as TryEnter with configurable timout (SD)
                pendingResponseObtainedCount = 0;
            }
            finally
            {
                Monitor.Exit(countersSyncLock);
            }
        }

        protected virtual void Consumer_RegularResponseObtained
            (
            object sender,
            JobProcessedEventArgs e
            )
        {
            //TODO: (SD) use SynchronizedCounter(s)
            try
            {
                Monitor.Enter(countersSyncLock);
                // TODO: That will be provided as TryEnter with configurable timout (SD)
                regularResponseObtainedCount += 1;
                pendingResponseObtainedCount = 0;
                WaitForLowPrioritySubmissionDelayResetEvent.Set();
            }
            finally
            {
                Monitor.Exit(countersSyncLock);
            }
        }

        protected virtual void Consumer_PendingResponseObtained
            (
            object sender,
            JobProcessedEventArgs e
            )
        {
            //TODO: (SD) use SynchronizedCounter(s)
            try
            {
                Monitor.Enter(countersSyncLock);
                // TODO: That will be provided as TryEnter with configurable timout (SD)
                pendingResponseObtainedCount += 1;
                WaitForLowPrioritySubmissionDelayResetEvent.Reset();
            }
            finally
            {
                Monitor.Exit(countersSyncLock);
            }
            // by the call to the above sum of
            // PendingResponseObtainedCount+RegularResponseObtainedCount should never be zero
            bool isLinkDown = false;

            lock (CountersSyncLock)
            {
                isLinkDown =
                    (PendingResponseObtainedCount + RegularResponseObtainedCount)
                    > ThresholdMinimumSampleCount
                    &&
                    RegularResponseObtainedCount /
                    (PendingResponseObtainedCount + RegularResponseObtainedCount) * 100
                    < RegularToPendingThreshold;
            }
            if (isLinkDown)
            {
                try
                {
                    // That event is supposed to be handled by the Navitaire's handler as well
                    Log.TraceData(Log.Source,TraceEventType.Warning,
                                         0,
                                         //**new ContextualLogEntry
                                         //    {
                                         //        Message =
                                                     string.Format
                                                     (
                                                     "Number of Regularly Obtained Responses: {0}" +
                                                     "Number of Pending Responses: {1}",
                                                     RegularResponseObtainedCount,
                                                     PendingResponseObtainedCount
                                                     )//,
                                             //**    ContextIdentifier = e.OperationContextShortcut
                                             );
                }
                catch
                {
                    // TODO: Log separately to the fall back log (SD)
                }
                ResetPendingResponseObtainedCount();
                ResetRegularResponseObtainedCount();
            }
        }
        protected virtual void CreateConsumers()
        {
            for (int i = 0; i < ConsumerThreadsCount; i++)
            {
                IProcess consumer = createConsumerFunction(ConsumerConfiguration.Name);

                consumer.Name += "_" + i;

                var rHandler = consumer as IResultHandler;

                if (rHandler != null)
                {
                    rHandler.PendingResultObtained +=
                        Consumer_PendingResponseObtained;
                    rHandler.RegularResultObtained +=
                        Consumer_RegularResponseObtained;
                }

                Consumers.Add(consumer);
            }
        }
    }
}