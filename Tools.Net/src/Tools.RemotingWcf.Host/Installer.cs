using System.ComponentModel;

namespace Tools.RemotingWcf.Host
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