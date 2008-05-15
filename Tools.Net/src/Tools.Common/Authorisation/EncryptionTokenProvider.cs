using System;
using System.Collections.Generic;

using System.Text;
using System.Security.Cryptography;
using System.Diagnostics;
using System.IO;
using Tools.Common.Cryptography;
using Tools.Common.Config;
using Tools.Common.Asserts;
using System.Configuration;

namespace Tools.Common.Authorisation
{
    public class EncryptionTokenProvider : ITokenProvider
    {
        IStringCryptoTransformer crypto;
        IConfigurationValueProvider configProvider;
        ITokenVolatileDataProvider volatileDataProvider;


        public EncryptionTokenProvider(IStringCryptoTransformer crypto,
            IConfigurationValueProvider configProvider)
        {
            this.crypto = crypto;
            this.configProvider = configProvider;
            this.volatileDataProvider = new TokenDateTimeProvider();
        }
        public EncryptionTokenProvider(IStringCryptoTransformer crypto,
            IConfigurationValueProvider configProvider,
            ITokenVolatileDataProvider volatileDataProvider)
            : this(crypto, configProvider)
        {
            ErrorTrap.AddRaisableAssertion<ArgumentNullException>(volatileDataProvider != null,
                "volatileDataProvider != null");
            this.volatileDataProvider = volatileDataProvider;
        }

        #region ITokenProvider Members

        public string IssueToken(string tokenSource)
        {
            ErrorTrap.AddRaisableAssertion<ConfigurationErrorsException>
                (!String.IsNullOrEmpty(configProvider["saltPhrase"]),
                "!String.IsNullOrEmpty(configProvider[\"saltPhrase\"]");
            StringBuilder sb = new StringBuilder();
            sb.Append("<token tokenSource=\"").Append(tokenSource).Append("\" volatile=\"").Append(
                volatileDataProvider.GetVolatileData()).Append("\" salt=\"").Append(
                configProvider["saltPhrase"]).Append("\" />");
            Debug.WriteLine("Generated token before encryption: " + sb.ToString());
            return crypto.Encrypt(sb.ToString());
        }

        #endregion
    }
}
