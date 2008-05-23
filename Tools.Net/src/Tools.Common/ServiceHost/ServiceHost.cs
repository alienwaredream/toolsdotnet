using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Diagnostics;
using System.ServiceProcess;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Configuration;
using System.Windows.Forms;
using Tools.Common.Exceptions;
using System.Threading;
using Tools.Common.Process;
using System.Diagnostics.CodeAnalysis;
using System.Security.Permissions;
using System.Globalization;


namespace Tools.Common.ServiceHost
{

	#region ServiceHost class
	
	public class ServiceHost : System.ServiceProcess.ServiceBase
	{
		#region Declarations

        private HostMode mode;

        public HostMode Mode
        {
            get { return mode; }
            protected set { mode = value; }
        }

        /// <summary>
        /// Stands for the win exit code.
        /// </summary>
        protected int exitCode = 0; //TODO:(SD) encapsulate
        private int customExitCode = 0;
        /// <summary>
        /// Custom application exit code.
        /// </summary>
        protected int CustomExitCode
        {
            get { return customExitCode; }
        }
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		#endregion

		#region Constructors

        public ServiceHost()
		{
			
			InitializeComponent();
		}


		#endregion

        #region Entry points

        /// <summary>
        /// The main entry point for the process
        /// </summary>
        /// <typeparam name="ServiceHostType">The type of the ervice host type.</typeparam>
        /// <param name="args">The args.</param>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "By design, there can't be a parameter of type ServiceHostType here.")]
        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlAppDomain)]
        protected static void EntryPoint<ServiceHostType>(string[] args)
            where ServiceHostType : ServiceHost, new()
        {
            //if (Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Log.Source.TraceInformation(
                "Entering the entry point of windows service with configuration file:" + 
                AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

            //Forces the principal info to be attached to the Thread
            AppDomain.CurrentDomain.SetPrincipalPolicy(System.Security.Principal.PrincipalPolicy.WindowsPrincipal);

            if ((args.Length > 0) && (args[0].ToLower().Contains("winapp")))
            {
                ServiceHost sh = new ServiceHostType();
                sh.mode = HostMode.WindowsApplication;
                Form hostForm = new ProcessForm(sh.OnStop, sh.OnStart, args);
                hostForm.Text = hostForm.Text + ": " + typeof(ServiceHostType).Name;

                Application.Run(hostForm);
            }
            else
            {
                System.ServiceProcess.ServiceBase[] ServicesToRun;
                ServiceHost sh = new ServiceHostType();
                sh.mode = HostMode.WindowsService;

                ServicesToRun = new System.ServiceProcess.ServiceBase[] { sh };
                ServiceBase.Run(ServicesToRun);
            }
        } 
        #endregion

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.ServiceName =
                ServiceHostResource.GenericServiceHostShortName;

		}

	
		#region Overrides

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>oh
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		/// <summary>
		/// Sets things in motion so your service can do its work.
		/// </summary>
		protected override void OnStart(string[] args)
		{
            //Debugger.Launch();
            Log.Source.TraceInformation(" Starting " + this.ServiceName);
		}
 
		/// <summary>
		/// Stops this service.
		/// </summary>
		protected override void OnStop()
		{
            this.ExitCode = 100;
            Log.Source.TraceInformation("Stopping " + this.ServiceName);
		}
	

		#endregion

		#region Handlers

		private static void CurrentDomain_UnhandledException(object sender, 
            UnhandledExceptionEventArgs e)
		{
            //TODO: (SD) Make subject of configuration from the command line argument
            bool ignoreHandlingErrors = false;

            bool shouldRethrow = true;

            try
            {
                Log.Source.TraceEvent(
                    TraceEventType.Error, 0, (e.ExceptionObject as Exception).ToString());
            }
            catch (Exception ex)
            {
                string logText =
                    String.Format(CultureInfo.InvariantCulture,
                    "Exception happened as a result of attempt to handle another exception." +
                    " The original exception info is: {0} \r\n and exception handling exception info is {1}",
                    e.ExceptionObject.ToString(), ex.ToString());

                LogToFallbackLog(logText);

                if (!ignoreHandlingErrors)
                    throw new Exception(
                        "As a result of an exception failure service is going to be shutdown." +
                        " Review exception handling configuration before restarting the service!" +
                        logText);
            }

            //if (shouldRethrow)
            //    throw e.ExceptionObject as Exception;
		}

        /// <summary>
        /// Logs to the fallback log.
        /// </summary>
        /// <param name="logText">The log text.</param>
        private static void LogToFallbackLog(string logText)
        {
            if (!EventLog.SourceExists(ServiceHostResource.GenericServiceHostShortName))
            {
                EventLog.CreateEventSource(
                    new EventSourceCreationData(ServiceHostResource.GenericServiceHostShortName, 
                    ServiceHostResource.ApplicationLogName));
            }
            EventLog.WriteEntry(ServiceHostResource.GenericServiceHostShortName,
                logText, EventLogEntryType.Error);
        }

		#endregion


	}

	#endregion

}
