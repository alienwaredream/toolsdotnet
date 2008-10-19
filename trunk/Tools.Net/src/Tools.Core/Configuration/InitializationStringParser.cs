using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;

namespace Tools.Core.Configuration
{
    public class InitializationStringParser : IInitializationStringParser
    {
        #region IInitializationStringParser Members

        public IDictionary<string, string> Parse(string initializationString)
        {
            if (String.IsNullOrEmpty(initializationString))
            {
                throw new ConfigurationErrorsException(String.Format(CultureInfo.InvariantCulture,
                                                                     "initialization string can't be empty or null"));
            }

            var ret = new Dictionary<string, string>();

            // If nothing split, assume this is just a non-parseable string
            if (!(initializationString.IndexOf(';') > 0))
                return null;

            string[] keyValuePairs = initializationString.Split(
                new[] {';'}, StringSplitOptions.RemoveEmptyEntries);


            for (int i = 0; i < keyValuePairs.Length; i++)
            {
                string[] keyValuePair = keyValuePairs[i].Split(
                    new[] {'='}, StringSplitOptions.RemoveEmptyEntries);

                if (keyValuePair.Length != 2)
                {
                    throw new ConfigurationErrorsException(String.Format(CultureInfo.InvariantCulture,
                                                                         "Invalid key/value configuration value in the initialization string. Required format is key = value; Provided string is {0}",
                                                                         keyValuePair));
                }
                string key = keyValuePair[0].Trim(new[] {' '});
                string value = keyValuePair[1].Trim(new[] {' '});

                if (String.IsNullOrEmpty(key))
                {
                    throw new ConfigurationErrorsException(String.Format(CultureInfo.InvariantCulture,
                                                                         "Invalid key configuration value in the initialization string. Key may not be empty; Provided configuration pair string is {0}",
                                                                         keyValuePair));
                }
                ret.Add(key.ToLower(), value);
            }
            return ret;
        }

        #endregion
    }
}