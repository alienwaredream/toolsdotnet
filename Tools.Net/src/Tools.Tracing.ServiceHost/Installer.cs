using System.ComponentModel;

namespace Tools.Tracing.ServiceHost
{
    [RunInstaller(true)]
    public partial class Installer : Tools.Processes.Host.Installer
    {
        public Installer()
            : base()
        {
            InitializeComponent();
        }
    }
}