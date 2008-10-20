using System;
using System.ComponentModel;
using System.Windows.Forms;
using Tools.Core.Configuration;
using Tools.Core.Utils;
using Tools.Tracing.Common;

namespace Tools.Tracing.UI
{
    /// <summary>
    /// Summary description for EventHandlerManagerConfigurationEditorControl.
    /// </summary>
    public class EventHandlerManagerConfigurationEditorControl : UserControl
    {
        private TraceEventHandlerManagerConfiguration _configuration;

        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private Container components;

        private ContextMenu controlContextMenu;
        private MenuItem copyToClipboardMenuItem;

        private TreeView filterTreeView;
        private Label label1;
        private MenuItem loadFromClipboardMenuItem;
        private MenuItem loadFromFileMenuItem;
        private MenuItem menuItem1;
        private MenuItem menuItem3;
        private OpenFileDialog openFileDialog1;
        private PropertyGrid propertyGrid1;
        private MenuItem refreshMenuItem;
        private SaveFileDialog saveFileDialog1;
        private MenuItem saveToFileMenuItem;
        private TextBox textBox1;

        public EventHandlerManagerConfigurationEditorControl()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            _configuration = new TraceEventHandlerManagerConfiguration();

            propertyGrid1.SelectedObject = _configuration;

            buildTreeView();

            filterTreeView.AfterSelect += filterTreeView_AfterSelect;

            // TODO: Add any initialization after the InitializeComponent call
        }

        public TraceEventHandlerManagerConfiguration Configuration
        {
            get { return _configuration; }
            set
            {
                _configuration = value;
                refresh();
            }
        }

        private void buildTreeView()
        {
            filterTreeView.Nodes.Clear();

            var root = new TreeNode(_configuration.Name);
            root.Tag = _configuration;

            foreach (TraceEventHandlerConfiguration handlerConfig in _configuration.Handlers)
            {
                //TreeNode nd = new TreeNode(handlerConfig.Name);
                //nd.Tag = handlerConfig;
                ////
                //nd.Nodes.Add
                //    (
                //    getTypeSourceTreeViewNode
                //        (
                //            handlerConfig.TypeActivationSource
                //        )
                //    );
                ////
                //foreach (TraceEventFilterConfiguration filter in handlerConfig.Filters)
                //{
                //    TreeNode nf = new TreeNode(filter.Name);
                //    nf.Tag = filter;
                //    nf.Nodes.Add
                //        (
                //        getTypeSourceTreeViewNode
                //            (
                //                filter.TypeActivationSource
                //            )
                //        );
                //    nd.Nodes.Add(nf);

                //}
                //root.Nodes.Add(nd);
            }

            filterTreeView.Nodes.Add(root);
            filterTreeView.ExpandAll();
        }

        private TreeNode getTypeSourceTreeViewNode(TypeActivationSource typeSource)
        {
            var root = new TreeNode("TypeSource");
            root.Tag = typeSource;

            var typeLocatorNode = new TreeNode("TypeLocator");
            typeLocatorNode.Tag = typeSource.TypeLocator;
            root.Nodes.Add(typeLocatorNode);

            var argumentsNode = new TreeNode("Arguments");

            foreach (ActivationArgument a in typeSource.Arguments)
            {
                var argNode = new TreeNode(a.Name);
                argNode.Tag = a;
                argumentsNode.Nodes.Add(argNode);
            }
            root.Nodes.Add(argumentsNode);

            return root;
        }

        private void filterTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            propertyGrid1.SelectedObject = e.Node.Tag;
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

        private void refresh()
        {
            buildTreeView();
        }

        private void saveToFileMenuItem_Click(object sender, EventArgs e)
        {
            // setConfigurationFromGui();
            saveFileDialog1.DefaultExt = "xml";
            saveFileDialog1.Filter = "Xml files (*.xml)|*.xml|All files (*.*)|*.*";
            DialogResult dr = saveFileDialog1.ShowDialog();

            if (dr == DialogResult.Cancel) return;
            if (saveFileDialog1.FileName != null || saveFileDialog1.FileName != String.Empty)
            {
                SerializationUtility.Serialize2File
                    (
                    _configuration, saveFileDialog1.FileName, false, false
                    );
            }
            textBox1.Text = saveFileDialog1.FileName;
        }

