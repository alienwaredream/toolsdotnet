using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using Tools.Processes;
using System.Security;
using Tools.Processes.Core;

namespace Tools.Processes.Host
{
    public partial class ProcessForm : Form
    {
        //private ServiceHost serviceHost;
        VoidAction stopDelegate;
        VoidStringArgsAction startDelegate;
        string[] startArguments;
        ConsoleTraceListener traceListener; 

        /// <summary>
        /// Gets the main tab control.
        /// </summary>
        /// <value>The main tab control.</value>
        public TabControl MainTabControl
        {
            get
            {
                return mainTabControl;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessForm"/> class.
        /// </summary>
        /// <param name="stopDelegate">The stop delegate.</param>
        /// <param name="startDelegate">The start delegate.</param>
        /// <param name="startArguments">The start arguments.</param>
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

            //SetProcessControlButtons(true);
            try
            {

                Console.SetOut(new TextControlTextWriter(this.logRichTextBox));
                //Trace.Listeners.Add(new ConsoleTraceListener(false));
            }
            catch (SecurityException ex)
            {
                ShowErrorMessage(MessagesResource.CannotLogToTestConsole + Environment.NewLine +
                    ex.ToString());
            }
        }

        private void ConnectDiagnosticListeners()
        {
            Console.WriteLine("Trying to connect to the System.ServiceModel trace source.");
            traceListener = new ConsoleTraceListener(false);
            TraceSource serviceModelTraceSource = new TraceSource("System.ServiceModel");
            serviceModelTraceSource.Listeners.Add(traceListener);
            serviceModelTraceSource.TraceInformation("Test information from the host form \r\n");

            TraceSource commonTraceSource = new TraceSource("Tools.Common");
            commonTraceSource.Listeners.Add(traceListener);
            commonTraceSource.TraceInformation("Test information from the Tools.Common \r\n");

        }
        private void DisconnectDiagnosticListeners()
        {
            //if (traceListener == null) return;

            //Trace.Listeners.Remove(traceListener);
            ////Trace.Listeners.Add(new TextControlWriterTraceListener(this.logRichTextBox));
            TraceSource serviceModelTraceSource = new TraceSource("System.ServiceModel");
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
        
        protected override void OnClosing(CancelEventArgs e)
        {
            StopProcess();
            base.OnClosing(e);
        }

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
                this.Text = "Debugging! " + this.Text;
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
                //TODO: (SD) process exceptional flow
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
    }
}