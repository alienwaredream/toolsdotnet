
using Microsoft.Build.Utilities;
using Microsoft.Build.Framework;
using System;
using System.Threading;
using System.Globalization;

namespace MultiProcBuild
{
    /// <summary>
    /// MSBuild task to delay the process for the required amount of time.
    /// </summary>
    /// <remarks>Realized via call to Thread.Sleep.</remarks>
    public class SleepTask : Microsoft.Build.Utilities.Task
    {
        /// <summary>
        /// Amount of time in milliseconds to sleep.
        /// </summary>
        [Microsoft.Build.Framework.Required()]
        public int Time { get; set; }

        public override bool Execute()
        {
            try
            {
                Thread.Sleep(Time);
                
                Log.LogMessage(MessageImportance.High, String.Format(CultureInfo.InvariantCulture,
                    "Task {0}, slept for {1} ms.", GetType().Name, Time.ToString()));
                return true;
            }
            catch (Exception ex)
            {
                Log.LogErrorFromException(ex, true);
                return false;
            }
        }

    }
}
