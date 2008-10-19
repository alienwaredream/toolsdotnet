namespace Tools.Processes.Core
{
    /// <summary>
    /// Summary description for ProcessMessage.
    /// </summary>
    public enum ProcessMessage
    {
        None = 0,
        Initialized = 1500,
        StartRequested = 1501,
        Started = 1502,
        StopRequested = 1503,
        Stopped = 1504,
    }
}