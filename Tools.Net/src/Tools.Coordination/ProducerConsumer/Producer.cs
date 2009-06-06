using System;
using System.Diagnostics;
using System.Threading;
using Tools.Coordination.Core;
using Tools.Core.Context;
using Tools.Failover;
using Tools.Processes.Core;

using Tools.Coordination.WorkItems;
using Tools.Core.Asserts;

namespace Tools.Coordination.ProducerConsumer
{
    /// <summary>
    /// Summary description for QueueWorkItemsProducer.
    /// </summary>
    public abstract class Producer : ThreadedProcess
    {
        #region Fields

        private IFailureExceptionHandler failureExceptionHandler;
        private ProcessorConfiguration configuration;

        #endregion

        #region Properties

        protected ProcessorConfiguration Configuration
        {
            get { return configuration; }
        }

        protected IFailureExceptionHandler FailureExceptionHandler
        {
            get { return failureExceptionHandler; }
            set { failureExceptionHandler = value; }
        }

        protected SubmissionPriority PriorityScope
        {
            get { return configuration.Priority; }
        }

        protected ProcessingStateData StateData { get; set; }
        // (SD) Only made protected to become testable through mock 
        protected WorkItemSlotCollection RetrievedItems
        {
            get { return StateData.RetrievedItems; }
        }

        #endregion Properties

        #region Constructors

        protected Producer() {}

        protected Producer(IFailureExceptionHandler failureExceptionHandler) : this()
        {
            this.failureExceptionHandler = failureExceptionHandler;
        }

        #endregion

        #region Methods

        public abstract WorkItem GetNextWorkItem(WorkItemSlotCollection slots);

        //TODO: (SD) set parameters method is rudimentary, subject ti refactor
        public virtual void SetParameters
            (
            ProcessorConfiguration config
            )
        {
            ErrorTrap.AddRaisableAssertion<ArgumentNullException>(config != null, "config != null");
            configuration = config;
// ReSharper disable PossibleNullReferenceException - Checked by ErrorTrap
            Name = config.Name;
// ReSharper restore PossibleNullReferenceException
            Description = config.Description;
        }

        #endregion Methods

        #region Methods

        protected virtual bool ReservePrioritySlot
            (
            SubmissionPriority priority
            )
        {
            if (RetrievedItems.Configuration.PrioritySlotCounts[priority].Count == 0)
                return false;
            int timeout = RetrievedItems.Configuration.PrioritySlotCounts[priority].Timeout;

            while (true)
            {
                lock (RetrievedItems.Counters[priority])
                {
                    #region Instrumenting - to be done via perf (SD)

                    int i = RetrievedItems.Counters[priority].ItemsPresentCount;
                    int j = RetrievedItems.Counters[priority].Value;
                    int k = RetrievedItems.Configuration.PrioritySlotCounts[priority].Count;

                    //if (Tools.Instrumentation.Common.InstrumentationManager.Level == InstrumentationLevel.Debug)
                    //{
                    Log.TraceData(Log.Source,
                        TraceEventType.Verbose,
                        20000,
                        "**" + Name + ": Reserve slot attempt (priority:" + priority +
                        ", items retrieved:" + i + ", requested: " + j +
                        ", slots count: " + k);

                    #endregion Instrumenting - to be done via perf mon (SD)

                    if (
                        i // already present
                        + j // requested
                        <
                        k // slots count
                        )
                    {
                        // TODO: Introduce the separate counter for that and
                        // avoid some more locking inside (SD).
                        RetrievedItems.Counters[priority].SyncIncrement();

                        #region Instrumenting

                        Log.TraceData(Log.Source,TraceEventType.Verbose,
                                             20000, "**" + Name + ": Reserve attempt succeeded.");

                        #endregion Instrumenting

                        return true;
                    }
                    if (!Monitor.Wait
                             (
                             RetrievedItems.Counters[priority],
                             timeout
                             ))
                    {
                        #region Instrumenting

                        Log.TraceData(Log.Source,TraceEventType.Verbose,
                                             20000, "**" + Name + ": Reserve attempt failed after waiting for, ms " + timeout);

                        #endregion Instrumenting

                        return false;
                    }
                }
            }
        }

