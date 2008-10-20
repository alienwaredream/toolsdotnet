namespace Tools.Tracing.Common
{
    /// <summary>
    /// Summary description for ApplicationLifeCycleType.
    /// TODO: I need to work onto right names for the values (SD), those are just temp.
    /// </summary>
    public enum ApplicationLifeCycleType
    {
        StartUp,
        Runtime,
        /// <summary>
        /// Left only for a moment, when there is no need to distinguish,
        /// but rather look for RegularShutdown or PanicShutdown to be identified 
        /// when you log (SD)
        /// </summary>
        //[Obsolete("Rather look for RegularShutdown or PanicShutdown to be identified ")]
        Shutdown,
        RegularShutdown,
        PanicShutdown
    }
}