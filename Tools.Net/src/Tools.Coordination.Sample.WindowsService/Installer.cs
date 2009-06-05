using System.ComponentModel;
using System.Diagnostics;

namespace Tools.Coordination.Sample.WindowsService
{
    [RunInstaller(true)]
    public partial class Installer : Processes.Host.Installer
    {
        protected void SetupCounters()
        {
            var performanceCounterInstaller =
                new PerformanceCounterInstaller
                    {
                        CategoryName = ServiceName
                    };

            Installers.Add(performanceCounterInstaller);

            #region Processing - item processing time

            var turnoverTimeCounterCreation = new CounterCreationData
                                                {
                                                    CounterName = "Item turnover time, ms",
                                                    CounterHelp = "Time, in milliseconds, to process the single item.",
                                                    CounterType = PerformanceCounterType.AverageTimer32
                                                };
            performanceCounterInstaller.Counters.Add(turnoverTimeCounterCreation);

            CounterCreationData familyTimeAverageCounterCreation = new CounterCreationData();
            familyTimeAverageCounterCreation.CounterName = "Item turnover time base, ms";
            familyTimeAverageCounterCreation.CounterHelp = "Time, in milliseconds, to process single item.";
            familyTimeAverageCounterCreation.CounterType = PerformanceCounterType.AverageBase;
            performanceCounterInstaller.Counters.Add(familyTimeAverageCounterCreation);

            // Add a counter to collection of  performanceCounterInstaller.

            #endregion Processing - item processing time

            #region Processing - Rate of items production per second

            var queueCounterCreation = new CounterCreationData
                                           {
                                               CounterName = "Produced items/sec",
                                               CounterType = PerformanceCounterType.RateOfCountsPerSecond32,
                                               CounterHelp =
                                                   "Rate of items production per second."
                                           };
            // Add a counter to collection of  performanceCounterInstaller.
            performanceCounterInstaller.Counters.Add(queueCounterCreation);

            #endregion Processing - Rate of items production per second
        }
    }
}