using System;
using System.Collections.Generic;

using System.Text;
using Tools.Common.Asserts;
using System.Data;
using System.Web.Script.Serialization;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;
using Tools.Common.DataAccess;
using Spring.Context;
using Spring.Context.Support;

namespace Tools.Common.DataTables
{



    public class DataTableProvider : IDataTableProvider
    {
        private IDataTableTransformer tableTransformer;

        public DataTableProvider()
        {
        }
        public DataTableProvider(IDataTableTransformer tableTransformer) : this()
        {
            this.tableTransformer = tableTransformer;
        }

        #region IDataTableProvider Members

        public System.Data.DataTable GetNamedDataTable(string dataTableName, string[] parameters)
        {
            IApplicationContext context = ContextRegistry.GetContext();
            QueryDetails queryDetails =
                context.GetObject(dataTableName) as QueryDetails;

            ErrorTrap.AddRaisableAssertion<ArgumentException>(queryDetails != null && 
                !String.IsNullOrEmpty(queryDetails.CommandName),
                "queryDetails != null && !String.IsNullOrEmpty(queryDetails.CommandName)");

            if (queryDetails.Parameters != null && queryDetails.Parameters.Count > 0)
            {
                ErrorTrap.AddRaisableAssertion<ArgumentException>(parameters != null && parameters.Length ==
                queryDetails.Parameters.Count,
                    "parameters != null && parameters.Length == queryDetails.Parameters.Count");
            }

            ConnectionStringSettings reportConnString = ConfigurationManager.ConnectionStrings["Reports"];

             ErrorTrap.AddRaisableAssertion<ConfigurationErrorsException>(
                reportConnString != null && !String.IsNullOrEmpty(reportConnString.ConnectionString),
                "reportConnString != null && !String.IsNullOrEmpty(reportConnString.ConnectionString." +
                " Setup reporting connection string with name Reports!");

            DataTable dt = new DataTable(dataTableName);

            using (SqlConnection conn =
                    new SqlConnection(reportConnString.ConnectionString))
            {
                SqlCommand command = new SqlCommand(queryDetails.CommandName, conn);
                command.CommandType = CommandType.StoredProcedure;
                // populate params
                if (queryDetails.Parameters != null)
                {
                    for (int i = 0; i < queryDetails.Parameters.Count; i++)
                    {
                        object paramValue = parameters[i];
                        // Not using trinary operator here, so no conversion is required and more prepared for
                        // the generic use (SD)
                        if (paramValue == null)
                            paramValue = DBNull.Value;


                        command.Parameters.Add(new SqlParameter(
                            queryDetails.Parameters[i], paramValue));
                    }
                }
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    conn.Open();

                    adapter.Fill(dt);
                }
            }

            if (tableTransformer != null) return tableTransformer.Transform(dt);

            return dt;
        }
        public string GetNamedDataTableJson(string dataTableName, string[] parameters)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            
            serializer.RegisterConverters(new JavaScriptConverter[] {   new JavaScriptDataTableConverter()});
            
            return serializer.Serialize(GetNamedDataTable(dataTableName, parameters));
        }
        public string GetNamedSortedDataTableJson(string dataTableName, string sortColumn, string sortOrder, string[] parameters)
        {
            throw new NotImplementedException("GetNamedDataTableJson not implemented!");
        }

        #endregion
    }
}
