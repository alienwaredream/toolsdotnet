namespace Tools.Coordination.ProducerConsumer
{
    /// <summary>
    /// Summary description for ConsumerManagerMessage.
    /// </summary>
    public enum ConsumerManagerMessage
    {
        StartingConsuming = 10600,
        StoppingConsuming = 10601,
        QueueWorkItemsConsumerManagerStartRequested = 10602,
        QueueWorkItemsConsumerManagerStarted = 10603,
        QueueWorkItemsConsumerManagerStopRequested = 10604,
        QueueWorkItemsConsumerManagerStopped = 10605,

        AbortingConsuming = 10650,
        ConsumersNotInstantiated = 10651,
        ConsumersStoppingTimeoutError = 10652,
    }
}