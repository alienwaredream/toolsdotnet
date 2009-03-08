using System;

using log4net;
using System.Diagnostics;
using System.Threading;
using log4net.Config;
using log4net.Util;
using System.IO;

namespace TestLog4NetConfigurations
{
    class Program
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {
            //XmlConfigurator.Configure(); 
            
            XmlConfigurator.ConfigureAndWatch(new FileInfo(SystemInfo.ConfigurationFileLocation));

            int i = 0;

            while (true)
            {
                if (++i % 10 == 0) log.Error("error", new Exception("This is a  test error"));

                log.Info("this is a test message, jnfudsnf isni guhguw eh rguwehriucenhru iewrnhtvcuerh uh");
                Console.WriteLine("Called log");
                Thread.SpinWait(100000000);
            }
        }
    }
}