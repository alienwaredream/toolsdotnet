using Tools.Core;
using Tools.Coordination.WorkItems;
using System;

namespace Tools.Coordination.Core
{
    public interface IJobProcessor<JobType> : IDescriptor, IDisposable
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