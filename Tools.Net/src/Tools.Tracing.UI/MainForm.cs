using System;
using System.ComponentModel;
using System.Runtime.Remoting;
using System.Threading;
using System.Windows.Forms;
using Tools.Core;
using Tools.Core.Context;
using Tools.Core.Utils;
using Tools.Tracing.Common;
using Tools.UI.Windows.Descriptors;
//using Tools.Tracing.Client.Handler;

namespace Tools.Tracing.UI
{
    /// <summary>
    /// Summary description for Form1.
    /// </summary>
    public class MainForm : Form
    {
        #region Global declarations

        private readonly EventHandlerManagerConfigurationEditorControl eventManagerConfigControl;
        private readonly TraceEventHandlerEventStub eventStub;
        private readonly EventMultiTracerControl eventTracerControl;
        private readonly ApplicationEventFilterControl filterControl;
        private readonly ObserversConfigurationControl observerConnectionsControl;
        private IContainer components;
        private TabPage connsTabPage;
        private DescriptiveListEditorControl descriptiveNameValueCollectionEditorControl1;
        private Label encryptionInfoLabel;
        private TabPage encryptionTabPage;
        private StatusBarPanel fileNameStatusBarPanel;

        private TabPage filterTabPage;
        private StatusBarPanel keyDownStatusBarPanel;
        private MainMenu mainMenu1;
        private TabControl mainTabControl;
        private ToolStripContainer mainToolStripContainer;
        private MenuItem menuItem1;
        private MenuItem menuItem2;
        private MenuItem menuItem3;
        private MenuItem menuItem4;
        private MenuItem menuItem5;
        private TabPage monitorTabPage;
        private OpenFileDialog openFileDialog1;
        private MenuItem openMenuItem;
        private MenuItem saveAsMenuItem;
        private SaveFileDialog saveFileDialog1;
        private MenuItem saveMenuItem;
        private MenuItem showTestToolBarMenuItem;
        private ToolStripButton startTestToolStripButton;
        private StatusBar statusBar1;
        private ToolStripButton stopTestToolStripButton;
        private long testIterations = 100;
        private ToolStripTextBox testIterationsCountToolStripTextBox;
        private ToolStrip testToolStrip;
        private Thread workerThread;
        private WorkspaceConfiguration workspaceConfiguration = new WorkspaceConfiguration();

        #endregion Global declarations

        #region Properties

        private ApplicationEventFilter eventFilter
        {
            get { return filterControl.Filter; }
            set
            {
                filterControl.Filter = value;
                eventTracerControl.Filter = value;
            }
        }

        #endregion

        #region Constructors

        public MainForm()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            eventTracerControl = new EventMultiTracerControl();
            eventTracerControl.Dock = DockStyle.Fill;
            eventTracerControl.TracingOptions = workspaceConfiguration.TracingOptions;
            //
            monitorTabPage.Controls.Add(eventTracerControl);
            //
            filterControl = new ApplicationEventFilterControl();
//			filterControl.Top = 0;
//			filterControl.Left = 0;
            filterControl.Dock = DockStyle.Fill;

            //
            filterTabPage.Controls.Add(filterControl);
            //

            //
            observerConnectionsControl = new ObserversConfigurationControl();
            observerConnectionsControl.Dock = DockStyle.Fill;
            connsTabPage.Controls.Add(observerConnectionsControl);
            //
            //
            eventManagerConfigControl = new EventHandlerManagerConfigurationEditorControl();
            eventManagerConfigControl.Dock = DockStyle.Fill;

            //
            eventStub = new TraceEventHandlerEventStub();
            eventStub.EventHandled += eventStub_EventHandled;
            //
            observerConnectionsControl.EventStub = eventStub;
            //
            eventFilter.Changed += filter_Changed;

            //
            eventTracerControl.Filter = eventFilter;

            Closing += MainForm_Closing;
            KeyDown += MainForm_KeyDown;
            mainTabControl.KeyDown += MainForm_KeyDown;
            ResizeEnd += MainForm_ResizeEnd;

            try
            {
                RemotingConfiguration.Configure
                    (
                    AppDomain.CurrentDomain.SetupInformation.ConfigurationFile
                    );
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            for (int i = 0; i < mainTabControl.TabPages.Count; i++)
            {
                mainTabControl.TabPages[i].Text += " (" + i + ")";
            }
            //this.testEntriesQtyTextBox.Text = this.testIterations.ToString();
        }

        private void MainForm_ResizeEnd(object sender, EventArgs e)
        {
            ApplicationEventsGuiSettings.Default.MainFormSize = Size;
        }

        #endregion Constructors

