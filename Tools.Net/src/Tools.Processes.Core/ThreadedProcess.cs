using System;
using System.Threading;

namespace Tools.Processes.Core
{
    /// <summary>
    /// Represents a process with its own thread of execution.
    /// </summary>
    public abstract class ThreadedProcess : Process
    {
        #region Fields

        private readonly ManualResetEvent _operationReset = new
            ManualResetEvent(false);

        private readonly ManualResetEvent _selfSuspendEvent =
            new ManualResetEvent(true);

        private Thread _workingThread;

        #endregion Fields

        #region Constructors

        #endregion Constructors

        #region Properties

        protected Thread WorkingThread
        {
            get { return _workingThread; }
        }

        /// <summary>
        /// For reseting the blocking operation the thread might be located in.
        /// </summary>
        protected ManualResetEvent OperationReset
        {
            get { return _operationReset; }
        }

        #endregion Properties

        #region Methods

        // TODO: change to CLS compliant names when name is given a thought (SD)
        protected abstract void StartInternal();

        protected bool SelfSuspend(TimeSpan timeout)
        {
            lock (ExecutionStateSyncObj)
            {
                ProcessExecutionState oldState = ExecutionState;
                SetExecutionState(ProcessExecutionState.SelfSuspended);
                _selfSuspendEvent.Reset();
                bool timeoutFlag = _selfSuspendEvent.WaitOne(timeout, false);
                SetExecutionState(oldState);
                return timeoutFlag;
            }
        }

        public override void Start()
        {
            _workingThread =
                new Thread
                    (
                    StartInternal) {Name = Name, IsBackground = true};

            SetExecutionState(ProcessExecutionState.Running);

            _workingThread.Start();
        }

        public override void Abort()
        {
            base.Abort();

            _workingThread.Abort();
            OnTerminated(new ProcessExitEventArgs {CompletionState = ProcessExitCode.Terminated});
        }

        public override void Stop()
        {
            //TODO: (SD) Review the bellow code as it points into the fact that
            // granularity of the base.Stop is not enough. We need to give a chance to
            // the process to stop itself if it is not in the WaitSleepJoin state.
            SetExecutionState(ProcessExecutionState.StopRequested);

            if (_workingThread != null) //TODO: (SD) to be more granular about it
            {
                _workingThread.Interrupt();

                _workingThread.Join();
            }
            base.Stop();
            OnTerminated(new ProcessExitEventArgs {CompletionState = ProcessExitCode.Terminated});
        }

        public override IAsyncResult BeginStop(object state, AsyncCallback callback)
        {
            // Very raw for a moment, just proof of concept (SD)
            VoidAction joinDelegate = Stop;

            return joinDelegate.BeginInvoke
                (
                callback,
                state
                );
        }

        #endregion Methods
    }
}