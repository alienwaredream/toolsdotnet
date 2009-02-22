using System;
using Microsoft.Build.BuildEngine;
using Microsoft.Build.Framework;
using System.Reflection;

namespace MSBuildHost
{
   class Program
    {
        [STAThread]
        static void Main(string[] args)
        {                      
            // We need to tell MSBuild where msbuild.exe is, so it can launch child nodes
            string parameters = @"MSBUILDLOCATION=" + System.Environment.GetFolderPath(System.Environment.SpecialFolder.System) + @"\..\Microsoft.NET\Framework\v3.5";
            
            // We need to tell MSBuild whether nodes should hang around for 60 seconds after the build is done in case they are needed again
            bool nodeReuse = true; // e.g.
            if (!nodeReuse)
            {
                parameters += ";NODEREUSE=false";
            }

            // We need to tell MSBuild the maximum number of nodes to use. It is usually fastest to pick about the same number as you have CPU cores
            int maxNodeCount = 3; // e.g.

            // Create the engine with this information
            Engine buildEngine = new Engine(null, ToolsetDefinitionLocations.Registry | ToolsetDefinitionLocations.ConfigurationFile, maxNodeCount, parameters);

            // Create a file logger with a matching forwarding logger, e.g.
            FileLogger fileLogger = new FileLogger();
            fileLogger.Verbosity = LoggerVerbosity.Detailed;
            Assembly engineAssembly = Assembly.GetAssembly(typeof(Engine));
            string loggerAssemblyName = engineAssembly.GetName().FullName;
            LoggerDescription fileLoggerForwardingLoggerDescription = new LoggerDescription("Microsoft.Build.BuildEngine.ConfigurableForwardingLogger", loggerAssemblyName, null, String.Empty, LoggerVerbosity.Detailed);

            // Create a regular console logger too, e.g.
            ConsoleLogger logger = new ConsoleLogger();
            logger.Verbosity = LoggerVerbosity.Normal;

            // Register all of these loggers
            buildEngine.RegisterDistributedLogger(fileLogger, fileLoggerForwardingLoggerDescription);
            buildEngine.RegisterLogger(logger);

            // Do a build
            buildEngine.BuildProjectFile("root.proj");

            // Finish cleanly
            buildEngine.Shutdown();
        }
    }
}
