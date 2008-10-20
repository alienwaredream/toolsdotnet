using System.ComponentModel;

namespace Tools.Tracing.ServiceHost
{
    [RunInstaller(true)]
    public partial class Installer : Processes.Host.Installer
    {
        public Installer()
        {
            InitializeComponent();
        }
    }
}