using System;
using System.Diagnostics;
using System.Security;
using System.Windows.Forms;
using Tools.Processes.Core;
using Tools.Processes.Host.Properties;

namespace Tools.Processes.Host
{
    public partial class ProcessForm : Form
    {
        //private ServiceHost serviceHost;
        private readonly TextControlTextWriter logTextWriter;
        private readonly string[] startArguments;
        private readonly VoidStringArgsAction startDelegate;
        private readonly VoidAction stopDelegate;
        private bool logConnected = true;
        private ConsoleTraceListener traceListener;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessForm"/> class.
        /// </summary>
        /// <param name="stopDelegate">The StopInternal delegate.</param>
        /// <param name="startDelegate">The StartInternal delegate.</param>
        /// <param name="startArguments">The StartInternal arguments.</param>
        public ProcessForm(
            VoidAction stopDelegate,
            VoidStringArgsAction startDelegate,
            string[] startArguments
            )
        {
            //this.allo
            //this.serviceHost = serviceHost;
            this.stopDelegate = stopDelegate;
            this.startDelegate = startDelegate;
            this.startArguments = startArguments;

            InitializeComponent();

            setupPropertyGrid.SelectedObject =
                AppDomain.CurrentDomain.SetupInformation;

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;


            //SetProcessControlButtons(true);
            try
            {
                logTextWriter = new TextControlTextWriter(outputListView,
                                                          Settings.Default.DescriptionRegex);
                Console.SetOut(logTextWriter);
                //Trace.Listeners.Add(new ConsoleTraceListener(false));
            }
            catch (SecurityException ex)
            {
                ShowErrorMessage(MessagesResource.CannotLogToTestConsole + Environment.NewLine +
                                 ex);
            }
        }

        /// <summary>
        /// Gets the main tab control.
        /// </summary>
        /// <value>The main tab control.</value>
        public TabControl MainTabControl
        {
            get { return mainTabControl; }
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ShowErrorMessage(
                "Unhandled exception during StartInternal! This will cause process shutdown. Review the error and correct: " +
                e.ExceptionObject);
        }

        private void ConnectDiagnosticListeners()
        {
            Console.WriteLine("Trying to connect to the System.ServiceModel trace source.");
            traceListener = new ConsoleTraceListener(false);
            var serviceModelTraceSource = new TraceSource("System.ServiceModel");
            serviceModelTraceSource.Listeners.Add(traceListener);
            serviceModelTraceSource.TraceInformation("Test information from the host form \r\n");

            var commonTraceSource = new TraceSource("Tools.Common");
            commonTraceSource.Listeners.Add(traceListener);
            commonTraceSource.TraceInformation("Test information from the Tools.Common \r\n");
        }

        private void DisconnectDiagnosticListeners()
        {
            //if (traceListener == null) return;

            //Trace.Listeners.Remove(traceListener);
            ////Trace.Listeners.Add(new TextControlWriterTraceListener(this.logRichTextBox));
            var serviceModelTraceSource = new TraceSource("System.ServiceModel");
            serviceModelTraceSource.Listeners.Add(traceListener);
        }

        /// <summary>
        /// Shows the error message.
        /// </summary>
        /// <param name="message">The message.</param>
        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message);
        }

        //private void SetProcessControlButtons(bool enableStart)
        //{
        //    if (serviceHost != null)
        //    {
        //        startProcessButton.Enabled = enableStart;
        //        stopProcessButton.Enabled = !enableStart;
        //    }
        //    //TODO: (SD) Complete for other states
        //    //stopProcessButton.Enabled = (process != null) && 
        //    //    (process.ExecutionState != ProcessExecutionState.Unstarted ||
        //    //    process.ExecutionState != ProcessExecutionState.Stopped ||
        //    //    process.ExecutionState != ProcessExecutionState.StopRequested) ;
        //    //startProcessButton.Enabled = (process != null && !stopProcessButton.Enabled);

        //}

        private void StopProcess()
        {
            //if (this.process != null && this.process.ExecutionState != ProcessExecutionState.Stopped)
            //{

            //    process.Stop();         
            //}
            if (stopDelegate != null)
            {
                stopDelegate();
            }
        }

        private void debugButton_Click(object sender, EventArgs e)
        {
            debugButton.Enabled = false;

            if (!Debugger.IsAttached)
            {
                Text = "Debugging! " + Text;
                Debugger.Launch();
            }
        }

        private void stopProcessButton_Click(object sender, EventArgs e)
        {
            StopProcess();

            //SetProcessControlButtons(true);       
        }

        private void startProcessButton_Click(object sender, EventArgs e)
        {
            StartProcess();
            //SetProcessControlButtons(false);
        }

        private void StartProcess()
        {
            //if (this.process != null && (this.process.ExecutionState == ProcessExecutionState.Stopped
            //    || this.process.ExecutionState == ProcessExecutionState.Unstarted))
            //{
            //    process.Start();
            //}
            if (startDelegate != null)
            {
                startDelegate(startArguments);
            }
            else
            {
                ShowErrorMessage("startdelegate is null. Nothing to StartInternal!");
            }
        }

        private void connectTracesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (connectTracesCheckBox.Checked)
            {
                ConnectDiagnosticListeners();
            }
            else
            {
                DisconnectDiagnosticListeners();
            }
        }

        private void outputListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (outputListView.SelectedItems.Count > 0)
            {
                logRichTextBox.Text = outputListView.SelectedItems[0].Tag.ToString();
            }
        }

        private void applyRegexToolStripButton_Click(object sender, EventArgs e)
        {
            logTextWriter.SetDescriptionRegexString(regexToolStripTextBox.Text);
        }

        private void ProcessForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.Save();
        }

        private void clearLogToolStripButton_Click(object sender, EventArgs e)
        {
            outputListView.Items.Clear();
            logRichTextBox.Clear();
        }

        private void ProcessForm_Load(object sender, EventArgs e)
        {
        }

        private void pauseLogStripButton_Click(object sender, EventArgs e)
        {
            logConnected = !logConnected;
            pauseLogStripButton.Text = (logConnected) ? "Disconnect Log" : "Connect Log";
            logTextWriter.Enabled = logConnected;
        }

        private void autoScrollToolStripButtonT_Click(object sender, EventArgs e)
        {
            logTextWriter.AutoScroll = autoScrollToolStripButton.Checked;
        }
    }
}