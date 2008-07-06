using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tools.Collaboration.Publishing.Host
{
    public class ServiceHost :
        Tools.Processes.Host.ProcessServiceHost<Tools.Wcf.Host.WcfHostProgram>
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
