using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Tools.Tracing.ClientManager;
using Tools.Tracing.Common;

namespace Tools.Tracing.UI
{
	/// <summary>
	/// Summary description for RemoteConnectionConfigurationControl.
	/// </summary>
	public class RemoteConnectionConfigurationControl : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.ColumnHeader nameColumnHeader;
		private System.Windows.Forms.ColumnHeader protocolColumnHeader;
		private System.Windows.Forms.ColumnHeader serviceHostColumnHeader;
		private System.Windows.Forms.ColumnHeader portColumnHeader;
		private System.Windows.Forms.ColumnHeader enabledColumnHeader;
		private System.Windows.Forms.Button newRecordButton;
		private System.Windows.Forms.Button deleteRecordButton;
		private System.Windows.Forms.ListView connectionsListView;
		private System.Windows.Forms.Panel commandPanel;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.TabControl propertiesTabControl;
		private System.Windows.Forms.TabPage propertiesTabPage;
		private System.Windows.Forms.Button downloadButton;
		private System.Windows.Forms.PropertyGrid editPropertyGrid;

		private RemoteConnectionConfigurationCollection _connections = 
			new RemoteConnectionConfigurationCollection();
		private RemoteConnectionInstanceCollection _connectionInstances = 
			new RemoteConnectionInstanceCollection();
		private TraceEventHandlerEventStub _eventStub = null;

		private RemoteConnectionConfiguration newConfigItem = null;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.TabControl customEditTabControl;
		private System.Windows.Forms.ImageList imageList2;
		private System.Windows.Forms.ColumnHeader uriColumnHeader;
		private System.Windows.Forms.ContextMenu remoteConfigInstanceContextMenu;
		private System.Windows.Forms.ContextMenu remoteSelectedItemContextMenu;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem closeMenuItem;
		private System.Windows.Forms.MenuItem uploadMenuItem;
		private RecordEditingMode editingMode = RecordEditingMode.None;

			//new RemoteConnectionConfiguration();
		// TODO: to be renamed to ConnectionConfigurations
		public RemoteConnectionConfigurationCollection Connections
		{
			get
			{
				return _connections;
			}
			set
			{
				_connections = value;
				setGuiFromConfig(_connections);
			}
		}
		public TraceEventHandlerEventStub EventStub
		{
			get
			{
				return _eventStub;
			}
			set
			{
				_eventStub = value;
			}
		}
		public RemoteConnectionInstanceCollection ConnectionInstances
		{
			get
			{
				return _connectionInstances;
			}
			set
			{
				_connectionInstances = value;
				setGuiFromConfig(_connections);
			}
		}		
		private void setGuiFromConfig(RemoteConnectionConfigurationCollection connections)
		{
			this.connectionsListView.Items.Clear();

			foreach (RemoteConnectionConfiguration rcc in connections)
			{
				this.addConnectionRecord(rcc, true);
			}
		}
		private void setGuiFromConfig(RemoteConnectionInstanceCollection connectionInstances)
		{
			this.connectionsListView.Items.Clear();

			foreach (RemoteConnectionInstance rci in connectionInstances)
			{
				this.addConnectionRecord(rci.Configuration, true);
			}
		}
		public RemoteConnectionConfigurationControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			_eventStub = new TraceEventHandlerEventStub();

//			eventStub.EventHandled +=new TraceEventDelegate(this.handlerEventStub_EventHandled);

		}
