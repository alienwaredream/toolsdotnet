using System;
using System.Diagnostics;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using Tools.Core;
using Tools.Core.Context;
using Tools.Core.Threading;
using Tools.Processes.Core;
using Tools.Coordination.WorkItems;
using Process=Tools.Processes.Core.Process;


namespace Tools.Coordination.ProducerConsumer
{
    /// <summary>
    /// Summary description for TimeOutSubmissionsCollector.
    /// </summary>
    public class TimeoutSubmissionsCollector : Process
    {
        #region Fields

        private AutoResetEvent delayInfLoopAutoResetEvent;
        private AutoResetEvent stopInfLoopAutoResetEvent;
        private Thread workingThread;
        protected WorkItemCollection _submittedItems;
        protected SynchronizedCounter _submittedItemsCounter;

        #endregion

        #region Properties

        protected TimeOutSubmissionsCollectorConfiguration Configuration { get;set; }

        #endregion

        protected override void OnStopped()
        {
            // TODO: Think about placement of base Process class,
            // it can be prefferable to have it lower as it gets in
            // the architecture, on the other side it can provide default logging;
            // can represent the need for delegates use then. Or logging can be located in the utility (SD)
            Log.TraceData(Log.Source, TraceEventType.Stop,
                                 TimeOutSubmissionsCollectorMessage.TimeOutSubmissionsCollectorStopped,
                                 // TODO: Better name can be Stopping, but Graig L. on other side use Startup name nicely,
                                 // stopping is just not sounding nice (SD)
                                 new ContextualLogEntry
                                     {
                                         Message =
                                             string.Format
                                             (
                                             "{0}: stopped. Stopped event is about to be raised.",
                                             Name
                                             ),
                                         // TODO: change for StartInternal global CI (SD)
                                         ContextIdentifier = new ContextIdentifier()
                                     });

            base.OnStopped();
        }

        public override void Start()
        {
            stopInfLoopAutoResetEvent = new AutoResetEvent(false);
            delayInfLoopAutoResetEvent = new AutoResetEvent(false);

            workingThread =
                new Thread
                    (
                    start);

            workingThread.Name = Name;
            workingThread.IsBackground = true;

            Log.TraceData(Log.Source,TraceEventType.Verbose,
                                 TimeOutSubmissionsCollectorMessage.TimeOutSubmissionsCollectorStarted,
                                 new ContextualLogEntry
                                     {
                                         Message =
                                             string.Format
                                             (
                                             "TimeOutSubmissionsCollector: '{0}' started",
                                             Name
                                             ),
                                         ContextIdentifier = new ContextIdentifier()
                                     });

            workingThread.Start();

            SetExecutionState(ProcessExecutionState.Running);
        }


        public override void Abort()
        {
            base.Abort();

            workingThread.Abort();
        }


        public override void Stop()
        {
            base.Stop();

            delayInfLoopAutoResetEvent.Set();

            bool stoppedWithinRegularTimeout = stopInfLoopAutoResetEvent.WaitOne
                (
                Configuration.CollectionShutdownTimeout,
                false
                );

            if (!stoppedWithinRegularTimeout)
            {
                Log.TraceData(Log.Source,TraceEventType.Warning,
                                     TimeOutSubmissionsCollectorMessage.RegularCollectionStopTimeout,
                                     new ContextualLogEntry
                                         {
                                             Message =
                                                 string.Format
                                                 (
                                                 "{0}: Regular collection could not be stopped within configured {1} ms." +
                                                 " Its thread will be interrupted to enable final collection.",
                                                 Name,
                                                 Configuration.CollectionShutdownTimeout
                                                 ),
                                             ContextIdentifier = new ContextIdentifier()
                                         });
                // TODO: Calling of interrupt/abort (especialy here) is subject for further reviews (SD)
                workingThread.Interrupt();
            }
            VoidDelegate finalCollectionDelegate =
                PerformRegularShutdownCollection;
            IAsyncResult ar =
                finalCollectionDelegate.BeginInvoke
                    (
                    finalCollectionCallback,
                    null
                    );

            bool finalCollectionRanWithinTimout = ar.AsyncWaitHandle.WaitOne
                (
                Configuration.FinalCollectionTimeout,
                false
                );

            if (!finalCollectionRanWithinTimout)
            {
                Log.TraceData(Log.Source,TraceEventType.Warning,
                                     TimeOutSubmissionsCollectorMessage.RegularCollectionStopTimeout,
                                     new ContextualLogEntry
                                         {
                                             Message =
                                                 string.Format
                                                 (
                                                 "{0}: Final collection could not be stopped within configured {1} ms." +
                                                 " Process will continue final collection, but can be a victum of abortion by OS.",
                                                 Name,
                                                 Configuration.FinalCollectionTimeout
                                                 ),
                                             ContextIdentifier = new ContextIdentifier()
                                         });
            }
        }

