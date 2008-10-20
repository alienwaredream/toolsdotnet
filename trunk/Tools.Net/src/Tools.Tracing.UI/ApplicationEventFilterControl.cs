using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;
using Tools.Core;
using Tools.Core.Utils;
using Tools.Tracing.Common;

namespace Tools.Tracing.UI
{
    /// <summary>
    /// Summary description for ApplicationEventFilterControl.
    /// </summary>
    public class ApplicationEventFilterControl : UserControl
    {
        private readonly FilterViewControl filterView;
        private ApplicationEventFilter _filter;
        private TextBox altActiveTextBox;
        private Panel commandsPanel;
        private IContainer components;
        private ContextMenu controlContextMenu;
        private MenuItem copyToClipboardMenuItem;
        private ImageList filterImageList;
        private PropertyGrid filterPropertyGrid;
        private Splitter filterSplitter;
        private TreeView filterTreeView;
        private MenuItem loadFromClipboardMenuItem;
        private MenuItem loadFromFileMenuItem;
        private MenuItem menuItem3;
        private OpenFileDialog openFileDialog1;
        private Panel panel1;
        private Panel panel2;
        private SaveFileDialog saveFileDialog1;
        private MenuItem saveToFileMenuItem;
        private TextBox textBox1;

        public ApplicationEventFilterControl()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            _filter = new ApplicationEventFilter();

            filterTreeView.ImageList = filterImageList;

            createTreeView(filterTreeView, null);

            filterTreeView.KeyDown += filterTreeView_KeyDown;
            filterTreeView.KeyUp += filterTreeView_KeyUp;

            //filter view
            filterView = new FilterViewControl();
            filterView.Left = filterSplitter.Right;
            filterView.Height = filterPropertyGrid.Top;
            filterView.Width = Width - filterSplitter.Width - filterTreeView.Width;
            filterView.Top = 0;
            filterView.Dock = DockStyle.Fill;
            panel2.Controls.Add(filterView);
        }

        public ApplicationEventFilter Filter
        {
            get { return _filter; }
            set
            {
                _filter = value;
                createTreeView(filterTreeView, _filter);
            }
        }

        private void createTreeView(TreeView treeview, ApplicationEventFilter sourceFilter)
        {
            filterTreeView.Nodes.Clear();
            var node = new TreeNode(typeof (TraceEvent).Name);
            node.ImageIndex = 4;
            filterTreeView.Nodes.Add(node);
            parseType(typeof (TraceEvent), typeof (TraceEvent).Name + "::", node, sourceFilter);
            filterTreeView.ExpandAll();
        }

        private void parseType
            (
            Type type,
            string propertyPath,
            TreeNode node,
            ApplicationEventFilter sourceFilter)
        {
            //Type type = parseObject.GetType();
            //TreeNode result = new TreeNode(type.Name);

            string pathRoot = propertyPath;

            foreach (PropertyInfo pi in type.GetProperties())
            {
                var childNode = new TreeNode(pi.Name);

                propertyPath = pathRoot;

                if (!pi.PropertyType.IsValueType && pi.PropertyType != typeof (String) &&
                    pi.PropertyType != typeof (object))
                {
                    propertyPath += pi.Name + ".";
                    parseType(pi.PropertyType, propertyPath, childNode, sourceFilter);
                    // Those are not for filtering onto
                    childNode.ImageIndex = 3;
                }
                else
                {
                    // Those to enable and disable and setup filters

                    // The long way with entry candidate is choosen in order to spare duplicate iteration
                    // via the filters collection.
                    FilterEntry entryCandidate = null;

                    string currentPath = propertyPath + pi.Name;

                    if (sourceFilter != null) entryCandidate = sourceFilter.FilterEntries[currentPath];
                    //
                    if (entryCandidate == null)
                    {
                        entryCandidate = new FilterEntry(currentPath);
                        _filter.FilterEntries.Add(entryCandidate);
                    }


                    childNode.ImageIndex = (entryCandidate.Enabled == false) ? 1 : 2;

                    propertyPath = pathRoot;

                    childNode.Tag = entryCandidate;
                }

                node.Nodes.Add(childNode);
            }
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

        private void filterPropertyGrid_Click(object sender, EventArgs e)
        {
        }

        private void filterSplitter_SplitterMoved(object sender, SplitterEventArgs e)
        {
            // TODO: Introduce a container for those onto this control
            filterPropertyGrid.Width = Width - filterTreeView.Width - 3;
            filterPropertyGrid.Left = filterSplitter.Right;
            filterView.Width = Width - filterTreeView.Width - 3;
            filterView.Left = filterSplitter.Right;
        }

        private void filterTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            filterPropertyGrid.SelectedObject = e.Node.Tag;
        }

