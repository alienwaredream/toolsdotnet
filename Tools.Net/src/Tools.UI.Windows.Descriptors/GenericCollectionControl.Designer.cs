using System;

namespace Tools.UI.Windows.Descriptors
{
    partial class GenericCollectionControl<T, SettingsType>
        where T : ICloneable, new()
        where SettingsType : IListSettings, new() 
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            this.components = new System.ComponentModel.Container();
            this.itemsListView = new System.Windows.Forms.ListView();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removeStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyAsNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.copyToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.retrieveFromClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.descriptorContainer = new Tools.UI.Windows.Descriptors.CollapsibleContainer();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // itemsListView
            // 
            this.itemsListView.AllowColumnReorder = true;
            this.itemsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.itemsListView.AutoArrange = false;
            this.itemsListView.FullRowSelect = true;
            this.itemsListView.GridLines = true;
            this.itemsListView.HideSelection = false;
            this.itemsListView.Location = new System.Drawing.Point(0, 67);
            this.itemsListView.Name = "itemsListView";
            this.itemsListView.Size = new System.Drawing.Size(341, 168);
            this.itemsListView.TabIndex = 0;
            this.itemsListView.View = System.Windows.Forms.View.Details;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Enabled = true;
            this.contextMenuStrip.GripMargin = new System.Windows.Forms.Padding(2);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeStripMenuItem,
            this.copyAsNewToolStripMenuItem,
            this.newToolStripMenuItem,
            this.toolStripSeparator1,
            this.copyToClipboardToolStripMenuItem,
            this.retrieveFromClipboardToolStripMenuItem});
            this.contextMenuStrip.Location = new System.Drawing.Point(23, 38);
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.contextMenuStrip.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.contextMenuStrip.Size = new System.Drawing.Size(194, 120);
            // 
            // removeStripMenuItem
            // 
            this.removeStripMenuItem.AutoToolTip = true;
            this.removeStripMenuItem.Image = Tools.UI.Windows.Descriptors.Properties.Resources.logoff_small;
            this.removeStripMenuItem.Name = "removeStripMenuItem";
            this.removeStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Control)
                        | System.Windows.Forms.Keys.R)));
            this.removeStripMenuItem.Text = "Remove";
            this.removeStripMenuItem.ToolTipText = "Removes the item.";
            this.removeStripMenuItem.Click += new System.EventHandler(this.removeStripMenuItem_Click);
            // 
            // copyAsNewToolStripMenuItem
            // 
            this.copyAsNewToolStripMenuItem.Image = Tools.UI.Windows.Descriptors.Properties.Resources.BttnImport;
            this.copyAsNewToolStripMenuItem.Name = "copyAsNewToolStripMenuItem";
            this.copyAsNewToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Control)
                        | System.Windows.Forms.Keys.C)));
            this.copyAsNewToolStripMenuItem.Text = "Copy As New";
            this.copyAsNewToolStripMenuItem.Click += new System.EventHandler(this.copyAsNewToolStripMenuItem_Click);
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Image = Tools.UI.Windows.Descriptors.Properties.Resources.sysfavr1;
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Control)
                        | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            // 
            // copyToClipboardToolStripMenuItem
            // 
            this.copyToClipboardToolStripMenuItem.Image = Tools.UI.Windows.Descriptors.Properties.Resources.comments;
            this.copyToClipboardToolStripMenuItem.Name = "copyToClipboardToolStripMenuItem";
            this.copyToClipboardToolStripMenuItem.Text = "Copy To Clipboard";
            this.copyToClipboardToolStripMenuItem.Click += new System.EventHandler(this.copyToClipboardToolStripMenuItem_Click);
            // 
            // retrieveFromClipboardToolStripMenuItem
            // 
            this.retrieveFromClipboardToolStripMenuItem.Image = Tools.UI.Windows.Descriptors.Properties.Resources.CRDFLE12;
            this.retrieveFromClipboardToolStripMenuItem.Name = "retrieveFromClipboardToolStripMenuItem";
            this.retrieveFromClipboardToolStripMenuItem.Text = "Retrieve From Clipboard";
            // 
            // descriptorContainer
            // 
            this.descriptorContainer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.descriptorContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.descriptorContainer.ContainedControl = null;
            this.descriptorContainer.Location = new System.Drawing.Point(0, -2);
            this.descriptorContainer.Name = "descriptorContainer";
            this.descriptorContainer.Size = new System.Drawing.Size(341, 70);
            this.descriptorContainer.TabIndex = 2;
            // 
            // GenericCollectionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.descriptorContainer);
            this.Controls.Add(this.itemsListView);
            this.Name = "GenericCollectionControl";
            this.Size = new System.Drawing.Size(341, 235);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView itemsListView;
        public System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem removeStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyAsNewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToClipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem retrieveFromClipboardToolStripMenuItem;
        private CollapsibleContainer descriptorContainer;
        private DescriptorControl descriptorControl1;
	}
}
