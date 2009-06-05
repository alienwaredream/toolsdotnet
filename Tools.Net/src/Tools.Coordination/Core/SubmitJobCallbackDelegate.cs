using Tools.Coordination.WorkItems;

namespace Tools.Coordination.Core
{
    public delegate void SubmitJobCallbackDelegate<JobType>
        (
        JobType job,
        WorkItem workItem,
        JobProcessedDelegate jobProcessedDelegate,
        SubmittingJobDelegate submittingJobDelegate
        );
}