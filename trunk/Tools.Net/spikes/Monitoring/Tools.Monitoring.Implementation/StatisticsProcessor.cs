using Tools.Coordination.Batch;
using Tools.Core.Asserts;
using System.Configuration;

namespace Tools.Monitoring.Implementation
{
    public class StatisticsProcessor : ScheduleTaskProcessor
    {
        private IStatisticsData statisticsData;
        private IStatisticsHandler statisticsHandler;

        public StatisticsProcessor() { }

        public StatisticsProcessor(IStatisticsData statisticsData, IStatisticsHandler statisticsHandler)
            : this()
        {
            this.statisticsData = statisticsData;
            this.statisticsHandler = statisticsHandler;
        }

        protected override void ExecuteSheduleTask()
        {
            base.ExecuteSheduleTask();

            ErrorTrap.AddRaisableAssertion<ConfigurationErrorsException>(
                statisticsData != null, "statisticsData != null.");

            ErrorTrap.AddRaisableAssertion<ConfigurationErrorsException>(
                statisticsHandler != null, "statisticsHandler != null.");


            statisticsHandler.ProcessStatistics();


        }
    }
}