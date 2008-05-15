using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;
using Tools.Common.Process;

namespace Tools.Common.ServiceHost
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