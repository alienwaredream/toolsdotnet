using System;
using System.Diagnostics;
using System.Threading;

namespace Tools.Processes.Core
{
    /// <summary>
    /// Summary description for Process.
    /// </summary>
    [Serializable]
    public abstract class Process : MarshalByRefObject, IProcess, IDisposable
    {
        #region Fields

        // that is made protected only for testability (SD)
        protected EventWaitHandle completedEvent =
            new ManualResetEvent(false);

        private volatile ProcessExecutionState executionState = ProcessExecutionState.Unstarted;
        private readonly object executionStateSyncObj = new object();

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Process"/> class.
        /// </summary>
        protected Process() :
            this("Process:" + Guid.NewGuid(),
                 "Process with automatically assigned name")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Process"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        protected Process(string name, string description)
        {
            Name = name;
            Description = description;
        }

        #endregion Constructors

        #region Properties
        /// <summary>
        /// Gets the completed event.
        /// </summary>
        /// <value>The completed event.</value>
        protected EventWaitHandle CompletedEvent
        {
            get { return completedEvent; }
        } 
        #endregion

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (CompletedEvent != null)
                CompletedEvent.Close();
        }

        #endregion

        #region IProcess Members

        /// <summary>
        /// Only providing the WaitHandle for outside access so outsiders can only
        /// wait, not control.
        /// </summary>
        public WaitHandle CompletedHandle
        {
            get { return completedEvent; }
        }

        /// <summary>
        /// Initializes this instance. Does nothing by default, override to provide some initialization work.
        /// </summary>
        public virtual void Initialize()
        {
            //
        }

        /// <summary>
        /// When implemented by the child class - starts the instance execution
        /// </summary>
        public abstract void Start();

        /// <summary>
        /// When implemented by the child class - stops the instance execution.
        /// This implementation only sets the execution state to <see cref="ProcessExecutionState.StopRequested"/>
        /// </summary>
        public virtual void Stop()
        {
            Log.Source.TraceData(TraceEventType.Stop, 0, string.Format("{0} is requested to Stop.",Name));
            OnStopping();
            OnStopped();
        }

        /// <summary>
        /// When implemented by the child class - aborts the instance execution.
        /// This implementation only sets the execution state to <see cref="ProcessExecutionState.AbortRequested"/>
        /// </summary>
        public virtual void Abort()
        {
            SetExecutionState(ProcessExecutionState.AbortRequested);
        }

        /// <summary>
        /// Gets current <see cref="ProcessExecutionState"/>. Thread safe.
        /// </summary>
        public ProcessExecutionState ExecutionState
        {
            get
            {
                lock (ExecutionStateSyncObj)
                {
                    return executionState;
                }
            }
        }

        public string Name { get; set; }

        /// <summary>
        /// Provides the description for the "thing" we need to describe.
        /// When implementing this member of interface and the type is supposed to be serializable to xml
        /// then internal guideline is to serialize it as an element as opposed to the <see cref="Name"/>
        /// </summary>
        /// <value></value>
        public string Description { get; set; }

        public object ExecutionStateSyncObj
        {
            get { return executionStateSyncObj; }
        }

        #endregion

        #region Events


        /// <summary>
        /// Event called inside Stop method. After finishing calling 
        /// this event the state is move from StopRequested to Stoped.
        /// </summary>
        public event EventHandler Stopping;

        public event EventHandler<ProcessExitEventArgs> Completed;

        public event EventHandler<ProcessExitEventArgs> Terminated;

        public event EventHandler Stopped;

        #endregion

        #region Non-implemented IProcess methods

        /// <summary>
        /// Provides async implementation of StopInternal
        /// </summary>
        /// <param name="state"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public virtual IAsyncResult BeginStop(object state, AsyncCallback callback)
        {
            throw new NotImplementedException
                (
                "BeginStop() method of the Process has not been implemented yet."
                );
        }

        /// <summary>
        /// Ends the StopInternal.
        /// </summary>
        /// <param name="ar">The ar.</param>
        public virtual void EndStop(IAsyncResult ar)
        {
            throw new NotImplementedException
                (
                "EndStop() method of the Process has not been implemented yet."
                );
        }

        /// <summary>
        /// Resumes this instance.
        /// </summary>
        public virtual void Resume()
        {
            throw new NotImplementedException
                (
                "Resume() method of the Process has not been implemented yet."
                );
        }

        /// <summary>
        /// Suspends this instance.
        /// </summary>
        public virtual void Suspend()
        {
            throw new NotImplementedException
                (
                "Suspend() method of the Process has not been implemented yet."
                );
        }

        #endregion Non-implemented IProcess methods

        #region Methods


        /// <summary>
        /// Thread safe method for setting the execution state by a child.
        /// </summary>
        /// <param name="state"></param>
        protected void SetExecutionState(ProcessExecutionState state)
        {
            lock (ExecutionStateSyncObj)
            {
                executionState = state;
            }
        } 
        #endregion

        #region OnEvent Methods
        /// <summary>
        /// Provides default implementation of the Completed pre-handler
        /// </summary>
        /// <param name="eventArgs"></param>
        protected virtual void OnCompleted(ProcessExitEventArgs eventArgs)
        {
            SetExecutionState(ProcessExecutionState.Completed);
            if (Completed != null)
                Completed(this, eventArgs);

            CompletedEvent.Set();
        }
        /// <summary>
        /// Called when [stopping].
        /// </summary>
        protected virtual void OnStopping()
        {
            SetExecutionState(ProcessExecutionState.StopRequested);

            if (Stopping != null)
            {
                Stopping(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Provides default implementation of the Completed pre-handler
        /// </summary>
        /// <param name="eventArgs"></param>
        protected virtual void OnTerminated(ProcessExitEventArgs eventArgs)
        {
            SetExecutionState(ProcessExecutionState.Terminated);
            // first notify handlers
            if (Terminated != null)
                Terminated(this, eventArgs);
            // signal waiters
            CompletedEvent.Set();
        }

        protected virtual void OnStopped()
        {
            SetExecutionState(ProcessExecutionState.Stopped);

            Log.Source.TraceData(TraceEventType.Stop, 0, string.Format
                                        (
                                        "{0} process is stopped. Stopped event is about to be raised.",
                                        Name
                                        ));

            if (Stopped != null)
            {
                Stopped(this, EventArgs.Empty);
            }
        } 
        #endregion
    }
}