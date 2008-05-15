using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Collections.Specialized;

namespace Tools.Common.Utils
{
    public static class ConversionUtility
    {

        private delegate bool TryParseDelegate<T>(string value, T input);

        public static long SafeConvertToLong(string testValue, long defaultValue, out bool success)
        {
            long retValue = defaultValue;

            success = long.TryParse(testValue, out retValue);

            if (success) return retValue;

            return defaultValue;
        }
        public static int SafeConvertToInt(string testValue, int defaultValue, out bool success)
        {
            int retValue = defaultValue;

            success = int.TryParse(testValue, out retValue);

            if (success) return retValue;

            return defaultValue;
        }
        /// <summary>
        /// Provides safe conversion either to the underlying value or to the default fallback
        /// value if object value is DBNull.Value
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static Int64 ConvertDBValueToInt64(object val, Int64 fallbackValue)
        {
            return ((val == DBNull.Value || val.ToString() == String.Empty) ? fallbackValue : Convert.ToInt64(val));
        }
        /// <summary>
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ConvertDBValueToString(object val, string fallbackValue)
        {
            return ((val == DBNull.Value || val == null) ? fallbackValue : val.ToString());
        }
    }
}
