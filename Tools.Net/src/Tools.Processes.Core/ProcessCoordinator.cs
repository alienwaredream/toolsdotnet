using System;
using System.Diagnostics;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using Tools.Core;
using Tools.Core.Context;
using Tools.Processes.Core;
using Process = Tools.Processes.Core.Process;
using System.Collections.Generic;
using Tools.Core.Threading;

namespace Tools.Processes.Core
{
    /// <summary>
    /// Summary description for QueueWorkManager.
    /// </summary>
    [Serializable]
    public class ProcessCoordinator : Process
    {
        #region Fields

        private TraceSource log = Log.Source;

        #region Required for IProcess

        private readonly object waitForProcessesStopSyncObj =
            new object();

        #endregion Required for IProcess

        //private WorkManagerConfiguration _configuration;
        private readonly ContextIdentifier contextIdentifier = new ContextIdentifier();

        public List<IProcess> Processes { get; set; }

        private readonly SynchronizedCounter numberOfProcessesRunning = new SynchronizedCounter();

        #endregion

        #region Properties

        /// <summary>
        /// The timeout in ms, given the whole process to StopInternal regularly.
        /// If that time is exceeded and subprocess have not been joined to the
        /// main execution flow abort will be issued to the process tree.
        /// </summary>
        public int TotalRegularStopTimeout { get; set; }

        #endregion Properties

        #region Constructor

        public ProcessCoordinator()
        {
            numberOfProcessesRunning.Zeroed += numberOfProcessesRunning_Zeroed;
            // Default value for the timeout
            TotalRegularStopTimeout = 20000;
            // Initialize the processes list, lets say that contract is not to have it null
            Processes = new List<IProcess>();
        }



        #endregion

        #region Methods


        void numberOfProcessesRunning_Zeroed()
        {
            lock(waitForProcessesStopSyncObj)
            {
                Monitor.PulseAll(waitForProcessesStopSyncObj);
            }
        }

        private void ProcessJoinCallback(IAsyncResult ar)
        {
            try
            {
                var aResult = (AsyncResult)ar;
                var batchesJoinDelegate =
                    (VoidDelegate)aResult.AsyncDelegate;

                log.TraceData(TraceEventType.Verbose,
                                     ProcessCoordinatorMessage.ErrorWhileStoppingProcess,
                                     new ContextualLogEntry
                                         {
                                             Message =
                                                 string.Format
                                                 (
                                                 "Manager callback called for stopping {0} process", 
                                                 ((aResult.AsyncState as Descriptor)!=null)? ((Descriptor)aResult.AsyncState).Name: "Unknown"),
                             
                                             ContextIdentifier = contextIdentifier
                                         });

                numberOfProcessesRunning.SyncDecrement();

                batchesJoinDelegate.EndInvoke(ar);

            }
            catch (Exception ex)
            {
                // TODO: Think about placement of base Process class,
                // it can be prefferable to have it lower as it gets in
                // the architecture, on the other side it can provide default logging;
                // can represent the need for delegates use then. Or logging can be located in the utility (SD)
                log.TraceData(TraceEventType.Error,
                                     ProcessCoordinatorMessage.ErrorWhileStoppingProcess,
                                     new ContextualLogEntry
                                         {
                                             Message =
                                                 string.Format
                                                 (
                                                 "Exception happened while trying to StopInternal the process {0}" +
                                                 " Exception text: {1}",
                                                 Descriptor.ProbeForName(ar.AsyncState),
                                                 ex
                                                 ),
                                             ContextIdentifier = contextIdentifier
                                         });
            }
        }

        public override void Start()
        {
            Processes.ForEach(p => p.Start());

            numberOfProcessesRunning.SyncValue = ((Processes == null) ? 0 : Processes.Count);

            SetExecutionState(ProcessExecutionState.Running);
        }

        public override void Abort()
        {
            base.Abort();
            //TODO: (SD) Add exception handling
            Processes.ForEach(p=>p.Abort());
        }

        public override void Stop()
        {

            // When the call to Stop is blocking we need to process callback, not an event (SD)
            foreach (IProcess process in Processes)
            {
                VoidDelegate batchesJoinDelegate = process.Stop;
                batchesJoinDelegate.BeginInvoke
                    (
                    ProcessJoinCallback,
                    process
                    );
            }


            bool processesStoppedWithinTimeout;
            // So nothing stops as to have all above items running in parallel, but (SD)
            lock (waitForProcessesStopSyncObj)
            {
                processesStoppedWithinTimeout =
                    Monitor.Wait(waitForProcessesStopSyncObj, TotalRegularStopTimeout);
            }

            if (!processesStoppedWithinTimeout)
            {
                // TODO: Handle the case when processesStoppedWithinTimeout is false (SD)
            }
            // No own thread of execution otherwise is present here, so just end the method
            // So we can count that upper IProcess will apply the async to the "parts" here and in this method we just call
            // the Stopped event so the process will manage to wait as well untill it gathers all stopped events
            // to finish its "Whole" method, or it can know the call is blocking, but that might break the 
            // pattern here (SD)

            base.Stop();
        }
        
        #endregion
    }
}