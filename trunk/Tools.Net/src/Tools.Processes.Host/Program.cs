using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;

namespace Tools.Processes.Host
{
    public class Program : ServiceHost
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