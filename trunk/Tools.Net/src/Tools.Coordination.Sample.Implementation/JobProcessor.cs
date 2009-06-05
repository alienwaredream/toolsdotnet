using System;
using System.Diagnostics;
using System.Threading;
using Tools.Core;
using Tools.Core.Context;
using Tools.Coordination.Core;
using Tools.Coordination.WorkItems;

namespace Tools.Coordination.Sample.Implementation
{
    public class JobProcessor : Descriptor, IJobProcessor<Job>
    {

        #region IJobProcessor<Job> Members

        public void ProcessJobWithEventCallback(
            Job job, WorkItem workItem, JobProcessedDelegate jobProcessedDelegate,
            SubmittingJobDelegate submittingJobDelegate)
        {
            Log.Source.TraceData(TraceEventType.Verbose,
                                 0, String.Format("processor called, simulating a delay. InternalId={0}",
                                                  workItem.ContextIdentifier.InternalId));
            Thread.Sleep(5000);

            jobProcessedDelegate(new JobProcessedEventArgs
                                         {
                                             Retry = false,
                                             Success = true,
                                             WorkItem = workItem,
                                             OperationContextShortcut =
                                                 (workItem == null) ? new ContextIdentifier() : workItem.ContextIdentifier
                                         });
        }

        #endregion
    }
}