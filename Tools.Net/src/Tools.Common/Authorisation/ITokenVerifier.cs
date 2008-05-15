using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Tools.Common.Authorisation
{
    [ServiceContract(Namespace = "http://Tools.Common/2008/02",
        Name = "ITokenProvider",
        SessionMode = SessionMode.Allowed)]
    public interface ITokenVerifier
    {
        [OperationContract(IsTerminating = false, IsOneWay = false, AsyncPattern = false, Action = "VerifyToken")]
        TokenVerificationResult VerifyToken(string tokenTarget, string token);
    }
}