        private void filterTreeView_KeyPress(object sender, KeyPressEventArgs e)
        {
            int i = 0;
        }

        private void filterTreeView_KeyDown(object sender, KeyEventArgs e)
        {
            altActiveTextBox.Text += " " + e.KeyCode;
            if (e.KeyCode == Keys.ControlKey) return;

            if (e.Control && e.KeyCode == Keys.E) OnEnableCurrentSelectedFilter();
            if (e.Control && e.KeyCode == Keys.D) OnDisableCurrentSelectedFilter();
        }

        private void OnDisableCurrentSelectedFilter()
        {
            setEnabledForSelectedNode(false);
        }

        private void OnEnableCurrentSelectedFilter()
        {
            setEnabledForSelectedNode(true);
        }

        private void setEnabledForSelectedNode(bool enabled)
        {
            TreeNode node = filterTreeView.SelectedNode;
            if (node == null) return;
            object o = node.Tag;
            if (o == null) return;
            var e = o as IEnabled;
            if (o == null) return;
            e.Enabled = enabled;
            filterPropertyGrid.SelectedObject = node.Tag;
            if (enabled)
            {
                node.ImageIndex = 2;
            }
            else
            {
                node.ImageIndex = 1;
            }
        }

        private void filterTreeView_KeyUp(object sender, KeyEventArgs e)
        {
            altActiveTextBox.Text = String.Empty;
        }

