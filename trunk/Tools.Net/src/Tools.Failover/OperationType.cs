namespace Tools.Failover
{
    /// <summary>
    /// Summary description for OperationType.
    /// </summary>
    public enum OperationType
    {
        /// <summary>
        /// Operation is done in the loop, if current instance of the 
        /// operation failed, then next instance of the operation would be
        /// normally created if not instructed otherwise explicitely.
        /// </summary>
        Loop,
        /// <summary>
        /// Operation is done on the single try basis, if it fails there will be no retry in the
        /// same context (like transactional context).
        /// </summary>
        Single
    }
}