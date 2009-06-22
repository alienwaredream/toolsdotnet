using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using Tools.Coordination.Core;
using Tools.Coordination.ProducerConsumer;
using Tools.Core;
using Tools.Core.Asserts;
using Tools.Core.Context;
using Tools.Processes.Core;
using Tools.Coordination.WorkItems;
using Process = Tools.Processes.Core.Process;

namespace Tools.Coordination.ProducerConsumer
{
    /// <summary>
    /// Summary description for ProducerManager.
    /// 
    /// </summary>
    public class ProducerManager : Process
    {
        #region Fields

        private List<IProcess> producers;

        private ContextIdentifier contextIdentifier;

        private IProcess retrievedItemsCleaner;

        private AutoResetEvent cleanerStopEvent;


        #endregion

        #region Properties

        public int RetrievedItemsCleanerInterval { get; set; }
        public int TotalCleanerRegularStopTimeout { get; set; }

        public bool RetrievalStorageIsRecoverable { get; set; }

        /// <summary>
        /// The timeout in ms, given to the cleaner to finish reading messages from
        /// the retrieval queue and returning them to MQ, before interrupt on its thread
        /// would be called.
        /// </summary>
        public int CleanerRegularStopTimeout { get; set; }
        /// <summary>
        /// Defines the number of active cleaner objects that will be moving items from the internal retrieval items 
        /// collection back to the MQ. Every active object is assumed to have its own thread of execution.
        /// </summary>
        public int RetrievalCleanersCount { get; set; }

        public List<ProcessorConfiguration> ProducersDefinition { get; set; }

        private int ProducingStopTimeout { get; set; }
        public int MaxTotalRetrievedItemsCount { get;set; }

        public WorkItemSlotCollection RetrievedItems { get; set; }

        public int TotalConsumerManagerStopTimeout { get; set; }

        #endregion Properties

        #region Constructors

        public ProducerManager()
        {
            RetrievedItemsCleanerInterval = 50;
            RetrievalStorageIsRecoverable = false;
            TotalCleanerRegularStopTimeout = 10000;
            CleanerRegularStopTimeout = 8000;
            RetrievalCleanersCount = 2;
        }

        #endregion

        #region Methods

        #region IProcess implementation

        public override void Start()
        {
            contextIdentifier = new ContextIdentifier();
            //activeProcessesCounter	= 0;
            cleanerStopEvent = new AutoResetEvent(false);

            producers =
                new List<IProcess>();

            uint producerCount = 0;

            #region Create producers

            foreach (ProcessorConfiguration processorConfiguration in ProducersDefinition)
            {
                producerCount = processorConfiguration.Count;

                for (int i = 0; i < producerCount; i++)
                {
                    Producer producer =
                        ProcessorFactory.CreateProducer
                            (
                            processorConfiguration.Name
                            );
                    
                    producer.SetParameters(new ProcessorConfiguration
                                               {
                                                   Count = processorConfiguration.Count,
                                                   Description = processorConfiguration.Description,
                                                   Enabled = processorConfiguration.Enabled,
                                                   // Change name to reflect the index of the 
                                                   // current producer
                                                   Name = processorConfiguration.Name + "_" + i,
                                                   Priority = processorConfiguration.Priority
                                               });

                    producers.Add(producer);
                }
            }

            #endregion Create producers

            Log.TraceData(Log.Source,TraceEventType.Verbose,
                                 ProducerManagerMessage.StartingProducing,
                                 new ContextualLogEntry
                                     {
                                         Message =
                                             string.Format
                                             (
                                             "'{0}': Starting Producing with {1} primary and {2} secondary producers.",
                                             Name,
                                             producerCount, 0
                                             ),
                                         ContextIdentifier = contextIdentifier
                                     });

            foreach (IProcess producer in producers)
            {
                producer.Start();
            }

            SetExecutionState(ProcessExecutionState.Running);
        }