        private void finalCollectionCallback(IAsyncResult ar)
        {
            try
            {
                var finalCollectionDelegate =
                    ((AsyncResult) ar).AsyncDelegate as VoidDelegate;
                finalCollectionDelegate.EndInvoke(ar);
            }
            catch (Exception ex)
            {
                Log.TraceData(Log.Source,TraceEventType.Error,
                                     TimeOutSubmissionsCollectorMessage.ErrorDuringFinalCollection,
                                     new ContextualLogEntry
                                         {
                                             Message =
                                                 string.Format
                                                 (
                                                 "{0}: Exception happen during performing final collection! " + ex,
                                                 Name
                                                 ),
                                             ContextIdentifier = new ContextIdentifier()
                                         });
            }
            finally
            {
                OnStopped();
            }
        }

        #region Methods

        private void start()
        {
            try
            {
                while (true)
                {
                    if (ExecutionState != ProcessExecutionState.Running)
                    {
                        Log.TraceData(Log.Source,TraceEventType.Stop,
                                             TimeOutSubmissionsCollectorMessage.BreakingFromInfiniteLoop,
                                             // TODO: Better name can be Stopping, but Graig L. on other side use Startup name nicely,
                                             // stopping is just not sounding nice (SD)
                                             new ContextualLogEntry
                                                 {
                                                     Message =
                                                         string.Format
                                                         (
                                                         "{0}: Finishing infinite loop for items collection on SIC.",
                                                         Name
                                                         ),
                                                     ContextIdentifier = new ContextIdentifier()
                                                 });

                        stopInfLoopAutoResetEvent.Set();

                        break;
                    }

                    PerformCollection();

                    delayInfLoopAutoResetEvent.WaitOne(Configuration.Interval, false);
                }
            }
            catch (Exception ex)
            {
                Log.TraceData(Log.Source,TraceEventType.Error,
                                     TimeOutSubmissionsCollectorMessage.UnexpectedErrorOccured,
                                     new ContextualLogEntry
                                         {
                                             Message =
                                                 string.Format
                                                 (
                                                 "'{0}': An error occured while being executed" +
                                                 " in a loop. Error message: {1}.",
                                                 Name,
                                                 ex
                                                 ),
                                             ContextIdentifier = new ContextIdentifier()
                                         });
            }
        }
        protected virtual void PerformCollection()
        {
            lock (_submittedItems)
            {
                if (_submittedItems.Count == 0) return;

                #region Log

                Log.TraceData(Log.Source,TraceEventType.Verbose,
                                     TimeOutSubmissionsCollectorMessage.RegularCollectionStarted,
                                     new ContextualLogEntry
                                         {
                                             Message =
                                                 string.Format
                                                 (
                                                 "{0}: PerformCollection started. Current count of items is {1}",
                                                 Name,
                                                 _submittedItems.Count
                                                 ),
                                             ContextIdentifier = new ContextIdentifier()
                                         });

                #endregion Log

                DateTime currentTimeStamp = DateTime.UtcNow;

                for (int i = _submittedItems.Count - 1; i > -1; i--)
                {
                    WorkItem workItem =
                        _submittedItems[i];

                    // First checks  for the case of response delay from the ext module when we
                    // //are giving up the possibility of handling pending as transaction(SD)
                    TimeSpan delta = currentTimeStamp - workItem.SubmittedToExternalRecipientAt;

                    if (
                        Configuration.ClearItems
                        &&
                        workItem.SyncStatus == WorkItemState.Submitted
                        &&
                        delta > TimeSpan.FromMilliseconds(Configuration.RemoveTimeout)
                        )
                    {
                        #region Log

                        /*
						ApplicationEventHandler.Instance.HandleEvent
							(
							new ApplicationEvent
							(
							BatchJobTimeoutCollectorMessage.
							RemovingItemFromSubmittedItems,
							ApplicationEventType.Warning,
							ApplicationLifeCycleType.Runtime,
							string.Format
							(
							"{0}: As a result of response delay {2} from the external module for more than {1} ms" +
							" item will be removed from the submitted items collection. Response will still be " +
							" handled if comes before the service restart and is not pending.",
							Name,
							RemoveTimeout,
							delta.Milliseconds
							),
							stateQueueWorkItem.ContextIdentifier,
							null
							)
							);
*/

                        #endregion Log

                        try
                        {
                            _submittedItems.Remove(workItem);
                        }
                        catch (Exception ex)
                        {
                            #region Log

                            Log.TraceData(Log.Source,TraceEventType.Error,
                                                 TimeOutSubmissionsCollectorMessage.RemovalFromSubmittedItemsFailed,
                                                 new ContextualLogEntry
                                                     {
                                                         Message =
                                                             string.Format
                                                             (
                                                             "{0}: Error removing item " +
                                                             "from SubmittedItems. Error message: {1}.",
                                                             Name,
                                                             ex
                                                             ),
                                                         ContextIdentifier = workItem.ContextIdentifier
                                                     });

                            #endregion Log

                            continue;
                        }

                        Log.TraceData(Log.Source,TraceEventType.Error,
                                             TimeOutSubmissionsCollectorMessage.ItemRemovedFromSubmittedItems,
                                             new ContextualLogEntry
                                                 {
                                                     Message =
                                                         string.Format
                                                         (
                                                         "{0}: As a result of response delay {1} from the external module for more than {2} ms" +
                                                         " item is removed from the submitted items collection. Response will still be " +
                                                         " handled if comes before the service restart and is not pending." +
                                                         " Recurrence will stay in the SIC, but " +
                                                         "consumers will be signalled to pickup one more message, so immidiate count of " +
                                                         "messages sent to the external component can exceed the configured value of MaxTotalSubmittedItemsCount. \r\n" +
                                                         "SubmittedItems.Count = {3}, _submittedItemsCounter.Value = {4}",
                                                         Name,
                                                         delta.TotalMilliseconds,
                                                         Configuration.RemoveTimeout,
                                                         _submittedItems.Count,
                                                         _submittedItemsCounter.Value
                                                         ),
                                                     ContextIdentifier = workItem.ContextIdentifier
                                                 });

                        // TODO: Review, but that we are allowing the grow of the SIC (SD)!
                        // TODO: also it should be checked if we are not checking for the physical
                        // count somewhere, because that will blow in that case (SD)!!!
                        _submittedItemsCounter.Decrement();
                        // TODO: think about use of resetevent (SD)
                        Monitor.Pulse(_submittedItems);
                    }
                        // Checks for the case of response delay from the ext module when only
                        // notification is required (SD)
                    else if (workItem.SyncStatus == WorkItemState.Submitted
                             &&
                             delta
                             > TimeSpan.FromMilliseconds(Configuration.ResponseTimeout)
                        )
                    {
                        Log.TraceData(Log.Source,TraceEventType.Warning,
                                             TimeOutSubmissionsCollectorMessage.ResponseFromExternalModuleDelayed,
                                             new ContextualLogEntry
                                                 {
                                                     Message =
                                                         string.Format
                                                         (
                                                         "{0}: Notification of the delayed response. The response from the external " +
                                                         "module was delayed for {1} ms which is more than {2} ms ResponseTimeout value.",
                                                         Name,
                                                         delta.TotalMilliseconds,
                                                         Configuration.ResponseTimeout
                                                         ),
                                                     ContextIdentifier = workItem.ContextIdentifier
                                                 });
                        // TODO: check if synchronized (SD), yes it is (SD)
                        workItem.SyncStatus = WorkItemState.ExternalEntityResponseDelayed;
                    }
                }
            }
        }

