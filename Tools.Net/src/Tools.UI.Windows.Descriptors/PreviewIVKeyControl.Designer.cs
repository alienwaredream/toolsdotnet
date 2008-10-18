namespace Tools.UI.Windows.Descriptors
{
    partial class PreviewIVKeyControl
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
            this.previewIVButton = new System.Windows.Forms.Button();
            this.previewKeyButton = new System.Windows.Forms.Button();
            this.previewIVMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.previewKeyMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.SuspendLayout();
            // 
            // previewIVButton
            // 
            this.previewIVButton.Location = new System.Drawing.Point(3, 32);
            this.previewIVButton.Name = "previewIVButton";
            this.previewIVButton.Size = new System.Drawing.Size(88, 23);
            this.previewIVButton.TabIndex = 5;
            this.previewIVButton.Text = "Preview IV";
            this.previewIVButton.UseVisualStyleBackColor = true;
            // 
            // previewKeyButton
            // 
            this.previewKeyButton.Location = new System.Drawing.Point(3, 3);
            this.previewKeyButton.Name = "previewKeyButton";
            this.previewKeyButton.Size = new System.Drawing.Size(88, 23);
            this.previewKeyButton.TabIndex = 6;
            this.previewKeyButton.Text = "Preview Key";
            this.previewKeyButton.UseVisualStyleBackColor = true;
            // 
            // previewIVMaskedTextBox
            // 
            this.previewIVMaskedTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.previewIVMaskedTextBox.Location = new System.Drawing.Point(97, 34);
            this.previewIVMaskedTextBox.Name = "previewIVMaskedTextBox";
            this.previewIVMaskedTextBox.Size = new System.Drawing.Size(141, 20);
            this.previewIVMaskedTextBox.TabIndex = 3;
            // 
            // previewKeyMaskedTextBox
            // 
            this.previewKeyMaskedTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.previewKeyMaskedTextBox.Location = new System.Drawing.Point(97, 5);
            this.previewKeyMaskedTextBox.Name = "previewKeyMaskedTextBox";
            this.previewKeyMaskedTextBox.Size = new System.Drawing.Size(141, 20);
            this.previewKeyMaskedTextBox.TabIndex = 4;
            // 
            // PreviewIVKeyControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.previewIVButton);
            this.Controls.Add(this.previewKeyButton);
            this.Controls.Add(this.previewIVMaskedTextBox);
            this.Controls.Add(this.previewKeyMaskedTextBox);
            this.Name = "PreviewIVKeyControl";
            this.Size = new System.Drawing.Size(242, 60);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button previewIVButton;
        private System.Windows.Forms.Button previewKeyButton;
        private System.Windows.Forms.MaskedTextBox previewIVMaskedTextBox;
        private System.Windows.Forms.MaskedTextBox previewKeyMaskedTextBox;
    }
}
