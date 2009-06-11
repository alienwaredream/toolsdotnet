using System;
using System.Diagnostics;
using System.Transactions;
using Tools.Coordination.ProducerConsumer;
using Tools.Coordination.WorkItems;
using Tools.Core.Context;
using Tools.Core.Utils;
using Tools.Failover;
using Tools.Logging;
using Tools.Processes.Core;
using TIBCO.EMS;
using Tools.Core.Configuration;
using Tools.Core.Asserts;
using System.Text;

namespace Tools.Coordination.Ems
{
    #region EmsReader class

    /// <summary>
    /// Summary description for EmsReader.
    /// </summary>
    public class EmsReader : Producer
    {
        private WorkItem workItemCandidate;

        EmsReaderQueue queue;

        #region Constructors

        private EmsReader()
        {

        }

        public EmsReader(IFailureExceptionHandler messageFailureExceptionHandler, EmsReaderQueue queue)
            : base(messageFailureExceptionHandler)
        {
            this.queue = queue;
        }

        #endregion

        #region Functions

        public override void Stop()
        {
            // Close implementation never throws
            queue.Close();

            this.SetExecutionState(ProcessExecutionState.StopRequested);

            base.Stop();

            this.SetExecutionState(ProcessExecutionState.Stopped);
        }

        public override WorkItem GetNextWorkItem(WorkItemSlotCollection slots)
        {
            //TODO: This will be working on one thread only so skipping the synchronization here
            Trace.CorrelationManager.ActivityId = Guid.NewGuid();

            try
            {
                queue.Open();
            }
            catch (Exception ex)
            {
                if (!queue.RecoverFromConnectionError(ex))
                {
                    Stop();
                }

                return null;
            }


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
                    WorkItem workItem = GetWorkItemFromQueue(PriorityScope);

                    if (workItem != null)
                    {
                        Log.TraceData(Log.Source, System.Diagnostics.TraceEventType.Verbose,
                            ProducerMessage.MessageRetrieved,
                            new ContextualLogEntry
                            {
                                Message = "Got a new work item" + workItem,
                                ContextIdentifier = new ContextIdentifier()
                            });

                        return workItem;
                    }
                }
                catch (Exception ex)
                {

                    // Interrupt should not be a problem
                    CancelPrioritySlotReservation(PriorityScope);

                    Log.TraceData(Log.Source, System.Diagnostics.TraceEventType.Error,
                        ProducerMessage.RetrieveMessageFailed,
                        new ContextualLogEntry
                        {
                            Message = "Error during getting work item." + ex,
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
        /// <param name="priority"></param>
        /// <returns>Retrieved work item or null if not found, didn't pass pre-check, etc.</returns>
        private WorkItem GetWorkItemFromQueue(SubmissionPriority priority)
        {
            //TODO: Pay great attention here, now workItemCandidate is an instance field!!! (SD)
            workItemCandidate = null;
            WorkItem workItem = null;
            TextMessage message = null;
            Message msgTest = null;

            #region Get message from queue

            //var transaction = new CommittableTransaction();

            try
            {
                #region Get next job from the queue

                //var 

                //using (DependentTransaction dependentTransaction =
                //    transaction.DependentClone(DependentCloneOption.BlockCommitUntilComplete))
                //{
                //    using (var scope = new TransactionScope(dependentTransaction))
                //    {
                //TODO: (SD) Provide timeout option
                msgTest = queue.ReadNext();

                message = msgTest as TextMessage;

                //    scope.Complete();
                //}

                //dependentTransaction.Complete();

                //    if (message == null)
                //    {
                //        // if token is equal to null then commit here, as 
                //        // consumer will not get to the item anyway.
                //        //if (transaction.TransactionInformation.Status == TransactionStatus.Active)
                //        //    transaction.Commit();
                //    }
                //}

                #endregion

                #region If job is not null create a work item for it

                if (message != null)
                {
                    //utf-8 is a default encoding for ems
                    workItemCandidate = new EmsWorkItem(0, 0, WorkItemState.AvailableForProcessing,
                        SubmissionPriority.Normal, Encoding.UTF8.GetBytes(message.Text), false, false, this.Name,
                        new ContextIdentifier { InternalId = 0, ExternalReference = message.CorrelationID, ExternalId = message.MessageID, ContextGuid = Trace.CorrelationManager.ActivityId },
                        queue, message)
                        {
                            //Transaction = transaction,
                            RetrievedAt = DateTime.Now
                        };

                    //Trace.CorrelationManager.ActivityId = .ContextUid;
                    Log.Source.TraceEvent(TraceEventType.Start, 0, "Received: " + message.CorrelationID);

                    // Set transaction on the work item
                }

                #endregion

                this.FailureExceptionHandler.ResetState();
            }
            catch (Exception ex)
            {
                // Cleanup will never throw.
                if (ex is EMSException) queue.Close();

                try
                {
                    queue.Rollback();
                    // Rollback the commitable transaction
                    //transaction.Rollback(ex);
                }
                finally
                {
                    //transaction.Dispose();
                }

                Log.TraceData(Log.Source,
                    TraceEventType.Error,
                    ProducerMessage.ErrorDuringObtainingTheWorkItem,
                    new ContextualLogEntry
                    {
                        Message =
                            "Exception happened when trying to get item from the message queue (" +
                            queue.ServerConfig.Url + "). " + Environment.NewLine + ex,
                        ContextIdentifier = ((workItemCandidate != null) ? workItemCandidate.ContextIdentifier : new ContextIdentifier())
                    });

                // To review what is the required handling here for us (SD)
                if (ExecutionState == ProcessExecutionState.Running)
                {
                    FailureExceptionType type = this.FailureExceptionHandler.HandleFailure(ex);
                    if (type == FailureExceptionType.NonRecoverable)
                        this.Stop();
                }
                else
                {
                    if (workItemCandidate != null)
                    {
                        Log.TraceData(Log.Source, System.Diagnostics.TraceEventType.Information,
                            ProducerMessage.RetrievedMessageReturnedToTheRetrievalQueue,
                            new ContextualLogEntry
                            {
                                Message = string.Format(
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
            }

            #endregion Get message from queue

            #region Pre-processing checks

            // Convert queue message to work item
            // In case sql broker no need to do (SD)
            if (workItemCandidate != null)
            {
                #region WorkItem Diagnostics

                workItemCandidate.AttachNote("Received from the " + priority + " Queue ");

                #endregion WorkItem Diagnostics

                // TODO: //**SD1 - Provide the check for 

                // No checks done now, see the DB driven implementation for the checks samples (SD)
                // It means as well that we can simply assign item candidate to be our work item
                workItem = workItemCandidate;

                // TODO: (SD) Message body will be the xml retrieved from the sql broker
                workItem.MessageBody = Encoding.UTF8.GetBytes(message.Text);
                //**message.GetObjectProperty
            }

            #endregion Pre-processing checks

            // Return retrieved work item (or null)
            return workItem;
        }


        #endregion
    }

    #endregion
}