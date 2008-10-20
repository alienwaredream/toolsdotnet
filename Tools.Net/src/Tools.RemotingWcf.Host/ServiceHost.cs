using Tools.Processes.Host;

namespace Tools.RemotingWcf.Host
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