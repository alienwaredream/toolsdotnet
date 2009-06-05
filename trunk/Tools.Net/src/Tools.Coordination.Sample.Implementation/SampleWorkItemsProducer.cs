using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Transactions;
using Tools.Coordination.Core;
using Tools.Coordination.ProducerConsumer;
using Tools.Coordination.WorkItems;
using Tools.Core.Context;
using Tools.Core.Utils;
using Tools.Failover;
using Tools.Logging;
using Tools.Processes.Core;

namespace Tools.Coordination.Sample.Implementation
{
    #region SampleWorkItemsProducer class

    /// <summary>
    /// Summary description for SampleWorkItemsProducer.
    /// </summary>
    public class SampleWorkItemsProducer : Producer
    {
        private WorkItem workItemCandidate;

        public PerformanceEventHandler PerformanceHandler { get; set; }
        private IJobProvider<Job> ItemProvider { get; set; }

        #region Constructors

        public SampleWorkItemsProducer(IFailureExceptionHandler exceptionHandler)
            : base(exceptionHandler)
        {
            Initialize();
        }
        public SampleWorkItemsProducer()
            : this(null)
        {

        }



        #endregion

        #region Methods

        public override void Initialize()
        {
            base.Initialize();
            PerformanceHandler = new PerformanceEventHandler(
    new PerformanceEventHandlerConfiguration
    {
        Counters =
            new List<PerfomanceCounterConfiguration>
                                {
                                    new PerfomanceCounterConfiguration
                                        {
                                            Name = "Load queue reads/sec",
                                            ClearOnStart = true,
                                            CounterType = PerformanceCounterType.RateOfCountsPerSecond32,
                                            EventId = "Reads From Queue/sec",
                                            Description = "Number of reads from the queue per second"
                                        },
                                },
        CategoryName = "Wds Eligibility Load Preprocessing #",
        Description = "Eligibility load performance counters.",
        EnableSetupOnInitialization = false,
        MachineName = ".",
        Name = ""
    });
            PerformanceHandler.Enabled = true;
        }

        public override WorkItem GetNextWorkItem
            (
            WorkItemSlotCollection slots
            )
        {
            workItemCandidate = null;

            #region Process

            if (ReservePrioritySlot(PriorityScope))
            {
                if (ExecutionState != ProcessExecutionState.Running)
                {
                    return null;
                }
                try
                {
                    WorkItem workItem = GetWorkItemFromQueue
                        ();

                    if (workItem != null)
                    {
                        Log.Source.TraceData(TraceEventType.Verbose,
                                             ProducerMessage.MessageRetrieved,
                                             new ContextualLogEntry
                                                 {
                                                     Message =
                                                         "Got a new work item" + workItem,
                                                     ContextIdentifier = new ContextIdentifier()
                                                 });

                        return workItem;
                    }
                }
                catch (Exception ex)
                {
                    // Interrupt should not be a problem
                    CancelPrioritySlotReservation(PriorityScope);

                    Log.Source.TraceData(TraceEventType.Error,
                                         ProducerMessage.RetrieveMessageFailed,
                                         new ContextualLogEntry
                                             {
                                                 Message =
                                                     "Error during getting work item." + ex,
                                                 ContextIdentifier = new ContextIdentifier()
                                             });


                    return null;
                }

                CancelPrioritySlotReservation(PriorityScope);
            }

            #endregion Process

            return null;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns>Retrieved work item or null if not found, didn't pass pre-check, etc.</returns>
        private WorkItem GetWorkItemFromQueue
            ()
        {
            //TODO: Pay great attention here, now workItemCandidate is an instance field!!! (SD)
            workItemCandidate = null;

            #region Get message from queue

            var transaction = new CommittableTransaction();

            try
            {

                Job job;


                using (DependentTransaction dependentTransaction =
                    transaction.DependentClone(DependentCloneOption.BlockCommitUntilComplete))
                {
                    using (var scope = new TransactionScope(dependentTransaction))
                    {
                        job = ItemProvider.GetNextItem();
                        scope.Complete();
                    }

                    dependentTransaction.Complete();
                }

                if (job != null)
                {
                    PerformanceHandler.HandleEvent("Reads From Queue/sec", 1);

                    workItemCandidate = new RequestWorkItem(job.ContextIdentifier.InternalId,
                                                            0, WorkItemState.AvailableForProcessing,
                                                            SubmissionPriority.Normal, null, false, false, "test",
                                                            new ContextIdentifier());
                    // Set transaction on the work item
                    workItemCandidate.Transaction = transaction;
                    workItemCandidate.RetrievedAt = DateTime.Now;
                    workItemCandidate.MessageBody = SerializationUtility.Serialize2ByteArray(job);
                }
                else
                {
                    workItemCandidate = null;
                }

                FailureExceptionHandler.ResetState();
            }
            catch (Exception ex)
            {
                try
                {
                    // Rollback the commitable transaction
                    transaction.Rollback(ex);
                }
                finally
                {
                    transaction.Dispose();
                }

                Log.Source.TraceData(TraceEventType.Error,
                                     ProducerMessage.ErrorDuringObtainingTheWorkItem,
                                     new ContextualLogEntry
                                         {
                                             Message =
                                                 "Exception happened when trying to get item from the message queue (" +
                                                 "queue.QueuePath" + "). " + Environment.NewLine + ex
                                             ,
                                             ContextIdentifier =
                                                 ((workItemCandidate != null)
                                                      ? workItemCandidate.ContextIdentifier
                                                      :
                                                          new ContextIdentifier())
                                         });
                // To review what is the required handling here for us (SD)
                if (ExecutionState == ProcessExecutionState.Running)
                {
                    FailureExceptionType type = FailureExceptionHandler.HandleFailure(ex);
                    if (type == FailureExceptionType.NonRecoverable)
                        Stop();
                }
                else
                {
                    if (workItemCandidate != null)
                    {
                        Log.Source.TraceData(TraceEventType.Information,
                                             ProducerMessage.RetrievedMessageReturnedToTheRetrievalQueue,
                                             new ContextualLogEntry
                                                 {
                                                     Message =
                                                         string.Format
                                                         (
                                                         "'{0}': Retrieved Recurrence(Id = {1}) Successfully Saved to the {2} queue",
                                                         Name,
                                                         workItemCandidate.Id,
                                                         ""
                                                         ),
                                                     ContextIdentifier = workItemCandidate.ContextIdentifier
                                                 });
                        return null;
                    }
                }
                if (workItemCandidate == null) return null;
            }

            #endregion Get message from queue

            #region Pre-processing checks

            WorkItem workItem = AssignWorkItemFromCandidate(workItemCandidate);

            #endregion Pre-processing checks

            // Return retrieved work item (or null)
            return workItem;
        }

        /// <summary>
        /// Assigns work item based onto the candidate. This method has to provide 
        /// chacks if required for the suitability of the candidate item and return
        /// null item if item is not suitable for processing or construct some other
        /// work item based on the candidate that would be applicable for processing.
        /// </summary>
        /// <param name="candidate">The candidate item to create work item from.</param>
        /// <returns>null or work item to process</returns>
        private static WorkItem AssignWorkItemFromCandidate(WorkItem candidate)
        {
            //(SD) This direct return should only happen when candidate is suitable
            // for processing. For this implementation there are no checks
            return candidate;
        }

        #endregion
    }

    #endregion
}