namespace Tools.Coordination.ProducerConsumer
{
    /// <summary>
    /// Summary description for RetrievedItemsCleanerMessage.
    /// </summary>
    public enum RetrievedItemsCleanerMessage
    {
        RetrievedItemsCleanerStartRequested = 11000,
        RetrievedItemsCleanerStarted = 11001,
        // TODO: add to DB (SD)
        SavingRetrievedMessagesStarted = 11002,
        // TODO: add to DB (SD)
        RetrievedMessageSuccessfullySaved = 11003,
        // TODO: add to DB (SD)
        FinishingNormally = 11004,
        RetrievedItemsCleanerStopRequested = 11005,
        RetrievedItemsCleanerStopped = 11006,
        RetrievedItemsCleanerFinishingRetrieving = 11007,

        // TODO: add to DB (SD)
        AbortRequested = 11050,

        // Errors

        // TODO: add to DB (SD)
        SavingRetrievedMessagesFailed = 11051,
        // TODO: add to DB (SD)
        SavingRetrievedMessageFailed = 11052,
        SendingMessageToMSMQFailed = 11053,
        //
    }
}