        private void loadFromFileMenuItem_Click(object sender, EventArgs e)
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
                    _configuration =
                        (TraceEventHandlerManagerConfiguration) SerializationUtility.DeserializeFromFile
                                                                    (
                                                                    openFileDialog1.FileName,
                                                                    typeof (TraceEventHandlerManagerConfiguration)
                                                                    );
                    // updateConfigurationGui();
                    // temp placement
                    buildTreeView();
                    textBox1.Text = openFileDialog1.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Probably not a valid TraceEventHandlerManagerConfiguration Document! " + ex);
                }
            }
        }

        private void refreshMenuItem_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void copyToClipboardMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject
                (
                SerializationUtility.Serialize2String(Configuration),
                true
                );
        }

        private void loadFromClipboardMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Text))
                {
                    _configuration =
                        (TraceEventHandlerManagerConfiguration) SerializationUtility.DeserializeFromString
                                                                    (
                                                                    Convert.ToString(
                                                                        Clipboard.GetDataObject().GetData(
                                                                            DataFormats.Text)),
                                                                    typeof (TraceEventHandlerManagerConfiguration)
                                                                    );
                    // updateConfigurationGui();
                    // temp placement
                    buildTreeView();
                    textBox1.Text = openFileDialog1.FileName;
                }
                else
                {
                    MessageBox.Show("No text data present in the clipboard!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Probably not a valid TraceEventHandlerManagerConfiguration Document! " + ex);
            }
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.filterTreeView = new System.Windows.Forms.TreeView();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.controlContextMenu = new System.Windows.Forms.ContextMenu();
            this.refreshMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.loadFromFileMenuItem = new System.Windows.Forms.MenuItem();
            this.saveToFileMenuItem = new System.Windows.Forms.MenuItem();
            this.loadFromClipboardMenuItem = new System.Windows.Forms.MenuItem();
            this.copyToClipboardMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Anchor =
                ((System.Windows.Forms.AnchorStyles)
                 ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                    | System.Windows.Forms.AnchorStyles.Left)
                   | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyGrid1.CommandsVisibleIfAvailable = true;
            this.propertyGrid1.LargeButtons = false;
            this.propertyGrid1.LineColor = System.Drawing.SystemColors.ScrollBar;
            this.propertyGrid1.Location = new System.Drawing.Point(240, 0);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(584, 408);
            this.propertyGrid1.TabIndex = 0;
            this.propertyGrid1.Text = "propertyGrid1";
            this.propertyGrid1.ViewBackColor = System.Drawing.SystemColors.Window;
            this.propertyGrid1.ViewForeColor = System.Drawing.SystemColors.WindowText;
            // 
            // filterTreeView
            // 
            this.filterTreeView.Anchor =
                ((System.Windows.Forms.AnchorStyles)
                 (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                   | System.Windows.Forms.AnchorStyles.Left)));
            this.filterTreeView.ImageIndex = -1;
            this.filterTreeView.Location = new System.Drawing.Point(0, 0);
            this.filterTreeView.Name = "filterTreeView";
            this.filterTreeView.SelectedImageIndex = -1;
            this.filterTreeView.Size = new System.Drawing.Size(240, 432);
            this.filterTreeView.TabIndex = 1;
            // 
            // textBox1
            // 
            this.textBox1.Anchor =
                ((System.Windows.Forms.AnchorStyles)
                 (((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                   | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Location = new System.Drawing.Point(288, 409);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(536, 20);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "";
            // 
            // label1
            // 
            this.label1.Anchor =
                ((System.Windows.Forms.AnchorStyles)
                 ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.Location = new System.Drawing.Point(248, 408);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 23);
            this.label1.TabIndex = 4;
            this.label1.Text = "File";
            // 
            // controlContextMenu
            // 
            this.controlContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[]
                                                           {
                                                               this.refreshMenuItem,
                                                               this.menuItem3,
                                                               this.loadFromFileMenuItem,
                                                               this.saveToFileMenuItem,
                                                               this.menuItem1,
                                                               this.loadFromClipboardMenuItem,
                                                               this.copyToClipboardMenuItem
                                                           });
            // 
            // refreshMenuItem
            // 
            this.refreshMenuItem.DefaultItem = true;
            this.refreshMenuItem.Index = 0;
            this.refreshMenuItem.Text = "Refresh";
            this.refreshMenuItem.Click += new System.EventHandler(this.refreshMenuItem_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 1;
            this.menuItem3.Text = "-";
            // 
            // loadFromFileMenuItem
            // 
            this.loadFromFileMenuItem.Index = 2;
            this.loadFromFileMenuItem.Text = "Load From File";
            this.loadFromFileMenuItem.Click += new System.EventHandler(this.loadFromFileMenuItem_Click);
            // 
            // saveToFileMenuItem
            // 
            this.saveToFileMenuItem.Index = 3;
            this.saveToFileMenuItem.Text = "Save To File";
            this.saveToFileMenuItem.Click += new System.EventHandler(this.saveToFileMenuItem_Click);
            // 
            // loadFromClipboardMenuItem
            // 
            this.loadFromClipboardMenuItem.Index = 5;
            this.loadFromClipboardMenuItem.Text = "Load From Clipboard";
            this.loadFromClipboardMenuItem.Click += new System.EventHandler(this.loadFromClipboardMenuItem_Click);
            // 
            // copyToClipboardMenuItem
            // 
            this.copyToClipboardMenuItem.Index = 6;
            this.copyToClipboardMenuItem.Text = "Copy To Clipboard";
            this.copyToClipboardMenuItem.Click += new System.EventHandler(this.copyToClipboardMenuItem_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 4;
            this.menuItem1.Text = "-";
            // 
            // EventHandlerManagerConfigurationEditorControl
            // 
            this.ContextMenu = this.controlContextMenu;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.filterTreeView);
            this.Controls.Add(this.propertyGrid1);
            this.Name = "EventHandlerManagerConfigurationEditorControl";
            this.Size = new System.Drawing.Size(824, 432);
            this.ResumeLayout(false);
        }

        #endregion
    }
}