using System;
using System.Collections;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using Tools.Core.Asserts;

namespace Tools.Core.Configuration
{
    //TODO: (SD) This is a quick one from the namevalue section, should be refactored to the
    // common base then.
    /// <summary>
    /// Provides values from the custom NameValue section. See the test project app.config file
    /// for how to setup the custom NameValue section.
    /// </summary>
    /// <remarks>Is not thread safe! The client should take care of thread synch!</remarks>
    public class SingleTagSectionConfigurationProvider : IConfigurationValueProvider
    {
        private readonly string configSectionName;
        private readonly Hashtable keyValueCollection;

        public SingleTagSectionConfigurationProvider(string configSectionName)
        {
            this.configSectionName = configSectionName;

            keyValueCollection =
                ConfigurationManager.GetSection(configSectionName) as Hashtable;

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
                if (keyValueCollection.ContainsKey(keyName))
                {
                    return keyValueCollection[keyName] == null
                               ?
                                   null
                               : keyValueCollection[keyName].ToString();
                }
                else
                {
                    Log.Source.TraceData(TraceEventType.Warning, 2014,
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