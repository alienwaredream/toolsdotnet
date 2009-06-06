using System.ComponentModel;
using System.Diagnostics;

namespace Wds.EligLoad.Preprocessing.Batch.WindowsService
{
    [RunInstaller(true)]
    public partial class Installer : Tools.Processes.Host.Installer
    {
        public Installer()
        {
            InitializeComponent();

            SetupCounters();
        }

        protected void SetupCounters()
        {
            //TODO: (SD) All of that will be setup with Spring.net
            var performanceCounterInstaller =
                new PerformanceCounterInstaller
                    {
                        CategoryName = ServiceName
                    };

            Installers.Add(performanceCounterInstaller);

            #region Processing - item processing times

            var turnoverTimeCounterCreation = new CounterCreationData
                                                {
                                                    CounterName = "Family Preprocessing time, ms",
                                                    CounterHelp = "Time, in milliseconds, to preprocess the single family.",
                                                    CounterType = PerformanceCounterType.AverageTimer32
                                                };
            performanceCounterInstaller.Counters.Add(turnoverTimeCounterCreation);

            var familyTimeAverageCounterCreation = new CounterCreationData
                                                                       {
                                                                           CounterName = "Family Preprocessing time base, ms",
                                                                           CounterHelp =
                                                                               "Time, in milliseconds, to preprocess single family.",
                                                                           CounterType =
                                                                               PerformanceCounterType.AverageBase
                                                                       };
            performanceCounterInstaller.Counters.Add(familyTimeAverageCounterCreation);

            // Add a counter to collection of  performanceCounterInstaller.

            #endregion Processing - item processing time

            #region Processing - Rate of items production per second

            var queueCounterCreation = new CounterCreationData
                                           {
                                               CounterName = "Family/sec",
                                               CounterType = PerformanceCounterType.RateOfCountsPerSecond32,
                                               CounterHelp =
                                                   "Rate of families preprocessed per second."
                                           };
            // Add a counter to collection of  performanceCounterInstaller.
            performanceCounterInstaller.Counters.Add(queueCounterCreation);

            #endregion Processing - Rate of items production per second

            #region Processing - Statistics - Families

            var familyTotalCounterCreation = new CounterCreationData{
                CounterName = "Total families",
                CounterType = PerformanceCounterType.NumberOfItems32,
                CounterHelp = "Total of families"
                };
            performanceCounterInstaller.Counters.Add(familyTotalCounterCreation);

            var familyUnprocessedCounterCreation = new CounterCreationData {
                CounterName = "Total unprocessed families",
                CounterType = PerformanceCounterType.NumberOfItems32,
                CounterHelp = "Total of families that have not been processed"
                };
            performanceCounterInstaller.Counters.Add(familyUnprocessedCounterCreation);

            var familyUnprocessedPercentageCounterCreation = new CounterCreationData {
                CounterName = "% Unprocessed families",
                CounterType = PerformanceCounterType.NumberOfItems32,
                CounterHelp = "Percentage of families that have not been processed"
            };
            performanceCounterInstaller.Counters.Add(familyUnprocessedPercentageCounterCreation);

            var familyProcessedCounterCreation = new CounterCreationData {
                CounterName = "Total processed families",
                CounterType = PerformanceCounterType.NumberOfItems32,
                CounterHelp = "Total of families that have been processed"
                };
            performanceCounterInstaller.Counters.Add(familyProcessedCounterCreation);

            var familyProcessedPercentageCounterCreation = new CounterCreationData {
                CounterName = "% Processed families",
                CounterType = PerformanceCounterType.NumberOfItems32,
                CounterHelp = "Percentage of families that have been processed"
            };
            performanceCounterInstaller.Counters.Add(familyProcessedPercentageCounterCreation);

            #endregion

            #region Processing - Statistics - Transactions

            var transactionsTotalCounterCreation = new CounterCreationData {
                CounterName = "Total transactions",
                CounterType = PerformanceCounterType.NumberOfItems32,
                CounterHelp = "Total of transactions that have not been processed"
                };
            performanceCounterInstaller.Counters.Add(transactionsTotalCounterCreation);

            var transactionsUnprocessedCounterCreation = new CounterCreationData {
                CounterName = "Unprocessed transactions",
                CounterType = PerformanceCounterType.NumberOfItems32,
                CounterHelp = "Total of transactions that have not been processed"
                };
            performanceCounterInstaller.Counters.Add(transactionsUnprocessedCounterCreation);

            var transactionsUnprocessedPercentageCounterCreation = new CounterCreationData{
                CounterName = "% Unprocessed transactions",
                CounterType = PerformanceCounterType.NumberOfItems32,
                CounterHelp = "Percentage of transactions that have not been processed"
            };
            performanceCounterInstaller.Counters.Add(transactionsUnprocessedPercentageCounterCreation);

            var transactionsProcessedCounterCreation = new CounterCreationData {
                CounterName = "Processed transactions",
                CounterType = PerformanceCounterType.NumberOfItems32,
                CounterHelp = "Total of transactions that have been processed"
                };
            performanceCounterInstaller.Counters.Add(transactionsProcessedCounterCreation);

            var transactionsProcessedPercentageCounterCreation = new CounterCreationData {
                CounterName = "% Unprocessed transactions",
                CounterType = PerformanceCounterType.NumberOfItems32,
                CounterHelp = "Percentage of transactions that have been processed"
            };
            performanceCounterInstaller.Counters.Add(transactionsProcessedPercentageCounterCreation);

            #endregion

            #region Processing - Statistics - Endpoints

            var conversingEndPointsCounterCreation = new CounterCreationData {
                CounterName = "Conversing endpoints",
                CounterType = PerformanceCounterType.NumberOfItems32,
                CounterHelp = "Number of conversing endpoints."
                };
            performanceCounterInstaller.Counters.Add(conversingEndPointsCounterCreation);

            #endregion Processing - Statistics
        }
    }
}