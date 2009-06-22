using System;
using System.Diagnostics;
using System.Threading;
using Tools.Core;
using Tools.Core.Context;
using Tools.Processes.Core;
using Tools.Coordination.WorkItems;
using Process=Tools.Processes.Core.Process;

namespace Tools.Coordination.ProducerConsumer
{
    /// <summary>
    /// Summary description for RetrievedItemsCleaner.
    /// 
    /// </summary>
    public class RetrievedItemsCleaner : Process
    {
        #region Fields

        private readonly ContextIdentifier _contextIdentifier;
        private readonly WorkItemSlotCollection _retrievedItems;

        //new AutoResetEvent(false);

        private readonly int _retrievedItemsCleanerInterval;
        private readonly int cleanerRegularStopTimeout;
        private AutoResetEvent _stopAutoResetEvent;
        private Thread _workingThread;

        #endregion Fields

        #region Constructors

        public RetrievedItemsCleaner
            (WorkItemSlotCollection retrievedItems, ContextIdentifier contextIdentifier, int retrievedItemsCleanerInterval, int cleanerRegularStopTimeout, string name, string description)
            : base
                (
                name,
                description
                )
        {
            _retrievedItems = retrievedItems;
            _contextIdentifier = contextIdentifier;
            _retrievedItemsCleanerInterval = retrievedItemsCleanerInterval;

            this.cleanerRegularStopTimeout = cleanerRegularStopTimeout;
        }

        #endregion Constructors

        #region Methods

        private void startSavingRetrievedMessages()
        {
            // Assumes that no interrupt exception can be thrown here.
            // Assumes that abort exception can be thrown here.

            WorkItem qwi = null;

            try
            {
                Log.TraceData(Log.Source,TraceEventType.Verbose,
                                     RetrievedItemsCleanerMessage.SavingRetrievedMessagesStarted,
                                     new ContextualLogEntry
                                         {
                                             Message =
                                                 string.Format
                                                 (
                                                 "'{0}': Started saving Retrieved Items",
                                                 Name
                                                 ),
                                             ContextIdentifier = _contextIdentifier
                                         });

                while (true)
                {
                    if (_retrievedItems == null)
                    {
                        break;
                    }
                    lock (_retrievedItems)
                    {
                        qwi = _retrievedItems.GetTopWorkItem();
                    }

                    if (qwi != null)
                    {
                        try
                        {
                            sendToRetrievalQueue(qwi);
                        }
                        catch (Exception ex)
                        {
                            // TODO: handle this case

                            Log.TraceData(Log.Source,TraceEventType.Error,
                                                 RetrievedItemsCleanerMessage.SavingRetrievedMessageFailed,
                                                 new ContextualLogEntry
                                                     {
                                                         Message =
                                                             string.Format
                                                             (
                                                             "'{0}': Error occured while saving retrieved message(Id = {1})." +
                                                             " Error message: {2}",
                                                             Name,
                                                             qwi.Id,
                                                             ex
                                                             ),
                                                         ContextIdentifier = qwi.ContextIdentifier
                                                     });
                        }
                        // TODO: possible inconsistency - qwi can be sent to the queue
                        // and not yet set to null when exception occurs
                        qwi = null;
                    }
                    else
                    {
                        if (ExecutionState != ProcessExecutionState.Running)
                        {
                            Log.TraceData(Log.Source,TraceEventType.Verbose,
                                                 RetrievedItemsCleanerMessage.RetrievedItemsCleanerFinishingRetrieving,
                                                 // TODO: Better name can be Stopping, but Graig L. on other side use Startup name nicely,
                                                 // stopping is just not sounding nice (SD)
                                                 new ContextualLogEntry
                                                     {
                                                         Message =
                                                             string.Format
                                                             (
                                                             "{0} finishing the inner while loop for reading from the retrieved items.",
                                                             Name
                                                             ),
                                                         ContextIdentifier = _contextIdentifier
                                                     });

                            _stopAutoResetEvent.Set();

                            break;
                        }
                    }

                    if (_retrievedItemsCleanerInterval != 0)
                    {
                        Thread.Sleep(_retrievedItemsCleanerInterval);
                    }
                }
            }
            catch (ThreadAbortException)
            {
                Log.TraceData(Log.Source,TraceEventType.Error,
                                     RetrievedItemsCleanerMessage.AbortRequested,
                                     new ContextualLogEntry
                                         {
                                             Message =
                                                 string.Format
                                                 (
                                                 "'{0}': Abort requested.",
                                                 Name),
                                             ContextIdentifier = _contextIdentifier
                                         });


                if (qwi != null)
                {
                    // TODO: handle this case
                }

                // TODO: dump all items in RetrievedItems
            }
            catch (Exception ex)
            {
                Log.TraceData(Log.Source,TraceEventType.Error,
                                     RetrievedItemsCleanerMessage.SavingRetrievedMessagesFailed,
                                     new ContextualLogEntry
                                         {
                                             Message =
                                                 string.Format
                                                 (
                                                 "'{0}': Error occured while saving retrieved messages." +
                                                 " Error message: {1}",
                                                 Name,
                                                 ex
                                                 ),
                                             ContextIdentifier = _contextIdentifier
                                         });

                if (qwi != null)
                {
                    // TODO: handle this case
                }

                // TODO: dump all items in RetrievedItems
            }

            Log.TraceData(Log.Source,TraceEventType.Verbose,
                                 RetrievedItemsCleanerMessage.FinishingNormally,
                                 new ContextualLogEntry
                                     {
                                         Message =
                                             string.Format
                                             (
                                             "'{0}': Finishing normally",
                                             Name
                                             ),
                                         ContextIdentifier = _contextIdentifier
                                     });
        }