        protected virtual void PerformRegularShutdownCollection()
        {
            lock (_submittedItems)
            {
                #region Log

                Log.TraceData(Log.Source,TraceEventType.Stop,
                                     TimeOutSubmissionsCollectorMessage.RegularShutdownCollectionStarted,
                                     new ContextualLogEntry
                                         {
                                             Message =
                                                 string.Format
                                                 (
                                                 "{0}: RegularShutdownCollection started. {1} items are present in the SIC.",
                                                 Name,
                                                 _submittedItems.Count
                                                 ),
                                             ContextIdentifier = new ContextIdentifier()
                                         });

                #endregion Log

                for (int i = _submittedItems.Count - 1; i > -1; i--)
                {
                    WorkItem workItem =
                        _submittedItems[i];

                    try
                    {
                        //_submittedItems.Remove(stateQueueWorkItem);
                    }
                    catch (Exception ex)
                    {
                        #region Log

                        Log.TraceData(Log.Source,TraceEventType.Stop,
                                             TimeOutSubmissionsCollectorMessage.RemovalFromSubmittedItemsFailed,
                                             new ContextualLogEntry
                                                 {
                                                     Message =
                                                         string.Format
                                                         (
                                                         "{0}: Error removing item " +
                                                         "from SubmittedItems. Error message: {1}.",
                                                         Name,
                                                         ex
                                                         ),
                                                     ContextIdentifier = workItem.ContextIdentifier
                                                 });

                        #endregion Log

                        continue;
                    }

                    #region Log

                    Log.TraceData(Log.Source,TraceEventType.Stop,
                                         TimeOutSubmissionsCollectorMessage.OrphanedItemProbability,
                                         new ContextualLogEntry
                                             {
                                                 Message =
                                                     Name +
                                                     ": Recurrence is present in submitted items collection during the " +
                                                     "normal shutdown. Application will still wait for responses " +
                                                     "from external component untill the service is stopped by operation " +
                                                     "system. Response handling for this item is NOT guaranteed!. See the log " +
                                                     " for particular Ids to find further events connected to the item. This " +
                                                     "message is logged as an error and should be considered as critical for the job instance.",
                                                 ContextIdentifier = workItem.ContextIdentifier
                                             });

                    #endregion Log
                }

                #region Log end of the collection

                Log.TraceData(Log.Source,TraceEventType.Stop,
                                     TimeOutSubmissionsCollectorMessage.RegularShutdownCollectionFinished,
                                     new ContextualLogEntry
                                         {
                                             Message =
                                                 string.Format
                                                 (
                                                 "{0}: RegularShutdownCollection finished. See the log for the items that can be orphaned",
                                                 Name
                                                 ),
                                             ContextIdentifier = new ContextIdentifier()
                                         });

                #endregion Log end of the collection
            }
        }

        #endregion
    }
}