using System;

namespace Tools.Failover
{
    public interface IFailureExceptionHandler
    {
        FailureExceptionType HandleFailure(Exception ex);
        void ResetState();
    }
}