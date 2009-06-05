namespace Tools.Failover
{
    public enum FailureExceptionType : byte
    {
        /// <summary>
        /// Non classified exception
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// The process is not supposed to recover from this exception.
        /// </summary>
        NonRecoverable,
        /// <summary>
        /// Process should be able to recover from this exception and continue.
        /// </summary>
        Recoverable
    }
}