        /// <summary>
        /// Send a QueueWorkItem to queue
        /// </summary>
        protected void sendToRetrievalQueue
            (
            WorkItem queueWorkItem
            )
        {
            string queuePath = null;


            try
            {
                //queuePath =
                //    QueuesUtility.SendToRetrievalQueue
                //        (
                //        queueWorkItem
                //        );

                Log.TraceData(Log.Source,TraceEventType.Information,
                                     RetrievedItemsCleanerMessage.RetrievedMessageSuccessfullySaved,
                                     new ContextualLogEntry
                                         {
                                             Message =
                                                 string.Format
                                                 (
                                                 "'{0}': Retrieved (Id = {1}) Successfully Saved to the {2} queue",
                                                 Name,
                                                 queueWorkItem.ContextIdentifier,
                                                 queuePath
                                                 ),
                                             ContextIdentifier = queueWorkItem.ContextIdentifier
                                         });
            }
            catch (Exception ex)
            {
                throw new BaseException(
                    RetrievedItemsCleanerMessage.SendingMessageToMSMQFailed,
                    null,
                    string.Format
                        (
                        "Sending Message to back MSMQ Failed. MSMQ path = {0}",
                        (queuePath ?? "Queue Can't be instantiated (is null)!")
                        ),
                    ex
                    );
            }
        }

        #endregion

        protected override void OnStopped()
        {
            // TODO: Think about placement of base Process class,
            // it can be prefferable to have it lower as it gets in
            // the architecture, on the other side it can provide default logging;
            // can represent the need for delegates use then. Or logging can be located in the utility (SD)
            Log.TraceData(Log.Source,TraceEventType.Verbose,
                                 RetrievedItemsCleanerMessage.RetrievedItemsCleanerStopped,
                                 // TODO: Better name can be Stopping, but Graig L. on other side use Startup name nicely,
                                 // stopping is just not sounding nice (SD)
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
            // TODO: Think about placement of base Process class,
            // it can be prefferable to have it lower as it gets in
            // the architecture, on the other side it can provide default logging;
            // can represent the need for delegates use then. (SD)
            Log.TraceData(Log.Source,TraceEventType.Verbose,
                                 RetrievedItemsCleanerMessage.RetrievedItemsCleanerStartRequested,
                                 new ContextualLogEntry
                                     {
                                         Message =
                                             string.Format
                                             (
                                             "{0} process is requested to StartInternal.",
                                             Name
                                             ),
                                         ContextIdentifier = _contextIdentifier
                                     });
            //
            _stopAutoResetEvent =
                new AutoResetEvent(false);

            _workingThread = new Thread
                (
                startSavingRetrievedMessages
                );
            _workingThread.IsBackground = true;
            _workingThread.Name = Name;

            _workingThread.Start();

            SetExecutionState(ProcessExecutionState.Running);

            // TODO: Think about placement of base Process class,
            // it can be prefferable to have it lower as it gets in
            // the architecture, on the other side it can provide default logging;
            // can represent the need for delegates use then. Or logging can be located in the utility (SD)
            Log.TraceData(Log.Source,TraceEventType.Verbose,
                                 RetrievedItemsCleanerMessage.RetrievedItemsCleanerStarted,
                                 new ContextualLogEntry
                                     {
                                         Message =
                                             string.Format
                                             (
                                             "{0} started. The sequence of this message can be intermixed with other active object messages.",
                                             Name
                                             ),
                                         ContextIdentifier = _contextIdentifier
                                     });
        }

        public override void Stop()
        {
            base.Stop();
            // TODO: Think about placement of base Process class,
            // it can be prefferable to have it lower as it gets in
            // the architecture, on the other side it can provide default logging;
            // can represent the need for delegates use then. Or logging can be located in the utility (SD)
            Log.TraceData(Log.Source,TraceEventType.Stop,
                                 RetrievedItemsCleanerMessage.RetrievedItemsCleanerStopRequested,
                                 // TODO: Better name can be Stopping, but Graig L. on other side use Startup name nicely,
                                 // stopping is just not sounding nice (SD)
                                 new ContextualLogEntry
                                     {
                                         Message =
                                             string.Format
                                             (
                                             "{0} process is requested to StopInternal.",
                                             Name
                                             ),
                                         ContextIdentifier = _contextIdentifier
                                     });
            //
            bool stoppedWithinregularTimeout = _stopAutoResetEvent.WaitOne
                (
                cleanerRegularStopTimeout,
                false
                );
            if (!stoppedWithinregularTimeout)
            {
                // TODO: Calling of interrupt is subject for further reviews (SD)
                _workingThread.Interrupt();
            }
            // Once we are reaching the end of the execution flow we can call the event
            // Very raw for a moment, just proof of concept (SD)
            VoidDelegate joinDelegate = join;
            //
            joinDelegate.BeginInvoke
                (
                joinCallback,
                null
                );
        }

        // TODO: Think about Stop(Timeout) (SD), days++, so far can't see if applicable (SD)
        private void joinCallback(IAsyncResult ar)
        {
            OnStopped();
        }

        private void join()
        {
            _workingThread.Join();
        }
    }
}