        #region Functions

        private void handleEvent(TraceEvent e)
        {
            //
            if (workspaceConfiguration.TracingOptions.LogToTraceControl)
            {
                eventTracerControl.Invoke
                    (
                    new TraceEventHandlerDelegate(eventTracerControl.HandleEvent),
                    new object[] {e}
                    );
            }

            TraceEventHandler.Instance.HandleEvent
                (
                e
                );
        }

        private void test()
        {
            DateTime preStartStamp = DateTime.Now;

            for (long i = 0; i < testIterations; i++)
            {
                handleEvent
                    (
                    new TraceEvent
                        (
                        i,
                        "SelfTest" + i,
                        new ContextIdentifier(),
                        null
                        ));
            }
            // Anonymous method use.
            VoidDelegate stopDelegate = delegate
                                            {
                                                testIterationsCountToolStripTextBox.Enabled = true;
                                                startTestToolStripButton.Enabled = true;
                                                stopTestToolStripButton.Enabled = false;
                                            };
            MessageBox.Show
                (
                String.Format
                    (
                    "Test finished. Items count is {0}. Execution time {1} ms",
                    testIterations,
                    (DateTime.Now - preStartStamp).TotalMilliseconds
                    ));

            Invoke
                (
                stopDelegate
                );
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Thread.CurrentThread.Name = "MainGuiThread";
            Application.Run(new MainForm());
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }


        private void switchControls(Control controlToEnable, Control controlToDisable)
        {
            controlToEnable.Enabled = true;
            controlToDisable.Enabled = false;
        }

        private void eventWrapper_EventHandled(object sender, TraceEventArgs eventArgs)
        {
        }

        private void eventStub_EventHandled(TraceEventArgs eventArgs)
        {
            handleEvent(eventArgs.Event);
        }

        private void MainForm_Closing(object sender, CancelEventArgs e)
        {
//			if (connected)
//			{
//				try
//				{
//					eventWrapper.EventHandled -= 
//						new TraceEventDelegate(eventStub.HandleEvent);
//				}
//				catch (Exception ex)
//				{
//					MessageBox.Show(ex.ToString());
//				}
//			}
            ApplicationEventsGuiSettings.Default.Save();
        }


        private void saveMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not implemented yet! Use save as.");
        }

        private void setWorkspaceConfigurationFromGui()
        {
            workspaceConfiguration = new WorkspaceConfiguration();
            workspaceConfiguration.Filter = eventFilter;
            workspaceConfiguration.ManagerConfiguration = eventManagerConfigControl.Configuration;
            workspaceConfiguration.ObserverConnections = observerConnectionsControl.Connections;
            workspaceConfiguration.TracingOptions = eventTracerControl.TracingOptions;
            workspaceConfiguration.KeyPointers = eventTracerControl.KeyPointers;
        }

        private void setGuiFromWorkspaceConfiguration()
        {
            reAssignOnChangeHandler(eventFilter, workspaceConfiguration.Filter);
            eventFilter = workspaceConfiguration.Filter;
            eventManagerConfigControl.Configuration = workspaceConfiguration.ManagerConfiguration;
            observerConnectionsControl.Connections = workspaceConfiguration.ObserverConnections;
            eventTracerControl.TracingOptions = workspaceConfiguration.TracingOptions;
            eventTracerControl.KeyPointers = workspaceConfiguration.KeyPointers;
        }

        private void reAssignOnChangeHandler(IChangeEventRaiser oldRaiser, IChangeEventRaiser newRaiser)
        {
            oldRaiser.Changed -= filter_Changed;
            newRaiser.Changed += filter_Changed;
        }

        private void saveAsMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.DefaultExt = "xml";
            saveFileDialog1.Filter = "Xml files (*.xml)|*.xml|All files (*.*)|*.*";
            DialogResult dr = saveFileDialog1.ShowDialog();

            if (dr == DialogResult.Cancel) return;

            setWorkspaceConfigurationFromGui();

