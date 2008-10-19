using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Tools.Core.Asserts;

namespace Tools.Core.Configuration
{
    /// <summary>
    /// Provides values from the custom NameValue section. See the test project app.config file
    /// for how to setup the custom NameValue section.
    /// </summary>
    /// <remarks>Is not thread safe! The client should take care of thread synch!</remarks>
    public class NameValueSectionConfigurationProvider : IConfigurationValueProvider
    {
        private readonly string configSectionName;
        private readonly NameValueCollection keyValueCollection;

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
                if (keyValueCollection.AllKeys.Contains(keyName))
                {
                    return keyValueCollection[keyName];
                }
                else
                {
                    Log.Source.TraceData(TraceEventType.Warning, 2013,
                                         String.Format(CultureInfo.InvariantCulture,
                                                       "Configuration key {0} is requested from the NameValue section {1} but" +
                                                       " it is not found!", keyName, configSectionName));
                    return null;
                    // (SD) no assert here as probing the config and fallback is absolutely
                    // natural scenario
                }
            }
        }

        #endregion
    }
}