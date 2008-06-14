using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;

namespace Tools.Wcf.Host
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