        protected virtual void CancelPrioritySlotReservation(SubmissionPriority priority)
        {
            lock (RetrievedItems.Counters[priority])
            {
                // TODO: Introduce the separate counter for that and
                // avoid some more locking inside (SD).

                #region Instrumenting

                //if (Tools.Instrumentation.Common.InstrumentationManager.Level == InstrumentationLevel.Debug)
                //{
                Log.TraceData(Log.Source,TraceEventType.Verbose,
                                     20000, "**" + Name + ": Cancelling slot reservation");
                //}

                #endregion Instrumenting

                RetrievedItems.Counters[priority].SyncDecrement();
            }
        }

        protected override void StartInternal()
        {
            WorkItem workItem = null;

            try
            {
                Log.TraceData(Log.Source,TraceEventType.Start,
                                     ProducerMessage.ThreadStarted,
                                     String.Format("'{0}': Thread Started", Name));

                while (true)
                {
                    Log.TraceData(Log.Source,TraceEventType.Verbose,
                                         ProducerMessage.QueringForAnItem,
                                         string.Format
                                             (
                                             "'{0}': Inside the while. Execution status {1}",
                                             Name, ExecutionState));
                    // TODO: review for using rather ExecutionState != ProcessExecutionState.Running (SD)
                    if (ExecutionState != ProcessExecutionState.Running)
                    {
                        break;
                    }

                    workItem = null;

                    if (ExecutionState != ProcessExecutionState.Running)
                    {
                        break;
                    }

                    #region Instrumenting

                    //if (Tools.Instrumentation.Common.InstrumentationManager.Level == InstrumentationLevel.High)
                    //{
                    Log.TraceData(Log.Source,TraceEventType.Verbose,
                                         ProducerMessage.QueringForAnItem,
                                         string.Format
                                                     (
                                                     "'{0}': About to call GetNextWorkItem",
                                                     Name
                                                     ));

                    #endregion Instrumenting

                    #region Get next work item

                    try
                    {
                        workItem = GetNextWorkItem
                            (
                            RetrievedItems
                            );
                    }
                    catch (Exception ex)
                    {
                        Log.TraceData(Log.Source,TraceEventType.Error,
                                             ProducerMessage.RetrieveMessageFailed,
                                             "Error during getting work item." + ex);
                    }

                    #endregion Get next work item

                    if (workItem == null) continue;

                    ProcessRetrievedWorkItem(workItem);
                    // TODO: possible inconsistency - message can be processed
                    // and not yet set to null when exception occurs
                }


                //				}
            }
            catch (ThreadInterruptedException)
            {
                try
                {
                    Log.TraceData(Log.Source,TraceEventType.Stop,
                                         ProducerMessage.ThreadInterrupted,
                                         string.Format
                                                     (
                                                     "'{0}': Thread Interrupted.",
                                                     Name));

                    if (workItem != null)
                    {
                        // TODO: (SD) return to it, why would not we store the message back?
                        Log.TraceData(Log.Source,TraceEventType.Error,
                                             ProducerMessage.RetrievedMessageNotExpected,
                                             string.Format
                                                         (
                                                         "'{0}': Thread has been Interrupted," +
                                                         " but no message was expected to be retrieved.",
                                                         Name));

                        ProcessRetrievedWorkItem(workItem);
                    }
                }
                catch (Exception ex)
                {
                    Log.TraceData(Log.Source,TraceEventType.Error,
                                         ProducerMessage.ErrorProcessingInterruptionBlock,
                                         "Exception when processing the interruption block. " + ex);
                }
            }
            catch (ThreadAbortException)
            {
                Log.TraceData(Log.Source,TraceEventType.Error,
                                     ProducerMessage.AbortRequested,
                                     string.Format
                                                 (
                                                 "'{0}': Abort requested.",
                                                 Name));

                if (workItem != null)
                {
                    //CheckStatusOnGlobalException(workItem);
                }
            }
            catch (Exception ex)
            {
                Log.TraceData(Log.Source,TraceEventType.Error,
                                     ProducerMessage.RetrievingMessagesFailed,
                                     string.Format
                                                 (
                                                 "'{0}': Error occured while retrieving messages." +
                                                 " Error desc.: {1}.",
                                                 Name,
                                                 ex));

                if (workItem != null)
                {
                    //CheckStatusOnGlobalException(workItem);
                }
            }
        }
        //(SD) Only made protected in order to enable for test accessor
        protected void ProcessRetrievedWorkItem(WorkItem item)
        {
            // Anywhere within this function interrupt can be called, so
            // all possible places where Thread can change its status to WaitSleepJoin
            // should be handled appropriately

            try
            {
                Monitor.Enter(RetrievedItems);
            }
            catch (ThreadInterruptedException)
            {
                // Assumes that at this point the lock cannot be owned

                Log.TraceData(Log.Source,TraceEventType.Stop,
                                     ProducerMessage.ThreadInterrupted,
                                     new ContextualLogEntry
                                         {
                                             Message =
                                                 string.Format
                                                 (
                                                 "'{0}': Thread Interrupted while" +
                                                 " trying to acquire the lock on RetrievedItems" +
                                                 " while processing a retrieved message(Id ={1})",
                                                 Name,
                                                 item.Id
                                                 ),
                                             ContextIdentifier = item.ContextIdentifier
                                         });

                try
                {
                    lock (RetrievedItems)
                    {
                        InsertToRetrievedItems(item);
                    }
                }
                catch (Exception ex)
                {
                    Log.TraceData(Log.Source,TraceEventType.Error,
                                         ProducerMessage.
                                             ErrorStoringItemToRetrievedCollection,
                                         new ContextualLogEntry
                                             {
                                                 Message =
                                                     "Exception when trying to store item tot he retrieved collection" +
                                                     ex,
                                                 ContextIdentifier = item.ContextIdentifier
                                             });
                }

                return;
            }

            try
            {
                InsertToRetrievedItems(item);
            }
            finally
            {
                Monitor.Exit(RetrievedItems);
            }
        }


