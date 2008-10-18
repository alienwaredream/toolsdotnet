using System.ComponentModel;
using System.ServiceProcess;

namespace Tools.Tracing.ServiceHost
{
	
	#region Installer class
	
	[RunInstaller(true)]
	public class Installer : System.Configuration.Install.Installer
	{
		#region Declarations
		
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private ServiceInstaller				srvInstaller;
		private ServiceProcessInstaller			spInstaller;

		#endregion

		#region Constructors

		public Installer() : base()
		{
	
			InitializeComponent();
			srvInstaller = new ServiceInstaller();
			spInstaller = new ServiceProcessInstaller();
			spInstaller.Account = ServiceAccount.LocalSystem;
			srvInstaller.StartType = ServiceStartMode.Manual;
			// TODO: Names assignement to be done via configuration (SD)
			srvInstaller.ServiceName = "PE1.90-Tools.Tracing.ServiceHost";
			srvInstaller.DisplayName = "PE1.90-Tools.Tracing.ServiceHost";
			Installers.Add(srvInstaller);
			Installers.Add(spInstaller);

		}


		#endregion

		#region Functions

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
