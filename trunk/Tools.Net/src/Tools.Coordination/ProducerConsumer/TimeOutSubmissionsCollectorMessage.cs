namespace Tools.Coordination.ProducerConsumer
{
    /// <summary>
    /// Summary description for TimeOutSubmissionsCollectorMessage.
    /// </summary>
    public enum TimeOutSubmissionsCollectorMessage
    {
        TimeOutSubmissionsCollectorStarted = 3900,
        BreakingFromInfiniteLoop = 3901,
        TimeOutSubmissionsCollectorStopped = 3903,
        UnexpectedErrorOccured = 3950,
        RegularCollectionStopTimeout = 3951,
        FinalCollectionTimeout = 3952,
        ErrorDuringFinalCollection = 3953,
        RemovingItemFromSubmittedItems = 9700,
        ResponseFromExternalModuleDelayed = 9702,
        RegularShutdownCollectionStarted = 9703,
        RegularShutdownCollectionFinished = 9704,
        RegularCollectionStarted = 9705,
        RegularCollectionFinished = 9706,
        RemovalFromSubmittedItemsFailed = 9751,
        OrphanedItemProbability = 9752,
        ItemRemovedFromSubmittedItems = 9753,
    }
}