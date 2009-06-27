using System;

using Tools.Coordination.Batch;

namespace Tools.Operations.Cleanup.Implementation
{
    internal class FilesZipper : ScheduleTaskProcessor
    {
        protected override void ExecuteSheduleTask()
        {
            Log.TraceData(Log.Source, System.Diagnostics.TraceEventType.Information, CleanupMessages.CleanupIterationStarted, "Started cleanup iteration");
        }
    }
}