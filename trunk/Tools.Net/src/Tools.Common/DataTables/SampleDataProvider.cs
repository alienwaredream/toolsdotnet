using System;
using System.Collections.Generic;

using System.Text;
using Tools.Common.Asserts;
using System.Data;
using System.Web.Script.Serialization;

namespace Tools.Common.DataTables
{
    public class SampleDataTableProvider : IDataTableProvider
    {

        #region IDataTableProvider Members

        public System.Data.DataTable GetNamedDataTable(string dataTableName, string[] parameters)
        {
            ErrorTrap.AddRaisableAssertion<ArgumentException>(parameters != null && parameters.Length > 1,
                "parameters != null && parameters.Length > 0");
            
            int rowCount = Convert.ToInt32(parameters[0]);
            int colCount = Convert.ToInt32(parameters[1]);

            DataTable dt = new DataTable(dataTableName);
            for (int i = 0; i < colCount; i++) dt.Columns.Add("Field " + i, typeof(string));

            for (int i = 0; i < rowCount; i++)
            {
                DataRow dr = dt.NewRow();

                for (int j = 0; j < colCount; j++)
                    dr[j] = i + j;

                dt.Rows.Add(dr);
                
            }
            return dt;


        }

        public string GetNamedDataTableJson(string dataTableName, string[] parameters)
        {
            throw new NotImplementedException("GetNamedDataTableJson not implemented!");
        }
        public string GetNamedSortedDataTableJson(string dataTableName, string sortColumn, string sortOrder, string[] parameters)
        {
            throw new NotImplementedException("GetNamedDataTableJson not implemented!");
        }

        #endregion
    }
}
