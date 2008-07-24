using System;
using System.Threading;
using Tools.Core;

namespace Tools.Processes.Core
{
    //TODO:(SD) Refactor to have an interface
	/// <summary>
	/// Summary description for ThreadedProcess.
	/// </summary>
	public abstract class ThreadedProcess : Process
	{
		#region Fields

		private Thread							_workingThread = null;
		private ManualResetEvent				_selfSuspendEvent = 
			new ManualResetEvent(true);

		private ManualResetEvent					_operationReset = new
			ManualResetEvent(false);

		#endregion Fields

		#region Constructors

		protected ThreadedProcess()
			: base()
		{

		}

		
		protected ThreadedProcess
			(
			string name, 
			string description
			) : base
			(
			name, 
			description
			)
		{
			
		}

		
		#endregion Constructors

		#region Properties

		protected Thread WorkingThread
		{
			get
			{
				return _workingThread;
			}
		}

		/// <summary>
		/// For reseting the blocking operation the thread might be located in.
		/// </summary>
		public ManualResetEvent OperationReset
		{
			get
			{
				return _operationReset;
			}
		}
		#endregion Properties

		#region Methods
		// TODO: change to CLS compliant names when name is given a thought (SD)
		protected abstract void start();

		protected bool SelfSuspend(TimeSpan timeout)
		{
			lock (executionStateSyncObj)
			{
				ProcessExecutionState oldState = this.ExecutionState;
				this.SetExecutionState(ProcessExecutionState.SelfSuspended);
				_selfSuspendEvent.Reset();
				bool timeoutFlag = _selfSuspendEvent.WaitOne(timeout, false);
				this.SetExecutionState(oldState);
				return timeoutFlag;
			}
		}
		protected void SelfResume()
		{
			lock (executionStateSyncObj)
			{
				if (this.ExecutionState==ProcessExecutionState.SelfSuspended)
				{
					_selfSuspendEvent.Set();
					return;
				}
				throw new ApplicationException
					(
					"Incorrect state for self resume. Current state is" + executionStateSyncObj
					);
			}
		}
		protected virtual void stop()
		{
			base.Stop();

			// Set the handle for the blocking operation (SD)
			OperationReset.Set();
			// TODO: Calling of interrupt is subject for further reviews (SD)
            //TODO:(SD) Setup use of working thread even for a threaded process to be an option.
            if (_workingThread != null)
            {
                _workingThread.Interrupt();

                _workingThread.Join();
            }
			// Let all registered to complete their actions connected to this stop.
			OnStopping();
            //
            OnTerminated(new ProcessExitEventArgs(ProcessExitCode.Terminated));
			// And now we can signal that we have finished.
			CompletedEvent.Set();

		}

		public override void Start()
		{
			_workingThread = 
				new Thread
				(
				new ThreadStart
				(
				this.start
				));

			_workingThread.Name = Name;
			_workingThread.IsBackground = true;

			_workingThread.Start();

			SetExecutionState(ProcessExecutionState.Running);
		}

		public override void Abort()
		{
			base.Abort();

			_workingThread.Abort();
            OnTerminated(new ProcessExitEventArgs(ProcessExitCode.Terminated));

		}
		public override void Stop()
		{
			stop();
		}

		public override IAsyncResult BeginStop(object state, AsyncCallback callback)
		{
			// Very raw for a moment, just proof of concept (SD)
			VoidAction joinDelegate = new VoidAction(Stop);
			
			return joinDelegate.BeginInvoke
				(
				new AsyncCallback
				(
				callback
				),
				new Descriptor(Name, Description)
				);
		}
		//		public override void EndStop(IAsyncResult ar)
		//		{
		//			VoidDelegate joinDelegate = (ar as AsyncResult).AsyncDelegate as VoidDelegate;
		//
		//			try
		//			{
		//				joinDelegate.EndInvoke(ar);
		//				// TODO: Handle the case for that separately in the case of exception (SD)
		//				OnStopped();
		//			}
		//			catch (Exception ex)
		//			{
		//				
		//				throw ex;
		//			}
		//		}

		#endregion Methods

        protected override void OnStopped()
        {
            base.OnStopped();
            //throw new NotImplementedException();
        }
    }
}
