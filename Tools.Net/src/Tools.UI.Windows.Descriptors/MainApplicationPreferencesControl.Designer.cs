namespace Tools.UI.Windows.Descriptors
{
    partial class MainApplicationPreferencesControl
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
            this.pathTextBox = new System.Windows.Forms.TextBox();
            this.pathGroupBox = new System.Windows.Forms.GroupBox();
            this.pathLabel = new System.Windows.Forms.Label();
            this.pathGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // pathTextBox
            // 
            this.pathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pathTextBox.Location = new System.Drawing.Point(6, 19);
            this.pathTextBox.Name = "pathTextBox";
            this.pathTextBox.Size = new System.Drawing.Size(228, 20);
            this.pathTextBox.TabIndex = 0;
            // 
            // pathGroupBox
            // 
            this.pathGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pathGroupBox.Controls.Add(this.pathLabel);
            this.pathGroupBox.Controls.Add(this.pathTextBox);
            this.pathGroupBox.Location = new System.Drawing.Point(3, 3);
            this.pathGroupBox.Name = "pathGroupBox";
            this.pathGroupBox.Size = new System.Drawing.Size(240, 99);
            this.pathGroupBox.TabIndex = 1;
            this.pathGroupBox.TabStop = false;
            this.pathGroupBox.Text = "Settings Path";
            // 
            // pathLabel
            // 
            this.pathLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pathLabel.Location = new System.Drawing.Point(6, 42);
            this.pathLabel.Name = "pathLabel";
            this.pathLabel.Size = new System.Drawing.Size(228, 33);
            this.pathLabel.TabIndex = 2;
            this.pathLabel.Text = "Application preferences will be read from this location.";
            // 
            // MainApplicationPreferencesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pathGroupBox);
            this.Name = "MainApplicationPreferencesControl";
            this.Size = new System.Drawing.Size(246, 238);
            this.pathGroupBox.ResumeLayout(false);
            this.pathGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox pathTextBox;
        private System.Windows.Forms.GroupBox pathGroupBox;
        private System.Windows.Forms.Label pathLabel;
    }
}
