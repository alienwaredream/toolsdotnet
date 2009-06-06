using System;
using System.Collections.Generic;
using System.Data;

using Tools.Core.Data;
using System.Data.Common;

namespace Tools.Monitoring.Implementation
{
    public class StatisticsData : CommonDB, IStatisticsData
    {
        public StatisticsData() { }

        public StatisticsData(string connectionName) : base(connectionName) { }

        public Dictionary<string, int> GatherStatistics()
        {
            Dictionary<string, int> results = new Dictionary<string, int>();

            using (IDbConnection connection = this.CreateConnection())
            {
                IDbCommand command = connection.CreateCommand(
                    (c) =>
                    {
                        c.CommandType = CommandType.StoredProcedure;
                        c.CommandText = "prGatherStatistics";
                    });

                using (command)
                {
                    IDataReader reader = this.ExecuteReader(command, CommandBehavior.CloseConnection);

                    while (reader.Read())
                    {
                        results.Add(reader["Name"].ToString(), ((reader.IsDBNull(reader.GetOrdinal("Value"))) ? 0 : Convert.ToInt32(reader["Value"])));
                    }
                }

                connection.Close();
            }

            return results;
        }
    }
}