namespace Tools.Coordination.WorkItems
{

    #region ResponseReceivedStatus enum

    /// <summary>
    /// Summary description for ResponseReceivedStatus.
    /// </summary>
    public enum ResponseReceivedStatus
    {
        ResponseObtained,
        EntryObtainedFromSubmittedItems,
        ObtainedEntryChecked,
        StateQueueWorkItemCreated,
        StateQueueWorkItemChecked,
        ItemRemovedFromSubmittedItems,
        SubmittedItemsCounterDecremented,
        ParamsUpdated,
        JonAndTransactionUpdated,
        MessageSentToResponseReceived,
        RegularResponseObtainedFired,
        ContextualTransactionRequestCreated,
        ContextualTransactionRequestChecked,
        FailedToSentResponseMessageToResponseQueue,
        MessageSentToPendingQueue,
        PendingResponseObtainedFired,
        MessageSendingToResponseReceivedSkipped
    }

    #endregion
}