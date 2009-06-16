using System;
using System.Collections.Generic;
using System.Data;

using System.Data.Common;
using System.Configuration;
using System.Data.OracleClient;


namespace Tools.Monitoring.Implementation
{
    public class StatisticsData : IStatisticsData
    {
        public StatisticsData() { }

        public Dictionary<string, int> GatherStatistics()
        {
            Dictionary<string, int> results = new Dictionary<string, int>();

            using (OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["SourceDB"].ConnectionString))
            {
                // create the command object and set attributes
                using (OracleCommand cmd = new OracleCommand("prov_monitor.getstatisticsforperiod", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.BindByName = true;

                    // create parameter object for the cursor
                    OracleParameter pRefCursor = new OracleParameter("out_CountersForPeriod", OracleType.Cursor);


                    // this is an output parameter so we must indicate that fact
                    pRefCursor.Direction = ParameterDirection.Output;

                    // add the parameter to the collection
                    cmd.Parameters.Add(pRefCursor);

                    OracleParameter pStartDate = new OracleParameter("in_PeriodStart", OracleType.DateTime);
                    pStartDate.Direction = ParameterDirection.Input;

                    OracleParameter pEndDate = new OracleParameter("in_PeriodEnd", OracleType.DateTime);
                    pEndDate.Direction = ParameterDirection.Input;

                    cmd.Parameters.Add(pStartDate);
                    cmd.Parameters.Add(pEndDate);

                    con.Open();

                    using (IDataReader dr = cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {
                            results.Add(dr["Name"].ToString(), ((dr.IsDBNull(dr.GetOrdinal("Value"))) ? 0 : Convert.ToInt32(dr["Value"])));
                        }
                    }


                }


                return results;
            }
        }
    }
}