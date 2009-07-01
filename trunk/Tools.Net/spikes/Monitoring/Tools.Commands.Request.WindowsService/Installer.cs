using System.ComponentModel;
using System.Diagnostics;

namespace Tools.Commands.Request.WindowsService
{
    [RunInstaller(true)]
    public partial class Installer : Tools.Processes.Host.Installer
    {
        public Installer()
        {
            InitializeComponent();

            SetupCounters();

            SetupEventLog();
        }

        protected void SetupEventLog()
        {
            // Create an instance of an EventLogInstaller.

            var eventLogInstaller = new EventLogInstaller();

            // Set the source name of the event log.
            eventLogInstaller.Source = "CmdExec";

            // Set the event log that the source writes entries to.
            eventLogInstaller.Log = "CmdExec";

            // Add eventLogInstaller to the Installer collection.
            Installers.Add(eventLogInstaller);

        }

        protected void SetupCounters()
        {

        }
    }
}