namespace Tools.Coordination.ProducerConsumer
{
    /// <summary>
    /// Summary description for RetrievedItemsCleanerManagerMessage.
    /// </summary>
    public enum RetrievedItemsCleanerManagerMessage
    {
        RetrievedItemsCleanerManagerStartRequested = 10900,
        RetrievedItemsCleanerManagerStarted = 10901,
        RetrievedItemsCleanerManagerStopRequested = 10902,
        RetrievedItemsCleanerManagerStopped = 10903,

        SendingMessagetoMSMQFailed = 10950,
    }
}