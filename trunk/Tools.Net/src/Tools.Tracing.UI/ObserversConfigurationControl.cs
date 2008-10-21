using System;
using System.ComponentModel;
using System.Windows.Forms;
using Tools.Remoting.Client.Common;
using Tools.Tracing.Common;

namespace Tools.Tracing.UI
{
    /// <summary>
    /// Summary description for ObserversConfigurationControl.
    /// </summary>
    public class ObserversConfigurationControl : UserControl
    {
        private EventsObserverInstanceCollection _connectionInstances =
            new EventsObserverInstanceCollection();

        private RemoteConnectionConfigurationCollection _connections =
            new RemoteConnectionConfigurationCollection();

        private TraceEventHandlerEventStub _eventStub;
        private IContainer components;

        private ColumnHeader connectedColumnHeader;
        private ListView connectionsListView;
        private MenuItem connectMenuItem;
        private MenuItem deleteRecordMenuItem;
        private MenuItem disconnectMenuItem;
        private RecordEditingMode editingMode = RecordEditingMode.None;
        private PropertyGrid editPropertyGrid;
        private ColumnHeader enabledColumnHeader;
        private ImageList imageList1;
        private ImageList imageList2;
        private ColumnHeader nameColumnHeader;
        private RemoteConnectionConfiguration newConfigItem;
        private MenuItem newRecordItem;
        private ColumnHeader portColumnHeader;
        private TabControl propertiesTabControl;
        private TabPage propertiesTabPage;
        private ColumnHeader protocolColumnHeader;
        private ContextMenu remoteSelectedItemContextMenu;
        private ColumnHeader serviceHostColumnHeader;
        private ColumnHeader uriColumnHeader;

        public ObserversConfigurationControl()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            _eventStub = new TraceEventHandlerEventStub();

//			eventStub.EventHandled +=new TraceEventDelegate(this.handlerEventStub_EventHandled);
        }

        //new RemoteConnectionConfiguration();
        // TODO: to be renamed to ConnectionConfigurations
        public RemoteConnectionConfigurationCollection Connections
        {
            get { return _connections; }
            set
            {
                _connections = value;
                setGuiFromConfig(_connections);
            }
        }

        public TraceEventHandlerEventStub EventStub
        {
            get { return _eventStub; }
            set { _eventStub = value; }
        }

        public EventsObserverInstanceCollection ConnectionInstances
        {
            get { return _connectionInstances; }
            set
            {
                _connectionInstances = value;
                setGuiFromConfig(_connections);
            }
        }

        private void setGuiFromConfig(RemoteConnectionConfigurationCollection connections)
        {
            connectionsListView.Items.Clear();

            foreach (RemoteConnectionConfiguration rcc in connections)
            {
                addConnectionRecord(rcc, true);
            }
        }

        private void setGuiFromConfig(EventsObserverInstanceCollection connectionInstances)
        {
            connectionsListView.Items.Clear();

            foreach (EventsObserverInstance rci in connectionInstances)
            {
                addConnectionRecord(rci.Configuration, true);
            }
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

        private void connectionsListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (editingMode == RecordEditingMode.New)
            {
                initiateNewRecord();
                editingMode = RecordEditingMode.Edit;
            }

            if (_connections.Count == 0 || connectionsListView.SelectedItems.Count == 0) return;

            // multiselect is not allowed
            RemoteConnectionConfiguration rcc =
                (connectionsListView.SelectedItems[0].Tag as RemoteConnectionInstance).Configuration;

            if (rcc != null)
            {
                editPropertyGrid.SelectedObject = rcc;
            }
            //TODO: Handle null value
        }

        private void addConnectionRecord(RemoteConnectionConfiguration rcc, bool init)
        {
            if (!_connections.Contains(rcc) || init)
            {
                var eoi =
                    new EventsObserverInstance
                        (
                        rcc,
                        _eventStub.HandleEvent);
                eoi.Changed += rcc_Changed;
                //rcc.Changed += new EventHandler(rcc_Changed);
                if (!init) _connections.Add(rcc);
                if (!init) ConnectionInstances.Add(eoi);

                // TODO: separate Gui thing from here

                connectionsListView.Items.Add
                    (
                    getListViewItemForRemoteConnectionInstance(eoi)
                    );

                //this.connectionsListView
            }
        }

        private void deleteConnectionRecord(RemoteConnectionConfiguration rcc)
        {
            if (_connections.Contains(rcc))
            {
                rcc.Changed -= rcc_Changed;
                _connections.Remove(rcc);
            }
        }

