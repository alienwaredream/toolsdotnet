using System;
using System.Diagnostics;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using Tools.Coordination.ProducerConsumer;
using Tools.Core.Asserts;
using Tools.Core.Context;
using Tools.Processes.Core;
using Tools.Coordination.Core;
using Tools.Coordination.WorkItems;
using Spring.Context.Support;
using System.Configuration;

namespace Tools.Coordination.ProducerConsumer
{
    /// <summary>
    /// Summary description for TowardsQueueWorkItemsConsumer.
    /// </summary>
    public abstract class JobConsumer<JobType> :
        Consumer, IResultHandler
        where JobType : class
    {
        #region Fields

        //

        protected bool responseReceivedHandlerAssigned;

        //private SubmissionType submissionType =
        //    SubmissionType.RegularSubmission;

        public event JobCompletedEventHandler RegularResultObtained;
        public event JobCompletedEventHandler PendingResultObtained;



        #endregion

        #region Constructors

        protected JobConsumer()
        {

        }

        protected JobConsumer(ProcessingStateData stateData): this()
        {
            ErrorTrap.AddRaisableAssertion<ArgumentNullException>(stateData != null, "stateData != null.");
            StateData = stateData;
        }

        #endregion

        #region Properties



        protected IJobProcessor<JobType> JobProcessor { get; set; }

        protected ProcessingStateData StateData { get; set; }
        protected WorkItemSlotCollection RetrievedItems { get { return StateData.RetrievedItems; } }

        protected WorkItemCollection SubmittedItems
        {
            get
            {
                return StateData.SubmittedItems;
            }
        }

        #endregion

        #region Methods

        public override void Stop()
        {
            JobProcessor.Dispose();

            base.Stop();
        }

        protected override void StartInternal()
        {
            SubmissionStatus submissionStatus = SubmissionStatus.Starting;
            try
            {
                #region Log

                Log.TraceData(Log.Source,TraceEventType.Start,
                                     JobConsumerMessage.
                                         QueueWorkItemsConsumerStarted,
                                     new ContextualLogEntry
                                         {
                                             Message =
                                                 string.Format
                                                 (
                                                 "{0}: Started consuming transaction requests.",
                                                 Name
                                                 ),
                                             ContextIdentifier = ContextIdentifier
                                         });

                #endregion Log

                ErrorTrap.AddRaisableAssertion<ConfigurationErrorsException>(StateData != null,
                                                                             "StateData != null. Configure StateData!");

                //JobDb jobDB = null;

                WorkItem workItem = null;
                bool jobPreCheckFlag = false;
                JobType job = default(JobType);


                while (true)
                {
                    #region Submission Loop

                    try
                    {
                        submissionStatus = SubmissionStatus.Retrieving;

                        if (ExecutionState != ProcessExecutionState.Running)
                        {
                            // No save action needed
                            return;
                        }

                        workItem = null;

                        ErrorTrap.Reset();


                        // *** UC1[7] - Retrieve transaction from the queue

                        #region Obtaining QueueWorkItem from the RetrievalItems Collection

                        lock (RetrievedItems)
                        {
                            if (ExecutionState != ProcessExecutionState.Running)
                            {
                                // No save action needed
                                return;
                            }

                            workItem = RetrievedItems.GetTopWorkItem();

                            while (workItem == null)
                            {
                                if (ExecutionState != ProcessExecutionState.Running)
                                {
                                    // No save action needed
                                    return;
                                }
                                Waiting = true;
                                Monitor.Wait(RetrievedItems);
                                Waiting = false;

                                workItem = RetrievedItems.GetTopWorkItem();
                            }

                            submissionStatus = SubmissionStatus.Retrieved;
                        }
                        Trace.CorrelationManager.ActivityId = workItem.ContextIdentifier.ContextGuid;

                        OnWorkItemRetrieved(workItem);

                        lock (RetrievedItems.Counters[workItem.SubmissionPriority])
                        {
                            if (ExecutionState == ProcessExecutionState.Running)
                            {
                                Monitor.Pulse(RetrievedItems.Counters[workItem.SubmissionPriority]);
                            }
                        }

                        #endregion Obtaining QueueWorkItem

                        #region Checking type of QueueWorkItem

                        submissionStatus = SubmissionStatus.CheckingItemType;

                        job = GetWorkItemBody(workItem);

                        if (job == null)
                        {
                            submissionStatus = SubmissionStatus.InvalidItemType;

                            Log.TraceData(Log.Source,TraceEventType.Error,
                                                 JobConsumerMessage.InvalidMessageType,
                                                 new ContextualLogEntry
                                                     {
                                                         Message =
                                                             string.Format
                                                             (
                                                             "{0}: Invalid type of retrieved message(Id = {1})." +
                                                             " Obtained type: {2}." +
                                                             "Process will continue with retrieving next message.",
                                                             Name,
                                                             workItem.ContextIdentifier,
                                                             job == null
                                                                 ?
                                                                     "null"
                                                                 : job.GetType().FullName
                                                             ),
                                                         ContextIdentifier = workItem.ContextIdentifier
                                                     });

                            continue;
                        }

                        #endregion Checking type of QueueWorkItem

                        #region Template method for descendants to check/set job state

                        submissionStatus = SubmissionStatus.PreHandlingJob;

                        VerificationResult handleResult = VerifyItemAfterRetrieval(workItem, job);

                        submissionStatus = SubmissionStatus.JobPreHandled;

                        jobPreCheckFlag = handleResult.PassedSuccessfuly;

                        if (!handleResult.PassedSuccessfuly)
                        {
                            submissionStatus =
                                SubmissionStatus.JobPreHandledAndLoggingUnsuccess;

                            #region Log

                            Log.TraceData(Log.Source,TraceEventType.Warning,
                                                 JobConsumerMessage.
                                                     PreHandleNotSuccessful,
                                                 new ContextualLogEntry
                                                     {
                                                         Message =
                                                             string.Format
                                                             (
                                                             "{0}: job PreHandle phase is not Successful." +
                                                             " WorkItem under scope will be discarded. Prehandle message is: {1}",
                                                             Name,
                                                             handleResult.Message
                                                             ),
                                                         ContextIdentifier = workItem.ContextIdentifier
                                                     });

                            #endregion Log

                            submissionStatus =
                                SubmissionStatus.JobPreHandledAndSendingToFailedQueue;
                            //**SD1 - Instead change the state of workItem to cancelled and persist it.
                            workItem.WorkItemState = WorkItemState.Cancelled;
                            //**SD1 - handle updateResult!!!
                            //WorkItemUpdateStateResult updateResult = WorkItemController.UpdateWorkItemState
                            //    (
                            //    workItem
                            //    );

                            continue;
                        }

                        #endregion Template method for descendants to check/set job state

                        #region Update jobTransaction status

                        submissionStatus =
                            SubmissionStatus.UpdatingJobProcessingStatus;

                        // Instantiating helper db object.
                        // If db is not available first sql exception in the chain will be raised here.
                        // Again, if icontext happened to be pre-cached before this moment sql exception
                        // will happen to the next statement not this one (SD)
                        //jobDB =
                        //    new JobDb
                        //    (
                        //         ConfigurationManager.ConnectionStrings["OperationConnectionString"].
                        //         ConnectionString
                        //    );
                        // This is the moment when sql exception will happen in db
                        // unavailability case either for pre-cached connection string case or not (SD).
                        // TODO: Check how is this handled after the change with HandleLoopException (SD) !!!
                        // Update jobTransaction.Status to SubmittedTo.
                        //jobDB.UpdateBatchStatus
                        //    (
                        //    job.OperationContext.ContextIdentifier.InternalId,
                        //    0
                        //    );

                        if (ExecutionState != ProcessExecutionState.Running)
                        {
                            throw new InconsistentStateException();
                        }

                        // *** UC1[8] - Submit transaction to the job
                        // *** UC9[10] - Submit transaction to the job

                        #endregion Update jobTransaction status

                        #region Processing the message

                        ProcessJob
                            (
                            job,
                            workItem,
                            ref submissionStatus
                            );

                        #endregion Processing the message
                    }
                    catch (InconsistentStateException)
                    {
                        #region Log and handle gracefull StopInternal

                        Log.TraceData(Log.Source,TraceEventType.Verbose,
                                             JobConsumerMessage.
                                                 UnexpectedSubmissionStatus,
                                             new ContextualLogEntry
                                                 {
                                                     Message =
                                                         string.Format
                                                         (
                                                         "{0}: InconsistentStateException thrown." +
                                                         "SubmissionStatus = {1}",
                                                         Name,
                                                         submissionStatus
                                                         ),
                                                     ContextIdentifier =
                                                         ((workItem == null)
                                                              ? ContextIdentifier
                                                              : workItem.ContextIdentifier)
                                                 });

                        HandleGracefulStop
                            (
                            submissionStatus,
                            workItem,
                            job,
                            jobPreCheckFlag
                            );

                        #endregion Log and handle gracefull StopInternal

                        break;
                    }
                    catch (Exception ex)
                    {
                        if (IsInterruptExceptionPresent(ex))
                        {
                            #region Log and handle gracefull StopInternal

                            Log.TraceData(Log.Source,TraceEventType.Stop,
                                                 JobConsumerMessage.ThreadInterrupted,
                                                 new ContextualLogEntry
                                                     {
                                                         Message =
                                                             string.Format
                                                             (
                                                             "{0}: ThreadInterruptedException thrown in the execution chain." +
                                                             "SubmissionStatus = {1}" + ex,
                                                             Name,
                                                             submissionStatus
                                                             ),
                                                         ContextIdentifier =
                                                             ((workItem == null)
                                                                  ? ContextIdentifier
                                                                  : workItem.ContextIdentifier)
                                                     });

                            HandleGracefulStop
                                (
                                submissionStatus,
                                workItem,
                                job,
                                jobPreCheckFlag
                                );

                            #endregion Log and handle gracefull StopInternal

                            break;
                        }
                        if (IsAbortExceptionPresent(ex))
                        {
                            //PanicShutdown

                            #region Log

                            Log.TraceData(Log.Source,TraceEventType.Stop,
                                                 JobConsumerMessage.AbortRequested,
                                                 new ContextualLogEntry
                                                     {
                                                         Message =
                                                             string.Format
                                                             (
                                                             "{0}: ThreadAbortException thrown in the execution chain." +
                                                             "SubmissionStatus = {1}" + ex,
                                                             Name,
                                                             Name,
                                                             submissionStatus
                                                             ),
                                                         ContextIdentifier =
                                                             ((workItem == null)
                                                                  ? ContextIdentifier
                                                                  : workItem.ContextIdentifier)
                                                     });

                            #endregion Log

                            break;
                        }

                        #region Log and handle gracefull loop exception

                        Log.TraceData(Log.Source,TraceEventType.Error,
                                             JobConsumerMessage.ErrorOccuredInConsumerLoop,
                                             new ContextualLogEntry
                                                 {
                                                     Message =
                                                         string.Format
                                                         (
                                                         "{0}: Error desc.:{1}.",
                                                         Name,
                                                         ex
                                                         ),
                                                     ContextIdentifier =
                                                         ((workItem == null)
                                                              ? ContextIdentifier
                                                              : workItem.ContextIdentifier)
                                                 });

                        HandleLoopException
                            (
                            submissionStatus,
                            workItem,
                            job,
                            jobPreCheckFlag
                            );

                        #endregion Log and handle gracefull loop exception
                    }

                    #endregion Submission Loop
                }
            }
            catch (ThreadInterruptedException)
            {
                Log.TraceData(Log.Source,TraceEventType.Stop,
                                     JobConsumerMessage.ThreadInterrupted,
                                     new ContextualLogEntry
                                         {
                                             Message =
                                                 string.Format
                                                 (
                                                 "{0}: ThreadInterruptedException thrown." +
                                                 "SubmissionStatus = {1}",
                                                 Name,
                                                 submissionStatus
                                                 ),
                                             ContextIdentifier = ContextIdentifier
                                         });
            }
            catch (ThreadAbortException)
            {
                Log.TraceData(Log.Source,TraceEventType.Stop,
                                     JobConsumerMessage.
                                         AbortRequested,
                                     new ContextualLogEntry
                                         {
                                             Message =
                                                 string.Format
                                                 (
                                                 "{0}:  ThreadAbortException thrown." +
                                                 "SubmissionStatus = {1}",
                                                 Name,
                                                 submissionStatus
                                                 ),
                                             ContextIdentifier = ContextIdentifier
                                         });
            }
            catch (Exception ex)
            {
                Log.TraceData(Log.Source,TraceEventType.Error,
                                     JobConsumerMessage.ErrorOccuredInConsumer,
                                     new ContextualLogEntry
                                         {
                                             Message =
                                                 string.Format
                                                 (
                                                 "{0}: Error desc.:{1}.",
                                                 Name,
                                                 ex
                                                 ),
                                             ContextIdentifier = ContextIdentifier
                                         });
                //throw;
            }
        }

        protected virtual JobType GetWorkItemBody(WorkItem workItem)
        {
            return workItem.GetObjectFromMessageBody() as JobType;

        }

        protected void UpdateJobWithResponse
            (
            JobProcessedEventArgs e
            )
        {
        }

        /// <summary>
        /// Process a request message.
        /// </summary>
        /// The request message to be processed.
        /// </param>
        /// <returns>Response object of ResponseTransResult type</returns>
        protected void ProcessJob
            (
            JobType job,
            WorkItem workItem,
            ref SubmissionStatus submissionStatus
            )
        {
            ErrorTrap.AddRaisableAssertion<ConfigurationErrorsException>(JobProcessor != null,
                                                                         "JobProcessor != null. Configure the consumer's JobProcessor!");

            submissionStatus = SubmissionStatus.RestoringOperationContext;

            bool shouldSubmit = true;

            IAsyncResult ar;

            lock (SubmittedItems)
            {
                #region Wait until can continue with submission

                while
                    (
                    !(ConsumerManager.SubmittedItemsCounter.Value < Configuration.MaxTotalSubmittedItemsCount)
                    )
                {
                    Log.TraceData(Log.Source,TraceEventType.Verbose,
                                         JobConsumerMessage.
                                             CallingMonitorWaitOnSubmittedItems,
                                         new ContextualLogEntry
                                             {
                                                 Message =
                                                     string.Format
                                                     (
                                                     "{0}: sendRequest - calling Monitor.Wait " +
                                                     "on SubmittedItems.\nSubmittedItems.Count = {1}" +
                                                     "\nSubmittedItemsCounter.Value = {2}",
                                                     Name,
                                                     SubmittedItems.Count,
                                                     ConsumerManager.SubmittedItemsCounter.Value
                                                     ),
                                                 ContextIdentifier = (workItem == null)
                                                                         ? ContextIdentifier
                                                                         :
                                                                             workItem.ContextIdentifier
                                             });

                    Monitor.Wait(SubmittedItems);
                }

                #endregion Wait until can continue with submission

                //**SD1 - return to it ASAP
                //stateQueueWorkItem.SetToSubmittedToExternalRecipientState();

                #region Sending QueueWorkItem to SubmittedItems

                submissionStatus =
                    SubmissionStatus.AddingToSubmittedItems;

                int submittedItemsCount;

                if (ExecutionState != ProcessExecutionState.Running)
                {
                    throw new InconsistentStateException();
                }

                workItem.SubmittedToExternalRecipientAt = DateTime.UtcNow;

                SubmittedItems.Add(workItem);

                submissionStatus =
                    SubmissionStatus.AddedToSubmittedItems;

                submittedItemsCount = SubmittedItems.Count;
                // SD** - Look onto this ASAP!
                //						(workItem as StateQueueWorkItem).SyncStatus = 
                //							WorkItemProcessStatus.SubmittedToSubmittedItems;

                Log.TraceData(Log.Source,TraceEventType.Verbose,
                                     JobConsumerMessage.
                                         ItemAddedToSubmittedItems,
                                     new ContextualLogEntry
                                         {
                                             Message =
                                                 string.Format
                                                 (
                                                 "{0}: Recurrence(Id = {1}) added to SubmittedItems. " +
                                                 "SubmittedItems.Count = {2}. InternalId={3}",
                                                 Name,
                                                 workItem.ContextIdentifier,
                                                 submittedItemsCount,
                                                 workItem.ContextIdentifier.InternalId
                                                 ),
                                             ContextIdentifier = workItem.ContextIdentifier
                                         });

                #endregion Sending QueueWorkItem to SubmittedItems

                ConsumerManager.SubmittedItemsCounter.Increment();

                /*
                                submissionStatus = SubmissionStatus.SubmittedItemsCounterIncremented;
                */

                #region Log

                Log.TraceData(Log.Source,TraceEventType.Verbose,
                                     JobConsumerMessage.SubmittingMessageToTheSender,
                                     new ContextualLogEntry
                                         {
                                             Message =
                                                 string.Format
                                                 (
                                                 "{0} - Submitting message to the JobProcessor (Name={3}, Type={4}) asynchronously." +
                                                 "\nSubmittedItems.Count = {1}, ConsumerManager.SubmittedItemsCounter.Value = {2}. OperationContextShortcut: \r\n {5}",
                                                 Name,
                                                 SubmittedItems.Count,
                                                 ConsumerManager.SubmittedItemsCounter.Value,
                                                 JobProcessor.Name,
                                                 JobProcessor.GetType().FullName,
                                                 String.Empty
                                                 ),
                                             ContextIdentifier = workItem.ContextIdentifier
                                         });

                #endregion Log
            }
            SubmitJobCallbackDelegate<JobType> del =
                JobProcessor.ProcessJobWithEventCallback;


            #region submission

            ar =
                del.BeginInvoke
                    (
                    job,
                    workItem,
                    Sender_ResponseReceived,
                    Sender_SubmittingRequest,
                    SubmitTJobCallback,
                    workItem
                    );

            submissionStatus = SubmissionStatus.SubmittedToSender;

            if (
                !ar.AsyncWaitHandle.WaitOne
                     (
                     Configuration.SubmissionQueuingProcessTimeout,
                     false
                     )
                )
            {
                submissionStatus = SubmissionStatus.LoggingSubmissionTimeOut;

                #region Log

                Log.TraceData(Log.Source,TraceEventType.Warning,
                                     JobConsumerMessage.SubmittingMessageToTheSenderTimedOut,
                                     new ContextualLogEntry
                                         {
                                             Message =
                                                 string.Format
                                                 (
                                                 "{0}: sendRequest - Sending message to the JobProcessor \"{3}\"" +
                                                 " has timed out.\n" +
                                                 "\nSubmittedItems.Count = {1}, ConsumerManager.SubmittedItemsCounter.Value = {2}",
                                                 Name,
                                                 SubmittedItems.Count,
                                                 ConsumerManager.SubmittedItemsCounter.Value,
                                                 JobProcessor.Name
                                                 ),
                                             ContextIdentifier = workItem.ContextIdentifier
                                         });

                #endregion Log
            }

            #endregion submission

            submissionStatus = SubmissionStatus.SubmissionCompleted;
        }
        // made protected only in order to access via test accessor (SD)
        protected void Sender_SubmittingRequest(object jobProcessor, SubmittingJobEventArgs e)
        {
            if (ExecutionState != ProcessExecutionState.Running)
            {
                e.Cancel = true;
                return;
            }
            if (!e.Retry)
            {
                #region Update WorkItem

                try
                {

                }
                catch (Exception ex)
                {
                    #region Log

                    Log.TraceData(Log.Source,TraceEventType.Error,
                                         JobConsumerMessage.ErrorOccuredInSubmittingDelegate,
                                         new ContextualLogEntry
                                             {
                                                 Message =
                                                     string.Format
                                                     (
                                                     "{0}: Error encountered while updating work item status to Submitted. Message will be returned to the Queue. "
                                                     + ex,
                                                     Name
                                                     ),
                                                 ContextIdentifier = e.WorkItem.ContextIdentifier
                                             });

                    #endregion Log

                    e.Cancel = true;

                    ReturnToRetrievalQueue(e.WorkItem);
                }

                #endregion Update WorkItem
            }
        }
        // Protected only in the order to make accessible for tests
        protected void SubmitTJobCallback(IAsyncResult ar)
        {
            //TODO: (SD) Provide better handling (show the type if error trapped)
            ErrorTrap.AddRaisableAssertion<ArgumentException>((ar as AsyncResult) != null &&
                                                              (ar as AsyncResult).AsyncDelegate as
                                                              SubmitJobCallbackDelegate<JobType> != null,
                                                              "AsyncDelegate of IAsyncResult implementation is expected to be of type SubmitJobCallbackDelegate<JobType>");
            // restore the delegate
            // ReSharper disable PossibleNullReferenceException - checked by ErrorTrap
            var del = (ar as AsyncResult).AsyncDelegate as SubmitJobCallbackDelegate<JobType>;
            // ReSharper restore PossibleNullReferenceException

            // restore the state
            ErrorTrap.AddRaisableAssertion<ArgumentException>((ar) != null &&
                                                              ar.AsyncState as WorkItem != null,
                                                              "IAsyncResult implementation instance parameter can't be null and its ar.AsyncState should be of type WorkItem, but provided type is " + ((ar != null && ar.AsyncState != null) ? ar.AsyncState.GetType().FullName : "type of null"));
            // ReSharper disable PossibleNullReferenceException - checked by ErrorTrap
            var state = ar.AsyncState as WorkItem;
            // ReSharper restore PossibleNullReferenceException
            try
            {
                del.EndInvoke(ar);
            }
            catch (Exception ex)
            {
                //if (state.WorkItem.Transaction != null && state.WorkItem.Transaction.TransactionInformation.Status == TransactionStatus.Active)
                //{
                //    state.WorkItem.Transaction.Rollback(ex);
                //}
                Log.TraceData(Log.Source,TraceEventType.Error,
                                     JobConsumerMessage.ErrorInSenderAsyncCall,
                                     new ContextualLogEntry
                                         {
                                             Message =
                                                 string.Format
                                                 (
                                                 "{0}: An error occured in async call of JobProcessor. " +
                                                 "Error desc.: {1}",
                                                 Name,
                                                 ex
                                                 ),
                                             ContextIdentifier = state.ContextIdentifier
                                         });
            }
            finally
            {
                //TODO: (SD) re-analyze this idea, because having Pulse only in the
                // response_received makes the contract vague.
                //lock (SubmittedItems)
                //{
                //    // if we are called back, decrement the counter
                //    ConsumerManager.SubmittedItemsCounter.Decrement();
                //    Monitor.Pulse(SubmittedItems);
                //}
            }
        }

        protected void FireRegularResponseObtained
            (
            ContextIdentifier operationContextShortcut
            )
        {
            if (RegularResultObtained != null)
            {
                lock (RegularResultObtained)
                {
                    RegularResultObtained
                        (
                        this,
                        new JobProcessedEventArgs { OperationContextShortcut = operationContextShortcut }
                        );
                }
            }
        }

        protected void FirePendingResponseObtained
            (
            ContextIdentifier operationContextShortcut
            )
        {
            if (PendingResultObtained != null)
            {
                lock (PendingResultObtained)
                {
                    PendingResultObtained
                        (
                        this,
                        new JobProcessedEventArgs { OperationContextShortcut = operationContextShortcut }
                        );
                }
            }
        }

        //TODO:**SD4** No need in too params will be refactored (SD)
        protected virtual VerificationResult VerifyItemAfterRetrieval
            (
            WorkItem item,
            JobType job
            )
        {
            #region Pre handle the work item to mitigate the effect of state changes between production and consuming

            // TODO: (SD) Subject to reintroduce a real prehandle

            return new VerificationResult { PassedSuccessfuly = true, GenerateNotification = false, Message = null };

            #endregion
        }

        #region SEH handlers

        // Only made protected in order to made available to test accessor
        protected void HandleGracefulStop
            (
            SubmissionStatus submissionStatus,
            WorkItem workItem,
            JobType job,
            bool jobPreCheckFlag
            )
        {
            //**SD - get back here ASAP!

            #region Commented out

            switch (submissionStatus)
            {
                case SubmissionStatus.Retrieving:
                    {
                        if (workItem != null)
                        {
                            ReturnToRetrievalQueue(workItem);
                        }
                        break;
                    }
                case SubmissionStatus.Retrieved:
                case SubmissionStatus.CheckingItemType:
                case SubmissionStatus.PreHandlingJob:
                case SubmissionStatus.AddingToSubmittedItems:
                    {
                        ReturnToRetrievalQueue(workItem);
                        break;
                    }
                case SubmissionStatus.RestoringOperationContext:
                    {
                        ReturnToRetrievalQueue(workItem);
                        lock (SubmittedItems)
                        {
                            SubmittedItems.Remove(workItem);
                            // Don't need to decrement because it is a StopInternal state
                            //ConsumerManager.SubmittedItemsCounter.Decrement();
                        }
                        break;
                    }
                case SubmissionStatus.InvalidItemType:
                    {
                        Log.TraceData(Log.Source,TraceEventType.Error,
                                             JobConsumerMessage.InvalidMessageType,
                                             new ContextualLogEntry
                                                 {
                                                     Message =
                                                         string.Format
                                                         (
                                                         "{0}: Invalid type of retrieved message(Id = {1})." +
                                                         " Obtained type: {2}." +
                                                         "Process will continue with retrieving next message.",
                                                         Name,
                                                         workItem.ContextIdentifier,
                                                         workItem.MessageBody == null
                                                             ?
                                                                 "null"
                                                             : workItem.MessageBody.GetType().FullName
                                                         ),
                                                     ContextIdentifier = workItem.ContextIdentifier
                                                 });

                        break;
                    }
                case SubmissionStatus.JobPreHandled:
                    {
                        if (jobPreCheckFlag)
                        {
                            ReturnToRetrievalQueue(workItem);
                        }
                        break;
                    }
                case SubmissionStatus.JobPreHandledAndLoggingUnsuccess:
                    {
                        Log.TraceData(Log.Source,TraceEventType.Verbose,
                                             JobConsumerMessage.PreHandleNotSuccessful,
                                             new ContextualLogEntry
                                                 {
                                                     Message =
                                                         string.Format
                                                         (
                                                         "{0}: PreHandle Not Successful.",
                                                         Name
                                                         ),
                                                     ContextIdentifier = workItem.ContextIdentifier
                                                 });

                        goto case SubmissionStatus.JobPreHandledAndSendingToFailedQueue;
                    }
                case SubmissionStatus.JobPreHandledAndSendingToFailedQueue:
                    {
                        break;
                    }
                case SubmissionStatus.JobPreHandledAndSentToFailedQueue:
                case SubmissionStatus.SubmissionCompleted:
                    {
                        break;
                    }
                case SubmissionStatus.AddedToSubmittedItems:
                case SubmissionStatus.UpdatingJobProcessingStatus:
                case SubmissionStatus.SubmittedItemsCounterIncremented:
                    {
                        ReturnToRetrievalQueue(workItem); //To review once again (SD)
                        break;
                    }
                case SubmissionStatus.JobSubmittedToSender:
                    {
                        Log.TraceData(Log.Source,TraceEventType.Warning,
                                             JobConsumerMessage.
                                                 StopRequestedAfterSubmissionToSender,
                                             new ContextualLogEntry
                                                 {
                                                     Message =
                                                         string.Format
                                                         (
                                                         "{0}: Stop requested after transaction had been " +
                                                         "submitted to job. Not able to verify submission state.",
                                                         Name
                                                         ),
                                                     ContextIdentifier = workItem.ContextIdentifier
                                                 });
                        break;
                    }
                case SubmissionStatus.LoggingSubmissionTimeOut:
                    {
                        Log.TraceData(Log.Source,TraceEventType.Error,
                                             JobConsumerMessage.
                                                 SubmittingMessageToTheSenderTimedOut,
                                             new ContextualLogEntry
                                                 {
                                                     Message =
                                                         string.Format
                                                         (
                                                         "{0}: sendRequest - Sending message to the JobProcessor" +
                                                         " has timed out.",
                                                         Name
                                                         ),
                                                     ContextIdentifier = workItem.ContextIdentifier
                                                 });

                        break;
                    }
                default:
                    {
                        Log.TraceData(Log.Source,TraceEventType.Error,
                                             JobConsumerMessage.
                                                 UnexpectedSubmissionStatus,
                                             new ContextualLogEntry
                                                 {
                                                     Message =
                                                         string.Format
                                                         (
                                                         "{0}: Unexpected submission status({1}) submitted" +
                                                         "to the handleRuntimeInconsistentStateException method",
                                                         Name,
                                                         submissionStatus
                                                         ),
                                                     ContextIdentifier = workItem.ContextIdentifier
                                                 });

                        break;
                    }
            }
            // We can't delete message here from the submittedQueue as we are
            // waiting for response till the last moment, and if response is pending 
            // job should be sent to the pending queue. Only if status is witinh the
            // check bellow we can remove it from the submitted queue as it has not
            // been submitted yet to the JobProcessor (SD).
            // The rest of items that reside in the 
            // submitted queue can be can be solved by last-moment checks run (SD).

            #endregion Commented out
        }


        // Only made protected to become available to the test accessor
        protected void HandleLoopException
            (
            SubmissionStatus submissionStatus,
            WorkItem workItem,
            JobType job,
            bool jobPreCheckFlag
            )
        {
            //**SD1 - Get back to it ASAP!

            #region Commented out

            switch (submissionStatus)
            {
                case SubmissionStatus.Retrieving:
                    {
                        if (workItem != null)
                        {
                            ReturnToRetrievalQueue(workItem);
                        }
                        break;
                    }
                case SubmissionStatus.Retrieved:
                case SubmissionStatus.CheckingItemType:
                case SubmissionStatus.PreHandlingJob:
                case SubmissionStatus.AddingToSubmittedItems:
                    {
                        ReturnToRetrievalQueue(workItem);
                        break;
                    }
                case SubmissionStatus.InvalidItemType:
                    {
                        Log.TraceData(Log.Source,TraceEventType.Error,
                                             JobConsumerMessage.InvalidMessageType,
                                             new ContextualLogEntry
                                                 {
                                                     Message =
                                                         string.Format
                                                         (
                                                         "{0}: Invalid type of retrieved message(Id = {1})." +
                                                         " Obtained type: {2}." +
                                                         "Process will continue with retrieving next message.",
                                                         Name,
                                                         workItem.ContextIdentifier,
                                                         workItem.MessageBody == null
                                                             ?
                                                                 "null"
                                                             : workItem.MessageBody.GetType().FullName
                                                         ),
                                                     ContextIdentifier = workItem.ContextIdentifier
                                                 });

                        break;
                    }
                case SubmissionStatus.JobPreHandled:
                    {
                        if (jobPreCheckFlag)
                        {
                            ReturnToRetrievalQueue(workItem);
                        }
                        else
                        {
                            //						SaveToSubmissionFailedQueue
                        }
                        break;
                    }
                case SubmissionStatus.JobPreHandledAndLoggingUnsuccess:
                    {
                        Log.TraceData(Log.Source,TraceEventType.Verbose,
                                             JobConsumerMessage.
                                                 PreHandleNotSuccessful,
                                             new ContextualLogEntry
                                                 {
                                                     Message =
                                                         string.Format
                                                         (
                                                         "{0}: PreHandle Not Successful.",
                                                         Name
                                                         ),
                                                     ContextIdentifier = workItem.ContextIdentifier
                                                 });

                        goto case SubmissionStatus.JobPreHandledAndSendingToFailedQueue;
                    }
                case SubmissionStatus.JobPreHandledAndSendingToFailedQueue:
                    {
                        //					SaveToSubmissionFailedQueue

                        break;
                    }
                case SubmissionStatus.JobPreHandledAndSentToFailedQueue:
                case SubmissionStatus.SubmissionCompleted:
                    {
                        break;
                    }
                case SubmissionStatus.AddedToSubmittedItems:
                case SubmissionStatus.UpdatingJobProcessingStatus:
                case SubmissionStatus.RestoringOperationContext:
                    {
                        ReturnToRetrievalQueue(workItem);
                        lock (SubmittedItems)
                        {
                            SubmittedItems.Remove(workItem);
                            ConsumerManager.SubmittedItemsCounter.Decrement();
                        }
                        break;
                    }
                case SubmissionStatus.SubmittedItemsCounterIncremented:
                    {
                        //					SaveToSubmissionFailedQueue

                        break;
                    }
                case SubmissionStatus.JobSubmittedToSender:
                    {
                        Log.TraceData(Log.Source,TraceEventType.Verbose,
                                             JobConsumerMessage.StopRequestedAfterSubmissionToSender,
                                             new ContextualLogEntry
                                                 {
                                                     Message =
                                                         string.Format
                                                         (
                                                         "{0}: Error occured after the transaction had been " +
                                                         "submitted to JobProcessor. Not able to verify submission state.",
                                                         Name
                                                         ),
                                                     ContextIdentifier = workItem.ContextIdentifier
                                                 });

                        //					SaveToSubmissionFailedQueue

                        break;
                    }
                case SubmissionStatus.LoggingSubmissionTimeOut:
                    {
                        Log.TraceData(Log.Source,TraceEventType.Error,
                                             JobConsumerMessage.SubmittingMessageToTheSenderTimedOut,
                                             new ContextualLogEntry
                                                 {
                                                     Message =
                                                         string.Format
                                                         (
                                                         "{0}: sendRequest - Sending message to the JobProcessor" +
                                                         " has timed out.",
                                                         Name
                                                         ),
                                                     ContextIdentifier = workItem.ContextIdentifier
                                                 });

                        //					SaveToSubmissionFailedQueue

                        break;
                    }
                default:
                    {
                        Log.TraceData(Log.Source,TraceEventType.Error,
                                             JobConsumerMessage.
                                                 UnexpectedSubmissionStatus,
                                             new ContextualLogEntry
                                                 {
                                                     Message =
                                                         string.Format
                                                         (
                                                         "{0}: Unexpected submission status({1}) submitted" +
                                                         "to the handleRuntimeInconsistentStateException method",
                                                         Name,
                                                         submissionStatus
                                                         ),
                                                     ContextIdentifier = workItem.ContextIdentifier
                                                 });

                        break;
                    }
            }

            #endregion Commented out
        }

        #endregion SEH handlers

        #endregion

        #region Handlers
        // Only made protected to enable accessibility to the test accessor (SD).
        protected void Sender_ResponseReceived
            (
            JobProcessedEventArgs e
            )
        {
            ResponseReceivedStatus responseStatus =
                ResponseReceivedStatus.ResponseObtained;

            WorkItem workItem = null;

            try
            {
                lock (SubmittedItems)
                {
                    // TODO: Change for indexer access with workItem reference (SD)
                    workItem = SubmittedItems.GetEntry(e.WorkItem.IdHash);

                    responseStatus = ResponseReceivedStatus.EntryObtainedFromSubmittedItems;

                    //responseStatus = ResponseReceivedStatus.StateQueueWorkItemCreated;

                    responseStatus = ResponseReceivedStatus.ObtainedEntryChecked;

                    if (workItem != null)
                    {
                        SubmittedItems.Remove(workItem);

                        responseStatus = ResponseReceivedStatus.ItemRemovedFromSubmittedItems;
                        // 
                        ConsumerManager.SubmittedItemsCounter.Decrement();

                        responseStatus = ResponseReceivedStatus.SubmittedItemsCounterDecremented;

                        #region Log

                        //if (InstrumentationManager.Level == InstrumentationLevel.Debug)
                        //{

                        Log.TraceData(Log.Source,TraceEventType.Verbose,
                                             JobConsumerMessage.ResponseReceivedFromTheSender,
                                             new ContextualLogEntry
                                                 {
                                                     Message =
                                                         string.Format
                                                         (
                                                         "{0}: sender_ResponseReceived - Recurrence(Id = {1}) removed " +
                                                         "from SubmittedItems.\nSubmittedItems.Count = {2}" +
                                                         "\nSubmittedItemsCounter.Value = {3}, InternalId={4}",
                                                         Name,
                                                         workItem.ContextIdentifier,
                                                         SubmittedItems.Count,
                                                         ConsumerManager.SubmittedItemsCounter.Value,
                                                         workItem.ContextIdentifier.InternalId
                                                         ),
                                                     ContextIdentifier = e.OperationContextShortcut
                                                 });
                        //}

                        #endregion Log

                        Monitor.Pulse(SubmittedItems);
                    }
                }

                #region Log if response is one of the delayed

                if (workItem == null)
                {
                    Log.TraceData(Log.Source,TraceEventType.Warning,
                                         JobConsumerMessage.
                                             DelayedMessageArrived,
                                         new ContextualLogEntry
                                             {
                                                 Message =
                                                     String.Format
                                                     (
                                                     "{0}: For MessageId={1} - Delayed message arrived",
                                                     Name,
                                                     e.WorkItem.IdHash
                                                     ),
                                                 ContextIdentifier = e.OperationContextShortcut
                                             });

                    workItem = e.WorkItem;
                }

                #endregion Log if response is one of the delayed

                if (e.Success.HasValue)
                {
                    // TODO: (SD) Update related items in the DB
                    responseStatus = ResponseReceivedStatus.ParamsUpdated;

                    // Interpret Transaction Response

                    #region Handle response

                    // update transaction and order acording to the response
                    UpdateJobWithResponse
                        (
                        e
                        );

                    responseStatus = ResponseReceivedStatus.JonAndTransactionUpdated;

                    #region Handle regular response (non-pending)

                    if (
                        e.Success.HasValue && e.Success.Value
                        )
                    {
                        workItem.WorkItemState = WorkItemState.Completed;

                        //WorkItemController.UpdateWorkItemState
                        //    (
                        //    workItem
                        //    );

                        FireRegularResponseObtained(e.OperationContextShortcut);

                        responseStatus = ResponseReceivedStatus.RegularResponseObtainedFired;
                    }

                    #endregion Handle regular response (non-pending)

                    #region Handle pending response

                    if (
                        e.Retry.HasValue && e.Retry.Value
                        )
                    {
                        #region Assert the workItem retreived is not null

                        if (workItem == null)
                        {
                            workItem = e.WorkItem;
                        }
                        // There should be no case now, that e.WorkItem would be null, on the 
                        // other side it should be assigned like the above in order to
                        // solve removed from SIC pendings (SD).

                        //						errorTrap.Assert
                        //							(
                        //							workItem != null,
                        //							TowardsQueueWorkItemsConsumerMessage.MessageRetrievalFromSubmittedItemsFailed,
                        //							string.Format
                        //							(
                        //							"{0}: " + 
                        //							"Could not obtain an item from Submitted Items Collection (SIC) when handling " +
                        //							" pending response. Most probably it is a delayed response and submitted item was removed.",
                        //							Name
                        //							),
                        //							e.OperationContextShortcut.ContextIdentifier
                        //							);
                        //						errorTrap.PropagateTrappedErrors
                        //							(
                        //							new TowardsQueueWorkItemsConsumerException
                        //							(
                        //							errorTrap.Text,
                        //							TowardsQueueWorkItemsConsumerMessage.
                        //							MessageRetrievalFromSubmittedItemsFailed,
                        //							e.OperationContextShortcut.ContextIdentifier,
                        //							null
                        //							)
                        //							);

                        #endregion Assert the stateQueueWorkItem retreived is not null

                        responseStatus =
                            ResponseReceivedStatus.ContextualTransactionRequestChecked;

                        // reset
                        //TODO: **SD1 - Handle invalid reset results ASAP (SD).
                        //WorkItemUpdateStateResult updateResult =
                        //    WorkItemController.ResetWorkItem
                        //        (
                        //        workItem,
                        //        true,
                        //        workItem.SubmissionPriority
                        //        );

                        //QueuesUtility.PendingQueue.Send
                        //    (
                        //    workItem
                        //    );

                        responseStatus =
                            ResponseReceivedStatus.MessageSentToPendingQueue;

                        FirePendingResponseObtained(e.OperationContextShortcut);

                        responseStatus =
                            ResponseReceivedStatus.PendingResponseObtainedFired;
                    }

                    #endregion Handle pending response

                    #endregion Sending QueueWorkItem to after response obtained Queue

                    #region Log the final status

                    Log.TraceData(Log.Source,TraceEventType.Verbose,
                                         JobConsumerMessage.WorkItemFullyProcessed,
                                         new ContextualLogEntry
                                             {
                                                 Message =
                                                     "WorkItem was fully processed. Final status is: " + responseStatus,
                                                 ContextIdentifier = e.OperationContextShortcut
                                             });

                    #endregion Log the final status
                }
                else
                {
                    Log.TraceData(Log.Source,TraceEventType.Error,
                                         JobConsumerMessage.
                                             ErrorOccuredInResponseReceivedDelegate,
                                         new ContextualLogEntry
                                             {
                                                 Message =
                                                     "Null Response from the job JobProcessor or Result within the Response is not present",
                                                 ContextIdentifier = e.WorkItem != null
                                                                         ?
                                                                             e.WorkItem.ContextIdentifier
                                                                         : ContextIdentifier
                                             });
                }

                #region Instrumentation

                // Handle performance event
                if (e.WorkItem != null)
                {
                    e.WorkItem.CompletedAt = DateTime.Now;

                    LogJobCompletion(e);
                }

                #endregion
            }
            // global catches for response received
            catch (ThreadAbortException)
            {
                Log.TraceData(Log.Source,TraceEventType.Stop,
                                     JobConsumerMessage.
                                         AbortRequested,
                                     new ContextualLogEntry
                                         {
                                             Message =
                                                 string.Format
                                                 (
                                                 "Process '{0}': ThreadAbortException thrown." +
                                                 "ResponseStatus = {1}",
                                                 Name,
                                                 responseStatus
                                                 ),
                                             ContextIdentifier = workItem != null
                                                                     ?
                                                                         workItem.ContextIdentifier
                                                                     : ContextIdentifier
                                         });
            }
            catch (Exception ex)
            {
                Log.TraceData(Log.Source,TraceEventType.Error,
                                     JobConsumerMessage.ErrorOccuredInResponseReceivedDelegate,
                                     new ContextualLogEntry
                                         {
                                             Message =
                                                 string.Format
                                                 (
                                                 "Process '{0}': ResponseStatus = {1}. Error desc.:{2}.",
                                                 Name,
                                                 responseStatus,
                                                 ex
                                                 ),
                                             ContextIdentifier = workItem != null
                                                                     ?
                                                                         workItem.ContextIdentifier
                                                                     : ContextIdentifier
                                         });
            }
        }

        protected virtual void LogJobCompletion(JobProcessedEventArgs e)
        {
        }

        public static JobConsumer<JobType> Create(string name)
        {
            return ContextRegistry.GetContext().GetObject(name) as JobConsumer<JobType>;
        }

        #region Proof of concept with abortion and interruption exceptions

        // TODO: ad-hoc added (SD)
        private static bool IsInterruptExceptionPresent(Exception ex)
        {
            if (ex is ThreadInterruptedException) return true;
            if (ex == null) return false;
            return IsInterruptExceptionPresent(ex.InnerException);
        }

        private static bool IsAbortExceptionPresent(Exception ex)
        {
            if (ex is ThreadAbortException) return true;
            if (ex == null) return false;
            return IsAbortExceptionPresent(ex.InnerException);
        }

        #endregion Proof of concept with abortion and interruption exceptions

        #endregion
    }
}