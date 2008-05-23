using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Configuration;
using Tools.Common.Asserts;
using System.Globalization;
using System.Diagnostics;

namespace Tools.Common.Config
{
    /// <summary>
    /// Provides values from the custom NameValue section. See the test project app.config file
    /// for how to setup the custom NameValue section.
    /// </summary>
    /// <remarks>Is not thread safe! The client should take care of thread synch!</remarks>
    public class NameValueSectionConfigurationProvider : IConfigurationValueProvider
    {
        private string configSectionName;
        private NameValueCollection keyValueCollection; 

        public NameValueSectionConfigurationProvider(string configSectionName)
        {
            this.configSectionName = configSectionName;


            keyValueCollection =
                ConfigurationManager.GetSection(configSectionName) as NameValueCollection;

            ErrorTrap.AddRaisableAssertion<ConfigurationErrorsException>
                (keyValueCollection != null, String.Format(CultureInfo.InvariantCulture,
                "Section {0} is not present in the configuration file or is misconfigured!" +
                " Setup the section properly!", this.configSectionName));
        }

        #region IConfigurationValueProvider Members


        public string this[string keyName]
        {
            get 
            {
                if (keyValueCollection.AllKeys.Contains<string>(keyName))
                {
                    return keyValueCollection[keyName];
                }
                else
                {
                    Log.Source.TraceData(TraceEventType.Warning, 2013,
                        String.Format(CultureInfo.InvariantCulture,
                        "Configuration key {0} is requested from the NameValue section {1} but" +
                        " it is not found!", keyName, this.configSectionName));
                    return null; 
                    // (SD) no assert here as probing the config and fallback is absolutely
                    // natural scenario
                }
            }
        }

        #endregion
    }
}
