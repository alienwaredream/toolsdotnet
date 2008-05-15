using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tools.Common.Authorisation
{
    [Flags()]
    public enum VerificationResultType
    {
        None = 0,
        Success = 1,
        Failure = 2,
        InvalidVolatileData = 4,
        SourceAndTargetMismatch = 8,

        FailureForVolatileData = Failure | InvalidVolatileData,
        FailureForSourceAndTargetMismatch = Failure | SourceAndTargetMismatch,
    }
}
