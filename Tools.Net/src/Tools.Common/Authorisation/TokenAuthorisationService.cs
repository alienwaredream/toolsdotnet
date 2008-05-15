using System.ServiceModel;
using System.ServiceModel.Web;
using Tools.Common.Cryptography;
using Tools.Common.Config;

namespace Tools.Common.Authorisation
{
    //TODO: (SD) Prepared for DI, but not applied yet
    [ServiceBehavior(Name = "TokenAuthorisationService",
        Namespace = "http://Tools.Common/2008/02")]
    public class TokenAuthorisationService : ITokenAuthorisationService
    {
        ITokenProvider tokenProvider;
        ITokenVerifier tokenVerifier;

        /// <summary>
        /// Creates the instance with default providers and configuration
        /// </summary>
        public TokenAuthorisationService()
        {
            // create default
            tokenProvider = new EncryptionTokenProvider(
                new StringCryptoTransformer(
                    new NameValueSectionConfigurationProvider(ConfigSectionsResource.DefaultStringEncryptorSectionName)),
                    new NameValueSectionConfigurationProvider(ConfigSectionsResource.DefaultEncryptionTokenProviderSectionName));
            tokenVerifier = new EncryptionTokenVerifier(
                new StringCryptoTransformer(
                    new NameValueSectionConfigurationProvider(ConfigSectionsResource.DefaultStringEncryptorSectionName)),
                    new TokenDateTimeVerifier());

        }
        public TokenAuthorisationService(ITokenProvider tokenProvider, ITokenVerifier tokenVerifier) : this()
        {
            this.tokenProvider = tokenProvider;
            this.tokenVerifier = tokenVerifier;
        }

        #region ITokenAuthorisationService Members

        [WebGet(ResponseFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public string IssueToken(string tokenSource)
        {
            return tokenProvider.IssueToken(tokenSource);
        }

        [WebGet(ResponseFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public TokenVerificationResult VerifyToken(string tokenTarget, string token)
        {
            return tokenVerifier.VerifyToken(tokenTarget, token);
        }

        #endregion
    }
}
