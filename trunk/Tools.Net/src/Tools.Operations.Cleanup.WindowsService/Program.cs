using System;
using System.Diagnostics;
using Spring.Context.Support;
using Tools.Processes.Core;

namespace Tools.Operations.Cleanup.WindowsService
{
    public class Program : ThreadedProcess
    {
        private IProcess process;
        /// <summary>
        /// Starts this instance.
        /// </summary>
        protected override void StartInternal()
        {
            try
            {

                Log.Source.TraceEvent(TraceEventType.Start, 0, GetType() + " start method called.");
                process = ContextRegistry.GetContext().GetObject("Coordinator") as IProcess;
                //Debugger.Launch();
                process.Start();
            }
            catch (Exception ex)
            {
                if (!EventLog.SourceExists("Tls.Cleanup"))
                {
                    EventLog.CreateEventSource("Tls.Cleanup", "Tls.Cleanup");
                }

                new EventLog("Tls.Cleanup", ".", "Tls.Cleanup").WriteEntry("Exception during start." + ex.ToString(), EventLogEntryType.Error, 16066);

                Log.Source.TraceEvent(TraceEventType.Error, 0, ex.ToString());

                throw ex;
            }
        }
        /// <summary>
        /// Stops this instance.
        /// </summary>
        public override void Stop()
        {
            process.Stop();

            base.Stop();
        }
    }
}