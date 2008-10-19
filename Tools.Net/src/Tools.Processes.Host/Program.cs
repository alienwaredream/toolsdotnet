namespace Tools.Processes.Host
{
    public class Program : ServiceHost
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