        private void refresh()
        {
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
                    _filter, saveFileDialog1.FileName, false, false
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
                    _filter =
                        (ApplicationEventFilter) SerializationUtility.DeserializeFromFile
                                                     (
                                                     openFileDialog1.FileName,
                                                     typeof (ApplicationEventFilter)
                                                     );
                    // updateConfigurationGui();
                    // temp placement
                    createTreeView(filterTreeView, _filter);
                    textBox1.Text = openFileDialog1.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Probably not a valid TraceEventFilter Document! " + ex);
                }
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
            var resources = new System.Resources.ResourceManager(typeof (ApplicationEventFilterControl));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.filterSplitter = new System.Windows.Forms.Splitter();
            this.filterTreeView = new System.Windows.Forms.TreeView();
            this.filterPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.filterImageList = new System.Windows.Forms.ImageList(this.components);
            this.commandsPanel = new System.Windows.Forms.Panel();
            this.altActiveTextBox = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.controlContextMenu = new System.Windows.Forms.ContextMenu();
            this.loadFromFileMenuItem = new System.Windows.Forms.MenuItem();
            this.saveToFileMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.loadFromClipboardMenuItem = new System.Windows.Forms.MenuItem();
            this.copyToClipboardMenuItem = new System.Windows.Forms.MenuItem();
            this.panel1.SuspendLayout();
            this.commandsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor =
                ((System.Windows.Forms.AnchorStyles)
                 ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                    | System.Windows.Forms.AnchorStyles.Left)
                   | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.filterSplitter);
            this.panel1.Controls.Add(this.filterTreeView);
            this.panel1.Controls.Add(this.filterPropertyGrid);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(936, 408);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Anchor =
                ((System.Windows.Forms.AnchorStyles)
                 ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                    | System.Windows.Forms.AnchorStyles.Left)
                   | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Location = new System.Drawing.Point(340, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(592, 144);
            this.panel2.TabIndex = 6;
            // 
            // filterSplitter
            // 
            this.filterSplitter.Location = new System.Drawing.Point(336, 0);
            this.filterSplitter.Name = "filterSplitter";
            this.filterSplitter.Size = new System.Drawing.Size(3, 408);
            this.filterSplitter.TabIndex = 5;
            this.filterSplitter.TabStop = false;
            this.filterSplitter.SplitterMoved +=
                new System.Windows.Forms.SplitterEventHandler(this.filterSplitter_SplitterMoved);
            // 
            // filterTreeView
            // 
            this.filterTreeView.Dock = System.Windows.Forms.DockStyle.Left;
            this.filterTreeView.ImageIndex = -1;
            this.filterTreeView.Location = new System.Drawing.Point(0, 0);
            this.filterTreeView.Name = "filterTreeView";
            this.filterTreeView.SelectedImageIndex = -1;
            this.filterTreeView.Size = new System.Drawing.Size(336, 408);
            this.filterTreeView.TabIndex = 4;
            this.filterTreeView.AfterSelect +=
                new System.Windows.Forms.TreeViewEventHandler(this.filterTreeView_AfterSelect);
            // 
            // filterPropertyGrid
            // 
            this.filterPropertyGrid.Anchor =
                ((System.Windows.Forms.AnchorStyles)
                 (((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                   | System.Windows.Forms.AnchorStyles.Right)));
            this.filterPropertyGrid.CommandsVisibleIfAvailable = true;
            this.filterPropertyGrid.LargeButtons = false;
            this.filterPropertyGrid.LineColor = System.Drawing.SystemColors.ScrollBar;
            this.filterPropertyGrid.Location = new System.Drawing.Point(340, 152);
            this.filterPropertyGrid.Name = "filterPropertyGrid";
            this.filterPropertyGrid.Size = new System.Drawing.Size(592, 256);
            this.filterPropertyGrid.TabIndex = 3;
            this.filterPropertyGrid.Text = "propertyGrid1";
            this.filterPropertyGrid.ViewBackColor = System.Drawing.SystemColors.Window;
            this.filterPropertyGrid.ViewForeColor = System.Drawing.SystemColors.WindowText;
            this.filterPropertyGrid.Click += new System.EventHandler(this.filterPropertyGrid_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor =
                ((System.Windows.Forms.AnchorStyles)
                 (((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                   | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Location = new System.Drawing.Point(155, 408);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(776, 20);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "";
            // 
            // filterImageList
            // 
            this.filterImageList.ImageSize = new System.Drawing.Size(16, 16);
            this.filterImageList.ImageStream =
                ((System.Windows.Forms.ImageListStreamer) (resources.GetObject("filterImageList.ImageStream")));
            this.filterImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // commandsPanel
            // 
            this.commandsPanel.Anchor =
                ((System.Windows.Forms.AnchorStyles)
                 ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.commandsPanel.Controls.Add(this.altActiveTextBox);
            this.commandsPanel.Location = new System.Drawing.Point(0, 408);
            this.commandsPanel.Name = "commandsPanel";
            this.commandsPanel.Size = new System.Drawing.Size(160, 24);
            this.commandsPanel.TabIndex = 3;
            // 
            // altActiveTextBox
            // 
            this.altActiveTextBox.Anchor =
                ((System.Windows.Forms.AnchorStyles)
                 ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.altActiveTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.altActiveTextBox.Location = new System.Drawing.Point(8, 8);
            this.altActiveTextBox.Name = "altActiveTextBox";
            this.altActiveTextBox.ReadOnly = true;
            this.altActiveTextBox.Size = new System.Drawing.Size(112, 13);
            this.altActiveTextBox.TabIndex = 0;
            this.altActiveTextBox.Text = "";
            // 
            // controlContextMenu
            // 
            this.controlContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[]
                                                           {
                                                               this.loadFromFileMenuItem,
                                                               this.saveToFileMenuItem,
                                                               this.menuItem3,
                                                               this.loadFromClipboardMenuItem,
                                                               this.copyToClipboardMenuItem
                                                           });
            // 
            // loadFromFileMenuItem
            // 
            this.loadFromFileMenuItem.Index = 0;
            this.loadFromFileMenuItem.Text = "Load From File";
            this.loadFromFileMenuItem.Click += new System.EventHandler(this.loadFromFileMenuItem_Click);
            // 
            // saveToFileMenuItem
            // 
            this.saveToFileMenuItem.Index = 1;
            this.saveToFileMenuItem.Text = "Save To File";
            this.saveToFileMenuItem.Click += new System.EventHandler(this.saveToFileMenuItem_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 2;
            this.menuItem3.Text = "-";
            // 
            // loadFromClipboardMenuItem
            // 
            this.loadFromClipboardMenuItem.Index = 3;
            this.loadFromClipboardMenuItem.Text = "Load From Clipboard";
            // 
            // copyToClipboardMenuItem
            // 
            this.copyToClipboardMenuItem.Index = 4;
            this.copyToClipboardMenuItem.Text = "Copy To Clipboard";
            // 
            // ApplicationEventFilterControl
            // 
            this.Controls.Add(this.commandsPanel);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.panel1);
            this.Name = "ApplicationEventFilterControl";
            this.Size = new System.Drawing.Size(936, 432);
            this.panel1.ResumeLayout(false);
            this.commandsPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion
    }
}