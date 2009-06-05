using Tools.Processes.Host;

namespace Tools.Coordination.Sample.WindowsService
{
    public class ServiceHost :
        ProcessServiceHost<Program>
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