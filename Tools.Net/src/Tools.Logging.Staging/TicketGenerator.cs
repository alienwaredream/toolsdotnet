using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Principal;
using System.Threading;
using System.Diagnostics;

namespace Tools.Logging
{
    /// <summary>
    /// Generates support ticket for the administrator reference.
    /// </summary>
    public static class TicketGenerator
    {
        #region Attributes

        private static int currentProcessId;
        private static long startTicks;
        private static string prefix;

        #endregion Attributes

        #region Constructors

        static TicketGenerator()
        {
            currentProcessId = System.Diagnostics.Process.GetCurrentProcess().Id;
            DateTime generationSessionStart = DateTime.Now;
            prefix = generationSessionStart.ToString("yyMMddHHmm-") + Environment.MachineName
                + "-" + currentProcessId + "-";
            startTicks = Stopwatch.GetTimestamp();
                //new DateTime(generationSessionStart.Year, generationSessionStart.Month,
                //generationSessionStart.Day, generationSessionStart.Hour, generationSessionStart.Minute, 0).Ticks;
            Debug.WriteLine("Timer is high resolution: " + Stopwatch.IsHighResolution);
        }

        #endregion Constructors

        /// <summary>
        /// Generates and returns new ticket string.
        /// Ticketing is used for support cases for example.
        /// </summary>
        /// <returns></returns>
        public static string GetTicket()
        {
            //TODO: (SD) subject to refactor, guid is used for fun and test purposes,
            // always wanted to test MS's implementation of GetHashCode for string :).
            // Concept 1
            // return Guid.NewGuid().ToString().GetHashCode().ToString("X"); // generates 1-3 dups per 100000.
            // The way bellow doesn't move thread to the waiting queue, although it is required
            // TODO: (SD) to take a look in documentation so stop watch resolution is sufficient with used SpinWait value. 
            // Concept 2
            Thread.SpinWait(10);
            return prefix + (Stopwatch.GetTimestamp() - startTicks).ToString("X");
            // Interestingly although that timer ticks are not counted from the calendar start.
            // Jes, I guess it only measures from the recent start! Anyway high resolution
            // frequency is quite high to be reasonable to show in hex.

            // And just playing for a sec with datetime now, that is if timer is low resolution
            //return (currentProcessId * 100 + DateTime.Now.Ticks - startTicks).ToString("X");

            // Concept 3
            //Thread.SpinWait(10);
            //return (new Random().Next(10000000) + currentProcessId*100000).ToString("X");
        }
    }
}
