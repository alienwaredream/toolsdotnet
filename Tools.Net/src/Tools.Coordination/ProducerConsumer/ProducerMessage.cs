namespace Tools.Coordination.ProducerConsumer
{
    /// <summary>
    /// Summary description for ProducerMessage.
    /// </summary>
    public enum ProducerMessage
    {
        
        MessageRetrieved = 2700,
        
        MessageAddedToRetrievedItems = 2701,
        
        ThreadStarted = 2702,
        
        RequestedItemsCounterIncremented = 2703,
        
        FinishingNormally = 2704,
        
        ThreadInterrupted = 2705,
        
        AbortRequestedWhileReceiving = 2706,
        				
        QueueWorkItemsProducerStopped = 2707,
        				
        QueringForAnItem = 2708,
        				
        GettingMessageAfterInterruption = 2709,
        				
        ReceiveOnRetrievalQueueFinished = 2710,


        RetrieveMessageFailed = 2751,
        
        InvalidDataType = 2752,
        
        MessageAdditionToRetrievedItemsFailed = 2753,
        
        InvalidInputParameter = 2754,
        
        AbortRequested = 2755,
        
        RetrievingMessagesFailed = 2756,
        
        RetrievedItemNotExpected = 2757,
        
        RetrievedItemsCleanerNotInstantiated = 2758,
        				
        ErrorOccuredWhileReceivingFromRetrievalQueue = 2759,
        				
        ErrorProcessingInterruptionBlock = 2760,
        				
        ErrorStoringItemToRetrievedCollection = 2761,
        //
        InconsitentWorkItemState = 2762,
        RetrievedMessageNotExpected = 2763,
        RetrievedItemHasDifferentPriority = 2764,

        RetrievedMessageReturnedToTheRetrievalQueue = 14101,
        InvalidWorkItemLockExpirationTimeoutMsConfiguration = 14151,
        ErrorDuringObtainingTheWorkItem = 14152,
        ErrorDuringReconcilingTheQueuedItemWithReferenceSource = 14153,
    }
}