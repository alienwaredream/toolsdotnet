using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceProcess;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Configuration;

namespace Tools.Tracing.ServiceHost
{

	#region RemotingService class
	
	public class RemotingService : System.ServiceProcess.ServiceBase
	{
		#region Declarations
		
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private string path;

		#endregion

		#region Constructors
		
		public RemotingService()
		{
			
			InitializeComponent();
			this.AutoLog = false;
			path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

		}


		#endregion

		// The main entry point for the process
		static void Main()
		{
			System.ServiceProcess.ServiceBase[] ServicesToRun;

			ServicesToRun = new System.ServiceProcess.ServiceBase[] { new RemotingService() };

			System.ServiceProcess.ServiceBase.Run(ServicesToRun);
		}

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// RemotingService
			// 
			// TODO: Names assignement to be done via configuration (SD)
			this.ServiceName = 
				"PE1.90-Tools.Tracing.ServiceHost";

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

//			string remotingConfigurationFile = 
//				System.Configuration.ConfigurationSettings.AppSettings.Get(
//				"RemotingConfigurationFile"
//				);

			// remoting configuration file path was successfully obtained
//			if (remotingConfigurationFile != null)
//			{
				
				try
				{
					// configure remoting from the file
					RemotingConfiguration.Configure(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
				}
				catch (Exception ex)
				{
					EventLog.WriteEntry(
						ServiceName, 
						"Error: " + ex.Message + "\r\nType: " + ex.GetType().ToString(), 
						EventLogEntryType.Error
						);

					return;
				}

				
				// logging remoting information ---------------------------------------------------

				foreach 
					(
					WellKnownServiceTypeEntry te 
						in RemotingConfiguration.GetRegisteredWellKnownServiceTypes()
					)
				{
					EventLog.WriteEntry
						(
						ServiceName, 
						"remoted object: " + te.ObjectUri, 
						EventLogEntryType.Information
						);
				}
				foreach (IChannel ch in ChannelServices.RegisteredChannels)
				{
					EventLog.WriteEntry
						(
						ServiceName, 
						"channel: " + ch.ChannelName, 
						EventLogEntryType.Information
						);
				}

				EventLog.WriteEntry
					(
					ServiceName, 
					"app directory: " + path, 
					EventLogEntryType.Information
					);

				// --------------------------------------------------------------------------------

//			}
//			else // remoting configuration file path could not be obtained
//			{
//				EventLog.WriteEntry
//					(
//					ServiceName, 
//					"Error: No RemotingConfigurationFile value present in configuration file", 
//					EventLogEntryType.Error
//					);
//			}

		}
 
		/// <summary>
		/// Stops this service.
		/// </summary>
		protected override void OnStop()
		{
			IChannel[] chn = ChannelServices.RegisteredChannels;
			foreach (IChannel ch in chn)
			{
				ChannelServices.UnregisterChannel(ch);
			}
			
			EventLog.WriteEntry
				(
				ServiceName, 
				"Successfully stopped. Settings cleared.", 
				EventLogEntryType.Information
				);
		}
	

		#endregion

	}

	#endregion

}
