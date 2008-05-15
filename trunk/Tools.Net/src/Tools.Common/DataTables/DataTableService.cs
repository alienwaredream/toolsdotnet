using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tools.Common.Asserts;
using System.Web.Script.Serialization;
using System.Runtime.Serialization.Json;
using System.Data;
using Tools.Common.Utils;

namespace Tools.Common.DataTables
{

    public class DataTableService : IDataTableProvider
    {
        private IDataTableProvider dataProvider;

        public DataTableService()
        {
            dataProvider = new SampleDataTableProvider();
        }

        public DataTableService(IDataTableProvider dataProvider) : this()
        {
            ErrorTrap.AddRaisableAssertion<InvalidOperationException>(dataProvider != null,
                "dataProvider != null");

            this.dataProvider = dataProvider;
        }

        #region IDataTableProvider Members

        public System.Data.DataTable GetNamedDataTable(string dataTableName, string[] parameters)
        {
            return dataProvider.GetNamedDataTable(dataTableName, parameters);
        }
        //TODO: (SD) Temporary placement only, until datatable is json serializable by .net
        public string GetNamedDataTableJson(string dataTableName, string[] parameters)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            //DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(DataTable));
            serializer.RegisterConverters(new JavaScriptConverter[] {
                new JavaScriptDataTableConverter()});
            return serializer.Serialize(dataProvider.GetNamedDataTable(dataTableName, parameters));
        }
        //TODO: (SD) Temporary placement only, until datatable is json serializable by .net
        public string GetNamedSortedDataTableJson(string dataTableName, string sortColumn, string sortOrder, string[] parameters)
        {
            SortOrder order = SortOrder.Asc;

            if (!String.IsNullOrEmpty(sortOrder)) 
                order = (SortOrder)Enum.Parse(typeof(SortOrder), sortOrder);

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            //DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(DataTable));
            serializer.RegisterConverters(new JavaScriptConverter[] {
                new JavaScriptDataTableConverter()});
            return serializer.Serialize(
                SortUtility.SortDataTable(dataProvider.GetNamedDataTable(dataTableName, parameters),
                    sortColumn, order, false));
        }
        #endregion
    }
}
