using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;
using System;
using System.Diagnostics;


namespace Tools.Operations.Cleanup.WindowsService
{
    public class CleanupServiceHost : 
        Tools.Processes.Host.ProcessServiceHost<Program>
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            EntryPoint<CleanupServiceHost>(args);
        }
    }
}