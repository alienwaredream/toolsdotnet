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
            this.setupPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.debugButton = new System.Windows.Forms.Button();
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.logTabPage = new System.Windows.Forms.TabPage();
            this.logRichTextBox = new System.Windows.Forms.RichTextBox();
            this.setupInfoTabPage = new System.Windows.Forms.TabPage();
            this.startProcessButton = new System.Windows.Forms.Button();
            this.stopProcessButton = new System.Windows.Forms.Button();
            this.connectTracesCheckBox = new System.Windows.Forms.CheckBox();
            this.mainTabControl.SuspendLayout();
            this.logTabPage.SuspendLayout();
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
            this.debugButton.Location = new System.Drawing.Point(580, 289);
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
            this.mainTabControl.Size = new System.Drawing.Size(725, 289);
            this.mainTabControl.TabIndex = 2;
            // 
            // logTabPage
            // 
            this.logTabPage.Controls.Add(this.logRichTextBox);
            this.logTabPage.Location = new System.Drawing.Point(4, 22);
            this.logTabPage.Name = "logTabPage";
            this.logTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.logTabPage.Size = new System.Drawing.Size(717, 263);
            this.logTabPage.TabIndex = 1;
            this.logTabPage.Text = "Output";
            this.logTabPage.UseVisualStyleBackColor = true;
            // 
            // logRichTextBox
            // 
            this.logRichTextBox.BackColor = System.Drawing.SystemColors.HotTrack;
            this.logRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logRichTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logRichTextBox.ForeColor = System.Drawing.Color.White;
            this.logRichTextBox.Location = new System.Drawing.Point(3, 3);
            this.logRichTextBox.Name = "logRichTextBox";
            this.logRichTextBox.ReadOnly = true;
            this.logRichTextBox.Size = new System.Drawing.Size(711, 257);
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
            // ProcessForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 313);
            this.Controls.Add(this.connectTracesCheckBox);
            this.Controls.Add(this.stopProcessButton);
            this.Controls.Add(this.startProcessButton);
            this.Controls.Add(this.mainTabControl);
            this.Controls.Add(this.debugButton);
            this.Name = "ProcessForm";
            this.Text = "ProcessForm";
            this.mainTabControl.ResumeLayout(false);
            this.logTabPage.ResumeLayout(false);
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
    }
}