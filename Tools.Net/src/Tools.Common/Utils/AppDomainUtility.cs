using System;

namespace Tools.Common.Utils
{
    /// <summary>
    /// Application Domain related utilities.
    /// </summary>
    public static class AppDomainUtility
    {
        /// <summary>
        /// Gets the current app domain descriptor.
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentAppDomainDescriptor()
        {
            return
                "[Machine: " + System.Environment.MachineName +
                ", Dir:" + AppDomain.CurrentDomain.BaseDirectory +
                ",PID:" + System.Diagnostics.Process.GetCurrentProcess().Id + 
                ", Id:" + AppDomain.CurrentDomain + "]";
        }
    }
}
