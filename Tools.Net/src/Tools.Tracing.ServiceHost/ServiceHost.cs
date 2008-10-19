namespace Tools.Tracing.ServiceHost
{
    public class ServiceHost :
        Tools.Processes.Host.ProcessServiceHost<Tools.RemotingWcf.Host.RemotingWcfHostProgram>
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            EntryPoint<ServiceHost>(args);
        }
    }
}