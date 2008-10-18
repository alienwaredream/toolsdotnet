namespace Tools.UI.Windows.Descriptors
{
    partial class CollapseToolBar
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
            this.expandButton = new System.Windows.Forms.Button();
            this.collapseButton = new System.Windows.Forms.Button();
            this.titleLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // expandButton
            // 
            this.expandButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.expandButton.Image = Tools.UI.Windows.Descriptors.Properties.Resources.Expand_small;
            this.expandButton.Location = new System.Drawing.Point(271, 0);
            this.expandButton.Name = "expandButton";
            this.expandButton.Size = new System.Drawing.Size(19, 19);
            this.expandButton.TabIndex = 1;
            this.expandButton.Click += new System.EventHandler(this.expandButton_Click);
            // 
            // collapseButton
            // 
            this.collapseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.collapseButton.Image = Tools.UI.Windows.Descriptors.Properties.Resources.Collapse_small;
            this.collapseButton.Location = new System.Drawing.Point(251, 0);
            this.collapseButton.Name = "collapseButton";
            this.collapseButton.Size = new System.Drawing.Size(19, 19);
            this.collapseButton.TabIndex = 0;
            this.collapseButton.Click += new System.EventHandler(this.collapseButton_Click);
            // 
            // titleLabel
            // 
            this.titleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.titleLabel.Location = new System.Drawing.Point(3, 3);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(242, 15);
            this.titleLabel.TabIndex = 2;
            // 
            // CollapseToolBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.expandButton);
            this.Controls.Add(this.collapseButton);
            this.MaximumSize = new System.Drawing.Size(3000, 20);
            this.MinimumSize = new System.Drawing.Size(41, 20);
            this.Name = "CollapseToolBar";
            this.Size = new System.Drawing.Size(292, 20);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button expandButton;
        private System.Windows.Forms.Button collapseButton;
        private System.Windows.Forms.Label titleLabel;





    }
}
