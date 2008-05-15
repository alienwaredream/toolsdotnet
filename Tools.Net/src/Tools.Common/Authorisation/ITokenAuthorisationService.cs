using System;
using System.Collections.Generic;

using System.Text;
using System.ServiceModel;

namespace Tools.Common.Authorisation
{
    [ServiceContract(Namespace = "http://Tools.Common/2008/02", 
        Name = "ITokenAuthorisationService", 
        SessionMode = SessionMode.Allowed)]
    public interface ITokenAuthorisationService : ITokenProvider, ITokenVerifier
    {
    }
}
