namespace Tools.Coordination.ProducerConsumer
{
    /// <summary>
    /// Summary description for ConsumerMessage.
    /// </summary>
    public enum ConsumerMessage
    {
        // TODO: add to DB (SD)
        StartingThread = 9600,
        // TODO: add to DB (SD)
        AbortingThread = 9601,
        QueueWorkItemsConsumerStopRequested = 9602,
        QueueWorkItemsConsumerStopped = 9603,

        // TODO: Add to DB (MS)					
        QueueWorkItemsConsumerStartRequested = 9604,
        // TODO: Add to DB (MS)					
        QueueWorkItemsConsumerStarted = 9605,
        // TODO: add to DB
        ReturningMessageToMQ = 9606,
        /// <summary>
        /// Happens when work item is retrieved from the store.
        /// </summary>
        WorkItemRetrieved = 9607,
        RetrievedItemReturnedToQueue = 9608,

        // TODO: add to DB (SD)
        StartRequestNotExpected = 9650,
        // TODO: add to DB (SD)
        ThreadStartFailed = 9651,
        // TODO: add to DB (SD)
        ThreadAbortFailed = 9652,
        //
        ErrorWhileStoppingConsumer = 9653,
        RetrievedItemCantBeReturned = 9654,
    }
}