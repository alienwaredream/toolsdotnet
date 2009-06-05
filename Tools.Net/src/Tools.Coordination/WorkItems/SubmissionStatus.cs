namespace Tools.Coordination.WorkItems
{
    public enum SubmissionStatus
    {
        None = 0,
        RestoringOperationContext = 1,
        AddingToSubmittedItems = 2,
        AddedToSubmittedItems = 3,
        SubmittedToSender = 4,
        LoggingSubmissionTimeOut = 5,
        PreHandlingJob = 6,
        JobPreHandled = 7,
        InvalidItemType = 8,
        JobPreHandledAndLoggingUnsuccess = 9,
        JobSubmittedToSender = 10,
        JobPreHandledAndSendingToFailedQueue = 11,
        SendingMessageToTheSenderTimedOut = 12,
        Retrieving = 13,
        Starting = 14,
        Retrieved = 15,
        CheckingItemType = 16,
        UpdatingJobProcessingStatus = 17,
        SubmittedItemsCounterIncremented = 18,
        SubmissionCompleted = 19,
        JobPreHandledAndSentToFailedQueue = 20
    }
}