namespace Tools.Core.Messaging
{
    /// <summary>
    /// Type of message
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        /// For non initialized values only.
        /// </summary>
        None = 0,
        /// <summary>
        /// Fatal error message
        /// </summary>
        Fatal = 1,
        /// <summary>
        /// Error message
        /// </summary>
        Error = 2,
        /// <summary>
        /// Warning message
        /// </summary>
        Warning = 3,
        /// <summary>
        /// Informational message
        /// </summary>
        Informational = 4,
    }
}