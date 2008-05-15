using System;
using System.Data;

using Tools.Common.Asserts;
using Tools.Common.DataTables;

namespace Tools.Common.UnitTests
{
    public class SampleTransposeDataProvider : IDataTableProvider
    {

        #region IDataTableProvider Members

        public System.Data.DataTable GetNamedDataTable(string dataTableName, string[] parameters)
        {
            ErrorTrap.AddRaisableAssertion<ArgumentException>(parameters != null && parameters.Length > 1,
                "parameters != null && parameters.Length > 0");
            
            int rowCount = Convert.ToInt32(parameters[0]);
            int colCount = Convert.ToInt32(parameters[1]);

            DataTable dt = new DataTable(dataTableName);
            for (int i = 0; i < colCount; i++) dt.Columns.Add("F" + i, typeof(string));

            for (int i = 0; i < rowCount; i++)
            {
                DataRow dr = dt.NewRow();



                for (int j = 0; j < colCount; j++)
                {


                    if (j == 2)
                    {
                        dr[j] = "Val_" + i % 3;
                        continue;
                    }
                    if (j == 3)
                    {
                        dr[j] = i * j;
                        continue;
                    }
                    dr[j] = "Val_" + i + "_" + j;

                    
                }

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
            throw new NotImplementedException("GetNamedSortedDataTableJson not implemented!");
        }

        #endregion
    }
}
