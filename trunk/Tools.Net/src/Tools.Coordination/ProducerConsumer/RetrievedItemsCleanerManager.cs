using System;
using System.Diagnostics;
using System.Threading;
using Tools.Coordination.ProducerConsumer;
using Tools.Core.Asserts;
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
    public class RetrievedItemsCleanerManager : Process
    {
        #region Fields

        #region Required for IProcess

        private readonly object waitForProcessesStopSyncObj =
            new object();

        private int activeProcessesCounter;

        #endregion Required for IProcess

        private readonly ContextIdentifier _contextIdentifier;
        private readonly bool _retrievalQueueRecoverable;
        private readonly WorkItemSlotCollection _retrievedItems;


        private readonly int _retrievedItemsCleanerInterval;
        private readonly int cleanerRegularStopTimeout;
        private readonly int cleanersCount;
        private IProcess[] cleaners;

        #endregion Fields

        #region Constructors

        public RetrievedItemsCleanerManager
            (
            WorkItemSlotCollection retrievedItems,
            bool retrievalQueueRecoverable,
            ContextIdentifier contextIdentifier,
            int retrievedItemsCleanerInterval,
            string name,
            string description,
            int cleanersCount,
            int cleanerRegularStopTimeout
            )
            : base
                (
                name,
                description
                )
        {
            _retrievedItems = retrievedItems;
            _retrievalQueueRecoverable = retrievalQueueRecoverable;
            _contextIdentifier = contextIdentifier;
            _retrievedItemsCleanerInterval = retrievedItemsCleanerInterval;

            this.cleanersCount = cleanersCount;
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
                Log.Source.TraceData(TraceEventType.Verbose,
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

                            Log.Source.TraceData(TraceEventType.Error,
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

                    Thread.Sleep(_retrievedItemsCleanerInterval);
                }
            }
            catch (ThreadAbortException)
            {
                Log.Source.TraceData(TraceEventType.Error,
                                     RetrievedItemsCleanerMessage.AbortRequested,
                                     new ContextualLogEntry
                                         {
                                             Message =
                                                 string.Format
                                                 (
                                                 "'{0}': Abort requested.",
                                                 Name
                                                 ),
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
                Log.Source.TraceData(TraceEventType.Error,
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

            Log.Source.TraceData(TraceEventType.Stop,
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
        /// <param name="message">The QueueWorkItem to be sent to the queue</param>
        protected void sendToRetrievalQueue
            (
            WorkItem workItem
            )
        {
            //**SD1 - return back one day
            // It should be distinguished here by priority if send to 
            // Low or Normal priorities queues (SD)

            #region Commented out

/*
			Message message			= new Message(queueWorkItem);
			message.Formatter		= new BinaryMessageFormatter();
			message.Recoverable		= _retrievalQueueRecoverable;
			
			// send the message to the queue
			try
			{
				_retrievalQueue.Send(message);
			}
			catch(Exception ex)
			{
				throw new RetrievedItemsCleanerManagerException
					(
					string.Format
					(
					"Sending Message to MSMQ Failed. MSMQ format name = {0}",
					_retrievalQueue.FormatName
					),
					RetrievedItemsCleanerManagerMessage.SendingMessagetoMSMQFailed,
					null,
					ex
					);
			}

			ApplicationEventHandler.Instance.HandleEvent
				(
				new ApplicationEvent
				(
				RetrievedItemsCleanerMessage.RetrievedMessageSuccessfullySaved,
				ApplicationEventType.Verbose,
				string.Format
				(
				"'{0}': Retrieved Recurrence(Id = {1}) Successfully Saved",
				Name,
				queueWorkItem.Id
				),
				queueWorkItem.ContextIdentifier,
				null
				)
				);
*/

            #endregion Commented out
        }

        #endregion

        protected override void OnStopped()
        {
            // TODO: Think about placement of base Process class,
            // it can be prefferable to have it lower as it gets in
            // the architecture, on the other side it can provide default logging;
            // can represent the need for delegates use then. Or logging can be located in the utility (SD)
            Log.Source.TraceData(TraceEventType.Stop,
                                 RetrievedItemsCleanerManagerMessage.RetrievedItemsCleanerManagerStopped,
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
            Log.Source.TraceData(TraceEventType.Start,
                                 RetrievedItemsCleanerManagerMessage.RetrievedItemsCleanerManagerStartRequested,
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
            cleaners = new RetrievedItemsCleaner[cleanersCount];
            activeProcessesCounter = 0;

            for (int i = 0; i < cleanersCount; i ++)
            {
                cleaners[i] =
                    new RetrievedItemsCleaner
                        (
                        _retrievedItems,
                        _contextIdentifier,
                        _retrievedItemsCleanerInterval,
                        cleanerRegularStopTimeout,
                        "RetrievedItemsCleaner#" + i,
                        "Moves items from the internal collection of retrieved items back to MSMQ."
                        );

                cleaners[i].Stopped += retrievedItemsCleaner_Stopped;
                // Why it would not be just assigned to the count? Answer can't be presented in one line (SD)
                cleaners[i].Start();
                activeProcessesCounter ++;
            }
            SetExecutionState(ProcessExecutionState.Running);
            // TODO: Think about placement of base Process class,
            // it can be prefferable to have it lower as it gets in
            // the architecture, on the other side it can provide default logging;
            // can represent the need for delegates use then. (SD)
            Log.Source.TraceData(TraceEventType.Start,
                                 RetrievedItemsCleanerManagerMessage.RetrievedItemsCleanerManagerStarted,
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
        }

        public override void Stop()
        {
            // TODO: the bellow is only an adhoc fix for calling Stop more times (SD)
            if (ExecutionState != ProcessExecutionState.Running) return;

            base.Stop();

            // TODO: Think about placement of base Process class,
            // it can be prefferable to have it lower as it gets in
            // the architecture, on the other side it can provide default logging;
            // can represent the need for delegates use then. Or logging can be located in the utility (SD)
            Log.Source.TraceData(TraceEventType.Stop,
                                 RetrievedItemsCleanerManagerMessage.RetrievedItemsCleanerManagerStopRequested,
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

            foreach (IProcess cleaner in cleaners)
            {
                cleaner.Stop();
            }
        }

//		// TODO: Think about Stop(Timeout) (SD), days++, so far can't see if applicable (SD)
//		private void joinCallback(IAsyncResult ar)
//		{
//			onStopped();
//		}

        private void retrievedItemsCleaner_Stopped(object sender, EventArgs e)
        {
            // The method is called in the threads of the process objects that will take them from 
            // the thread pool when calling the delegate asynchroniously (SD)

            // Forgiving to someone passed the incorrect type, let him react to the error event (SD)
            // ErrorTrap.Reset();
            unregisterProcessObject(sender);

            lock (waitForProcessesStopSyncObj)
            {
                // When counter is zero we know that producers finished their work
                activeProcessesCounter --;
                if (activeProcessesCounter == 0)
                {
                    OnStopped();
                }
            }
            //TODO: So far it is not type dependent, good candidate to move (SD)
            // Actually happened to be as for additional process, that are not main,
            // but that can be resolved in the template/hooks pattern (SD)
        }

        // TODO: Move to the common hierachy class (SD)
        private void unregisterProcessObject(object obj)
        {
            // It is always a good idea to unregister (SD :)
            var process = obj as IProcess;
            // 
            ErrorTrap.AddAssertion
                (
                process != null,
                ProducerManagerMessage.StoppedEventOwnerOfIncorrectType,
                String.Format
                    (
                    "Incorrect Stopped event caller type {0}, expected IProcess",
                    ((obj == null) ? "null" : obj.GetType().FullName)
                    ));
            // We don't want to propagate this error (SD)
            if (!ErrorTrap.HasErrors)
            {
                // TODO: Name of the delegate is the only specific thing here, make as a param (SD)
                process.Stopped -= retrievedItemsCleaner_Stopped;
            }
        }
    }
}