        protected virtual void InsertToRetrievedItems(WorkItem item)
        {
            // Assumed the lock for RetrievedItems is already acquired

            try
            {
                // TODO: Can be located into the RetrievedItems and no need
                // to sync twice (SD).
                lock (RetrievedItems)
                {
                    RetrievedItems.AddWorkItem(item);
                    RetrievedItems.Counters[item.SubmissionPriority].SyncDecrement();

                    // TODO: Refactor both to one, so consumers are also happy (**SD1)				
                    lock (RetrievedItems.Counters[item.SubmissionPriority])
                    {
                        Monitor.Pulse(RetrievedItems.Counters[item.SubmissionPriority]);
                    }
                }
                Monitor.Pulse(RetrievedItems);

                Log.TraceData(Log.Source,TraceEventType.Verbose,
                                     ProducerMessage.MessageAddedToRetrievedItems,
                                     new ContextualLogEntry
                                         {
                                             Message =
                                                 string.Format
                                                 (
                                                 "'{0}': Recurrence(Id = {1}) added to RetrievedItems.",
                                                 Name,
                                                 item.Id
                                                 ),
                                             ContextIdentifier = item.ContextIdentifier
                                         });
            }
            catch (Exception ex)
            {
                // TODO: handle this case (SD)

                Log.TraceData(Log.Source,TraceEventType.Error,
                                     ProducerMessage.MessageAdditionToRetrievedItemsFailed,
                                     new ContextualLogEntry
                                         {
                                             Message =
                                                 string.Format
                                                 (
                                                 "'{0}': Recurrence(Id = {1}) could not be added " +
                                                 "to the RetrievedItems. Error message: {2}",
                                                 Name,
                                                 item.IdHash,
                                                 ex
                                                 )
                                         });
            }
        }

        #endregion
    }
}
