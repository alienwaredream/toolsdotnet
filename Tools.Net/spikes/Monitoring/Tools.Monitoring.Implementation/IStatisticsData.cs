using System.Collections.Generic;
using System.Data;

namespace Tools.Monitoring.Implementation
{
    public interface IStatisticsData
    {
        Dictionary<string, int> GatherStatistics();
    }
}