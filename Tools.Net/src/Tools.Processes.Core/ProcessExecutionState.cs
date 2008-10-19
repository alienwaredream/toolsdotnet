using System;

namespace Tools.Processes.Core
{

    #region Enum ProcessExecutionState

    // TODO: Should there be any "Aborted" state? (SD)
    /// <summary>
    /// Provides possible states of the process/activity which is executed.
    /// While rendering almost all values from ThreadState has its specifics
    /// when compared to the physical nature of the Thread.
    /// </summary>
    [Flags]
    public enum ProcessExecutionState
    {
        /// <summary>
        /// Only for initialization states.
        /// </summary>
        None = 0,
        /// <summary>
        /// The IProcess has been started, it is not blocked, and there is no pending ProcessAbortException.
        /// </summary>
        Running = 1,
        /// <summary>
        /// The IProcess is being requested to StopInternal (by calling its Stop method). 
        /// This is an external or internal
        /// intervention of StopInternal. Either conditions where not fulfilled to run or an exceptional
        /// state was encoutered. In case when StopInternal is intervened due to the exceptional state 
        /// set the corresponding state within IProcess and throw/re-throw an exception then.
        /// Once the IProcess achieved the consistent and clean state while stopping
        /// it should set its ExecutionState to Stopped.
        /// </summary>
        StopRequested = 2,
        /// <summary>
        /// The IProcess is being requested to suspend.
        /// </summary>
        SuspendRequested = 4,
        /// <summary>
        /// The IProcess.Start method has not been invoked on the IProcess.
        /// </summary>
        Unstarted = 8,
        /// <summary>
        /// The IProcess has stopped.
        /// Stopped state can be achieved by either calling to the Stop or Abort.
        /// Normally call to the Stop should preceed the call to the Abort.
        /// TODO: Think about the dedicated aborted state.
        /// </summary>
        Stopped = 16,
        /// <summary>
        /// The IProcess is in the waiting state. E.g. waiting for a resource to become
        /// available. Where "resource" is not always the kind of "resource" as meant
        /// for thread. 
        /// 
        /// </summary>
        WaitSleepJoin = 32,
        /// <summary>
        /// The IProcess has been suspended.
        /// Tr.: To cause something to StopInternal temporarily.
        /// </summary>
        Suspended = 64,
        /// <summary>
        /// The IProcess.Abort method has been invoked on the IProcess, but the IProcess has not yet received the pending ProcessAbortException that will attempt to terminate it.
        /// </summary>
        AbortRequested = 128,
        /// <summary>
        /// IProcess has been finished in the natural way. It is supposed that all
        /// clean-up operation will take place in the IProcess before setting this state.
        /// </summary>
        Finished = 256,
        SelfSuspended = 512,
        /// <summary>
        /// Process is running but doing no processing.
        /// </summary>
        Idle = 1024,
        /// <summary>
        /// Process is running and processing its work items.
        /// TODO: Find better name for it.
        /// </summary>
        NonIdle = 2048,
        Completed = 4096,
        Terminated = 8192,
    }

    #endregion
}