        private void saveRecordButton_Click(object sender, EventArgs e)
        {
            // TODO: Validate
            addConnectionRecord(editPropertyGrid.SelectedObject as RemoteConnectionConfiguration, false);
        }

//		private void deleteRecordButton_Click(object sender, System.EventArgs e)
//		{
//			// TODO: Validate
//			deleteConnectionRecord(editPropertyGrid.SelectedObject as RemoteConnectionConfiguration);
//			// TODO: separate Gui thing from here
//			setGuiFromConfig(_connections);
//		}
        private void initiateNewRecord()
        {
            if (editingMode == RecordEditingMode.New)
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

            editPropertyGrid.SelectedObject = newConfigItem;
        }

//		private void newRecordButton_Click(object sender, System.EventArgs e)
//		{
//			initiateNewRecord();
//		}
        private ListViewItem getListViewItemForRemoteConnectionInstance(RemoteConnectionInstance rci)
        {
            var lvi =
                new ListViewItem
                    (
                    new[]
                        {
                            rci.IsConnected.ToString(),
                            rci.Configuration.Enabled.ToString(),
                            rci.Configuration.Name,
                            rci.Configuration.Uri,
                            rci.Configuration.ProtocolType.ToString(),
                            rci.Configuration.ServiceHost,
                            rci.Configuration.Port
                        },
                    0
                    );

            lvi.Tag = rci;

            return lvi;
        }

        private void rcc_Changed(object sender, EventArgs e)
        {
            if (connectionsListView.SelectedItems.Count == 0)
                throw new Exception("Unexpected situation - connectionsListView.SelectedItems.Count should be 1 here");
            for (int i = 0; i < connectionsListView.SelectedItems[0].SubItems.Count; i ++)
            {
                connectionsListView.SelectedItems[0].SubItems[i] =
                    getListViewItemForRemoteConnectionInstance(sender as RemoteConnectionInstance).SubItems[i];
            }
        }

        private void connectConnector(RemoteConnectionInstance rci)
        {
            if (rci.IsConnected)
            {
                MessageBox.Show("This connection instance has already connected!");
                return;
            }

            rci.Connect();
        }

        private void disconnectConnector(RemoteConnectionInstance rci)
        {
            if (!rci.IsConnected)
            {
                MessageBox.Show("This connection instance is not connected!");
                return;
            }
            rci.Disconnect();
        }

        private void commandPanel_Paint(object sender, PaintEventArgs e)
        {
        }

//		private void connectButton_Click(object sender, System.EventArgs e)
//		{
//			if (connectionsListView.SelectedItems.Count == 0) return; // TODO: throw an exception
//			
//			try
//			{
//				this.connectConnector
//					(
//					connectionsListView.SelectedItems[0].Tag as RemoteConnectionInstance
//					);
//			}
//			catch (Exception ex)
//			{
//				MessageBox.Show(ex.ToString());
//				return;
//			}
//		}

        private void uploadButton_Click(object sender, EventArgs e)
        {
        }

        private void closeMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void uploadMenuItem_Click(object sender, EventArgs e)
        {
        }


//		private void disconnectButton_Click(object sender, System.EventArgs e)
//		{
//			if (connectionsListView.SelectedItems.Count == 0) return; // TODO: throw an exception
//			
//			try
//			{
//				this.disconnectConnector
//					(
//					connectionsListView.SelectedItems[0].Tag as RemoteConnectionInstance
//					);
//			}
//			catch (Exception ex)
//			{
//				MessageBox.Show(ex.ToString());
//				return;
//			}
//		}

        private void newRecordItem_Click(object sender, EventArgs e)
        {
            initiateNewRecord();
        }

