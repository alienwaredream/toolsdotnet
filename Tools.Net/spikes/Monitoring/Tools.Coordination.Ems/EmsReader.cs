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

namespace Tools.Coordination.Ems
{
    #region EmsReader class

    /// <summary>
    /// Summary description for EmsReader.
    /// </summary>
    public class EmsReader : Producer
    {
        private WorkItem workItemCandidate;

        private SessionConfiguration SessionConfig { get; set; }
        private ServerConfiguration ServerConfig { get; set; }
        private EMSQueueConfiguration QueueConfig { get; set; }

        private ConnectionFactory factory;
        private Connection connection;
        private Session session;
        private MessageConsumer consumer;
        private Destination destination;


        public PerformanceEventHandler PerformanceHandler { get; set; }

        #region Constructors

        public EmsReader(IFailureExceptionHandler failureExceptionHandler)
            : base(failureExceptionHandler)
        {
            PerformanceHandler = new PerformanceEventHandler(
               new PerformanceEventHandlerConfiguration
               {
                   Counters =
                       new System.Collections.Generic.List<PerfomanceCounterConfiguration>{
                        new PerfomanceCounterConfiguration{
                            Name="Dispatched Items/sec", 
                            ClearOnStart = true, 
                            CounterType = System.Diagnostics.PerformanceCounterType.RateOfCountsPerSecond32, 
                            EventId = "Dispatched Items/sec",
                            Description = "Number of reads from the queue per second"},
                    },
                   CategoryName = "Tools.Coordination.Ems",
                   Description = "Tools.Coordination.Ems load performance counters.",
                   EnableSetupOnInitialization = false,
                   MachineName = ".",
                   Name = ""
               }) { Enabled = true };
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
                            new ContextualLogEntry {
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
                        new ContextualLogEntry {
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
            Message message = null;

            Trace.CorrelationManager.ActivityId = Guid.NewGuid();


            #region Get message from queue

            var transaction = new CommittableTransaction();
            try
            {
                #region Get next job from the DB

                //var 

                using (DependentTransaction dependentTransaction =
                    transaction.DependentClone(DependentCloneOption.BlockCommitUntilComplete))
                {
                    using (var scope = new TransactionScope(dependentTransaction))
                    {
                        //TODO: (SD) Provide timeout option
                        message = consumer.Receive();
                        scope.Complete();
                    }

                    dependentTransaction.Complete();

                    if (message == null)
                    {
                        // if token is equal to null then commit here, as 
                        // consumer will not get to the item anyway.
                        if (transaction.TransactionInformation.Status == TransactionStatus.Active)
                            transaction.Commit();
                    }
                }

                #endregion

                #region If job is not null create a work item for it

                if (message != null)
                {
                    PerformanceHandler.HandleEvent("Items/sec", 1);

                    workItemCandidate = new RequestWorkItem(0, 0, WorkItemState.AvailableForProcessing,
                        SubmissionPriority.Normal, null, false, false, "test",
                        new ContextIdentifier { InternalId = 0, ExternalReference = message.CorrelationID, ExternalId = message.MessageID })
                        {
                            Transaction = transaction,
                            RetrievedAt = DateTime.Now
                        };

                    //**Trace.CorrelationManager.ActivityId = .ContextUid;
                    Log.Source.TraceEvent(TraceEventType.Start, 0, "Received the item " + message.CorrelationID);

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
                    new ContextualLogEntry {
                        Message =
                            "Exception happened when trying to get item from the message queue (" +
                            this.ServerConfig.Url + "). " + Environment.NewLine + ex,
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
                                Message = string.Format (
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
                workItem.MessageBody = message.CorrelationIDAsBytes;
                    //**message.GetObjectProperty
            }

            #endregion Pre-processing checks

            // Return retrieved work item (or null)
            return workItem;
        }


        private void Connect()
        {
            try
            {
                factory = new ConnectionFactory(this.ServerConfig.Url, this.ServerConfig.ClientId);
            }
            catch (EMSException e)
            {
                Log.TraceData(Log.Source, TraceEventType.Error, 15000, "URL/Client ID is wrong. " + e.ToString());
                throw;
            }

            IConfigurationValueProvider configProvider = new SingleTagSectionConfigurationProvider(this.ServerConfig.AuthenticationSectionName);

            try
            {
                connection = factory.CreateConnection(configProvider["userName"], configProvider["password"]);
            }
            catch (EMSException e)
            {
                Log.TraceData(Log.Source, TraceEventType.Error, 15001, "Username/Password is wrong. " + e.ToString());
                throw;
            }

            try
            {
                session = connection.CreateSession(this.SessionConfig.IsTransactional, SessionConfig.Mode);
            }
            catch (EMSException e)
            {
                Log.TraceData(Log.Source, TraceEventType.Error, 15002, "Error during session creation. " + e.ToString());
                throw;
            }

            try
            {
                destination =
                    CreateDestination(session, QueueConfig.Name, QueueConfig.Type);

                consumer =
                    session.CreateConsumer(destination, QueueConfig.MessageSelector,
                                           QueueConfig.NoLocal);

                connection.Start();
            }
            catch (EMSException e)
            {
                Log.TraceData(Log.Source, TraceEventType.Error, 15003, "Initialization error. " + e);
                throw;
            }
        }
        private static Destination CreateDestination(Session sess, string name, QueueType type)
        {
            Destination dest;
            switch (type)
            {
                case QueueType.Queue:
                    dest = sess.CreateQueue(name);
                    break;
                case QueueType.Topic:
                    dest = sess.CreateTopic(name);
                    break;
                default:
                    throw new ApplicationException("Internal error");
            }
            return dest;
        }

        //private void MessageReceiveOrTimeoutCallback
        //    (
        //    object state,
        //    bool timedOut
        //    )
        //{
        //    try
        //    {
        //        if (!timedOut)
        //        {
        //            IAsyncResult ar = (IAsyncResult)state;

        //            if (ar.IsCompleted)
        //            {
        //                //workItemCandidate = queue.EndReceive(ar);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //MessageQueue.ClearConnectionCache();

        //        Log.Source.TraceData(System.Diagnostics.TraceEventType.Error,
        //            ProducerMessage.ErrorDuringObtainingTheWorkItem,
        //            new ContextualLogEntry
        //            {
        //                Message =
        //                    "MessageReceiveOrTimeoutCallback: Exception happened when trying to get item from the message queue (" +
        //                    "" + "). " + Environment.NewLine + ex.ToString()
        //                ,
        //                ContextIdentifier =
        //                ((workItemCandidate != null) ? workItemCandidate.ContextIdentifier :
        //                new ContextIdentifier())
        //            });
        //    }
        //    finally
        //    {
        //        // The only case that can break this is thread being aborted here, but
        //        // that should not happen under any normal circumstances except service panic
        //        // stop (SD).
        //        OperationReset.Set();
        //    }
        //}

        #endregion
    }

    #endregion
}