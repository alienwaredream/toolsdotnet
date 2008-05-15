using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Tools.Common.DataTables
{
    [ServiceContract(Namespace = "",
        Name = "DataTableService",
        SessionMode = SessionMode.Allowed)]
    public interface IDataTableProvider
    {
        [OperationContract(IsTerminating = false, IsOneWay = false, AsyncPattern = false, Action = "GetNamedDataTable")]
        [WebGet(RequestFormat=WebMessageFormat.Json, ResponseFormat=WebMessageFormat.Json)]
        DataTable GetNamedDataTable(string dataTableName, string[] parameters);
        [OperationContract(IsTerminating = false, IsOneWay = false, AsyncPattern = false, Action = "GetNamedDataTableJson")]
        [WebGet(RequestFormat=WebMessageFormat.Json, ResponseFormat=WebMessageFormat.Json)]
        string GetNamedDataTableJson(string dataTableName, string[] parameters);
        [OperationContract(IsTerminating = false, IsOneWay = false, AsyncPattern = false, Action = "GetNamedSortedDataTableJson")]
        [WebGet(RequestFormat=WebMessageFormat.Json, ResponseFormat=WebMessageFormat.Json)]
        string GetNamedSortedDataTableJson(string dataTableName, string sortColumn, string sortOrder, string[] parameters);
    }
}
