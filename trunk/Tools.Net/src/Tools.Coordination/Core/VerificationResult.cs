using System;

namespace Tools.Coordination.Core
{
    public struct VerificationResult
    {
        public bool PassedSuccessfuly
        {
            get; set;
        }

        public bool GenerateNotification
        {
            get; set;
        }

        public string Message
        {
            get; set;
        }
    }
}