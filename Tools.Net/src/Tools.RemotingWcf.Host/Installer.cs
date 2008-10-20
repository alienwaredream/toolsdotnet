using System.ComponentModel;

namespace Tools.RemotingWcf.Host
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