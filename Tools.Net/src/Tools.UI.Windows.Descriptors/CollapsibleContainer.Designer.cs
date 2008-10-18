namespace Tools.UI.Windows.Descriptors
{
    partial class CollapsibleContainer
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
            this.containerPanel = new System.Windows.Forms.Panel();
            this.collapseToolBar = new Tools.UI.Windows.Descriptors.CollapseToolBar();
            this.SuspendLayout();
            // 
            // containerPanel
            // 
            this.containerPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.containerPanel.Location = new System.Drawing.Point(0, 21);
            this.containerPanel.Name = "containerPanel";
            this.containerPanel.Size = new System.Drawing.Size(332, 140);
            this.containerPanel.TabIndex = 1;
            // 
            // collapseToolBar
            // 
            this.collapseToolBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.collapseToolBar.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.collapseToolBar.Location = new System.Drawing.Point(0, 0);
            this.collapseToolBar.MaximumSize = new System.Drawing.Size(3000, 20);
            this.collapseToolBar.MinimumSize = new System.Drawing.Size(41, 20);
            this.collapseToolBar.Name = "collapseToolBar";
            this.collapseToolBar.Size = new System.Drawing.Size(333, 20);
            this.collapseToolBar.TabIndex = 0;
            this.collapseToolBar.Title = null;
            this.collapseToolBar.Load += new System.EventHandler(this.collapseToolBar_Load);
            // 
            // CollapsibleContainer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.containerPanel);
            this.Controls.Add(this.collapseToolBar);
            this.Name = "CollapsibleContainer";
            this.Size = new System.Drawing.Size(333, 160);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel containerPanel;
        private CollapseToolBar collapseToolBar;
    }
}
