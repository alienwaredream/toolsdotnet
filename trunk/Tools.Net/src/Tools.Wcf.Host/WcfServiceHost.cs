using System.Collections.Generic;
using System.Text;
using System;
using System.Diagnostics;

namespace Tools.Wcf.Host
{
    public class WcfServiceHost : 
        Tools.Processes.Host.ProcessServiceHost<Tools.Wcf.Host.WcfHostProgram>
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            EntryPoint<WcfServiceHost>(args);
        }
    }
}