using System;
using System.Threading;
using Tools.Core;

namespace Tools.Processes.Core
{
	#region Interface IProcess

	/// <summary>
	/// Denotes the process or activity which can be started, interrupted, stopped 
	/// either externally or due to the internal reasons.
	/// Even if copying the most of the Thread logic it just follows the need to
	/// have a run state and start, interrupt, but also specific states to wait
	/// and sleep conditions that are distinct from the Thread's.
	/// It requires to implement also <see cref="IDescriptor"/> as far as we are expected
	/// to be able to identify and describe the running activity.
	/// </summary>
	public interface IProcess : IDescriptor
	{
        void Initialize();
		/// <summary>
		/// 
		/// </summary>
		void Start();
		/// <summary>
		/// 
		/// </summary>
		void Stop();
		/// <summary>
		/// Provides async implementation of stop
		/// </summary>
		/// <returns></returns>
		IAsyncResult BeginStop(object state,AsyncCallback callback);

		void EndStop(IAsyncResult ar);
		/// <summary>
		/// 
		/// </summary>
		void Abort();

		void Suspend();

		void Wait();

		void Resume();

		ProcessExecutionState ExecutionState {get;}

		event System.EventHandler Stopping;

        event System.EventHandler<ProcessExitEventArgs> Completed;
        event System.EventHandler<ProcessExitEventArgs> Terminated;

        WaitHandle CompletedHandle { get;}

        event System.EventHandler Stopped;
	}

	#endregion
}
