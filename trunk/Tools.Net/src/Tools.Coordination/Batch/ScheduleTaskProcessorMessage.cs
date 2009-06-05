namespace Tools.Coordination.Batch
{
    /// <summary>
    /// Summary description for ProducerMessage.
    /// </summary>
    public enum ScheduleTaskProcessorMessage
    {
        Started = 2702,
        FinishingNormally = 2704,
        ThreadInterrupted = 2705,
        Stopped = 2706,
        AbortRequested = 2707,
        ScheduledTaskIsAboutToBeExecuted = 2708,
        SuspendingTheProcessUntilTheNextRun = 2709,
        ErrorWhileExecutingScheduledTask = 2751
    }
}