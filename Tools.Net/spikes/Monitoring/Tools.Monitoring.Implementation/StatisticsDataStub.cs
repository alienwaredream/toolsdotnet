using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Tools.Monitoring.Implementation
{
    public class StatisticsDataStub : IStatisticsData
    {
        public StatisticsDataStub() { }

        public Dictionary<string, int> GatherStatistics()
        {
            Dictionary<string, int> results = new Dictionary<string, int>();


            results.Add("Command Avg Execution time, ms", new Random().Next(1000));
            results.Add("New Commands", new Random().Next(1000));
            results.Add("Commands in process", new Random().Next(1000));
            results.Add("Completed commands", 0);


            return results;
        }
    }
}