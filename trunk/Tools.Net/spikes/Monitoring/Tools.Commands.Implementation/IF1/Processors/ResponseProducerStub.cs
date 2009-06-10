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

namespace Tools.Commands.Implementation.IF1.Processors
{
    #region EmsReader class

    /// <summary>
    /// Summary description for EmsReader.
    /// </summary>
    public class ResponseProducerStub : Producer
    {
        private WorkItem workItemCandidate;

        int IdSequence = 0;

        #region Constructors

        private ResponseProducerStub(IFailureExceptionHandler exHandler)
            : base(exHandler)
        {

        }

        #endregion

        #region Functions


        public override WorkItem GetNextWorkItem(WorkItemSlotCollection slots)
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
            Tools.Commands.Implementation.IF1.req item = null;

            Trace.CorrelationManager.ActivityId = Guid.NewGuid();


            #region Get message from queue

            var transaction = new CommittableTransaction();

            try
            {
                #region Get next job from the queue

                //var 

                using (DependentTransaction dependentTransaction =
                    transaction.DependentClone(DependentCloneOption.BlockCommitUntilComplete))
                {
                    using (var scope = new TransactionScope(dependentTransaction))
                    {
                        //TODO: (SD) Provide timeout option
                        item = new Tools.Commands.Implementation.IF1.req
                        {
                            reqId = (++IdSequence).ToString(),
                            processingStatus = "P",
                            errorDesc = "ok",
                            returnValue = "ok",
                            updateMechanism = "JMS"
                        }; 
                        
                        scope.Complete();
                    }

                    dependentTransaction.Complete();

                    if (item == null)
                    {
                        // if token is equal to null then commit here, as 
                        // consumer will not get to the item anyway.
                        if (transaction.TransactionInformation.Status == TransactionStatus.Active)
                            transaction.Commit();
                    }
                }

                #endregion

                #region If job is not null create a work item for it

                if (item != null)
                {
                    workItemCandidate = new RequestWorkItem(0, 0, WorkItemState.AvailableForProcessing,
                        SubmissionPriority.Normal, Encoding.UTF8.GetBytes(SerializationUtility.Serialize2String(item)), false, false, this.Name,
                        new ContextIdentifier { InternalId = 0, ExternalReference = item.ToString(), ExternalId = item.ToString() })
                        {
                            Transaction = transaction,
                            RetrievedAt = DateTime.Now
                        };

                    //**Trace.CorrelationManager.ActivityId = .ContextUid;
                    Log.Source.TraceEvent(TraceEventType.Start, 0, "Received the item " + item);

                    // Set transaction on the work item
                }

                #endregion

                this.FailureExceptionHandler.ResetState();
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

                Log.TraceData(Log.Source,
                    TraceEventType.Error,
                    ProducerMessage.ErrorDuringObtainingTheWorkItem,
                    new ContextualLogEntry
                    {
                        Message =
                            "Exception happened when trying to get item " + Environment.NewLine + ex,
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