//		private void handlerEventStub_EventHandled(TraceEventArgs eventArgs)
//		{
//			// TODO: Handle event
//			int i = 0;
//			//eventArgs.Event.Handled = false;
//			TraceEventHandler.Instance.HandleEvent(eventArgs.Event);
//		}
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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.connectionsListView = new System.Windows.Forms.ListView();
			this.enabledColumnHeader = new System.Windows.Forms.ColumnHeader();
			this.nameColumnHeader = new System.Windows.Forms.ColumnHeader();
			this.uriColumnHeader = new System.Windows.Forms.ColumnHeader();
			this.protocolColumnHeader = new System.Windows.Forms.ColumnHeader();
			this.serviceHostColumnHeader = new System.Windows.Forms.ColumnHeader();
			this.portColumnHeader = new System.Windows.Forms.ColumnHeader();
			this.remoteSelectedItemContextMenu = new System.Windows.Forms.ContextMenu();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.commandPanel = new System.Windows.Forms.Panel();
			this.newRecordButton = new System.Windows.Forms.Button();
			this.deleteRecordButton = new System.Windows.Forms.Button();
			this.downloadButton = new System.Windows.Forms.Button();
			this.propertiesTabControl = new System.Windows.Forms.TabControl();
			this.propertiesTabPage = new System.Windows.Forms.TabPage();
			this.editPropertyGrid = new System.Windows.Forms.PropertyGrid();
			this.customEditTabControl = new System.Windows.Forms.TabControl();
			this.remoteConfigInstanceContextMenu = new System.Windows.Forms.ContextMenu();
			this.closeMenuItem = new System.Windows.Forms.MenuItem();
			this.uploadMenuItem = new System.Windows.Forms.MenuItem();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.imageList2 = new System.Windows.Forms.ImageList(this.components);
			this.commandPanel.SuspendLayout();
			this.propertiesTabControl.SuspendLayout();
			this.propertiesTabPage.SuspendLayout();
			this.SuspendLayout();
			// 
			// connectionsListView
			// 
			this.connectionsListView.AllowColumnReorder = true;
			this.connectionsListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.connectionsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																								  this.enabledColumnHeader,
																								  this.nameColumnHeader,
																								  this.uriColumnHeader,
																								  this.protocolColumnHeader,
																								  this.serviceHostColumnHeader,
																								  this.portColumnHeader});
			this.connectionsListView.ContextMenu = this.remoteSelectedItemContextMenu;
			this.connectionsListView.FullRowSelect = true;
			this.connectionsListView.GridLines = true;
			this.connectionsListView.Location = new System.Drawing.Point(0, 0);
			this.connectionsListView.MultiSelect = false;
			this.connectionsListView.Name = "connectionsListView";
			this.connectionsListView.Size = new System.Drawing.Size(736, 166);
			this.connectionsListView.TabIndex = 0;
			this.connectionsListView.View = System.Windows.Forms.View.Details;
			this.connectionsListView.SelectedIndexChanged += new System.EventHandler(this.connectionsListView_SelectedIndexChanged);
			// 
			// enabledColumnHeader
			// 
			this.enabledColumnHeader.Text = "Enabled";
			// 
			// nameColumnHeader
			// 
			this.nameColumnHeader.Text = "Name";
			this.nameColumnHeader.Width = 104;
			// 
			// uriColumnHeader
			// 
			this.uriColumnHeader.Text = "Uri";
			this.uriColumnHeader.Width = 300;
			// 
			// protocolColumnHeader
			// 
			this.protocolColumnHeader.Text = "Protocol";
			this.protocolColumnHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// serviceHostColumnHeader
			// 
			this.serviceHostColumnHeader.Text = "ServiceHost";
			this.serviceHostColumnHeader.Width = 110;
			// 
			// portColumnHeader
			// 
			this.portColumnHeader.Text = "Port";
			this.portColumnHeader.Width = 43;
			// 
			// remoteSelectedItemContextMenu
			// 
			this.remoteSelectedItemContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																										  this.menuItem2,
																										  this.menuItem3,
																										  this.menuItem4,
																										  this.menuItem5,
																										  this.menuItem6,
																										  this.menuItem7});
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 0;
			this.menuItem2.Text = "New";
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 1;
			this.menuItem3.Text = "Delete";
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 2;
			this.menuItem4.Text = "Download";
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 3;
			this.menuItem5.Text = "Upload";
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 4;
			this.menuItem6.Text = "Connect";
			// 
			// menuItem7
			// 
			this.menuItem7.Index = 5;
			this.menuItem7.Text = "Disconnect";
			// 
			// commandPanel
			// 
			this.commandPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.commandPanel.Controls.Add(this.newRecordButton);
			this.commandPanel.Controls.Add(this.deleteRecordButton);
			this.commandPanel.Controls.Add(this.downloadButton);
			this.commandPanel.Location = new System.Drawing.Point(0, 168);
			this.commandPanel.Name = "commandPanel";
			this.commandPanel.Size = new System.Drawing.Size(736, 24);
			this.commandPanel.TabIndex = 1;
			this.commandPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.commandPanel_Paint);
			// 
			// newRecordButton
			// 
			this.newRecordButton.Location = new System.Drawing.Point(0, 0);
			this.newRecordButton.Name = "newRecordButton";
			this.newRecordButton.TabIndex = 0;
			this.newRecordButton.Text = "New";
			this.newRecordButton.Click += new System.EventHandler(this.newRecordButton_Click);
			// 
			// deleteRecordButton
			// 
			this.deleteRecordButton.Location = new System.Drawing.Point(72, 0);
			this.deleteRecordButton.Name = "deleteRecordButton";
			this.deleteRecordButton.TabIndex = 0;
			this.deleteRecordButton.Text = "Delete";
			this.deleteRecordButton.Click += new System.EventHandler(this.deleteRecordButton_Click);
			// 
			// downloadButton
			// 
			this.downloadButton.Location = new System.Drawing.Point(216, 0);
			this.downloadButton.Name = "downloadButton";
			this.downloadButton.TabIndex = 0;
			this.downloadButton.Text = "Download";
			this.downloadButton.Click += new System.EventHandler(this.downloadButton_Click);
			// 
			// propertiesTabControl
			// 
			this.propertiesTabControl.Alignment = System.Windows.Forms.TabAlignment.Bottom;
			this.propertiesTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.propertiesTabControl.Controls.Add(this.propertiesTabPage);
			this.propertiesTabControl.Location = new System.Drawing.Point(440, 192);
			this.propertiesTabControl.Multiline = true;
			this.propertiesTabControl.Name = "propertiesTabControl";
			this.propertiesTabControl.SelectedIndex = 0;
			this.propertiesTabControl.Size = new System.Drawing.Size(296, 240);
			this.propertiesTabControl.TabIndex = 3;
			// 
			// propertiesTabPage
			// 
			this.propertiesTabPage.Controls.Add(this.editPropertyGrid);
			this.propertiesTabPage.Location = new System.Drawing.Point(4, 4);
			this.propertiesTabPage.Name = "propertiesTabPage";
			this.propertiesTabPage.Size = new System.Drawing.Size(288, 214);
			this.propertiesTabPage.TabIndex = 0;
			this.propertiesTabPage.Text = "Properties";
			// 
			// editPropertyGrid
			// 
			this.editPropertyGrid.CommandsVisibleIfAvailable = true;
			this.editPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.editPropertyGrid.LargeButtons = false;
			this.editPropertyGrid.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.editPropertyGrid.Location = new System.Drawing.Point(0, 0);
			this.editPropertyGrid.Name = "editPropertyGrid";
			this.editPropertyGrid.Size = new System.Drawing.Size(288, 214);
			this.editPropertyGrid.TabIndex = 0;
			this.editPropertyGrid.Text = "propertyGrid1";
			this.editPropertyGrid.ToolbarVisible = false;
			this.editPropertyGrid.ViewBackColor = System.Drawing.SystemColors.Window;
			this.editPropertyGrid.ViewForeColor = System.Drawing.SystemColors.WindowText;
			// 
			// customEditTabControl
			// 
			this.customEditTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.customEditTabControl.ContextMenu = this.remoteConfigInstanceContextMenu;
			this.customEditTabControl.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.customEditTabControl.Location = new System.Drawing.Point(0, 192);
			this.customEditTabControl.Name = "customEditTabControl";
			this.customEditTabControl.SelectedIndex = 0;
			this.customEditTabControl.Size = new System.Drawing.Size(440, 240);
			this.customEditTabControl.TabIndex = 4;
			// 
			// remoteConfigInstanceContextMenu
			// 
			this.remoteConfigInstanceContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																											this.closeMenuItem,
																											this.uploadMenuItem});
			// 
			// closeMenuItem
			// 
			this.closeMenuItem.Index = 0;
			this.closeMenuItem.Text = "Close";
			this.closeMenuItem.Click += new System.EventHandler(this.closeMenuItem_Click);
			// 
			// uploadMenuItem
			// 
			this.uploadMenuItem.Index = 1;
			this.uploadMenuItem.Text = "Upload";
			this.uploadMenuItem.Click += new System.EventHandler(this.uploadMenuItem_Click);
			// 
			// imageList1
			// 
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// imageList2
			// 
			this.imageList2.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// RemoteConnectionConfigurationControl
			// 
			this.Controls.Add(this.customEditTabControl);
			this.Controls.Add(this.propertiesTabControl);
			this.Controls.Add(this.commandPanel);
			this.Controls.Add(this.connectionsListView);
			this.Name = "RemoteConnectionConfigurationControl";
			this.Size = new System.Drawing.Size(736, 432);
			this.commandPanel.ResumeLayout(false);
			this.propertiesTabControl.ResumeLayout(false);
			this.propertiesTabPage.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void connectionsListView_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (editingMode==RecordEditingMode.New)
			{
				initiateNewRecord();
				editingMode = RecordEditingMode.Edit;
			}

			if (_connections.Count == 0 || connectionsListView.SelectedItems.Count == 0) return;

			// multiselect is not allowed
			RemoteConnectionConfiguration rcc = 
				connectionsListView.SelectedItems[0].Tag as RemoteConnectionConfiguration;

			if (rcc !=null)
			{
				
				this.editPropertyGrid.SelectedObject = rcc;
			}
			//TODO: Handle null value
		}
		private void addConnectionRecord(RemoteConnectionConfiguration rcc, bool init)
		{
			if (!_connections.Contains(rcc)||init)
			{

				rcc.Changed += new EventHandler(rcc_Changed);
				if (!init) _connections.Add(rcc);

				// TODO: separate Gui thing from here

				connectionsListView.Items.Add
					(
					getListViewItemForRemoteConnectionConfiguration(rcc)
					);

				//this.connectionsListView
			}
		}
		private void deleteConnectionRecord(RemoteConnectionConfiguration rcc)
		{
			if (_connections.Contains(rcc))
			{
				rcc.Changed -= new EventHandler(rcc_Changed);
				_connections.Remove(rcc);

			}
		}
		private void saveRecordButton_Click(object sender, System.EventArgs e)
		{
			
			// TODO: Validate
			addConnectionRecord(editPropertyGrid.SelectedObject as RemoteConnectionConfiguration, false);
		}

		private void deleteRecordButton_Click(object sender, System.EventArgs e)
		{
			// TODO: Validate
			deleteConnectionRecord(editPropertyGrid.SelectedObject as RemoteConnectionConfiguration);
			// TODO: separate Gui thing from here
			setGuiFromConfig(_connections);
		}
		private void initiateNewRecord()
		{
			if (this.editingMode == RecordEditingMode.New)
			{
				// TODO: offer record to be added
				DialogResult res = 
					MessageBox.Show
					(
					this,
					"There is a new record to add before you start editing the next one.\r\n" +
					"If you want to add it select Yes.\r\n " +
					"If you don't want to add it select No.\r\n" +
					"If you want to return to the record editing before adding select Cancel.",
					"Confirm adding new record",
					MessageBoxButtons.YesNoCancel,
					MessageBoxIcon.Question,
					MessageBoxDefaultButton.Button1
					);

				if (res == DialogResult.Cancel) return;

				if (res == DialogResult.Yes) 
				{
					addConnectionRecord(newConfigItem, false);
					
				}

			}

			editingMode = RecordEditingMode.New;

			newConfigItem = new RemoteConnectionConfiguration();
			newConfigItem.Name = _connections.GetDefaultConnectionName();

			this.editPropertyGrid.SelectedObject = newConfigItem;
		}
		private void newRecordButton_Click(object sender, System.EventArgs e)
		{
			initiateNewRecord();
		}
		private ListViewItem getListViewItemForRemoteConnectionConfiguration(RemoteConnectionConfiguration rcc)
		{
			ListViewItem lvi = 
				new ListViewItem
				(
				new string[] 
					{
						rcc.Enabled.ToString(), 
						rcc.Name, 
						rcc.Uri,
						rcc.ProtocolType.ToString(), 
						rcc.ServiceHost,
						rcc.Port},
				0
				);

			lvi.Tag = rcc;

			return lvi;
		}
		private void rcc_Changed(object sender, EventArgs e)
		{
			if (connectionsListView.SelectedItems.Count == 0)
				throw new Exception("Unexpected situation - connectionsListView.SelectedItems.Count should be 1 here");
			for (int i = 0; i < connectionsListView.SelectedItems[0].SubItems.Count; i ++)
			{
				connectionsListView.SelectedItems[0].SubItems[i] = 
					getListViewItemForRemoteConnectionConfiguration(sender as RemoteConnectionConfiguration).SubItems[i];
			}

		}
		private TraceEventHandlerManagerConfiguration getRemoteConfiguration(RemoteConnectionConfiguration rcc)
		{
			TraceEventHandlerManagerClient client =
				new TraceEventHandlerManagerClient
				(
				rcc.ServiceHost,
				rcc.Port,
				rcc.Uri
				);
			return client.GetConfiguration();
		}

		private void downloadButton_Click(object sender, System.EventArgs e)
		{
			if (connectionsListView.SelectedItems.Count == 0) return;


			EventHandlerManagerConfigurationEditorControl ehmc = new EventHandlerManagerConfigurationEditorControl();
			RemoteConnectionConfiguration rcc = 
				connectionsListView.SelectedItems[0].Tag as RemoteConnectionConfiguration;
			try
			{
				ehmc.Configuration = 
					getRemoteConfiguration
					(
					rcc
					);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
				return;
			}

			ehmc.Dock = DockStyle.Fill;

			TabPage tbPage = new TabPage((this.connectionsListView.SelectedItems[0].Tag as RemoteConnectionConfiguration).Name);
			RemoteEventHandlerManagerConfiguration rehmc = 
				new RemoteEventHandlerManagerConfiguration
				(
				rcc,
				ehmc.Configuration
				);
			tbPage.Tag = rehmc;
			tbPage.Controls.Add(ehmc);

			this.customEditTabControl.Controls.Add(tbPage);

		}

		private void commandPanel_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
		
		}


		private void uploadButton_Click(object sender, System.EventArgs e)
		{
		
		}

		private void closeMenuItem_Click(object sender, System.EventArgs e)
		{
			customEditTabControl.Controls.Remove
				(
				customEditTabControl.SelectedTab
				);
		}

		private void uploadMenuItem_Click(object sender, System.EventArgs e)
		{
			RemoteEventHandlerManagerConfiguration rehmc = 
				customEditTabControl.SelectedTab.Tag as RemoteEventHandlerManagerConfiguration;
			uploadRemoteConfiguration
				(
				rehmc.RemoteConnectionConfiguration,
				rehmc.TraceEventHandlerManagerConfiguration
				);
		}

		private void uploadRemoteConfiguration
			(
			RemoteConnectionConfiguration rcc,
			TraceEventHandlerManagerConfiguration ehmc
			)
		{
			TraceEventHandlerManagerClient client =
				new TraceEventHandlerManagerClient
				(
				rcc.ServiceHost,
				rcc.Port,
				rcc.Uri
				);
			client.LoadConfiguration(ehmc);
		}

	}
}
