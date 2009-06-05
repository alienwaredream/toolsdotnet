namespace Tools.Coordination.WorkItems
{
    /// <summary>
    /// Summary description for WorkItemProcessStatus.
    /// </summary>
    public enum WorkItemProcessStatus
    {
        None = 0,
        //Obtaining							= 1,
        Obtained = 2,
        SubmittedToSubmittedItems = 3,
        //SubmittedToSubmittedQueue			= 4,
        RemovedFromSubmittedItems = 5,
        ResponseReceived = 6,
        SubmittedToReceived = 7,
        //RemovedFromSubmittedQueue			= 8,
        SubmittedToFailed = 9,
        Aborting = 10,
        Cleaning = 11,
        ResubmissionInitiated = 12,
        PostResponseReceivedWorkDone = 13,
        SubmittedToExternalRecipient = 20,
        DelayNotifiedAfterSubmissionToExternalRecipient = 21
    }
}