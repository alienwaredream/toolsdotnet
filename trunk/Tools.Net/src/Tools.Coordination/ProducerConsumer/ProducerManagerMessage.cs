namespace Tools.Coordination.ProducerConsumer
{
    /// <summary>
    /// Summary description for ProducerManagerMessage.
    /// </summary>
    public enum ProducerManagerMessage
    {
        // Regular events
        // TODO: add to DB (SD)
        StartingProducing = 10700,
        // TODO: add to DB (SD)
        StoppingProducing = 10701,
        // TODO: add to DB (SD)
        AbortingProducing = 10702,
        // TODO: Assign number and add to db (SD)
        ProducerCalledCallback = 10703,
        // Errors

        // TODO: add to DB (SD)
        ProducersNotInstantiated = 10750,
        // TODO: Assign number and add to db (SD)
        StoppedEventOwnerOfIncorrectType = 10751,
        ProducersStoppingTimeoutError = 10752,
        CleanerManagerStoppingTimeout = 10753,
        ErrorWhileStoppingProducer = 10754,
    }
}