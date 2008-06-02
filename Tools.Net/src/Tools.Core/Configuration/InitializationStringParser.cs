using System;
using System.Collections.Generic;
using System.Text;
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
            Dictionary<string, string> ret = new Dictionary<string, string>();

            string[] keyValuePairs = initializationString.Split(
                new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < keyValuePairs.Length; i++)
            {
                string[] keyValuePair = keyValuePairs[i].Split(
                    new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);

                if (keyValuePair.Length != 2)
                {
                    throw new ConfigurationErrorsException(String.Format(CultureInfo.InvariantCulture,
                        "Invalid key/value configuration value in the initialization string. Required format is key = value; Provided string is {0}",
                        keyValuePair));
                }
                string key = keyValuePair[0].Trim(new char[]{' '});
                string value = keyValuePair[1].Trim(new char[] { ' ' });

                if (String.IsNullOrEmpty(key))
                {
                    throw new ConfigurationErrorsException(String.Format(CultureInfo.InvariantCulture,
                        "Invalid key configuration value in the initialization string. Key may not be empty; Provided configuration pair string is {0}",
                        keyValuePair));
                }
                ret.Add(key, value);
            }
            return ret;
        }

        #endregion
    }
}
