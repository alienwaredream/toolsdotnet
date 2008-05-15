using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using Tools.Common.Config;
using Tools.Common.Asserts;
using System.Configuration;
using System.Diagnostics;
using Tools.Common.Logging;
using Tools.Common.Utils;

namespace Tools.Common.Authorisation
{
    public class TokenDateTimeVerifier : ITokenVolatileDataVerifier
    {
        private IConfigurationValueProvider configProvider;

        public TokenDateTimeVerifier()
        {
            this.configProvider = new NameValueSectionConfigurationProvider(
                ConfigSectionsResource.DefaultTokenDateTimeVerifierSectionName);
        }

        public TokenDateTimeVerifier(IConfigurationValueProvider configProvider)
        {
            this.configProvider = configProvider;
        }

        #region ITokenVolatileDataVerifier Members

        public TokenVerificationResult VerifyVolatileData(string volatileData)
        {
            DateTime tokenDate = Convert.ToDateTime(volatileData, CultureInfo.InvariantCulture);

            bool timeWindowDefined = false;

            int timeWindowsSeconds = ConversionUtility.SafeConvertToInt(
                configProvider["timeWindowSeconds"], -1, out timeWindowDefined);

            ErrorTrap.AddRaisableAssertion<ConfigurationErrorsException>(timeWindowDefined,
                "timeWindowDefined. Configuration key timeWindowSeconds is required!");

            DateTime validUntil = tokenDate.AddSeconds(timeWindowsSeconds);
            DateTime now = DateTime.UtcNow;

            Log.Source.TraceData(TraceEventType.Verbose, 1002,
                (String.Format(CultureInfo.InvariantCulture,
                "DateTime token validation. now:  {0}, tokenDate {1}, timeWindowsSeconds {2}, validUntil {3}",
                now, tokenDate, timeWindowsSeconds, validUntil)));

            if (validUntil > now)
            {
                return new TokenVerificationResult { ResultType = VerificationResultType.Success, Message = "Success" };
            }

            Log.Source.TraceData(TraceEventType.Warning, 1001,
                String.Format(CultureInfo.InvariantCulture,
                "DateTime token validation failed. now:  {0}, tokenDate {1}, timeWindowsSeconds {2}, validUntil {3}",
                now, tokenDate, timeWindowsSeconds, validUntil));

            return new TokenVerificationResult { ResultType = VerificationResultType.Failure, Message = "Token expired!" };

        }

        #endregion
    }
}
