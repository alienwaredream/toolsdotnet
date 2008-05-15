using System;
using System.Collections.Generic;

using System.Text;
using Tools.Common.Cryptography;
using Tools.Common.Config;
using System.Xml.Linq;
using Tools.Common.Asserts;
using System.Globalization;
using Tools.Common.Logging;
using System.Diagnostics;

namespace Tools.Common.Authorisation
{
    public class EncryptionTokenVerifier : ITokenVerifier
    {
        IStringCryptoTransformer crypto;
        ITokenVolatileDataVerifier volatileDataVerifier;

        public EncryptionTokenVerifier(IStringCryptoTransformer crypto,
            ITokenVolatileDataVerifier volatileDataVerifier)
        {
            this.crypto = crypto;
            this.volatileDataVerifier = volatileDataVerifier;
        }
        #region ITokenVerifier Members

        public TokenVerificationResult VerifyToken(string tokenTarget, string token)
        {
            try
            {
                Log.Source.TraceData(TraceEventType.Verbose, 1003, "Verifying token: " + token);
                
                //TODO: (SD) think about using validating Parse, but not now...

                XElement tokenXml = XElement.Parse(crypto.Decrypt(token));

                XAttribute tokenSourceAttribute = tokenXml.Attribute((XName)"tokenSource");
                XAttribute volatileDataAttribute = tokenXml.Attribute((XName)"volatile");
                XAttribute saltAttribute = tokenXml.Attribute((XName)"salt");

                ErrorTrap.AddAssertion(tokenSourceAttribute != null, "tokenSourceAttribute!=null");
                ErrorTrap.AddAssertion(volatileDataAttribute != null, "volatileDataAttribute!=null");
                ErrorTrap.AddAssertion(saltAttribute != null, "saltAttribute!=null");

                ErrorTrap.RaiseTrappedErrors<ArgumentException>();

                TokenVerificationResult volatileDataVerificationResult = 
                    volatileDataVerifier.VerifyVolatileData(volatileDataAttribute.Value);

                if (volatileDataVerificationResult.ResultType !=
                        VerificationResultType.Success)
                {
                    return new TokenVerificationResult
                    {
                        ResultType = VerificationResultType.FailureForVolatileData,
                        Message = "Token verification error!"
                    };
                }

                if (tokenSourceAttribute.Value != tokenTarget)
                {
                    Log.Source.TraceData(TraceEventType.Warning, 1005, 
                        String.Format(CultureInfo.InvariantCulture,
                        "Token source {0} and target {1} values mismatch! ", 
                        tokenSourceAttribute.Value, tokenTarget));

                    return new TokenVerificationResult
                    {
                        ResultType = VerificationResultType.FailureForSourceAndTargetMismatch,
                        Message = "Token verification error!"
                    };
                }
                //TODO: (SD) Complete the negative cases above, so the client can get granular info
                // intent is not to supress info on this level. we do some logs but only those that are 
                // very much relevant here.
                if (tokenSourceAttribute.Value == tokenTarget && 
                        volatileDataVerificationResult.ResultType == VerificationResultType.Success)
                {
                    return new TokenVerificationResult
                    {
                        ResultType = VerificationResultType.Success,
                        Message = "Success"
                    };
                }
                return new TokenVerificationResult
                {
                    ResultType = VerificationResultType.Failure,
                    Message = "Token verification error!"
                };

            }
            catch (Exception ex)
            {
                //TODO: (SD) setup exception handling block for tests and change here to the exc.block
                Log.Source.TraceData(TraceEventType.Error, 1006, 
                    "Exception during token verification: " + token + 
                    "Exception detail:" + ex.ToString());

                return new TokenVerificationResult
                {
                    ResultType = VerificationResultType.Failure,
                    Message = "Token verification error!"
                };
            }

        }

        #endregion
    }
}