            if (saveFileDialog1.FileName != null || saveFileDialog1.FileName != String.Empty)
            {
                SerializationUtility.Serialize2File
                    (
                    workspaceConfiguration, saveFileDialog1.FileName, false, false
                    );
            }
            fileNameStatusBarPanel.Text = saveFileDialog1.FileName;
        }

        private void openMenuItem_Click(object sender, EventArgs e)
        {
            // Example: "Text files (*.txt)|*.txt|All files (*.*)|*.*"
            openFileDialog1.DefaultExt = "xml";
            openFileDialog1.Filter = "Xml files (*.xml)|*.xml|All files (*.*)|*.*";

            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.Cancel) return;
            if (openFileDialog1.FileName != null || openFileDialog1.FileName != String.Empty)
            {
                try
                {
                    workspaceConfiguration =
                        (WorkspaceConfiguration) SerializationUtility.DeserializeFromFile
                                                     (
                                                     openFileDialog1.FileName,
                                                     typeof (WorkspaceConfiguration)
                                                     );
                    // updateConfigurationGui();
                    // temp placement
                    setGuiFromWorkspaceConfiguration();
                    fileNameStatusBarPanel.Text = saveFileDialog1.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Probably not a valid TraceEventFilter Document! " + ex);
                }
            }
        }

        private void filter_Changed(object sender, EventArgs e)
        {
//			MessageBox.Show(this, SerializationUtility.Serialize2String(this.eventFilter));
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            keyDownStatusBarPanel.Text += " " + e.KeyCode;
            if (e.KeyCode == Keys.ControlKey) return;

            if (e.Control && e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9)
            {
                int tabIndex = e.KeyCode - Keys.D0;
                if (tabIndex > mainTabControl.TabCount)
                {
                    MessageBox.Show(this, "Invalid tab index!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                mainTabControl.SelectedIndex = tabIndex;
            }

            //Trace.WriteLine("Key=" + Convert.ToInt32(e.KeyCode).ToString());
            keyDownStatusBarPanel.Text = String.Empty;
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.monitorTabPage = new System.Windows.Forms.TabPage();
            this.encryptionTabPage = new System.Windows.Forms.TabPage();
            this.encryptionInfoLabel = new System.Windows.Forms.Label();
            this.connsTabPage = new System.Windows.Forms.TabPage();
            this.filterTabPage = new System.Windows.Forms.TabPage();
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.fileNameStatusBarPanel = new System.Windows.Forms.StatusBarPanel();
            this.keyDownStatusBarPanel = new System.Windows.Forms.StatusBarPanel();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.saveAsMenuItem = new System.Windows.Forms.MenuItem();
            this.saveMenuItem = new System.Windows.Forms.MenuItem();
            this.openMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.showTestToolBarMenuItem = new System.Windows.Forms.MenuItem();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.mainToolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.testToolStrip = new System.Windows.Forms.ToolStrip();
            this.startTestToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.stopTestToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.testIterationsCountToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.mainTabControl.SuspendLayout();
            this.encryptionTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) (this.fileNameStatusBarPanel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.keyDownStatusBarPanel)).BeginInit();
            this.mainToolStripContainer.ContentPanel.SuspendLayout();
            this.mainToolStripContainer.TopToolStripPanel.SuspendLayout();
            this.mainToolStripContainer.SuspendLayout();
            this.testToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTabControl
            // 
            this.mainTabControl.Controls.Add(this.monitorTabPage);
            this.mainTabControl.Controls.Add(this.encryptionTabPage);
            this.mainTabControl.Controls.Add(this.connsTabPage);
            this.mainTabControl.Controls.Add(this.filterTabPage);
            this.mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabControl.Location = new System.Drawing.Point(0, 0);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.Padding = new System.Drawing.Point(0, 0);
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(936, 212);
            this.mainTabControl.TabIndex = 0;
            this.mainTabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // monitorTabPage
            // 
            this.monitorTabPage.Location = new System.Drawing.Point(4, 22);
            this.monitorTabPage.Name = "monitorTabPage";
            this.monitorTabPage.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.monitorTabPage.Size = new System.Drawing.Size(928, 186);
            this.monitorTabPage.TabIndex = 0;
            this.monitorTabPage.Text = "Monitor";
            this.monitorTabPage.UseVisualStyleBackColor = true;
            // 
            // encryptionTabPage
            // 
            this.encryptionTabPage.Controls.Add(this.encryptionInfoLabel);
            this.encryptionTabPage.Location = new System.Drawing.Point(4, 22);
            this.encryptionTabPage.Name = "encryptionTabPage";
            this.encryptionTabPage.Size = new System.Drawing.Size(928, 186);
            this.encryptionTabPage.TabIndex = 9;
            this.encryptionTabPage.Text = "Encryption";
            this.encryptionTabPage.UseVisualStyleBackColor = true;
            // 
            // encryptionInfoLabel
            // 
            this.encryptionInfoLabel.AutoSize = true;
            this.encryptionInfoLabel.Location = new System.Drawing.Point(8, 10);
            this.encryptionInfoLabel.Name = "encryptionInfoLabel";
            this.encryptionInfoLabel.Size = new System.Drawing.Size(457, 13);
            this.encryptionInfoLabel.TabIndex = 0;
            this.encryptionInfoLabel.Text =
                "This tab will be used for managing keys, containers and encryption/decryption of " +
                "the messages.";
            // 
            // connsTabPage
            // 
            this.connsTabPage.Location = new System.Drawing.Point(4, 22);
            this.connsTabPage.Name = "connsTabPage";
            this.connsTabPage.Size = new System.Drawing.Size(928, 186);
            this.connsTabPage.TabIndex = 6;
            this.connsTabPage.Text = "Observers";
            this.connsTabPage.UseVisualStyleBackColor = true;
            // 
            // filterTabPage
            // 
            this.filterTabPage.Location = new System.Drawing.Point(4, 22);
            this.filterTabPage.Name = "filterTabPage";
            this.filterTabPage.Size = new System.Drawing.Size(928, 186);
            this.filterTabPage.TabIndex = 1;
            this.filterTabPage.Text = "Filter";
            this.filterTabPage.UseVisualStyleBackColor = true;
            // 
            // statusBar1
            // 
            this.statusBar1.Location = new System.Drawing.Point(0, 244);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[]
                                                {
                                                    this.fileNameStatusBarPanel,
                                                    this.keyDownStatusBarPanel
                                                });
            this.statusBar1.ShowPanels = true;
            this.statusBar1.Size = new System.Drawing.Size(936, 22);
            this.statusBar1.SizingGrip = false;
            this.statusBar1.TabIndex = 1;
            this.statusBar1.Text = "statusBar1";
            // 
            // fileNameStatusBarPanel
            // 
            this.fileNameStatusBarPanel.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            this.fileNameStatusBarPanel.Name = "fileNameStatusBarPanel";
            this.fileNameStatusBarPanel.Width = 10;
            // 
            // keyDownStatusBarPanel
            // 
            this.keyDownStatusBarPanel.Name = "keyDownStatusBarPanel";
            this.keyDownStatusBarPanel.Text = "None";
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[]
                                                  {
                                                      this.menuItem1,
                                                      this.menuItem2,
                                                      this.menuItem5
                                                  });
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[]
                                                  {
                                                      this.saveAsMenuItem,
                                                      this.saveMenuItem,
                                                      this.openMenuItem,
                                                      this.menuItem4
                                                  });
            this.menuItem1.Text = "&File";
            // 
            // saveAsMenuItem
            // 
            this.saveAsMenuItem.Index = 0;
            this.saveAsMenuItem.Text = "Save &As ...";
            this.saveAsMenuItem.Click += new System.EventHandler(this.saveAsMenuItem_Click);
            // 
            // saveMenuItem
            // 
            this.saveMenuItem.Index = 1;
            this.saveMenuItem.Text = "&Save";
            this.saveMenuItem.Click += new System.EventHandler(this.saveMenuItem_Click);
            // 
            // openMenuItem
            // 
            this.openMenuItem.Index = 2;
            this.openMenuItem.Text = "&Open";
            this.openMenuItem.Click += new System.EventHandler(this.openMenuItem_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 3;
            this.menuItem4.Text = "Options";
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 1;
            this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[]
                                                  {
                                                      this.menuItem3
                                                  });
            this.menuItem2.Text = "Help";
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 0;
            this.menuItem3.Text = "About";
            this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 2;
            this.menuItem5.MenuItems.AddRange(new System.Windows.Forms.MenuItem[]
                                                  {
                                                      this.showTestToolBarMenuItem
                                                  });
            this.menuItem5.Text = "View";
            // 
            // showTestToolBarMenuItem
            // 
            this.showTestToolBarMenuItem.Checked =
                global::Tools.Tracing.UI.ApplicationEventsGuiSettings.Default.showTestToolBarMenuItemChecked;
            this.showTestToolBarMenuItem.Index = 0;
            this.showTestToolBarMenuItem.Text = "Test Toolbar";
            this.showTestToolBarMenuItem.Click += new System.EventHandler(this.menuItem6_Click);
            // 
            // mainToolStripContainer
            // 
            this.mainToolStripContainer.Anchor =
                ((System.Windows.Forms.AnchorStyles)
                 ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                    | System.Windows.Forms.AnchorStyles.Left)
                   | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // mainToolStripContainer.ContentPanel
            // 
            this.mainToolStripContainer.ContentPanel.Controls.Add(this.mainTabControl);
            this.mainToolStripContainer.ContentPanel.Size = new System.Drawing.Size(936, 212);
            this.mainToolStripContainer.Location = new System.Drawing.Point(0, 1);
            this.mainToolStripContainer.Name = "mainToolStripContainer";
            this.mainToolStripContainer.Size = new System.Drawing.Size(936, 237);
            this.mainToolStripContainer.TabIndex = 2;
            this.mainToolStripContainer.Text = "toolStripContainer1";
            // 
            // mainToolStripContainer.TopToolStripPanel
            // 
            this.mainToolStripContainer.TopToolStripPanel.Controls.Add(this.testToolStrip);
            // 
            // testToolStrip
            // 
            this.testToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.testToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
                                                  {
                                                      this.startTestToolStripButton,
                                                      this.stopTestToolStripButton,
                                                      this.testIterationsCountToolStripTextBox
                                                  });
            this.testToolStrip.Location = new System.Drawing.Point(3, 0);
            this.testToolStrip.Name = "testToolStrip";
            this.testToolStrip.Size = new System.Drawing.Size(108, 25);
            this.testToolStrip.TabIndex = 0;
            // 
            // startTestToolStripButton
            // 
            this.startTestToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.startTestToolStripButton.Image = global::Tools.Tracing.UI.Properties.Resources.servicerunning;
            this.startTestToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.startTestToolStripButton.Name = "startTestToolStripButton";
            this.startTestToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.startTestToolStripButton.Text = "startTestToolStripButton";
            this.startTestToolStripButton.ToolTipText = "Start test events submission";
            this.startTestToolStripButton.Click += new System.EventHandler(this.startTestToolStripButton_Click);
            // 
            // stopTestToolStripButton
            // 
            this.stopTestToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.stopTestToolStripButton.Image = global::Tools.Tracing.UI.Properties.Resources.servicestopped;
            this.stopTestToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stopTestToolStripButton.Name = "stopTestToolStripButton";
            this.stopTestToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.stopTestToolStripButton.Text = "stopTestToolStripButton";
            this.stopTestToolStripButton.ToolTipText = "Stop test events submission";
            this.stopTestToolStripButton.Click += new System.EventHandler(this.stopTestToolStripButton_Click);
            // 
            // testIterationsCountToolStripTextBox
            // 
            this.testIterationsCountToolStripTextBox.Name = "testIterationsCountToolStripTextBox";
            this.testIterationsCountToolStripTextBox.Size = new System.Drawing.Size(50, 25);
            this.testIterationsCountToolStripTextBox.Text = "100";
            this.testIterationsCountToolStripTextBox.ToolTipText = "Number of test events to generate";
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(936, 266);
            this.Controls.Add(this.mainToolStripContainer);
            this.Controls.Add(this.statusBar1);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Size",
                                                                   global::Tools.Tracing.UI.ApplicationEventsGuiSettings
                                                                       .Default, "MainFormSize", true,
                                                                   System.Windows.Forms.DataSourceUpdateMode.
                                                                       OnPropertyChanged));
            this.Menu = this.mainMenu1;
            this.Name = "MainForm";
            this.Text = "EventTracer";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.mainTabControl.ResumeLayout(false);
            this.encryptionTabPage.ResumeLayout(false);
            this.encryptionTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize) (this.fileNameStatusBarPanel)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.keyDownStatusBarPanel)).EndInit();
            this.mainToolStripContainer.ContentPanel.ResumeLayout(false);
            this.mainToolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this.mainToolStripContainer.TopToolStripPanel.PerformLayout();
            this.mainToolStripContainer.ResumeLayout(false);
            this.mainToolStripContainer.PerformLayout();
            this.testToolStrip.ResumeLayout(false);
            this.testToolStrip.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        #endregion Functions

        private void menuItem3_Click(object sender, EventArgs e)
        {
            new AboutBox().ShowDialog(this);
        }

        private void startTestToolStripButton_Click(object sender, EventArgs e)
        {
            testIterationsCountToolStripTextBox.Enabled = false;
            startTestToolStripButton.Enabled = false;
            stopTestToolStripButton.Enabled = true;
            //
            testIterations = Convert.ToInt64(testIterationsCountToolStripTextBox.Text);
            //
            workerThread = new Thread(test);
            workerThread.Priority = ThreadPriority.BelowNormal;
            workerThread.IsBackground = true;
            workerThread.Start();
        }

        private void stopTestToolStripButton_Click(object sender, EventArgs e)
        {
            testIterationsCountToolStripTextBox.Enabled = true;
            startTestToolStripButton.Enabled = true;
            stopTestToolStripButton.Enabled = false;

            try
            {
                workerThread.Abort();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void menuItem6_Click(object sender, EventArgs e)
        {
            var mnuItem = (MenuItem) sender;

            mnuItem.Checked = !mnuItem.Checked;

            testToolStrip.Visible = mnuItem.Checked;
        }
    }
}