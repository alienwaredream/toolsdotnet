using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using Tools.Logging;
using System.Security.Cryptography;

namespace Tools.Monitoring.Implementation
{
    public class StatisticsHandler : IStatisticsHandler
    {
        private IStatisticsData statisticsData;
        private PerformanceEventHandler performanceHandler;

        public bool TestingIsEnabled { get; set; }
        public string TestPath { get; set; }

        public StatisticsHandler() { }

        public StatisticsHandler(IStatisticsData statisticsData)
            : this()
        {
            this.statisticsData = statisticsData;
        }

        public bool ProcessStatistics()
        {
            Dictionary<string, int> data = this.statisticsData.GatherStatistics();

            this.UpdatePerformanceHandler(
                this.CreatePerformanceCounters(data)
                );

            this.UpdatePerformanceCounters(data);



            return false;
        }

        internal List<PerfomanceCounterConfiguration> CreatePerformanceCounters(Dictionary<string, int> statistics)
        {
            // create the statistics counters
            List<PerfomanceCounterConfiguration> counters = new List<PerfomanceCounterConfiguration>();
            foreach (KeyValuePair<string, int> stat in statistics)
                counters.Add(
                    new PerfomanceCounterConfiguration
                    {
                        Name = stat.Key,
                        ClearOnStart = true,
                        CounterType = PerformanceCounterType.NumberOfItems64,
                        EventId = stat.Key,
                        Description = stat.Key
                    });

            return counters;
        }

        internal void UpdatePerformanceHandler(List<PerfomanceCounterConfiguration> counters)
        {
            this.performanceHandler = new PerformanceEventHandler(
                new PerformanceEventHandlerConfiguration
                {
                    Counters = counters,
                    CategoryName = "Tools.Monitoring",
                    Description = "Tools.Monitoring performance counters.",
                    EnableSetupOnInitialization = false,
                    MachineName = ".",
                    Name = ""
                });

            // enable the performance handler
            this.performanceHandler.Enabled = true;
        }

        internal void UpdatePerformanceCounters(Dictionary<string, int> statistics)
        {
            foreach (KeyValuePair<string, int> stat in statistics)
                this.performanceHandler.HandleEvent(stat.Key, stat.Value);
        }

        internal bool InternalProcessStatistics(Dictionary<string, int> statistics)
        {
            int totalTransactions = 0;
            bool totalTransactionsIsValid = false;

            if (statistics.ContainsKey("Total transactions"))
            {
                totalTransactionsIsValid = true;
                totalTransactions = statistics["Total transactions"];
            }

            int totalFamilies = 0;
            bool totalFamiliesIsValid = false;
            if (statistics.ContainsKey("Total families"))
            {
                totalFamiliesIsValid = true;
                totalFamilies = statistics["Total families"];
            }

            int totalEndpoints = 0;
            bool totalEndpointsIsValid = false;
            if (statistics.ContainsKey("Conversing endpoints"))
            {
                totalEndpointsIsValid = true;
                totalEndpoints = statistics["Conversing endpoints"];
            }

            // the number of transactions must be equal or higher to the number of families
            if (totalTransactionsIsValid && totalFamiliesIsValid)
            {
                if (totalTransactions < totalFamilies)
                {
                    this.TraceInvalidStatistics(
                        "StatisticsHandler: Statistics validation failed (totalTransactions < totalFamilies). Statistics:", statistics);

                    return false;
                }
            }

            Log.Source.TraceData(TraceEventType.Information, 0,
                "StatisticsHandler: Statistics validation success"
                );

            return true;
        }

        private void TraceInvalidStatistics(string message, Dictionary<string, int> statistics)
        {
            DataContractSerializer serializer = new DataContractSerializer(
                typeof(Dictionary<string, int>)
                );

            using (MemoryStream sw = new MemoryStream())
            {
                serializer.WriteObject(sw, statistics);
                Log.Source.TraceData(
                    TraceEventType.Error, 0, message + Encoding.UTF8.GetString(sw.ToArray())
                    );
            }
        }
    }
}