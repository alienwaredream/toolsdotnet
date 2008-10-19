using System;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Security.Permissions;
using System.ServiceProcess;
using Tools.Core.Utils;

namespace Tools.Processes.Host
{

    #region Installer class

    [RunInstaller(true)]
    public abstract class Installer : System.Configuration.Install.Installer
    {
        #region Fields

        private readonly ServiceProcessInstaller spInstaller;
        private readonly ServiceInstaller srvInstaller;

        private string serviceName;
        

        /// <summary>
        /// Required designer variable.
        /// </summary>
#pragma warning disable 219
        private Container components;
#pragma warning restore 219
        #endregion

        #region Properties
        /// <summary>
        /// The full file path to the file with the installers
        /// </summary>
        private string installSource;

        public string InstallSource
        {
            get { return installSource; }
        }
        /// <summary>
        /// Service name to install.
        /// </summary>
        /// <remarks>This implies that there is only one service per installer. But this is 
        /// true for the current implementation.</remarks>
        public string ServiceName
        {
            get { return serviceName; }
            private set { serviceName = value; }
        }

        #endregion Properties

        #region Constructors

        protected Installer()
        {
            try
            {
                //Debugger.Launch();
                InitializeComponent();
                srvInstaller = new ServiceInstaller();
                spInstaller = new ServiceProcessInstaller();

                EstablishServiceProperties(srvInstaller, spInstaller);

                Installers.Add(srvInstaller);
                Installers.Add(spInstaller);

            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                throw;
            }
        }

        /// <summary>
        /// Establishes the service properties.
        /// </summary>
        /// <param name="serviceInstaller">The SRV installer.</param>
        /// <param name="processInstaller">The sp installer.</param>
        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlAppDomain)]
        private void EstablishServiceProperties(
            ServiceInstaller serviceInstaller, ServiceProcessInstaller processInstaller)
        {
            //ServiceHostConfigSection config = 
            //    ConfigurationManager.GetSection(this.GetType().FullName) as ServiceHostConfigSection;  
            installSource = Environment.GetCommandLineArgs()[Environment.GetCommandLineArgs().Length - 1];


            Configuration config = ConfigurationManager.OpenExeConfiguration(
                installSource);
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
            //TODO: (SD) Check if there is a null or exception for the non-existing config (as returns from machine anyway!)
            var configSection = config.GetSection(
                                    typeof (Installer).FullName) as ServiceHostInstallConfigSection;
            // Check for the AllValues already covered by the configuration manager and the required constrains in the config section,
            // but just for any case.
            if (configSection == null ||
                !CompareUtility.AllValuesSetAsString(configSection.Name, configSection.DisplayName,
                                                     configSection.Description))
            {
                throw new ConfigurationErrorsException(
                    String.Format(
                        "Configuration error. Expected section {0} not found or doesn't contain mandatory items:  {1} ",
                        typeof (Installer).FullName, "name, displayName, description"),
                    config.FilePath,
                    0);
            }

            serviceInstaller.ServiceName = configSection.Name;
            serviceInstaller.DisplayName = configSection.DisplayName;
            serviceInstaller.Description = configSection.Description;

            ServiceName = configSection.Name;

            //TODO: (SD) Provide configuration for those values and for the rest of them as well.
            processInstaller.Account = ServiceAccount.NetworkService;
            serviceInstaller.StartType = ServiceStartMode.Manual;
        }

        //(SD) No test coverage for this one

        /// <summary>
        /// Handles the AssemblyResolve event of the AppDomain.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="System.ResolveEventArgs"/> instance containing the event data.</param>
        /// <returns></returns>
        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            var consoleListener = new ConsoleTraceListener(false);
            if (!Trace.Listeners.Contains(consoleListener))
            {
                Trace.Listeners.Add(consoleListener);
            }
            // Can't use anything but basics here as it may be exactly the source of the 
            // failure. 
            // Changing it from the trace to console as well, to keep it simple.
            Trace.WriteLine(String.Format("Regular attempt to resolve the assembly {0} failed." +
                                          " Will try to load from {1}", args.Name, ResolveAssemblyPath(args.Name)));

            if (Trace.Listeners.Contains(consoleListener))
            {
                Trace.Listeners.Remove(consoleListener);
            }
            return Assembly.LoadFrom(ResolveAssemblyPath(args.Name));
        }

        private string ResolveAssemblyPath(string assemblyDllName)
        {
            return Path.GetDirectoryName(InstallSource) + @"\" + assemblyDllName;
        }

        #endregion

        #region Installation methods

        //protected override void OnBeforeInstall(System.Collections.IDictionary savedState)
        //{
        //    //Debugger.Launch();
        //    base.OnBeforeInstall(savedState);
        //    SetupCounters();
        //}

        #endregion

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }

        #endregion
    }

    #endregion
}