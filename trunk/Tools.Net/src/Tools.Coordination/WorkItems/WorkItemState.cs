namespace Tools.Coordination.WorkItems
{
    // TODO: rename to WorkItemStatus
    /// <summary>
    /// All states that can be re-assigned for resubmission should have numbers below 30.
    /// </summary>
    public enum WorkItemState : short
    {
        /// <summary>
        /// Initialized state, is not legal and should be set by the WorkItem 
        /// creator.
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// Waiting for submission to external entity
        /// </summary>
        AvailableForProcessing = 10,
        //AvailableForProcessing = 10,		
        /// <summary>
        /// Waiting for connection to external entity
        /// </summary>
        WaitingForConnection = 20,

        /// <summary>
        /// Submitted to external entity.
        /// </summary>
        Submitted = 30,

        /// <summary>
        /// Assigned when external entity has not responded within the setup timeout.
        /// </summary>
        ExternalEntityResponseDelayed = 40,
        /// <summary>
        /// An error occurred
        /// </summary>
        Error = 100,
        /// <summary>
        /// Transition to this status occurs when we can't continue with item
        /// processing as business flow state is not consistent with required for the
        /// item operation.
        /// Example:
        /// We pick up <see cref="WorkItem"/> for pending resubmission and job has been manually
        /// resolved till this moment. We will check the job state before processing the pending
        /// resubmission operation and cancel the operation. WorkItem state will be updated to Cancelled.
        /// </summary>
        Cancelled = 150,
        /// <summary>
        /// Completed processing.
        /// </summary>
        Completed = 200,
    }
}