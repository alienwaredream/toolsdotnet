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

            InternalProcessStatistics(data);

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
                    CategoryName = "Tools.Monitoring.Service",
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
            int newCommands = 0;
            bool newCommandsIsValid = false;

            if (statistics.ContainsKey("New Commands"))
            {
                newCommandsIsValid = true;
                newCommands = statistics["New Commands"];
            }

            int completedCommands = 0;
            bool completedCommandsIsValid = false;

            if (statistics.ContainsKey("Completed commands"))
            {
                completedCommandsIsValid = true;
                completedCommands = statistics["Completed commands"];
            }

            // the number of transactions must be equal or higher to the number of families
            if (newCommands > 0 && completedCommands == 0)
            {

                    this.TraceStatistics(
                        "Health Checker: There were no commands completed within configured period!\r\n", statistics,
                        EventLogEntryType.Warning, 1);

                    return false;
            }

            TraceStatistics("Health monitoring stats: \r\n", statistics, EventLogEntryType.Information, 2);



            return true;
        }

        private void TraceStatistics(string message, Dictionary<string, int> statistics, EventLogEntryType entryType, int eventId)
        {
            DataContractSerializer serializer = new DataContractSerializer(
                typeof(Dictionary<string, int>)
                );

            StringBuilder sb = new StringBuilder();

            foreach (string s in statistics.Keys)
            {
                sb.Append(s).Append(":").Append(statistics[s]).Append(Environment.NewLine);
            }

            EventLog eventLog = new EventLog("Foris-Monitoring", ".", "Foris-Monitoring");

            eventLog.WriteEntry(message + sb.ToString(), entryType, eventId);


    //        Log.Source.TraceData(
    //TraceEventType.Error, 0, message + sb.ToString()
    //);

            //using (MemoryStream sw = new MemoryStream())
            //{
            //    //serializer.WriteObject(sw, statistics);

            //    string stats = Encoding.UTF8.GetString(sw.ToArray());

            //    Log.Source.TraceData(
            //        TraceEventType.Error, 0, message + stats
            //        );

            //    EventLog eventLog = new EventLog("Foris-Monitoring", ".", "Foris-Monitoring");

            //    eventLog.WriteEntry(message + stats, entryType, eventId);
            //}
        }
    }
}