using System;
using System.Threading;

namespace Tools.Processes.Core
{
	/// <summary>
	/// Summary description for Process.
	/// </summary>
	public abstract class Process : MarshalByRefObject, IProcess, IDisposable
	{
		#region Global Declarations

		private string name;
		private string description;
		//
		private ProcessExecutionState executionState = ProcessExecutionState.Unstarted;
		private ProcessCompletionStatus completionStatus = ProcessCompletionStatus.Unknown;

		// TODO: correct to guidelines ASAP (SD)
		protected object executionStateSyncObj = new object();
		protected object completionStatusSyncObj = new object();

        private ManualResetEvent _completedEvent =
            new ManualResetEvent(false);

		#endregion Global Declarations
		
		#region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Process"/> class.
        /// </summary>
		protected Process(): 
            this ("Process:"+Guid.NewGuid().ToString(), 
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
			this.name = name;
			this.description = description;
		}

		#endregion Constructors
		
		#region IProcess Members

        /// <summary>
        /// Only providing the WaitHandle for outside access so outsiders can only
        /// wait, not control.
        /// </summary>
        public WaitHandle CompletedHandle
        {
            get
            {
                return _completedEvent;
            }
        }
        /// <summary>
        /// Gets the completed event.
        /// </summary>
        /// <value>The completed event.</value>
        protected ManualResetEvent CompletedEvent
        {
            get
            {
                return _completedEvent;
            }
        }

        /// <summary>
        /// Initializes this instance. Does nothing by default, override to provide some initialization work.
        /// </summary>
        public virtual void Initialize()
        {
            //
        }

        /// <summary>
        /// Called when [stopping].
        /// </summary>
		protected virtual void OnStopping()
		{
			SetExecutionState(ProcessExecutionState.Stopped);
		
			if (Stopping != null)
			{
				Stopping(this, System.EventArgs.Empty);
			}
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
			SetExecutionState(ProcessExecutionState.StopRequested);
            OnStopping();

            SetExecutionState(ProcessExecutionState.Stopped);
            _completedEvent.Set();
		}

        /// <summary>
        /// When implemented by the child class - aborts the instance execution.
        /// This implementation only sets the execution state to <see cref="ProcessExecutionState.AbortRequested"/>
        /// </summary>
		public virtual void Abort()
		{
			SetExecutionState(ProcessExecutionState.AbortRequested);
		}

		#region Non-implemented IProcess methods

        /// <summary>
        /// Provides async implementation of stop
        /// </summary>
        /// <param name="state"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
		public virtual IAsyncResult BeginStop(object state,AsyncCallback callback)
		{
			throw new NotImplementedException
				(
				"BeginStop() method of the Process has not been implemented yet."
				);		
		}

        /// <summary>
        /// Ends the stop.
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
        /// Waits this instance.
        /// </summary>
		public virtual void Wait()
		{
			throw new NotImplementedException
				(
				"Wait() method of the Process has not been implemented yet."
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
				"Suspend() method of the ProcessManagementWrapper has not been implemented yet."
				);
		}

		#endregion Non-implemented IProcess methods

		/// <summary>
		/// Gets current <see cref="ProcessExecutionState"/>. Thread safe.
		/// </summary>
		public ProcessExecutionState ExecutionState
		{
			get
			{
				lock (executionStateSyncObj)
				{
					return this.executionState;
				}
			}
		}
        /// <summary>
        /// Gets or sets the completion status.
        /// </summary>
        /// <value>The completion status.</value>
		public ProcessCompletionStatus CompletionStatus
		{
			get
			{
				lock (completionStatusSyncObj)
				{
					return this.completionStatus;
				}
			}
			set
			{
				lock (completionStatusSyncObj)
				{
					this.completionStatus = value;
				}
			}
		}
		/// <summary>
		/// Thread safe method for setting the execution state by a child.
		/// </summary>
		/// <param name="state"></param>
		protected void SetExecutionState(ProcessExecutionState state)
		{
			lock (executionStateSyncObj)
			{
				this.executionState = state;
			}
		}

        /// <summary>
        /// Event called inside Stop method. After finishing calling 
        /// this event the state is move from StopRequested to Stoped.
        /// </summary>
		public event EventHandler Stopping;

        public event System.EventHandler<ProcessExitEventArgs> Completed;
        /// <summary>
        /// Provides default implementation of the Completed pre-handler
        /// </summary>
        /// <param name="eventArgs"></param>
        protected virtual void OnCompleted(ProcessExitEventArgs eventArgs)
        {
            if (Completed != null)
                Completed(this, eventArgs);
            SetExecutionState(ProcessExecutionState.Completed);
            CompletedEvent.Set();
        }
        public event System.EventHandler<ProcessExitEventArgs> Terminated;
        /// <summary>
        /// Provides default implementation of the Completed pre-handler
        /// </summary>
        /// <param name="eventArgs"></param>
        protected virtual void OnTerminated(ProcessExitEventArgs eventArgs)
        {
            if (Terminated != null)
                Terminated(this, eventArgs);
            SetExecutionState(ProcessExecutionState.Terminated);
            CompletedEvent.Set();
        }

		#endregion

		#region IDescriptor Members

		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

        /// <summary>
        /// Provides the description for the "thing" we need to describe.
        /// When implementing this member of interface and the type is supposed to be serializable to xml
        /// then internal guideline is to serialize it as an element as opposed to the <see cref="Name"/>
        /// </summary>
        /// <value></value>
		public string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
			}
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

        protected virtual void OnStopped()
        {
            SetExecutionState(ProcessExecutionState.Stopped);

            if (Stopped != null)
            {
                Stopped(this, System.EventArgs.Empty);
            }

        }
        public event System.EventHandler Stopped;
    }
}
