using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Xml;
using System.Xml.XPath;
using System.Diagnostics;

using Tools.Tracing.Common;
using Tools.UI.Windows.Descriptors;
using Tools.Core.Utils;
using Tools.Core;
using Tools.Core.Context;


namespace Tools.Tracing.UI
{

	/// <summary>
	/// Summary description for LogControl.
	/// </summary>
	public partial class EventMultiTracerControl : 
		System.Windows.Forms.UserControl, ITraceEventHandler
	{
		
		#region Global Declarations

		private int scrollCounter = 0;
		private int countToShow = 10;
		private bool filtered = false;
        private bool searchIsRunning = false;
		private System.Windows.Forms.RichTextBox richTextBox1;
        private ContextHolderPointersControl keysControl;
		private IContainer components;
        private delegate void IntVoidDelegate(int val);
        private int filteredMessagesCount = 0;

        #region Column Headers


        #endregion Column Headers


		private System.Windows.Forms.ContextMenu monitorContolContextMenu;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;

		private System.Windows.Forms.TabPage propertyTabPage;
		private System.Windows.Forms.TabPage FilterTabPage;
		private System.Windows.Forms.PropertyGrid tracingOptionsPropertyGrid;
		private System.Windows.Forms.TabControl tracingTabControl;

		private FilterViewControl _filterViewControl = null;
		private TraceEventCollection _eventsCache = new TraceEventCollection();
		private TraceEventCollection _runEventsCache = new TraceEventCollection();
		private System.Windows.Forms.MenuItem menuItem9;
		private System.Windows.Forms.SaveFileDialog saveMonitorToFileDialog;
		private System.Windows.Forms.MenuItem saveMonitorToFileMenuItem;
		private System.Windows.Forms.MenuItem runFileToMonitorMenuItem;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private TracingOptions _tracingOptions = null;

        #region LogView Details

        float realLVFontSize = 0f;
        private System.Windows.Forms.TabControl logEntryViewsTabControl;

		private TextTransformerTabPage logTransform1TabPage;
		private TextTransformerTabPage logTransform2TabPage;
		private System.Windows.Forms.TabPage xPathTabPage;
        private System.Windows.Forms.TabPage ieTabPage;

        #endregion LogView Details

        private ApplicationEventFilter _filter = null;

		private DescriptiveListEditorControl xPathStatementControl = null;
		private System.Windows.Forms.MenuItem clearAllMenuItem;
		private System.Windows.Forms.MenuItem clearCacheMenuItem;
		private System.Windows.Forms.MenuItem clearGuiMenuItem;
        private int currentCount = 0;

		// proof of concept for filtering
		private XmlDocument xEventsCache = new XmlDocument();
        private XPathNavigator navigator;
		private SplitContainer splitContainer1;
		private SplitContainer splitContainer2;
		private SplitContainer splitContainer3;
		private ContextMenuStrip filterContextMenuStrip;
		private ToolStripMenuItem applyFilterToolStripMenuItem;
		private ToolStripMenuItem cancelFilterToolStripMenuItem;
		private TabPage xQueryTabPage;
        //private DescriptiveListEditorControl xQueryDescriptiveNameValueCollectionEditorControl;
		private MonitorPanel monitorPanel;
        private ToolStrip toolStrip1;
        private ToolStripButton showGridViewToolStripButton;
        private ToolStripComboBox filterBySameFieldToolStripComboBox;
        private ToolStrip countersToolStrip;
        private ToolStripLabel shownCountToolStripLabel;
        private ToolStripTextBox shownCountToolStripTextBox;
        private ToolStripLabel cachedCountToolStripLabel;
        private ToolStripTextBox cachedCountToolStripTextBox;
        private ToolStripLabel filteredOutCountToolStripLabel;
        private ToolStripTextBox filteredOutCountToolStripTextBox;
        private ToolStripSplitButton toolStripSplitButton;
        private ToolStripMenuItem cachedCountToolStripMenuItem;
        private ToolStripMenuItem shownCountToolStripMenuItem;
        private ToolStripMenuItem filteredOutToolStripMenuItem;
        private ToolStripContainer monitorToolStripContainer;
        private ToolStripButton filterByFieldToolStripButton;
        private ToolStripProgressBar searchToolStripProgressBar;
        private BackgroundWorker searchBackgroundWorker;
        private MenuItem saveFilteredMonitorMenuItem;
        private ContextMenuStrip monitorControlContextMenuStrip;
        private ToolStripMenuItem saveFilteredMonitorToolStripMenuItem;
		private XmlDocument xEventsFilteredCache = new XmlDocument();
		//		private XPathDocument xPathDocument = new XPathDocument(
        private delegate void VoidDelegate();

		#endregion Global Declarations

		#region Properties

		public ApplicationEventFilter Filter
		{
			get
			{
				return _filter;
			}
			set
			{
				if (_filter == value) return;
				_filter = value;
				_filter.Changed += new EventHandler(filter_Changed);
				_filterViewControl.Filter = _filter;
			}
		}
		
		public TracingOptions TracingOptions
		{
			get
			{
				return _tracingOptions;
			}
			set
			{
				_tracingOptions = (value == null) ? new TracingOptions() : value;
				tracingOptionsPropertyGrid.SelectedObject = _tracingOptions;
			}
		}
        public List<ContextHolderIdDescriptorPointer> KeyPointers
        {
            get
            {
                return this.keysControl.Pointers;
            }
            set
            {
                keysControl.Pointers = value;
            }
        }

		#endregion Properties

		#region Constructors

		public EventMultiTracerControl()
		{
			try
			{
				InitializeComponent();

				_tracingOptions = new TracingOptions();
				this.tracingOptionsPropertyGrid.SelectedObject = _tracingOptions;

				_filterViewControl = new FilterViewControl();
				_filterViewControl.Dock = DockStyle.Fill;
				this.FilterTabPage.Controls.Add(_filterViewControl);
				//
				xPathStatementControl = new DescriptiveListEditorControl();
				xPathStatementControl.Dock = DockStyle.Fill;
				xPathStatementControl.LoadFromFile(TracingOptions.XPathFilePath);
				this.xPathTabPage.Controls.Add(xPathStatementControl);

				//_filterViewControl.Filter = 
				// TODO: This will be a dynamic section (SD)
				this.logTransform1TabPage = new TextTransformerTabPage();
				this.logTransform2TabPage = new TextTransformerTabPage
					(
					new XsltTextTransformer
					(
					AppDomain.CurrentDomain.SetupInformation.ApplicationBase +
					@"XsltEventTransform.xslt"
					));
				// 
				// logTransform1TabPage
				// 
				this.logTransform1TabPage.Location = new System.Drawing.Point(4, 22);
				this.logTransform1TabPage.Name = "logTransform1TabPage";
				this.logTransform1TabPage.Size = 
					new Size
					(
					logEntryViewsTabControl.Width,
					logEntryViewsTabControl.Height - 22
					);
				this.logTransform1TabPage.TabIndex = 0;
				this.logTransform1TabPage.Text = "Xml";
				// 
				// logTransform2TabPage
				// 
				this.logTransform2TabPage.Location = new System.Drawing.Point(4, 22);
				this.logTransform2TabPage.Name = "logTransform2TabPage";
				this.logTransform2TabPage.Size = 
					new Size
					(
					logEntryViewsTabControl.Width,
					logEntryViewsTabControl.Height - 22
					);
				this.logTransform2TabPage.TabIndex = 1;
				this.logTransform2TabPage.Text = "Message";
				//
				this.logEntryViewsTabControl.Controls.Add(this.logTransform1TabPage);
				this.logEntryViewsTabControl.Controls.Add(this.logTransform2TabPage);
				//
				//
				this.Resize +=new EventHandler(LogControl_Resize);

				#region  xpath quering proof of concept

				XmlElement xRoot = xEventsCache.CreateElement(null, "ArrayOfApplicationEvent", null);
				xEventsCache.AppendChild(xRoot);

				if (xPathStatementControl.ContextMenuStrip == null)
				{
					xPathStatementControl.ContextMenuStrip =
						new ContextMenuStrip();
				}
				xPathStatementControl.ContextMenuStrip.Items.AddRange
					(
					new ToolStripMenuItem[2]
					{
					applyFilterToolStripMenuItem,
					cancelFilterToolStripMenuItem
					});
				applyFilterToolStripMenuItem.Click += new EventHandler(applyMenuItem_Click);
				cancelFilterToolStripMenuItem.Click +=new EventHandler(cancelFilterMenuItem_Click);
				
				XmlElement xFilterRoot = xEventsFilteredCache.CreateElement
					(
					null, 
					"ArrayOfApplicationEvent", 
					null
					);
				xEventsFilteredCache.AppendChild(xFilterRoot);

				//xPathStatementControl.applyMenuItem.Click +=new EventHandler(applyMenuItem_Click);
				//xPathStatementControl.cancelFilterMenuItem.Click +=new EventHandler(cancelFilterMenuItem_Click);

				#endregion  xpath quering proof of concept

				#region Subscribing to the list view scroll event

				monitorPanel.Scroll +=new ScrollEventHandler(listView1_Scrolled);
				

				#endregion Subscribing to the list view scroll event

                keysControl = new ContextHolderPointersControl();
                keysControl.Dock = DockStyle.Fill;

                xQueryTabPage.Controls.Add(keysControl);
                
                monitorPanel.ContextMenu = this.monitorContolContextMenu;
				monitorPanel.ItemsListView.SelectedIndexChanged += new EventHandler(listView1_SelectedIndexChanged);
				monitorPanel.ItemsListView.SetScrollBarMaxValue(1);
				countToShow = 20; //listView1.DisplayRectangle.Height / Convert.ToUInt16(listView1.Font.GetHeight());
                this.filterBySameFieldToolStripComboBox.ComboBox.DataSource = TraceRecord.CorrelationFieldNames;

                searchBackgroundWorker.DoWork += new DoWorkEventHandler(searchBackgroundWorker_DoFilterBySameFieldWork);
                searchBackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(searchBackgroundWorker_RunWorkerCompleted);

                
                navigator = xEventsCache.CreateNavigator();
                navigator.MoveToChild(XPathNodeType.Element);

			}
			catch (Exception e)
			{
				MessageBox.Show(e.ToString());
			}


		}

		
		#endregion Constructors

		#region IDisposable implementation

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		
		//		public void Clear()
		//		{
		//			ClearGui();
		//			ClearEventsCache();
		//		}

		public void ClearGui()
		{
			richTextBox1.Clear();
			monitorPanel.ClearItems();
			monitorPanel.ItemsListView.SetMaxIndex(1);
            monitorPanel.MessagesCount = 0;
            filteredMessagesCount = 0;
            showFilteredOutMessagesCount(filteredMessagesCount);
            showShownMessagesCount(0);
			// TODO: Iterate through the collection in the future (SD)
			logTransform1TabPage.SetText(String.Empty);
            // POC - to test
            System.GC.Collect();
			//logTransform2TabPage.SetText(String.Empty);
		}

		public void ClearEventsCache()
		{
			_eventsCache.Clear();
            currentCount = 0;
			xEventsCache.DocumentElement.RemoveAll();
            showCachedMessagesCount(0);
		}

		public void ClearAll()
		{
			ClearGui();
			ClearEventsCache();
		}

		#endregion IDisposable implementation

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.monitorContolContextMenu = new System.Windows.Forms.ContextMenu();
            this.clearAllMenuItem = new System.Windows.Forms.MenuItem();
            this.clearCacheMenuItem = new System.Windows.Forms.MenuItem();
            this.clearGuiMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.saveMonitorToFileMenuItem = new System.Windows.Forms.MenuItem();
            this.runFileToMonitorMenuItem = new System.Windows.Forms.MenuItem();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.tracingTabControl = new System.Windows.Forms.TabControl();
            this.propertyTabPage = new System.Windows.Forms.TabPage();
            this.tracingOptionsPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.FilterTabPage = new System.Windows.Forms.TabPage();
            this.xPathTabPage = new System.Windows.Forms.TabPage();
            this.xQueryTabPage = new System.Windows.Forms.TabPage();
            this.logEntryViewsTabControl = new System.Windows.Forms.TabControl();
            this.saveMonitorToFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.monitorToolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.countersToolStrip = new System.Windows.Forms.ToolStrip();
            this.shownCountToolStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.shownCountToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.cachedCountToolStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.cachedCountToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.filteredOutCountToolStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.filteredOutCountToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSplitButton = new System.Windows.Forms.ToolStripSplitButton();
            this.cachedCountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shownCountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filteredOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.showGridViewToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.filterBySameFieldToolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.filterByFieldToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.searchToolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.filterContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.applyFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cancelFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.saveFilteredMonitorMenuItem = new System.Windows.Forms.MenuItem();
            this.monitorPanel = new Tools.Tracing.UI.MonitorPanel();
            this.monitorControlContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.saveFilteredMonitorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tracingTabControl.SuspendLayout();
            this.propertyTabPage.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.monitorToolStripContainer.BottomToolStripPanel.SuspendLayout();
            this.monitorToolStripContainer.ContentPanel.SuspendLayout();
            this.monitorToolStripContainer.SuspendLayout();
            this.countersToolStrip.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.filterContextMenuStrip.SuspendLayout();
            this.monitorControlContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // monitorContolContextMenu
            // 
            this.monitorContolContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.clearAllMenuItem,
            this.clearCacheMenuItem,
            this.clearGuiMenuItem,
            this.menuItem9,
            this.menuItem2});
            // 
            // clearAllMenuItem
            // 
            this.clearAllMenuItem.Index = 0;
            this.clearAllMenuItem.Text = "Clear &All";
            this.clearAllMenuItem.Click += new System.EventHandler(this.clearAllMenuItem_Click);
            // 
            // clearCacheMenuItem
            // 
            this.clearCacheMenuItem.Index = 1;
            this.clearCacheMenuItem.Text = "Clear &Cache";
            this.clearCacheMenuItem.Click += new System.EventHandler(this.clearCacheMenuItem_Click);
            // 
            // clearGuiMenuItem
            // 
            this.clearGuiMenuItem.Index = 2;
            this.clearGuiMenuItem.Text = "Clear &Gui";
            this.clearGuiMenuItem.Click += new System.EventHandler(this.clearGuiMenuItem_Click);
            // 
            // menuItem9
            // 
            this.menuItem9.Index = 3;
            this.menuItem9.Text = "-";
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 4;
            this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem3,
            this.saveMonitorToFileMenuItem,
            this.runFileToMonitorMenuItem,
            this.saveFilteredMonitorMenuItem});
            this.menuItem2.Text = "File";
            // 
            // menuItem3
            // 
            this.menuItem3.Enabled = false;
            this.menuItem3.Index = 0;
            this.menuItem3.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
            this.menuItem3.Text = "Open File To Monitor";
            this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
            // 
            // saveMonitorToFileMenuItem
            // 
            this.saveMonitorToFileMenuItem.Index = 1;
            this.saveMonitorToFileMenuItem.Text = "Save Monitor";
            this.saveMonitorToFileMenuItem.Click += new System.EventHandler(this.saveMonitorToFileMenuItem_Click);
            // 
            // runFileToMonitorMenuItem
            // 
            this.runFileToMonitorMenuItem.Index = 2;
            this.runFileToMonitorMenuItem.Text = "Run File To Monitor";
            this.runFileToMonitorMenuItem.Click += new System.EventHandler(this.runFileToMonitorMenuItem_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(544, 246);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // tracingTabControl
            // 
            this.tracingTabControl.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tracingTabControl.Controls.Add(this.propertyTabPage);
            this.tracingTabControl.Controls.Add(this.FilterTabPage);
            this.tracingTabControl.Controls.Add(this.xPathTabPage);
            this.tracingTabControl.Controls.Add(this.xQueryTabPage);
            this.tracingTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tracingTabControl.Location = new System.Drawing.Point(0, 0);
            this.tracingTabControl.Multiline = true;
            this.tracingTabControl.Name = "tracingTabControl";
            this.tracingTabControl.SelectedIndex = 0;
            this.tracingTabControl.Size = new System.Drawing.Size(321, 179);
            this.tracingTabControl.TabIndex = 5;
            // 
            // propertyTabPage
            // 
            this.propertyTabPage.Controls.Add(this.tracingOptionsPropertyGrid);
            this.propertyTabPage.Location = new System.Drawing.Point(4, 4);
            this.propertyTabPage.Name = "propertyTabPage";
            this.propertyTabPage.Size = new System.Drawing.Size(313, 153);
            this.propertyTabPage.TabIndex = 0;
            this.propertyTabPage.Text = "Properties";
            // 
            // tracingOptionsPropertyGrid
            // 
            this.tracingOptionsPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tracingOptionsPropertyGrid.HelpVisible = false;
            this.tracingOptionsPropertyGrid.Location = new System.Drawing.Point(0, 0);
            this.tracingOptionsPropertyGrid.Name = "tracingOptionsPropertyGrid";
            this.tracingOptionsPropertyGrid.Size = new System.Drawing.Size(313, 153);
            this.tracingOptionsPropertyGrid.TabIndex = 0;
            this.tracingOptionsPropertyGrid.ToolbarVisible = false;
            // 
            // FilterTabPage
            // 
            this.FilterTabPage.Location = new System.Drawing.Point(4, 4);
            this.FilterTabPage.Name = "FilterTabPage";
            this.FilterTabPage.Size = new System.Drawing.Size(313, 153);
            this.FilterTabPage.TabIndex = 1;
            this.FilterTabPage.Text = "Filter";
            // 
            // xPathTabPage
            // 
            this.xPathTabPage.Location = new System.Drawing.Point(4, 4);
            this.xPathTabPage.Name = "xPathTabPage";
            this.xPathTabPage.Size = new System.Drawing.Size(313, 153);
            this.xPathTabPage.TabIndex = 2;
            this.xPathTabPage.Text = "XPath";
            // 
            // xQueryTabPage
            // 
            this.xQueryTabPage.Location = new System.Drawing.Point(4, 4);
            this.xQueryTabPage.Name = "xQueryTabPage";
            this.xQueryTabPage.Size = new System.Drawing.Size(313, 153);
            this.xQueryTabPage.TabIndex = 3;
            this.xQueryTabPage.Text = "Keys";
            // 
            // logEntryViewsTabControl
            // 
            this.logEntryViewsTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logEntryViewsTabControl.Location = new System.Drawing.Point(0, 0);
            this.logEntryViewsTabControl.Name = "logEntryViewsTabControl";
            this.logEntryViewsTabControl.SelectedIndex = 0;
            this.logEntryViewsTabControl.Size = new System.Drawing.Size(596, 179);
            this.logEntryViewsTabControl.TabIndex = 6;
            // 
            // saveMonitorToFileDialog
            // 
            this.saveMonitorToFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(928, 376);
            this.splitContainer1.SplitterDistance = 190;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 8;
            this.splitContainer1.Text = "splitContainer1";
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.monitorToolStripContainer);
            this.splitContainer3.Size = new System.Drawing.Size(924, 186);
            this.splitContainer3.SplitterDistance = 157;
            this.splitContainer3.TabIndex = 8;
            this.splitContainer3.Text = "splitContainer3";
            // 
            // monitorToolStripContainer
            // 
            // 
            // monitorToolStripContainer.BottomToolStripPanel
            // 
            this.monitorToolStripContainer.BottomToolStripPanel.Controls.Add(this.countersToolStrip);
            this.monitorToolStripContainer.BottomToolStripPanel.Controls.Add(this.toolStrip1);
            this.monitorToolStripContainer.BottomToolStripPanel.Click += new System.EventHandler(this.toolStripContainer1_BottomToolStripPanel_Click);
            // 
            // monitorToolStripContainer.ContentPanel
            // 
            this.monitorToolStripContainer.ContentPanel.Controls.Add(this.monitorPanel);
            this.monitorToolStripContainer.ContentPanel.Size = new System.Drawing.Size(924, 107);
            this.monitorToolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.monitorToolStripContainer.Location = new System.Drawing.Point(0, 0);
            this.monitorToolStripContainer.Name = "monitorToolStripContainer";
            this.monitorToolStripContainer.Size = new System.Drawing.Size(924, 157);
            this.monitorToolStripContainer.TabIndex = 8;
            this.monitorToolStripContainer.Text = "tool";
            // 
            // countersToolStrip
            // 
            this.countersToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.countersToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.shownCountToolStripLabel,
            this.shownCountToolStripTextBox,
            this.cachedCountToolStripLabel,
            this.cachedCountToolStripTextBox,
            this.filteredOutCountToolStripLabel,
            this.filteredOutCountToolStripTextBox,
            this.toolStripSplitButton});
            this.countersToolStrip.Location = new System.Drawing.Point(3, 0);
            this.countersToolStrip.Name = "countersToolStrip";
            this.countersToolStrip.Size = new System.Drawing.Size(417, 25);
            this.countersToolStrip.TabIndex = 9;
            // 
            // shownCountToolStripLabel
            // 
            this.shownCountToolStripLabel.Name = "shownCountToolStripLabel";
            this.shownCountToolStripLabel.Size = new System.Drawing.Size(39, 22);
            this.shownCountToolStripLabel.Text = "Shown";
            // 
            // shownCountToolStripTextBox
            // 
            this.shownCountToolStripTextBox.Enabled = false;
            this.shownCountToolStripTextBox.Name = "shownCountToolStripTextBox";
            this.shownCountToolStripTextBox.ReadOnly = true;
            this.shownCountToolStripTextBox.Size = new System.Drawing.Size(80, 25);
            this.shownCountToolStripTextBox.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cachedCountToolStripLabel
            // 
            this.cachedCountToolStripLabel.Name = "cachedCountToolStripLabel";
            this.cachedCountToolStripLabel.Size = new System.Drawing.Size(43, 22);
            this.cachedCountToolStripLabel.Text = "Cached";
            // 
            // cachedCountToolStripTextBox
            // 
            this.cachedCountToolStripTextBox.Enabled = false;
            this.cachedCountToolStripTextBox.Name = "cachedCountToolStripTextBox";
            this.cachedCountToolStripTextBox.ReadOnly = true;
            this.cachedCountToolStripTextBox.Size = new System.Drawing.Size(80, 25);
            // 
            // filteredOutCountToolStripLabel
            // 
            this.filteredOutCountToolStripLabel.Name = "filteredOutCountToolStripLabel";
            this.filteredOutCountToolStripLabel.Size = new System.Drawing.Size(61, 22);
            this.filteredOutCountToolStripLabel.Text = "FilteredOut";
            // 
            // filteredOutCountToolStripTextBox
            // 
            this.filteredOutCountToolStripTextBox.Enabled = false;
            this.filteredOutCountToolStripTextBox.Name = "filteredOutCountToolStripTextBox";
            this.filteredOutCountToolStripTextBox.ReadOnly = true;
            this.filteredOutCountToolStripTextBox.Size = new System.Drawing.Size(80, 25);
            // 
            // toolStripSplitButton
            // 
            this.toolStripSplitButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cachedCountToolStripMenuItem,
            this.shownCountToolStripMenuItem,
            this.filteredOutToolStripMenuItem});
            this.toolStripSplitButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton.Name = "toolStripSplitButton";
            this.toolStripSplitButton.Size = new System.Drawing.Size(16, 22);
            this.toolStripSplitButton.Text = "toolStripSplitButton1";
            // 
            // cachedCountToolStripMenuItem
            // 
            this.cachedCountToolStripMenuItem.Checked = true;
            this.cachedCountToolStripMenuItem.CheckOnClick = true;
            this.cachedCountToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cachedCountToolStripMenuItem.Name = "cachedCountToolStripMenuItem";
            this.cachedCountToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.cachedCountToolStripMenuItem.Text = "Cached Count";
            // 
            // shownCountToolStripMenuItem
            // 
            this.shownCountToolStripMenuItem.Checked = true;
            this.shownCountToolStripMenuItem.CheckOnClick = true;
            this.shownCountToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.shownCountToolStripMenuItem.Name = "shownCountToolStripMenuItem";
            this.shownCountToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.shownCountToolStripMenuItem.Text = "Shown Count";
            // 
            // filteredOutToolStripMenuItem
            // 
            this.filteredOutToolStripMenuItem.Checked = true;
            this.filteredOutToolStripMenuItem.CheckOnClick = true;
            this.filteredOutToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.filteredOutToolStripMenuItem.Name = "filteredOutToolStripMenuItem";
            this.filteredOutToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.filteredOutToolStripMenuItem.Text = "Filtered Out";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showGridViewToolStripButton,
            this.filterBySameFieldToolStripComboBox,
            this.filterByFieldToolStripButton,
            this.searchToolStripProgressBar});
            this.toolStrip1.Location = new System.Drawing.Point(486, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(181, 25);
            this.toolStrip1.TabIndex = 8;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
            // 
            // showGridViewToolStripButton
            // 
            this.showGridViewToolStripButton.CheckOnClick = true;
            this.showGridViewToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.showGridViewToolStripButton.Image = global::Tools.Tracing.UI.Properties.Resources.ico163;
            this.showGridViewToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.showGridViewToolStripButton.Name = "showGridViewToolStripButton";
            this.showGridViewToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.showGridViewToolStripButton.Text = "Grid";
            this.showGridViewToolStripButton.CheckedChanged += new System.EventHandler(this.showGridViewToolStripButton_CheckedChanged);
            // 
            // filterBySameFieldToolStripComboBox
            // 
            this.filterBySameFieldToolStripComboBox.Name = "filterBySameFieldToolStripComboBox";
            this.filterBySameFieldToolStripComboBox.Size = new System.Drawing.Size(121, 25);
            this.filterBySameFieldToolStripComboBox.SelectedIndexChanged += new System.EventHandler(this.filterBySameFieldToolStripComboBox_SelectedIndexChanged);
            // 
            // filterByFieldToolStripButton
            // 
            this.filterByFieldToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.filterByFieldToolStripButton.Image = global::Tools.Tracing.UI.Properties.Resources.ico565;
            this.filterByFieldToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.filterByFieldToolStripButton.Name = "filterByFieldToolStripButton";
            this.filterByFieldToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.filterByFieldToolStripButton.Text = "Correlate by field";
            this.filterByFieldToolStripButton.Click += new System.EventHandler(this.filterByFieldToolStripButton_Click);
            // 
            // searchToolStripProgressBar
            // 
            this.searchToolStripProgressBar.Name = "searchToolStripProgressBar";
            this.searchToolStripProgressBar.Size = new System.Drawing.Size(100, 22);
            this.searchToolStripProgressBar.Step = 5;
            this.searchToolStripProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.searchToolStripProgressBar.Visible = false;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.logEntryViewsTabControl);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tracingTabControl);
            this.splitContainer2.Size = new System.Drawing.Size(928, 183);
            this.splitContainer2.SplitterDistance = 600;
            this.splitContainer2.SplitterWidth = 3;
            this.splitContainer2.TabIndex = 0;
            this.splitContainer2.Text = "splitContainer2";
            // 
            // filterContextMenuStrip
            // 
            this.filterContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.applyFilterToolStripMenuItem,
            this.cancelFilterToolStripMenuItem});
            this.filterContextMenuStrip.Name = "filterContextMenuStrip";
            this.filterContextMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.filterContextMenuStrip.Size = new System.Drawing.Size(185, 48);
            // 
            // applyFilterToolStripMenuItem
            // 
            this.applyFilterToolStripMenuItem.Name = "applyFilterToolStripMenuItem";
            this.applyFilterToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.applyFilterToolStripMenuItem.Text = "Apply Current Filter";
            this.applyFilterToolStripMenuItem.Click += new System.EventHandler(this.applyFilterToolStripMenuItem_Click);
            // 
            // cancelFilterToolStripMenuItem
            // 
            this.cancelFilterToolStripMenuItem.Name = "cancelFilterToolStripMenuItem";
            this.cancelFilterToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.cancelFilterToolStripMenuItem.Text = "Cancel Current Filter";
            // 
            // searchBackgroundWorker
            // 
            this.searchBackgroundWorker.WorkerReportsProgress = true;
            this.searchBackgroundWorker.WorkerSupportsCancellation = true;
            this.searchBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.searchBackgroundWorker_ProgressChanged);
            // 
            // saveFilteredMonitorMenuItem
            // 
            this.saveFilteredMonitorMenuItem.Index = 3;
            this.saveFilteredMonitorMenuItem.Text = "Save Filtered Monitor";
            this.saveFilteredMonitorMenuItem.Click += new System.EventHandler(this.saveFilteredMonitorMenuItem_Click);
            this.saveFilteredMonitorMenuItem.Popup += new System.EventHandler(this.saveFilteredMonitorMenuItem_Popup);
            // 
            // monitorPanel
            // 
            this.monitorPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.monitorPanel.ContextMenuStrip = this.monitorControlContextMenuStrip;
            this.monitorPanel.CountToShow = 10;
            this.monitorPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.monitorPanel.GridLines = false;
            this.monitorPanel.Location = new System.Drawing.Point(0, 0);
            this.monitorPanel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.monitorPanel.Name = "monitorPanel";
            this.monitorPanel.Size = new System.Drawing.Size(924, 107);
            this.monitorPanel.TabIndex = 7;
            // 
            // monitorControlContextMenuStrip
            // 
            this.monitorControlContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveFilteredMonitorToolStripMenuItem});
            this.monitorControlContextMenuStrip.Name = "monitorControlContextMenuStrip";
            this.monitorControlContextMenuStrip.Size = new System.Drawing.Size(188, 48);
            // 
            // saveFilteredMonitorToolStripMenuItem
            // 
            this.saveFilteredMonitorToolStripMenuItem.Name = "saveFilteredMonitorToolStripMenuItem";
            this.saveFilteredMonitorToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.saveFilteredMonitorToolStripMenuItem.Text = "Save Filtered Monitor";
            this.saveFilteredMonitorToolStripMenuItem.Click += new System.EventHandler(this.saveFilteredMonitorToolStripMenuItem_Click);
            // 
            // EventMultiTracerControl
            // 
            this.Controls.Add(this.splitContainer1);
            this.Name = "EventMultiTracerControl";
            this.Size = new System.Drawing.Size(928, 376);
            this.tracingTabControl.ResumeLayout(false);
            this.propertyTabPage.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.monitorToolStripContainer.BottomToolStripPanel.ResumeLayout(false);
            this.monitorToolStripContainer.BottomToolStripPanel.PerformLayout();
            this.monitorToolStripContainer.ContentPanel.ResumeLayout(false);
            this.monitorToolStripContainer.ResumeLayout(false);
            this.monitorToolStripContainer.PerformLayout();
            this.countersToolStrip.ResumeLayout(false);
            this.countersToolStrip.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.filterContextMenuStrip.ResumeLayout(false);
            this.monitorControlContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		#region ITraceEventHandler Members


        // TODO: this to be called asynchroniously (SD)
        public void HandleEvent(TraceEvent e)
        {
            #region Locals

            XmlDocument xd = null;
            TraceEvent tempEvent = e;

            #endregion
            
            try
            {
                if (TracingOptions.EnableFilter)
                {
                    if (!this.Filter.Test(e))
                    {
                        showFilteredOutMessagesCount(++filteredMessagesCount);
                        return;
                    }
                }

                #region If pre-cache - Add to xEventsCache

                if (this.TracingOptions.PreCacheEvents)
                {
                    // Combines both proof of concepts, one for objects and one for xdoc repreentation;
                    // _eventsCache.Add(e);

                    //xd = new XmlDocument();

                    //xd.LoadXml
                    //    (
                    //    SerializationUtility.Serialize2String
                    //    (
                    //    tempEvent
                    //    ));
                } 

                #endregion

            }
            catch (Exception ex)
            {
                tempEvent = new TraceEvent
                (
                999999,
                "Event Rendering Error: " + ex.ToString() +
                System.Environment.NewLine +
                "Original message: " + e.Message
                );
                if (this.TracingOptions.PreCacheEvents)
                {
                    xd = new XmlDocument();
                    xd.LoadXml
                    (
                    SerializationUtility.Serialize2String
                    (
                    tempEvent
                    ));
                }
            }
            currentCount++; //xEventsCache.DocumentElement.ChildNodes.Count;
            if (this.TracingOptions.PreCacheEvents)
            {
                //XmlNode nd = xEventsCache.ImportNode
                //    (
                //    xd.DocumentElement,
                //    true
                //    );
                //xEventsCache.DocumentElement.AppendChild
                //    (
                //    nd
                //    );
                navigator.AppendChild
                    (
                    SerializationUtility.Serialize2String
                        (
                        tempEvent
                        ));
                showCachedMessagesCount(currentCount);
            }

            #region add record to the list view

            if (!filtered)
            {
                addApplicationEventToListView(tempEvent, true);
                showShownMessagesCount(currentCount);
            }

            #endregion add record to the list view


        }
		public void addApplicationEventToListView(TraceEvent e, bool addsToCount)
		{
			if (addsToCount)
			{
				monitorPanel.MessagesCount++;
			}

			ListViewItem li = new ListViewItem
				(
				(new TraceRecord(e)).PropertiesArray
				);
			li.Tag = e;

			this.Invoke
				(
				new AddListViewDelegate
				(
				this.addListItem
				),
				new object[]
				{
					li
				}
				);
		
		}
		private void addListItem(ListViewItem li)
		{
//			listView1.InsertToTop(li);
			monitorPanel.AddItem
				(
				li
				);
            //showMessagesCount(monitorPanel.ItemsListView.Items.Count);
		}
		public void AddHandler(ITraceEventHandler handler)
		{
			throw new NotImplementedException("ApplicationEventLogHandler AddHandler method not implemented!");
		}
		public void RemoveHandler(ITraceEventHandler handler)
		{
			throw new NotImplementedException("ApplicationEventLogHandler RemoveHandler method not implemented!");
		}
		private void HandleEvent(TraceEventArgs aea)
		{
			this.HandleEvent(aea.Event);
		}

		#endregion

		#region Functions

		#region ListView events

		private void listView1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (monitorPanel.ItemsListView.SelectedItems.Count == 1) 
			{		
				TextTransformerTabPage selectedPage = 
					logEntryViewsTabControl.SelectedTab as TextTransformerTabPage;

				if (selectedPage!=null)
				{
                    try
                    {
                        object selItem = monitorPanel.ItemsListView.SelectedItems[0].Tag;

                        TraceEvent ae = selItem as TraceEvent;
                        string originalMessage = null;
                        //if (ae.ProtectionType == DataProtectionType.Encryption)
                        //{
                        //    string cpPath = null;
                        //    foreach (ContextHolderIdDescriptorPointer cp in keysControl.Pointers)
                        //    {
                        //        if (cp.ContextHolderId == ae.ContextIdentifier.ContextHolderId)
                        //            cpPath = cp.Url;
                        //    }
                        //    if (!String.IsNullOrEmpty(cpPath))
                        //    {
                        //        originalMessage = ae.Message;
                        //        ae.Message =
                        //            ec.architecture.security.cryptography.symmetricalgorithm.SymmetricAlgorithmUtility.Decrypt
                        //        (
                        //        ae.Message,
                        //        (SymmetricAlgorithmCryptoParameters)SerializationUtility.DeserializeFromFile
                        //        (
                        //        cpPath,
                        //        typeof(SymmetricAlgorithmCryptoParameters)
                        //        )
                        //        );
                        //        try
                        //        {
                        //        selectedPage.SetText
                        //        (
                        //        SerializationUtility.Serialize2String(ae)
                        //        );
                        //        }
                        //        catch (Exception ex)
                        //        {
                        //            selectedPage.SetText
                        //                (
                        //                "Original text can't be shown due to the following exception: " +
                        //                ex.ToString()
                        //                );
                        //        }
                        //        finally 
                        //        {
                        //            ae.Message = originalMessage;
                        //        }
                        //    }
                        //}
                        //else
                        //{
                            selectedPage.SetText
                            (
                            SerializationUtility.Serialize2String(selItem)
                            );
                        //}
                    }
                    catch (Exception ex)
                    {
                        selectedPage.SetText
                            (
                            ex.ToString()
                            );
                    }
					return;
				}

			}
			
		}

		#endregion ListView events

		#region Layout

		private void LogControl_Resize(object sender, EventArgs e)
		{
			//this.SuspendLayout();
			int oldHeight = monitorPanel.Height;
			monitorPanel.Height = this.Height * 2 / 4;
			if (oldHeight != monitorPanel.Height)
			{
				this.showEventsFromIndex(monitorPanel.ItemsListView.CurrentIndex);
			}
            countToShow =
                (int)Math.Floor
                (
                Convert.ToDouble(monitorPanel.ItemsListView.ClientRectangle.Height) /
                realLVFontSize
                ) - 2;
            
            monitorPanel.CountToShow = countToShow;
            //monitorPanel1.CountToShow = countToShow;
                //Convert.ToInt16(monitorPanel1.ItemsListView.Font.GetHeight());
			//visCountStatusBarPanel.Text = countToShow.ToString();
			adjustControls();
			//this.ResumeLayout();
		}
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            realLVFontSize =
                Graphics.FromHdc
                (
                e.Graphics.GetHdc()).MeasureString("aa", this.Font
                ).Height;
            LogControl_Resize(null, null);
        }
		private void splitter1_SplitterMoved_1(object sender, System.Windows.Forms.SplitterEventArgs e)
		{
			adjustControls();
		}
		private void adjustControls()
		{
//			lowerPanel.Height = Height - splitter1.Height - this.listView1.Height - statusBar1.Height;
//			lowerPanel.Top = splitter1.Bottom;
//			statusBar1.Top = lowerPanel.Bottom;
		}

		#endregion Layout

		#region Menu events

		private void clearAllMenuItem_Click(object sender, System.EventArgs e)
		{
			ClearAll();
		}

		private void saveMonitorToFileMenuItem_Click(object sender, System.EventArgs e)
		{
			//if (this._eventsCache.Count > 0)
            saveMonitorToFile(false);
		}

        private void saveMonitorToFile(bool saveFilterOnly)
        {
            XmlDocument docToSave = xEventsCache;
            if (saveFilterOnly) docToSave = xEventsFilteredCache;

            if (docToSave != null)
            {
                string startElement = String.Empty;

                //				if (File.Exists(this.TracingOptions.AutoFlushFilePath))
                //				{
                //					startElement = "<Block>";
                //				}
                //				string tmp = SerializationUtility.Serialize2String(_eventsCache);
                try
                {
                    saveMonitorToFileDialog.DefaultExt = "xml";
                    saveMonitorToFileDialog.FileName = 
                        "etLog_" + DateTime.UtcNow.ToString("dd-MMM-yyTHH-mm-ss") +
                        ((saveFilterOnly) ? "_Filtered" : String.Empty);
                    saveMonitorToFileDialog.Filter = "Xml files (*.xml)|*.xml|All files (*.*)|*.*";
                    DialogResult dr = saveMonitorToFileDialog.ShowDialog();

                    if (dr == DialogResult.Cancel) return;
                    if (saveMonitorToFileDialog.FileName != null || saveMonitorToFileDialog.FileName != String.Empty)
                    {
                        // SerializationUtility.Serialize2File(this._eventsCache, saveFileDialog1.FileName);
                        docToSave.Save(saveMonitorToFileDialog.FileName);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

		
		private void runFileToMonitorMenuItem_Click(object sender, System.EventArgs e)
		{
			// Example: "Text files (*.txt)|*.txt|All files (*.*)|*.*"
			openFileDialog1.DefaultExt = "xml";
			openFileDialog1.Filter = "Xml files (*.xml)|*.xml|All files (*.*)|*.*";

			DialogResult dr = openFileDialog1.ShowDialog();
			if (dr==DialogResult.Cancel) return;
			if (openFileDialog1.FileName!=null||openFileDialog1.FileName!=String.Empty)
			{
				try
				{
					// TODO: both approaches should be tested and profiled here (SD)
					
//					_runEventsCache =
//						(TraceEventCollection)SerializationUtility.DeserializeFromFile
//						(
//						openFileDialog1.FileName,
//						typeof(TraceEventCollection)
//						);

					xEventsCache.Load(openFileDialog1.FileName);

					Thread workerThread = new Thread(new ThreadStart(this.restoreEvents));
					workerThread.IsBackground = true;
					workerThread.Priority = ThreadPriority.BelowNormal;
					workerThread.Name = this.GetType().Name + "_worker";
					workerThread.Start();

				}
				catch (Exception ex)
				{
					MessageBox.Show("Probably not a valid TraceEventCollection Document! " + ex.ToString());
				}
			}
		}
		
		
		#endregion menu events

		private void filter_Changed(object sender, EventArgs e)
		{
			//dataBind();
		}

		
	
		
		#endregion Functions

		// TODO: remove when proof of concept is finished (SD)
		private void applyMenuItem_Click(object sender, EventArgs e)
		{
            DescriptiveNameValue<string> dnv = xPathStatementControl.SelectedValue;

            if (dnv == null)
            {
                MessageBox.Show
                (
                "There is no value currently selected! Select the value and retry."
                );
                return;
            }
            ClearGui();
            filterByXPath(dnv.Value);
		}

        private void filterByXPath(string xPathExpression)
        {
            try
            {
                if (xEventsCache != null)
                {
                    filtered = true;
                    xEventsFilteredCache.DocumentElement.RemoveAll();

                    XmlNodeList ndl = xEventsCache.DocumentElement.SelectNodes
                        (
                        xPathExpression
                        );
                    IntVoidDelegate del = delegate (int val)
                    {
                        showShownMessagesCount(val);
                    };
                    this.Invoke(del, ndl.Count);

                    for (int i = 0; i < ndl.Count; i++)
                    {
                        TraceEvent ae =
                            SerializationUtility.DeserializeFromString
                            (
                            ndl.Item(i).OuterXml,
                            typeof(TraceEvent)
                            ) as TraceEvent;

                        this.addApplicationEventToListView(ae, true);

                        //xEventsFilteredCache.ImportNode(ndl.Item(i), true);
                        XmlNode xn = xEventsFilteredCache.ImportNode(ndl.Item(i), true);

                        xEventsFilteredCache.DocumentElement.AppendChild(xn);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "XPath Expression: " + xPathExpression);
            }
        }
		
		
		private void restoreEvents()
		{
			XmlDocument xd = xEventsCache;
			showEventsFromIndex(xd.DocumentElement.ChildNodes.Count - countToShow);
			monitorPanel.MessagesCount = xd.DocumentElement.ChildNodes.Count;
		}
		
		
		private void cancelFilterMenuItem_Click(object sender, EventArgs e)
		{
            cancelFilter();
		}

        private void cancelFilter()
        {
            try
            {
                if (xEventsCache != null)
                {
                    //XmlNodeList ndl = xEventsCache.SelectNodes(xPathStatementControl.CurrentValue);
                    ClearGui();
                    filtered = false;
                    xEventsFilteredCache.DocumentElement.RemoveAll();

                    Thread workerThread = new Thread(new ThreadStart(restoreEvents));
                    workerThread.IsBackground = true;
                    workerThread.Priority = ThreadPriority.BelowNormal;
                    workerThread.Name = this.GetType().Name + "_worker1";
                    workerThread.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

		private void clearCacheMenuItem_Click(object sender, System.EventArgs e)
		{
			ClearEventsCache();
		}

		private void clearGuiMenuItem_Click(object sender, System.EventArgs e)
		{
			ClearGui();
		}

		private void listView1_Scrolled(object sender, ScrollEventArgs e)
		{
			if (e.Type!=ScrollEventType.EndScroll)
			{
				showEventsFromIndex(e.NewValue);
			}
			
		}
		
        private void showEventsFromIndex(int index)
		{
            if (index < 0) index = 0;
            XmlDocument xd = getViewSource();
			int count = xd.DocumentElement.ChildNodes.Count;
			int currentPointer = count - index - countToShow;
			currentPointer = (currentPointer > 0) ? currentPointer : 0;

			monitorPanel.SuspendLayout();
			monitorPanel.ClearItems();
			monitorPanel.ItemsListView.SetMaxIndex(count);

			//
			for (int i = currentPointer; i < count - index; i++)
			{
				//Trace.WriteLine("i="+i.ToString());

                // TODO: That is a very ad-hoc untill serialization bend as it is done for collections:
                // xsi:type (SD)

                addApplicationEventToListView
                    (
                    getResolvedEvent
                    (
                    xd.DocumentElement.ChildNodes[i]
                    ),
                    false
                    );
            }
			monitorPanel.ResumeLayout();
		}
        private TraceEvent getResolvedEvent(XmlNode eventNode)
        {
            // TODO: That is a very ad-hoc untill serialization bend as it is done for collections:
            // xsi:type (SD)
            string nodeName = eventNode.Name;

            if (nodeName == "TraceEvent")
            {
                return
                    SerializationUtility.DeserializeFromString
                    (
                    eventNode.OuterXml,
                    typeof(TraceEvent)
                    ) as TraceEvent;
            }
            else
            {
                throw new Exception("Unknown node:" + nodeName);
            }
            return null;
        }
		private XmlDocument getViewSource()
		{
			return (filtered) ? xEventsFilteredCache : xEventsCache;
		}

        private void showGridViewToolStripButton_CheckedChanged(object sender, EventArgs e)
        {
            monitorPanel.GridLines = showGridViewToolStripButton.Checked;
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        #region counters

        private void showShownMessagesCount(int val)
        {
            shownCountToolStripTextBox.Text = val.ToString();
        }
        private void showCachedMessagesCount(int val)
        {
            IntVoidDelegate del = delegate(int vald)
            {
                cachedCountToolStripTextBox.Text = vald.ToString();
            };
            this.Invoke(del, val);
        }
        private void showFilteredOutMessagesCount(int val)
        {
            IntVoidDelegate del = delegate(int vald)
            {
                filteredOutCountToolStripTextBox.Text = vald.ToString();
            };
            this.Invoke(del, val);
        }

        #endregion counters

        private void toolStripContainer1_BottomToolStripPanel_Click(object sender, EventArgs e)
        {

        }

        private void filterByFieldToolStripButton_Click(object sender, EventArgs e)
        {
            filterFromSameThread(false);
        }

        private void filterFromSameThread(bool overrideFilterCancel)
        {
            try
            {
                if (searchIsRunning)
                {
                    searchBackgroundWorker.CancelAsync();
                    return;
                    //searchBackgroundWorker.
                }

                if (!overrideFilterCancel&&filterByFieldToolStripButton.Checked == true)
                {
                    cancelFilter();
                    filtered = false;
                    filterByFieldToolStripButton.Checked = false;
                    return;
                }
                if (monitorPanel.ItemsListView.SelectedItems.Count == 1)
                {
                    //this.filterByFieldToolStripButton.Image = 
                    filterByFieldToolStripButton.Image = global::Tools.Tracing.UI.Properties.Resources.servicestopped;
                    searchIsRunning = true;
                    filterBySameFieldToolStripComboBox.Enabled = false;
                    searchToolStripProgressBar.Visible = true;

                    searchBackgroundWorker.RunWorkerAsync
                        (
                        monitorPanel.ItemsListView.SelectedItems[0].Tag
                        );

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void searchBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            searchIsRunning = false;
            filterByFieldToolStripButton.Image = global::Tools.Tracing.UI.Properties.Resources.ico565;
            filterBySameFieldToolStripComboBox.Enabled = true;
            searchToolStripProgressBar.Visible = false;
            searchToolStripProgressBar.Value = 0;
        }

        void searchBackgroundWorker_DoFilterBySameFieldWork(object sender, DoWorkEventArgs e)
        {
            filterBySameField(e.Argument);
        }

        private void filterBySameField(object selectedEventObject)
        {
            #region Commented out

            //if (xEventsCache != null)
            //{
            //    filtered = true;
            //    xEventsFilteredCache.DocumentElement.RemoveAll();

            //    XmlNodeList ndl = xEventsCache.DocumentElement.ChildNodes;

            //    int eventsCount = ndl.Count;

            //    //object selItem = monitorPanel.ItemsListView.SelectedItems[0].Tag;

            //    TraceEvent aev = selectedEventObject as TraceEvent;
            //    VoidDelegate del = delegate()
            //    {
            //        ClearGui();
            //        filterByFieldToolStripButton.Checked = true;
            //    };
            //    this.Invoke(del);

            //    int reportProgressFloor = (eventsCount - (eventsCount % 20));
            //    string adHocFilterFieldName = null;
            //    VoidDelegate getAdHocFilterValue = delegate
            //        {
            //            adHocFilterFieldName = filterBySameFieldToolStripComboBox.Text;
            //        };
            //    this.Invoke(getAdHocFilterValue);
                
            //    for (int i = 0; i < eventsCount; i++)
            //    {
            //        //Thread.Sleep(100);
            //        if (searchBackgroundWorker.CancellationPending)
            //        {
            //            VoidDelegate cancelFilteringDelegate = delegate
            //            {
            //                cancelFilter();
            //            };
            //            this.Invoke
            //                (
            //                cancelFilteringDelegate
            //                );
            //            return;
            //        }
            //        if (i % (reportProgressFloor/20) == 0)
            //        {
            //            int ratio = 100 - (eventsCount - i)*100 / eventsCount;
            //            searchBackgroundWorker.ReportProgress(ratio);
            //        }
                    
            //        TraceEvent ae =
            //            SerializationUtility.DeserializeFromString2
            //            (
            //            ndl.Item(i).OuterXml,
            //            typeof(TraceEvent)
            //            ) as TraceEvent;
                    

            //        if (
            //            TraceRecord.GetFieldValueByName
            //            (
            //            adHocFilterFieldName,
            //            ae
            //            ) !=
            //            TraceRecord.GetFieldValueByName
            //            (
            //            adHocFilterFieldName,
            //            aev
            //            ))
            //        {
            //            continue;
            //        }

            //        this.addApplicationEventToListView(ae, true);
            //        XmlNode xn = xEventsFilteredCache.ImportNode(ndl.Item(i), true);

            //        xEventsFilteredCache.DocumentElement.AppendChild(xn);
            //    }
            //    searchBackgroundWorker.ReportProgress(100);

            //  }

            #endregion Commented out

            string adHocFilterFieldName = null;
            VoidDelegate getAdHocFilterValue = delegate
                {
                    adHocFilterFieldName = filterBySameFieldToolStripComboBox.Text;
                };
            this.Invoke(getAdHocFilterValue);

            TraceEvent aev = selectedEventObject as TraceEvent;
            VoidDelegate del = delegate()
            {
                ClearGui();
                filterByFieldToolStripButton.Checked = true;
            };
            this.Invoke(del);
            string fieldValue =
                TraceRecord.GetFieldValueByName
                (
                adHocFilterFieldName,
                aev
                );
            string xPathExpression = null;

            for (int i = 0; i < TraceRecord.CorrelationFieldNames.Length; i++)
            {
                if (adHocFilterFieldName.Equals(TraceRecord.CorrelationFieldNames[i]))
                {
                    xPathExpression = TraceRecord.CorrelationFieldXPathExpressions[i].Replace("{%Value}", fieldValue);
                    break;
                }
            }
            filterByXPath(xPathExpression);

        }
        private void applyFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void filterBySameFieldToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (filtered)
            {
                filterFromSameThread(true);
            }
        }

        private void searchBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            searchToolStripProgressBar.Value = e.ProgressPercentage;
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void menuItem3_Click(object sender, EventArgs e)
        {

        }

        private void saveFilteredMonitorMenuItem_Click(object sender, EventArgs e)
        {
            saveMonitorToFile(true);
        }

        private void saveFilteredMonitorMenuItem_Popup(object sender, EventArgs e)
        {
            saveFilteredMonitorMenuItem.Enabled = filtered;
        }

        private void saveFilteredMonitorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //saveFilteredMonitorToolStripMenuItem
        }
    }
}
