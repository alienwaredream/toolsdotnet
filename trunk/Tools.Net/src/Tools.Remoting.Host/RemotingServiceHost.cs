using Tools.Processes.Host;

namespace Tools.Remoting.Host
{
    public class RemotingServiceHost :
        ProcessServiceHost<RemotingHostProgram>
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static void Main(string[] args)
        {
            EntryPoint<RemotingServiceHost>(args);
        }
    }
}