namespace Tools.Coordination.Core
{
    public interface IResultHandler
    {
        event JobCompletedEventHandler PendingResultObtained;
        event JobCompletedEventHandler RegularResultObtained;
    }
}