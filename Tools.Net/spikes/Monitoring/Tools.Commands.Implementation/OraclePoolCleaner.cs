using System;
using Tools.Coordination.Batch;
using System.Diagnostics;

namespace Tools.Commands.Implementation
{
    public  class OraclePoolCleaner : ScheduleTaskProcessor
    {
        Guid poolCleaningActivity = Guid.NewGuid();
        bool startup = true;

        protected override void ExecuteSheduleTask()
        {
            if (!startup)
            {
                startup = false;

                Trace.CorrelationManager.ActivityId = poolCleaningActivity;

                System.Data.OracleClient.OracleConnection.ClearAllPools();

                Log.TraceData(Log.Source, System.Diagnostics.TraceEventType.Verbose, CommandMessages.CleaningConnectionsPool, String.Format("ClearAllPools command issued"));


            }
        }
    }
}