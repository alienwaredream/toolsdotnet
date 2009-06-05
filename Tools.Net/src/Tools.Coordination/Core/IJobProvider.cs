namespace Tools.Coordination.Core
{
    public interface IJobProvider<JobType>
    {
        JobType GetNextItem();
    }
}