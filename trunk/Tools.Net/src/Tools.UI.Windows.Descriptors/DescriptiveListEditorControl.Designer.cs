using Tools.Core;

namespace Tools.UI.Windows.Descriptors
{
    partial class DescriptiveListEditorControl
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
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.filePathToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.saveToolStrip = new System.Windows.Forms.ToolStrip();
            this.saveAllToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.settingsToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.findToolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.findToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.marksPresentationToolStrip = new System.Windows.Forms.ToolStrip();
            this.marksPresentationToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.splitContainer.SuspendLayout();
            this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.saveToolStrip.SuspendLayout();
            this.findToolStrip.SuspendLayout();
            this.marksPresentationToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.splitContainer.Size = new System.Drawing.Size(401, 278);
            this.splitContainer.SplitterDistance = 139;
            this.splitContainer.TabIndex = 1;
            this.splitContainer.Text = "splitContainer1";
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.statusStrip1);
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(401, 278);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(401, 325);
            this.toolStripContainer1.TabIndex = 2;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.saveToolStrip);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.findToolStrip);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.marksPresentationToolStrip);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filePathToolStripStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.Size = new System.Drawing.Size(401, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // filePathToolStripStatusLabel
            // 
            this.filePathToolStripStatusLabel.Name = "filePathToolStripStatusLabel";
            this.filePathToolStripStatusLabel.Size = new System.Drawing.Size(27, 17);
            this.filePathToolStripStatusLabel.Text = "File:";
            // 
            // saveToolStrip
            // 
            this.saveToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.saveToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveAllToolStripButton,
            this.openToolStripButton,
            this.toolStripSeparator1,
            this.settingsToolStripButton});
            this.saveToolStrip.Location = new System.Drawing.Point(140, 0);
            this.saveToolStrip.Name = "saveToolStrip";
            this.saveToolStrip.Size = new System.Drawing.Size(160, 25);
            this.saveToolStrip.TabIndex = 0;
            this.saveToolStrip.Text = "toolStrip1";
            // 
            // saveAllToolStripButton
            // 
            this.saveAllToolStripButton.Image = global::Tools.UI.Windows.Descriptors.Properties.Resources.DISKS04;
            this.saveAllToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveAllToolStripButton.Name = "saveAllToolStripButton";
            this.saveAllToolStripButton.Size = new System.Drawing.Size(66, 22);
            this.saveAllToolStripButton.Text = "Save As";
            this.saveAllToolStripButton.ToolTipText = "Save all to file or clipboard";
            this.saveAllToolStripButton.Click += new System.EventHandler(this.saveAllToolStripButton_Click);
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.Image = global::Tools.UI.Windows.Descriptors.Properties.Resources.FOLDER05;
            this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Size = new System.Drawing.Size(53, 22);
            this.openToolStripButton.Text = "Open";
            this.openToolStripButton.ToolTipText = "Open From File or Clipboard";
            this.openToolStripButton.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // settingsToolStripButton
            // 
            this.settingsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.settingsToolStripButton.Image = global::Tools.UI.Windows.Descriptors.Properties.Resources.FullScreen;
            this.settingsToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.settingsToolStripButton.Name = "settingsToolStripButton";
            this.settingsToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.settingsToolStripButton.Text = "toolStripButton2";
            this.settingsToolStripButton.Click += new System.EventHandler(this.settingsToolStripButton_Click);
            // 
            // findToolStrip
            // 
            this.findToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.findToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton3,
            this.findToolStripTextBox});
            this.findToolStrip.Location = new System.Drawing.Point(3, 0);
            this.findToolStrip.Name = "findToolStrip";
            this.findToolStrip.Size = new System.Drawing.Size(137, 25);
            this.findToolStrip.TabIndex = 1;
            this.findToolStrip.Text = "toolStrip1";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = global::Tools.UI.Windows.Descriptors.Properties.Resources.more;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton3.Text = "findToolStripButton";
            // 
            // findToolStripTextBox
            // 
            this.findToolStripTextBox.Name = "findToolStripTextBox";
            this.findToolStripTextBox.Size = new System.Drawing.Size(100, 25);
            // 
            // marksPresentationToolStrip
            // 
            this.marksPresentationToolStrip.CanOverflow = false;
            this.marksPresentationToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.marksPresentationToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.marksPresentationToolStripButton});
            this.marksPresentationToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.marksPresentationToolStrip.Location = new System.Drawing.Point(300, 0);
            this.marksPresentationToolStrip.Name = "marksPresentationToolStrip";
            this.marksPresentationToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.marksPresentationToolStrip.Size = new System.Drawing.Size(35, 25);
            this.marksPresentationToolStrip.TabIndex = 0;
            // 
            // marksPresentationToolStripButton
            // 
            this.marksPresentationToolStripButton.CheckOnClick = true;
            this.marksPresentationToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.marksPresentationToolStripButton.Image = global::Tools.UI.Windows.Descriptors.Properties.Resources.icon_go_up;
            this.marksPresentationToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.marksPresentationToolStripButton.Name = "marksPresentationToolStripButton";
            this.marksPresentationToolStripButton.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.marksPresentationToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.marksPresentationToolStripButton.Text = "toolStripButton4";
            this.marksPresentationToolStripButton.Click += new System.EventHandler(this.marksPresentationToolStripButton_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "xml";
            this.openFileDialog.Filter = "Xml Files (*.xml)|*.xml";
            this.openFileDialog.Title = "File opne dialog";
            // 
            // DescriptiveListEditorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStripContainer1);
            this.Name = "DescriptiveListEditorControl";
            this.Size = new System.Drawing.Size(401, 325);
            this.splitContainer.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.saveToolStrip.ResumeLayout(false);
            this.saveToolStrip.PerformLayout();
            this.findToolStrip.ResumeLayout(false);
            this.findToolStrip.PerformLayout();
            this.marksPresentationToolStrip.ResumeLayout(false);
            this.marksPresentationToolStrip.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer;
        private ContainersControl
            <ListSettings, DescriptiveList<DescriptiveNameValue<string>>, DescriptiveNameValue<string>> dnvListControl;
		private System.Windows.Forms.ToolStripContainer toolStripContainer1;
		private System.Windows.Forms.ToolStrip saveToolStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStrip findToolStrip;
		private System.Windows.Forms.ToolStripTextBox findToolStripTextBox;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel filePathToolStripStatusLabel;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStrip marksPresentationToolStrip;
        private System.Windows.Forms.ToolStripButton saveAllToolStripButton;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolStripButton settingsToolStripButton;
        private System.Windows.Forms.ToolStripButton marksPresentationToolStripButton;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
	}
}
