namespace Tools.Processes.Host
{
    partial class ProcessForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProcessForm));
            this.setupPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.debugButton = new System.Windows.Forms.Button();
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.logTabPage = new System.Windows.Forms.TabPage();
            this.outputToolStrip = new System.Windows.Forms.ToolStrip();
            this.regexToolStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.regexToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.applyRegexToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.clearLogToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.traceShutdownToolStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.traceShutdownToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.outputSplitContainer = new System.Windows.Forms.SplitContainer();
            this.outputListView = new System.Windows.Forms.ListView();
            this.descriptionColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.logRichTextBox = new System.Windows.Forms.RichTextBox();
            this.setupInfoTabPage = new System.Windows.Forms.TabPage();
            this.startProcessButton = new System.Windows.Forms.Button();
            this.stopProcessButton = new System.Windows.Forms.Button();
            this.connectTracesCheckBox = new System.Windows.Forms.CheckBox();
            this.traceShutdownDurationScToolStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mainTabControl.SuspendLayout();
            this.logTabPage.SuspendLayout();
            this.outputToolStrip.SuspendLayout();
            this.outputSplitContainer.Panel1.SuspendLayout();
            this.outputSplitContainer.Panel2.SuspendLayout();
            this.outputSplitContainer.SuspendLayout();
            this.setupInfoTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // setupPropertyGrid
            // 
            this.setupPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.setupPropertyGrid.HelpVisible = false;
            this.setupPropertyGrid.Location = new System.Drawing.Point(3, 3);
            this.setupPropertyGrid.Name = "setupPropertyGrid";
            this.setupPropertyGrid.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.setupPropertyGrid.Size = new System.Drawing.Size(711, 257);
            this.setupPropertyGrid.TabIndex = 0;
            // 
            // debugButton
            // 
            this.debugButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.debugButton.Location = new System.Drawing.Point(689, 289);
            this.debugButton.Name = "debugButton";
            this.debugButton.Size = new System.Drawing.Size(141, 23);
            this.debugButton.TabIndex = 1;
            this.debugButton.Text = "Attach debugger!";
            this.debugButton.UseVisualStyleBackColor = true;
            this.debugButton.Click += new System.EventHandler(this.debugButton_Click);
            // 
            // mainTabControl
            // 
            this.mainTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mainTabControl.Controls.Add(this.logTabPage);
            this.mainTabControl.Controls.Add(this.setupInfoTabPage);
            this.mainTabControl.Location = new System.Drawing.Point(0, -1);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(834, 289);
            this.mainTabControl.TabIndex = 2;
            // 
            // logTabPage
            // 
            this.logTabPage.Controls.Add(this.outputToolStrip);
            this.logTabPage.Controls.Add(this.outputSplitContainer);
            this.logTabPage.Location = new System.Drawing.Point(4, 22);
            this.logTabPage.Name = "logTabPage";
            this.logTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.logTabPage.Size = new System.Drawing.Size(826, 263);
            this.logTabPage.TabIndex = 1;
            this.logTabPage.Text = "Output";
            this.logTabPage.UseVisualStyleBackColor = true;
            // 
            // outputToolStrip
            // 
            this.outputToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.regexToolStripLabel,
            this.regexToolStripTextBox,
            this.applyRegexToolStripButton,
            this.toolStripSeparator1,
            this.clearLogToolStripButton,
            this.toolStripSeparator2,
            this.traceShutdownToolStripLabel,
            this.traceShutdownToolStripTextBox,
            this.traceShutdownDurationScToolStripLabel,
            this.toolStripSeparator3});
            this.outputToolStrip.Location = new System.Drawing.Point(3, 3);
            this.outputToolStrip.Name = "outputToolStrip";
            this.outputToolStrip.Size = new System.Drawing.Size(820, 25);
            this.outputToolStrip.TabIndex = 2;
            this.outputToolStrip.Text = "toolStrip1";
            // 
            // regexToolStripLabel
            // 
            this.regexToolStripLabel.Name = "regexToolStripLabel";
            this.regexToolStripLabel.Size = new System.Drawing.Size(104, 22);
            this.regexToolStripLabel.Text = "Description Regex:";
            // 
            // regexToolStripTextBox
            // 
            this.regexToolStripTextBox.Name = "regexToolStripTextBox";
            this.regexToolStripTextBox.Size = new System.Drawing.Size(200, 25);
            this.regexToolStripTextBox.Text = global::Tools.Processes.Host.Properties.Settings.Default.DescriptionRegex;
            // 
            // applyRegexToolStripButton
            // 
            this.applyRegexToolStripButton.BackColor = System.Drawing.Color.Wheat;
            this.applyRegexToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.applyRegexToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("applyRegexToolStripButton.Image")));
            this.applyRegexToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.applyRegexToolStripButton.Name = "applyRegexToolStripButton";
            this.applyRegexToolStripButton.Size = new System.Drawing.Size(76, 22);
            this.applyRegexToolStripButton.Text = "Apply Regex";
            this.applyRegexToolStripButton.Click += new System.EventHandler(this.applyRegexToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // clearLogToolStripButton
            // 
            this.clearLogToolStripButton.BackColor = System.Drawing.Color.Wheat;
            this.clearLogToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.clearLogToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("clearLogToolStripButton.Image")));
            this.clearLogToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.clearLogToolStripButton.Name = "clearLogToolStripButton";
            this.clearLogToolStripButton.Size = new System.Drawing.Size(38, 22);
            this.clearLogToolStripButton.Text = "Clear";
            this.clearLogToolStripButton.Click += new System.EventHandler(this.clearLogToolStripButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // traceShutdownToolStripLabel
            // 
            this.traceShutdownToolStripLabel.Name = "traceShutdownToolStripLabel";
            this.traceShutdownToolStripLabel.Size = new System.Drawing.Size(114, 22);
            this.traceShutdownToolStripLabel.Text = "Trace Shutdown for:";
            // 
            // traceShutdownToolStripTextBox
            // 
            this.traceShutdownToolStripTextBox.Name = "traceShutdownToolStripTextBox";
            this.traceShutdownToolStripTextBox.Size = new System.Drawing.Size(30, 25);
            this.traceShutdownToolStripTextBox.Text = global::Tools.Processes.Host.Properties.Settings.Default.TraceShutdownDurationSc;
            // 
            // outputSplitContainer
            // 
            this.outputSplitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.outputSplitContainer.Location = new System.Drawing.Point(3, 31);
            this.outputSplitContainer.Name = "outputSplitContainer";
            this.outputSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // outputSplitContainer.Panel1
            // 
            this.outputSplitContainer.Panel1.Controls.Add(this.outputListView);
            // 
            // outputSplitContainer.Panel2
            // 
            this.outputSplitContainer.Panel2.Controls.Add(this.logRichTextBox);
            this.outputSplitContainer.Size = new System.Drawing.Size(820, 229);
            this.outputSplitContainer.SplitterDistance = 108;
            this.outputSplitContainer.TabIndex = 1;
            // 
            // outputListView
            // 
            this.outputListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.descriptionColumnHeader});
            this.outputListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputListView.FullRowSelect = true;
            this.outputListView.GridLines = true;
            this.outputListView.Location = new System.Drawing.Point(0, 0);
            this.outputListView.MultiSelect = false;
            this.outputListView.Name = "outputListView";
            this.outputListView.Size = new System.Drawing.Size(820, 108);
            this.outputListView.TabIndex = 0;
            this.outputListView.UseCompatibleStateImageBehavior = false;
            this.outputListView.View = System.Windows.Forms.View.Details;
            this.outputListView.SelectedIndexChanged += new System.EventHandler(this.outputListView_SelectedIndexChanged);
            // 
            // descriptionColumnHeader
            // 
            this.descriptionColumnHeader.Text = "Description";
            this.descriptionColumnHeader.Width = 485;
            // 
            // logRichTextBox
            // 
            this.logRichTextBox.BackColor = System.Drawing.SystemColors.HotTrack;
            this.logRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logRichTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logRichTextBox.ForeColor = System.Drawing.Color.White;
            this.logRichTextBox.Location = new System.Drawing.Point(0, 0);
            this.logRichTextBox.Name = "logRichTextBox";
            this.logRichTextBox.ReadOnly = true;
            this.logRichTextBox.Size = new System.Drawing.Size(820, 117);
            this.logRichTextBox.TabIndex = 0;
            this.logRichTextBox.Text = "";
            // 
            // setupInfoTabPage
            // 
            this.setupInfoTabPage.Controls.Add(this.setupPropertyGrid);
            this.setupInfoTabPage.Location = new System.Drawing.Point(4, 22);
            this.setupInfoTabPage.Name = "setupInfoTabPage";
            this.setupInfoTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.setupInfoTabPage.Size = new System.Drawing.Size(717, 263);
            this.setupInfoTabPage.TabIndex = 0;
            this.setupInfoTabPage.Text = "Info";
            this.setupInfoTabPage.UseVisualStyleBackColor = true;
            // 
            // startProcessButton
            // 
            this.startProcessButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.startProcessButton.Location = new System.Drawing.Point(7, 289);
            this.startProcessButton.Name = "startProcessButton";
            this.startProcessButton.Size = new System.Drawing.Size(75, 23);
            this.startProcessButton.TabIndex = 3;
            this.startProcessButton.Text = "Start";
            this.startProcessButton.UseVisualStyleBackColor = true;
            this.startProcessButton.Click += new System.EventHandler(this.startProcessButton_Click);
            // 
            // stopProcessButton
            // 
            this.stopProcessButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.stopProcessButton.Location = new System.Drawing.Point(88, 289);
            this.stopProcessButton.Name = "stopProcessButton";
            this.stopProcessButton.Size = new System.Drawing.Size(75, 23);
            this.stopProcessButton.TabIndex = 4;
            this.stopProcessButton.Text = "Stop";
            this.stopProcessButton.UseVisualStyleBackColor = true;
            this.stopProcessButton.Click += new System.EventHandler(this.stopProcessButton_Click);
            // 
            // connectTracesCheckBox
            // 
            this.connectTracesCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.connectTracesCheckBox.AutoSize = true;
            this.connectTracesCheckBox.Location = new System.Drawing.Point(170, 291);
            this.connectTracesCheckBox.Name = "connectTracesCheckBox";
            this.connectTracesCheckBox.Size = new System.Drawing.Size(102, 17);
            this.connectTracesCheckBox.TabIndex = 5;
            this.connectTracesCheckBox.Text = "Connect Traces";
            this.connectTracesCheckBox.UseVisualStyleBackColor = true;
            this.connectTracesCheckBox.CheckedChanged += new System.EventHandler(this.connectTracesCheckBox_CheckedChanged);
            // 
            // traceShutdownDurationScToolStripLabel
            // 
            this.traceShutdownDurationScToolStripLabel.Name = "traceShutdownDurationScToolStripLabel";
            this.traceShutdownDurationScToolStripLabel.Size = new System.Drawing.Size(18, 22);
            this.traceShutdownDurationScToolStripLabel.Text = "sc";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // ProcessForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 313);
            this.Controls.Add(this.connectTracesCheckBox);
            this.Controls.Add(this.stopProcessButton);
            this.Controls.Add(this.startProcessButton);
            this.Controls.Add(this.mainTabControl);
            this.Controls.Add(this.debugButton);
            this.Name = "ProcessForm";
            this.Text = "ProcessForm";
            this.Load += new System.EventHandler(this.ProcessForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProcessForm_FormClosing);
            this.mainTabControl.ResumeLayout(false);
            this.logTabPage.ResumeLayout(false);
            this.logTabPage.PerformLayout();
            this.outputToolStrip.ResumeLayout(false);
            this.outputToolStrip.PerformLayout();
            this.outputSplitContainer.Panel1.ResumeLayout(false);
            this.outputSplitContainer.Panel2.ResumeLayout(false);
            this.outputSplitContainer.ResumeLayout(false);
            this.setupInfoTabPage.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PropertyGrid setupPropertyGrid;
        private System.Windows.Forms.Button debugButton;
        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage setupInfoTabPage;
        private System.Windows.Forms.TabPage logTabPage;
        private System.Windows.Forms.RichTextBox logRichTextBox;
        private System.Windows.Forms.Button startProcessButton;
        private System.Windows.Forms.Button stopProcessButton;
        private System.Windows.Forms.CheckBox connectTracesCheckBox;
        private System.Windows.Forms.ToolStrip outputToolStrip;
        private System.Windows.Forms.SplitContainer outputSplitContainer;
        private System.Windows.Forms.ListView outputListView;
        private System.Windows.Forms.ColumnHeader descriptionColumnHeader;
        private System.Windows.Forms.ToolStripTextBox regexToolStripTextBox;
        private System.Windows.Forms.ToolStripButton applyRegexToolStripButton;
        private System.Windows.Forms.ToolStripLabel regexToolStripLabel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton clearLogToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel traceShutdownToolStripLabel;
        private System.Windows.Forms.ToolStripTextBox traceShutdownToolStripTextBox;
        private System.Windows.Forms.ToolStripLabel traceShutdownDurationScToolStripLabel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}