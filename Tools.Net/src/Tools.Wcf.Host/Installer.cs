using System.ComponentModel;

namespace Tools.Wcf.Host
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