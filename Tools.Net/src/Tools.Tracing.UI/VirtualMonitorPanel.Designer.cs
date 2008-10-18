namespace Tools.Tracing.UI
{
	partial class VirtualMonitorPanel
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
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
            this.monitorListView = new Tools.Tracing.UI.ScrollableListView();
            this.TimeHeader = new System.Windows.Forms.ColumnHeader();
            this.CHIdHeader = new System.Windows.Forms.ColumnHeader();
            this.IIdHeader = new System.Windows.Forms.ColumnHeader();
            this.IPIdHeader = new System.Windows.Forms.ColumnHeader();
            this.ElIdHeader = new System.Windows.Forms.ColumnHeader();
            this.ERHeader = new System.Windows.Forms.ColumnHeader();
            this.EventIdHeader = new System.Windows.Forms.ColumnHeader();
            this.EventNameHeader = new System.Windows.Forms.ColumnHeader();
            this.TypeHeader = new System.Windows.Forms.ColumnHeader();
            this.MessageHeader = new System.Windows.Forms.ColumnHeader();
            this.ThreadNameHeader = new System.Windows.Forms.ColumnHeader();
            this.HostHeader = new System.Windows.Forms.ColumnHeader();
            this.PrincipalHeader = new System.Windows.Forms.ColumnHeader();
            this.AppDomainNameHeader = new System.Windows.Forms.ColumnHeader();
            this.classHeader = new System.Windows.Forms.ColumnHeader();
            this.MethodHeader = new System.Windows.Forms.ColumnHeader();
            this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.countersToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.countersToolStrip);
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.monitorListView);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(801, 161);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(801, 211);
            this.toolStripContainer1.TabIndex = 2;
            this.toolStripContainer1.Text = "toolStripContainer1";
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
            this.countersToolStrip.TabIndex = 0;
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
            // monitorListView
            // 
            this.monitorListView.AllowColumnReorder = true;
            this.monitorListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.TimeHeader,
            this.CHIdHeader,
            this.IIdHeader,
            this.IPIdHeader,
            this.ElIdHeader,
            this.ERHeader,
            this.EventIdHeader,
            this.EventNameHeader,
            this.TypeHeader,
            this.MessageHeader,
            this.ThreadNameHeader,
            this.HostHeader,
            this.PrincipalHeader,
            this.AppDomainNameHeader,
            this.classHeader,
            this.MethodHeader});
            this.monitorListView.CountToShow = 8;
            this.monitorListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.monitorListView.FullRowSelect = true;
            this.monitorListView.HideSelection = false;
            this.monitorListView.Location = new System.Drawing.Point(0, 0);
            this.monitorListView.MinIndex = 0;
            this.monitorListView.MultiSelect = false;
            this.monitorListView.Name = "monitorListView";
            this.monitorListView.Size = new System.Drawing.Size(801, 161);
            this.monitorListView.TabIndex = 1;
            this.monitorListView.UseCompatibleStateImageBehavior = false;
            this.monitorListView.View = System.Windows.Forms.View.Details;
            this.monitorListView.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // TimeHeader
            // 
            this.TimeHeader.Name = "TimeHeader";
            this.TimeHeader.Text = "Time";
            this.TimeHeader.Width = 103;
            // 
            // CHIdHeader
            // 
            this.CHIdHeader.Name = "CHIdHeader";
            this.CHIdHeader.Text = "CHId";
            this.CHIdHeader.Width = 46;
            // 
            // IIdHeader
            // 
            this.IIdHeader.Name = "IIdHeader";
            this.IIdHeader.Text = "IId";
            this.IIdHeader.Width = 50;
            // 
            // IPIdHeader
            // 
            this.IPIdHeader.Name = "IPIdHeader";
            this.IPIdHeader.Text = "IPId";
            this.IPIdHeader.Width = 50;
            // 
            // ElIdHeader
            // 
            this.ElIdHeader.Name = "ElIdHeader";
            this.ElIdHeader.Text = "ElId";
            this.ElIdHeader.Width = 50;
            // 
            // ERHeader
            // 
            this.ERHeader.Name = "ERHeader";
            this.ERHeader.Text = "ER";
            this.ERHeader.Width = 100;
            // 
            // EventIdHeader
            // 
            this.EventIdHeader.Name = "EventIdHeader";
            this.EventIdHeader.Text = "EventId";
            this.EventIdHeader.Width = 53;
            // 
            // EventNameHeader
            // 
            this.EventNameHeader.Name = "EventNameHeader";
            this.EventNameHeader.Text = "EventName";
            this.EventNameHeader.Width = 150;
            // 
            // TypeHeader
            // 
            this.TypeHeader.Name = "TypeHeader";
            this.TypeHeader.Text = "Type";
            this.TypeHeader.Width = 61;
            // 
            // MessageHeader
            // 
            this.MessageHeader.Name = "MessageHeader";
            this.MessageHeader.Text = "Message";
            this.MessageHeader.Width = 200;
            // 
            // ThreadNameHeader
            // 
            this.ThreadNameHeader.Name = "ThreadNameHeader";
            this.ThreadNameHeader.Text = "ThreadName";
            // 
            // HostHeader
            // 
            this.HostHeader.Name = "HostHeader";
            this.HostHeader.Text = "Host";
            this.HostHeader.Width = 80;
            // 
            // PrincipalHeader
            // 
            this.PrincipalHeader.Name = "PrincipalHeader";
            this.PrincipalHeader.Text = "Principal";
            this.PrincipalHeader.Width = 100;
            // 
            // AppDomainNameHeader
            // 
            this.AppDomainNameHeader.Name = "AppDomainNameHeader";
            this.AppDomainNameHeader.Text = "AppDomain";
            // 
            // classHeader
            // 
            this.classHeader.Name = "classHeader";
            this.classHeader.Text = "Class";
            // 
            // MethodHeader
            // 
            this.MethodHeader.Name = "MethodHeader";
            this.MethodHeader.Text = "Method";
            // 
            // MonitorPanel
            // 
            this.Controls.Add(this.toolStripContainer1);
            this.Name = "MonitorPanel";
            this.Size = new System.Drawing.Size(801, 211);
            this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.countersToolStrip.ResumeLayout(false);
            this.countersToolStrip.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private ScrollableListView monitorListView;
		private System.Windows.Forms.ColumnHeader TimeHeader;
		private System.Windows.Forms.ColumnHeader CHIdHeader;
		private System.Windows.Forms.ColumnHeader IIdHeader;
		private System.Windows.Forms.ColumnHeader IPIdHeader;
		private System.Windows.Forms.ColumnHeader ElIdHeader;
		private System.Windows.Forms.ColumnHeader ERHeader;
		private System.Windows.Forms.ColumnHeader EventIdHeader;
		private System.Windows.Forms.ColumnHeader EventNameHeader;
		private System.Windows.Forms.ColumnHeader TypeHeader;
		private System.Windows.Forms.ColumnHeader MessageHeader;
		private System.Windows.Forms.ColumnHeader ThreadNameHeader;
		private System.Windows.Forms.ColumnHeader HostHeader;
		private System.Windows.Forms.ColumnHeader PrincipalHeader;
		private System.Windows.Forms.ColumnHeader AppDomainNameHeader;
		private System.Windows.Forms.ColumnHeader classHeader;
        private System.Windows.Forms.ColumnHeader MethodHeader;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip countersToolStrip;
        private System.Windows.Forms.ToolStripTextBox shownCountToolStripTextBox;
        private System.Windows.Forms.ToolStripLabel shownCountToolStripLabel;
        private System.Windows.Forms.ToolStripLabel cachedCountToolStripLabel;
        private System.Windows.Forms.ToolStripTextBox cachedCountToolStripTextBox;
        private System.Windows.Forms.ToolStripLabel filteredOutCountToolStripLabel;
        private System.Windows.Forms.ToolStripTextBox filteredOutCountToolStripTextBox;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton;
        private System.Windows.Forms.ToolStripMenuItem cachedCountToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shownCountToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filteredOutToolStripMenuItem;
	}
}