        private void deleteRecordMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: Validate
            deleteConnectionRecord(editPropertyGrid.SelectedObject as RemoteConnectionConfiguration);
            // TODO: separate Gui thing from here
            setGuiFromConfig(_connections);
        }

        private void connectMenuItem_Click(object sender, EventArgs e)
        {
            if (connectionsListView.SelectedItems.Count == 0) return; // TODO: throw an exception

            try
            {
                connectConnector
                    (
                    connectionsListView.SelectedItems[0].Tag as RemoteConnectionInstance
                    );
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
        }

        private void disconnectMenuItem_Click(object sender, EventArgs e)
        {
            if (connectionsListView.SelectedItems.Count == 0) return; // TODO: throw an exception

            try
            {
                disconnectConnector
                    (
                    connectionsListView.SelectedItems[0].Tag as RemoteConnectionInstance
                    );
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
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
            this.connectedColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.enabledColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.nameColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.uriColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.protocolColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.serviceHostColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.portColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.remoteSelectedItemContextMenu = new System.Windows.Forms.ContextMenu();
            this.newRecordItem = new System.Windows.Forms.MenuItem();
            this.deleteRecordMenuItem = new System.Windows.Forms.MenuItem();
            this.connectMenuItem = new System.Windows.Forms.MenuItem();
            this.disconnectMenuItem = new System.Windows.Forms.MenuItem();
            this.propertiesTabControl = new System.Windows.Forms.TabControl();
            this.propertiesTabPage = new System.Windows.Forms.TabPage();
            this.editPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.propertiesTabControl.SuspendLayout();
            this.propertiesTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // connectionsListView
            // 
            this.connectionsListView.AllowColumnReorder = true;
            this.connectionsListView.Anchor =
                ((System.Windows.Forms.AnchorStyles)
                 (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                   | System.Windows.Forms.AnchorStyles.Right)));
            this.connectionsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[]
                                                          {
                                                              this.connectedColumnHeader,
                                                              this.enabledColumnHeader,
                                                              this.nameColumnHeader,
                                                              this.uriColumnHeader,
                                                              this.protocolColumnHeader,
                                                              this.serviceHostColumnHeader,
                                                              this.portColumnHeader
                                                          });
            this.connectionsListView.ContextMenu = this.remoteSelectedItemContextMenu;
            this.connectionsListView.FullRowSelect = true;
            this.connectionsListView.GridLines = true;
            this.connectionsListView.Location = new System.Drawing.Point(0, 0);
            this.connectionsListView.MultiSelect = false;
            this.connectionsListView.Name = "connectionsListView";
            this.connectionsListView.Size = new System.Drawing.Size(736, 166);
            this.connectionsListView.TabIndex = 0;
            this.connectionsListView.View = System.Windows.Forms.View.Details;
            this.connectionsListView.SelectedIndexChanged +=
                new System.EventHandler(this.connectionsListView_SelectedIndexChanged);
            // 
            // connectedColumnHeader
            // 
            this.connectedColumnHeader.Text = "Connected";
            this.connectedColumnHeader.Width = 70;
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
            this.remoteSelectedItemContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[]
                                                                      {
                                                                          this.newRecordItem,
                                                                          this.deleteRecordMenuItem,
                                                                          this.connectMenuItem,
                                                                          this.disconnectMenuItem
                                                                      });
            // 
            // newRecordItem
            // 
            this.newRecordItem.Index = 0;
            this.newRecordItem.Text = "New";
            this.newRecordItem.Click += new System.EventHandler(this.newRecordItem_Click);
            // 
            // deleteRecordMenuItem
            // 
            this.deleteRecordMenuItem.Index = 1;
            this.deleteRecordMenuItem.Text = "Delete";
            this.deleteRecordMenuItem.Click += new System.EventHandler(this.deleteRecordMenuItem_Click);
            // 
            // connectMenuItem
            // 
            this.connectMenuItem.Index = 2;
            this.connectMenuItem.Text = "Connect";
            this.connectMenuItem.Click += new System.EventHandler(this.connectMenuItem_Click);
            // 
            // disconnectMenuItem
            // 
            this.disconnectMenuItem.Index = 3;
            this.disconnectMenuItem.Text = "Disconnect";
            this.disconnectMenuItem.Click += new System.EventHandler(this.disconnectMenuItem_Click);
            // 
            // propertiesTabControl
            // 
            this.propertiesTabControl.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.propertiesTabControl.Anchor =
                ((System.Windows.Forms.AnchorStyles)
                 ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                    | System.Windows.Forms.AnchorStyles.Left)
                   | System.Windows.Forms.AnchorStyles.Right)));
            this.propertiesTabControl.Controls.Add(this.propertiesTabPage);
            this.propertiesTabControl.Location = new System.Drawing.Point(0, 168);
            this.propertiesTabControl.Multiline = true;
            this.propertiesTabControl.Name = "propertiesTabControl";
            this.propertiesTabControl.SelectedIndex = 0;
            this.propertiesTabControl.Size = new System.Drawing.Size(736, 264);
            this.propertiesTabControl.TabIndex = 3;
            // 
            // propertiesTabPage
            // 
            this.propertiesTabPage.Controls.Add(this.editPropertyGrid);
            this.propertiesTabPage.Location = new System.Drawing.Point(4, 4);
            this.propertiesTabPage.Name = "propertiesTabPage";
            this.propertiesTabPage.Size = new System.Drawing.Size(728, 238);
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
            this.editPropertyGrid.Size = new System.Drawing.Size(728, 238);
            this.editPropertyGrid.TabIndex = 0;
            this.editPropertyGrid.Text = "propertyGrid1";
            this.editPropertyGrid.ToolbarVisible = false;
            this.editPropertyGrid.ViewBackColor = System.Drawing.SystemColors.Window;
            this.editPropertyGrid.ViewForeColor = System.Drawing.SystemColors.WindowText;
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
            // ObserversConfigurationControl
            // 
            this.Controls.Add(this.propertiesTabControl);
            this.Controls.Add(this.connectionsListView);
            this.Name = "ObserversConfigurationControl";
            this.Size = new System.Drawing.Size(736, 432);
            this.propertiesTabControl.ResumeLayout(false);
            this.propertiesTabPage.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion
    }
}