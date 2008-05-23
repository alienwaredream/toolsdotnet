using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Configuration;
using Tools.Core.Asserts;
using System.Globalization;
using System.Diagnostics;

namespace Tools.Core.Configuration
{
    /// <summary>
    /// Provides values from the custom NameValue section. See the test project app.config file
    /// for how to setup the custom NameValue section.
    /// </summary>
    /// <remarks>Is not thread safe! The client should take care of thread synch!</remarks>
    public class ConnectionStringConfigurationProvider : IConfigurationValueProvider
    {

        #region IConfigurationValueProvider Members


        public string this[string connectionStringName]
        {
            get 
            {
                if (
                        (ConfigurationManager.ConnectionStrings[connectionStringName] == null)
                        )
                {

                    Log.Source.TraceData(TraceEventType.Warning, 2013,
                        String.Format(CultureInfo.InvariantCulture,
                        "Connection configuration for name {0} is not present in the config file",
                        connectionStringName));
                    return null;
                }
                return ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
            }
        }

        #endregion
    }
}
