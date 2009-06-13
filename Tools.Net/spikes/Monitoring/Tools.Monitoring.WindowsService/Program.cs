using System;
using System.Diagnostics;
using Spring.Context.Support;
using Tools.Processes.Core;

namespace Tools.Monitoring.WindowsService
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
                Log.Source.TraceEvent(TraceEventType.Error, 0, ex.ToString());
            }
        }
        /// <summary>
        /// Stops this instance.
        /// </summary>
        public override void Stop()
        {
            base.Stop(); // base stop should 
            Log.Source.TraceEvent(TraceEventType.Start, 0, GetType() + " Stop method called.");
        }
    }
}