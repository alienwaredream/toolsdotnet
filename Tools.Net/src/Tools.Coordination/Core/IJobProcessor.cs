using Tools.Core;
using Tools.Coordination.WorkItems;

namespace Tools.Coordination.Core
{
    public interface IJobProcessor<JobType> : IDescriptor
    {
        void ProcessJobWithEventCallback
            (
            JobType job,
            WorkItem workItem,
            JobProcessedDelegate jobProcessedDelegate,
            SubmittingJobDelegate submittingJobDelegate
            );
    }
}