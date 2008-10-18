namespace Tools.UI.Windows.Descriptors
{
    partial class PasswordEntryControl
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
            this.maskedFirstEntryTextBox = new System.Windows.Forms.MaskedTextBox();
            this.maskedConfirmPswTextBox = new System.Windows.Forms.MaskedTextBox();
            this.pswLabel = new System.Windows.Forms.Label();
            this.confirmPswLabel = new System.Windows.Forms.Label();
            this.previewIVKeyControl1 = new Tools.UI.Windows.Descriptors.PreviewIVKeyControl();
            this.SuspendLayout();
            // 
            // maskedFirstEntryTextBox
            // 
            this.maskedFirstEntryTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.maskedFirstEntryTextBox.Location = new System.Drawing.Point(100, 3);
            this.maskedFirstEntryTextBox.Name = "maskedFirstEntryTextBox";
            this.maskedFirstEntryTextBox.Size = new System.Drawing.Size(127, 20);
            this.maskedFirstEntryTextBox.TabIndex = 0;
            // 
            // maskedConfirmPswTextBox
            // 
            this.maskedConfirmPswTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.maskedConfirmPswTextBox.Location = new System.Drawing.Point(100, 29);
            this.maskedConfirmPswTextBox.Name = "maskedConfirmPswTextBox";
            this.maskedConfirmPswTextBox.Size = new System.Drawing.Size(127, 20);
            this.maskedConfirmPswTextBox.TabIndex = 0;
            // 
            // pswLabel
            // 
            this.pswLabel.AutoSize = true;
            this.pswLabel.Location = new System.Drawing.Point(3, 6);
            this.pswLabel.Name = "pswLabel";
            this.pswLabel.Size = new System.Drawing.Size(81, 13);
            this.pswLabel.TabIndex = 1;
            this.pswLabel.Text = "Enter Password";
            // 
            // confirmPswLabel
            // 
            this.confirmPswLabel.AutoSize = true;
            this.confirmPswLabel.Location = new System.Drawing.Point(3, 32);
            this.confirmPswLabel.Name = "confirmPswLabel";
            this.confirmPswLabel.Size = new System.Drawing.Size(91, 13);
            this.confirmPswLabel.TabIndex = 1;
            this.confirmPswLabel.Text = "Confirm Password";
            // 
            // previewIVKeyControl1
            // 
            this.previewIVKeyControl1.Location = new System.Drawing.Point(0, 52);
            this.previewIVKeyControl1.Name = "previewIVKeyControl1";
            this.previewIVKeyControl1.Size = new System.Drawing.Size(230, 57);
            this.previewIVKeyControl1.TabIndex = 2;
            // 
            // PasswordEntryControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.previewIVKeyControl1);
            this.Controls.Add(this.confirmPswLabel);
            this.Controls.Add(this.pswLabel);
            this.Controls.Add(this.maskedConfirmPswTextBox);
            this.Controls.Add(this.maskedFirstEntryTextBox);
            this.Name = "PasswordEntryControl";
            this.Size = new System.Drawing.Size(230, 112);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MaskedTextBox maskedFirstEntryTextBox;
        private System.Windows.Forms.MaskedTextBox maskedConfirmPswTextBox;
        private System.Windows.Forms.Label pswLabel;
        private System.Windows.Forms.Label confirmPswLabel;
        private PreviewIVKeyControl previewIVKeyControl1;
    }
}
