using System.Runtime.Serialization;
using System;
using System.Collections.Generic;

namespace Tools.Common.Authorisation
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public interface ITokenAuthorisationServiceChannel : ITokenAuthorisationService, System.ServiceModel.IClientChannel
    {
    }

    public partial class TokenAuthorisationClient : System.ServiceModel.ClientBase<ITokenAuthorisationService>, ITokenAuthorisationService
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemMessageServiceClient"/> class.
        /// </summary>
        public TokenAuthorisationClient()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemMessageServiceClient"/> class.
        /// </summary>
        /// <param name="endpointConfigurationName">Name of the endpoint configuration.</param>
        public TokenAuthorisationClient(string endpointConfigurationName)
            :
                base(endpointConfigurationName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemMessageServiceClient"/> class.
        /// </summary>
        /// <param name="endpointConfigurationName">Name of the endpoint configuration.</param>
        /// <param name="remoteAddress">The remote address.</param>
        public TokenAuthorisationClient(string endpointConfigurationName, string remoteAddress)
            :
                base(endpointConfigurationName, remoteAddress)
        {
        }




        #region ITokenAuthorisationService Members

        public string IssueToken(string tokenSource)
        {
            return base.Channel.IssueToken(tokenSource);
        }

        public TokenVerificationResult VerifyToken(string tokenTarget, string token)
        {
            return base.Channel.VerifyToken(tokenTarget, token);
        }

        #endregion
    }
}
