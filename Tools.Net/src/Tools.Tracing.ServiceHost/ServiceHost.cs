using Tools.Processes.Host;
using Tools.RemotingWcf.Host;

namespace Tools.Tracing.ServiceHost
{
    public class ServiceHost :
        ProcessServiceHost<RemotingWcfHostProgram>
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static void Main(string[] args)
        {
            EntryPoint<ServiceHost>(args);
        }
    }
}