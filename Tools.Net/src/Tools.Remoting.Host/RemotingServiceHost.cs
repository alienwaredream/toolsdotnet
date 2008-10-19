namespace Tools.Remoting.Host
{
    public class RemotingServiceHost : 
        Tools.Processes.Host.ProcessServiceHost<RemotingHostProgram>
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            EntryPoint<RemotingServiceHost>(args);
        }
    }
}