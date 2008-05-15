using System;
using System.Collections.Generic;

using System.Text;
using System.Runtime.Serialization;

namespace Tools.Common.Authorisation
{
    [DataContract]
    public class TokenVerificationResult
    {
        //(SD) the bellow fuss for the duplicating the code is for interop purposes ;(
        [DataMember()]
        public int Code { 
            get {return (int)ResultType; } 
            set { ResultType = (VerificationResultType)value;} 
        }
        
        public VerificationResultType ResultType { get; set; }

        [DataMember]
        public string Message { get; set; }
    }
}