        /// <summary>
        /// Also to be thought about BeginStop, but not sure about BeginStart (SD)
        /// </summary>
        public override void Stop()
        {

            Log.TraceData(Log.Source,TraceEventType.Stop,
                                 ProducerManagerMessage.StoppingProducing,
                                 new ContextualLogEntry
                                     {
                                         Message =
                                             string.Format
                                             (
                                             "{0}: Thread Id = {1}': Stopping Producing.",
                                             Name,
                                             Thread.CurrentThread.ManagedThreadId
                                             ),
                                         ContextIdentifier = contextIdentifier
                                     });


            if (
                ErrorTrap.AddAssertion
                    (
                    producers != null,
                    ProducerManagerMessage.ProducersNotInstantiated,
                    string.Format
                        (
                        "{0}: Thread Id = {1}': Producers Not Instantiated",
                        Name,
                        Thread.CurrentThread.ManagedThreadId
                        ),
                    contextIdentifier
                    )
                )
            {
                var whs = new WaitHandle[producers.Count];

                for (int i = 0; i < producers.Count; i++)
                {
                    IAsyncResult ar =
                        producers[i].BeginStop
                            (
                            null,
                            producerStoppedCallback);

                    whs[i] = ar.AsyncWaitHandle;
                }

                // Starting the process that will move messages from the retrievedItems internal collection
                // to the retrieval queue
                retrievedItemsCleaner = new RetrievedItemsCleanerManager
                    (
                    RetrievedItems,
                    RetrievalStorageIsRecoverable,
                    contextIdentifier,
                    RetrievedItemsCleanerInterval,
                    "RetrievedItems Cleaner",
                    "Stores retrieved items back to the Retrieval queue.",
                    RetrievalCleanersCount,
                    CleanerRegularStopTimeout
                    );
                retrievedItemsCleaner.Stopped += RetrievedItemsCleaner_Stopped;

                retrievedItemsCleaner.Start();

                bool processesStoppedWithinTimeout = WaitHandle.WaitAll
                    (
                    whs,
                    ProducingStopTimeout,
                    false
                    );

                if (!processesStoppedWithinTimeout)
                {
                    Log.TraceData(Log.Source,TraceEventType.Error,
                                         ProducerManagerMessage.ProducersStoppingTimeoutError,
                                         new ContextualLogEntry
                                             {
                                                 Message =
                                                     string.Format
                                                     (
                                                     "{0}: Some of the producers were not able to StopInternal within timeout of {1} ms." +
                                                     " Process will continue its regular shutdown by calling RetrievedItemsCleaner StopInternal.",
                                                     Name,
                                                     ProducingStopTimeout
                                                     ),
                                                 ContextIdentifier = contextIdentifier
                                             });
                }

                retrievedItemsCleaner.Stop();

                if (!cleanerStopEvent.WaitOne
                         (
                         TotalCleanerRegularStopTimeout,
                         false
                         ))
                {
                    Log.TraceData(Log.Source,TraceEventType.Error,
                                         ProducerManagerMessage.CleanerManagerStoppingTimeout,
                                         new ContextualLogEntry
                                             {
                                                 Message =
                                                     string.Format
                                                     (
                                                     "{0}: Cleaner manager was not able to StopInternal within {1} ms." +
                                                     " RetrievedItemsCleaner will continue to run but can be interrupted abruptly by OS.",
                                                     Name,
                                                     TotalCleanerRegularStopTimeout
                                                     ),
                                                 ContextIdentifier = contextIdentifier
                                             });
                    // TODO: we can think about calling abort onto the cleaner manager here. (SD)
                }
            }
            base.Stop();

            OnStopped();
        }

        private void producerStoppedCallback(IAsyncResult ar)
        {
            // TODO: Implementation of this method contradicts with our initial with MS
            // idea to call EndStop, we will be reviewing it later (SD).
            // TODO: Also regular logging for StopInternal is placed in the source class right now, but
            // actually the call is going to be settled here. (SD)
            Descriptor ds = null;
            try
            {

                var voidDelegate = (ar as AsyncResult).AsyncDelegate as VoidAction;

                voidDelegate.EndInvoke(ar);

                ds = ar.AsyncState as Descriptor;

                Log.TraceData(Log.Source,TraceEventType.Information,
                                     ProducerManagerMessage.ProducerCalledCallback,
                                     new ContextualLogEntry
                                         {
                                             Message =
                                                 string.Format
                                                 (
                                                 "{0}: Producer {1} called the StopInternal callback",
                                                 Name,
                                                 ((ds == null) ? "Unknown" : ds.Name)
                                                 ),
                                             ContextIdentifier = contextIdentifier
                                         });
            }
            catch (Exception ex)
            {
                // TODO: Think about placement of base Process class,
                // it can be prefferable to have it lower as it gets in
                // the architecture, on the other side it can provide default logging;
                // can represent the need for delegates use then. Or logging can be located in the utility (SD)
                Log.TraceData(Log.Source,TraceEventType.Error,
                                     ProducerManagerMessage.ErrorWhileStoppingProducer,
                                     new ContextualLogEntry
                                         {
                                             Message =
                                                 string.Format
                                                 (
                                                 "Exception happened while trying to StopInternal producer {0} " +
                                                 ". Exception text: {1}",
                                                 ((ds == null) ? "Unknown" : ds.Name),
                                                 ex
                                                 ),
                                             ContextIdentifier = contextIdentifier
                                         });
            }
        }


        public override void Abort()
        {
            base.Abort();

            Log.TraceData(Log.Source,TraceEventType.Error,
                                 ProducerManagerMessage.AbortingProducing,
                                 new ContextualLogEntry
                                     {
                                         Message =
                                             string.Format
                                             (
                                             "{0}: Thread Id = {1}': Aborting Producing.",
                                             Name,
                                             Thread.CurrentThread.ManagedThreadId
                                             ),
                                         ContextIdentifier = contextIdentifier
                                     });
            //			System.Diagnostics.Debugger.Launch();

            if (
                ErrorTrap.AddAssertion
                    (
                    producers != null,
                    ProducerManagerMessage.ProducersNotInstantiated,
                    string.Format
                        (
                        "{0}: Thread Id = {1}': Producers Not Instantiated",
                        Name,
                        Thread.CurrentThread.ManagedThreadId
                        ),
                    contextIdentifier
                    )
                )
            {
                foreach (IProcess producer in producers)
                {
                    producer.Abort();
                }
            }

            if (
                ErrorTrap.AddAssertion
                    (
                    retrievedItemsCleaner != null,
                    ProducerMessage.RetrievedItemsCleanerNotInstantiated,
                    string.Format
                        (
                        "{0}: Thread Id = {1}': RetrievedItems Cleaner Not Instantiated",
                        Name,
                        Thread.CurrentThread.ManagedThreadId
                        ),
                    contextIdentifier
                    )
                )
            {
                retrievedItemsCleaner.Abort();
            }
        }

        #endregion IProcess implementation

        #endregion

        private void RetrievedItemsCleaner_Stopped(object sender, EventArgs e)
        {
            cleanerStopEvent.Set();
            // TODO: Log this (SD)
        }
    }
}