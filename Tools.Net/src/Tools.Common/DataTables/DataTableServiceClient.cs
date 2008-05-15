using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using Tools.Common.DataAccess;

namespace Tools.Common.DataTables
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public interface IDataTableServiceChannel : IDataTableProvider, System.ServiceModel.IClientChannel
    {
    }

    public partial class DataTableServiceClient : System.ServiceModel.ClientBase<IDataTableProvider>, IDataTableProvider
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemMessageServiceClient"/> class.
        /// </summary>
        public DataTableServiceClient()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemMessageServiceClient"/> class.
        /// </summary>
        /// <param name="endpointConfigurationName">Name of the endpoint configuration.</param>
        public DataTableServiceClient(string endpointConfigurationName)
            :
                base(endpointConfigurationName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemMessageServiceClient"/> class.
        /// </summary>
        /// <param name="endpointConfigurationName">Name of the endpoint configuration.</param>
        /// <param name="remoteAddress">The remote address.</param>
        public DataTableServiceClient(string endpointConfigurationName, string remoteAddress)
            :
                base(endpointConfigurationName, remoteAddress)
        {
        }

        #region IDataTableProvider Members

        public System.Data.DataTable GetNamedDataTable(string dataTableName, string[] parameters)
        {
            return base.Channel.GetNamedDataTable(dataTableName, parameters);
        }
        public string GetNamedDataTableJson(string dataTableName, string[] parameters)
        {
            return base.Channel.GetNamedDataTableJson(dataTableName, parameters);
        }
        public string GetNamedSortedDataTableJson(string dataTableName, string sortColumn, string sortOrder, string[] parameters)
        {
            return base.Channel.GetNamedSortedDataTableJson(dataTableName, sortColumn, sortOrder, parameters);
        }

        #endregion
    }
}
