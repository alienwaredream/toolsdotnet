namespace Tools.RemotingWcf.Host
{
    public class ServiceHost :
        Tools.Processes.Host.ProcessServiceHost<RemotingWcfHostProgram>
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