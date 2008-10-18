namespace Tools.UI.Windows.Descriptors
{
    partial class SymmetricEncryptionSettingsControl
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
            this.algorithmNameComboBox = new System.Windows.Forms.ComboBox();
            this.algorithmNameLabel = new System.Windows.Forms.Label();
            this.cipherModeComboBox = new System.Windows.Forms.ComboBox();
            this.cipherModeLabel = new System.Windows.Forms.Label();
            this.paddingLabel = new System.Windows.Forms.Label();
            this.paddingTypeComboBox = new System.Windows.Forms.ComboBox();
            this.keySizeComboBox = new System.Windows.Forms.ComboBox();
            this.keySizeLabel = new System.Windows.Forms.Label();
            this.blockSizeComboBox = new System.Windows.Forms.ComboBox();
            this.blockSizesLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // algorithmNameComboBox
            // 
            this.algorithmNameComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.algorithmNameComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.algorithmNameComboBox.FormattingEnabled = true;
            this.algorithmNameComboBox.Location = new System.Drawing.Point(59, 3);
            this.algorithmNameComboBox.Name = "algorithmNameComboBox";
            this.algorithmNameComboBox.Size = new System.Drawing.Size(222, 21);
            this.algorithmNameComboBox.TabIndex = 1;
            this.algorithmNameComboBox.SelectedIndexChanged += new System.EventHandler(this.algorithmNameComboBox_SelectedIndexChanged);
            // 
            // algorithmNameLabel
            // 
            this.algorithmNameLabel.AutoSize = true;
            this.algorithmNameLabel.Location = new System.Drawing.Point(3, 6);
            this.algorithmNameLabel.Name = "algorithmNameLabel";
            this.algorithmNameLabel.Size = new System.Drawing.Size(50, 13);
            this.algorithmNameLabel.TabIndex = 3;
            this.algorithmNameLabel.Text = "Algorithm";
            // 
            // cipherModeComboBox
            // 
            this.cipherModeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cipherModeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cipherModeComboBox.FormattingEnabled = true;
            this.cipherModeComboBox.Location = new System.Drawing.Point(199, 30);
            this.cipherModeComboBox.Name = "cipherModeComboBox";
            this.cipherModeComboBox.Size = new System.Drawing.Size(82, 21);
            this.cipherModeComboBox.TabIndex = 5;
            // 
            // cipherModeLabel
            // 
            this.cipherModeLabel.AutoSize = true;
            this.cipherModeLabel.Location = new System.Drawing.Point(159, 33);
            this.cipherModeLabel.Name = "cipherModeLabel";
            this.cipherModeLabel.Size = new System.Drawing.Size(34, 13);
            this.cipherModeLabel.TabIndex = 6;
            this.cipherModeLabel.Text = "Mode";
            // 
            // paddingLabel
            // 
            this.paddingLabel.AutoSize = true;
            this.paddingLabel.Location = new System.Drawing.Point(5, 33);
            this.paddingLabel.Name = "paddingLabel";
            this.paddingLabel.Size = new System.Drawing.Size(46, 13);
            this.paddingLabel.TabIndex = 8;
            this.paddingLabel.Text = "Padding";
            // 
            // paddingTypeComboBox
            // 
            this.paddingTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.paddingTypeComboBox.FormattingEnabled = true;
            this.paddingTypeComboBox.Location = new System.Drawing.Point(59, 30);
            this.paddingTypeComboBox.Name = "paddingTypeComboBox";
            this.paddingTypeComboBox.Size = new System.Drawing.Size(72, 21);
            this.paddingTypeComboBox.TabIndex = 7;
            // 
            // keySizeComboBox
            // 
            this.keySizeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.keySizeComboBox.FormattingEnabled = true;
            this.keySizeComboBox.Location = new System.Drawing.Point(59, 57);
            this.keySizeComboBox.Name = "keySizeComboBox";
            this.keySizeComboBox.Size = new System.Drawing.Size(72, 21);
            this.keySizeComboBox.TabIndex = 5;
            // 
            // keySizeLabel
            // 
            this.keySizeLabel.AutoSize = true;
            this.keySizeLabel.Location = new System.Drawing.Point(6, 60);
            this.keySizeLabel.Name = "keySizeLabel";
            this.keySizeLabel.Size = new System.Drawing.Size(48, 13);
            this.keySizeLabel.TabIndex = 6;
            this.keySizeLabel.Text = "Key Size";
            // 
            // blockSizeComboBox
            // 
            this.blockSizeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.blockSizeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.blockSizeComboBox.FormattingEnabled = true;
            this.blockSizeComboBox.Location = new System.Drawing.Point(199, 57);
            this.blockSizeComboBox.Name = "blockSizeComboBox";
            this.blockSizeComboBox.Size = new System.Drawing.Size(82, 21);
            this.blockSizeComboBox.TabIndex = 5;
            // 
            // blockSizesLabel
            // 
            this.blockSizesLabel.AutoSize = true;
            this.blockSizesLabel.Location = new System.Drawing.Point(136, 63);
            this.blockSizesLabel.Name = "blockSizesLabel";
            this.blockSizesLabel.Size = new System.Drawing.Size(57, 13);
            this.blockSizesLabel.TabIndex = 6;
            this.blockSizesLabel.Text = "Block Size";
            // 
            // SymmetricEncryptionSettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.paddingLabel);
            this.Controls.Add(this.paddingTypeComboBox);
            this.Controls.Add(this.blockSizesLabel);
            this.Controls.Add(this.keySizeLabel);
            this.Controls.Add(this.cipherModeLabel);
            this.Controls.Add(this.blockSizeComboBox);
            this.Controls.Add(this.keySizeComboBox);
            this.Controls.Add(this.cipherModeComboBox);
            this.Controls.Add(this.algorithmNameLabel);
            this.Controls.Add(this.algorithmNameComboBox);
            this.Name = "SymmetricEncryptionSettingsControl";
            this.Size = new System.Drawing.Size(284, 83);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox algorithmNameComboBox;
        private System.Windows.Forms.Label algorithmNameLabel;
        private System.Windows.Forms.ComboBox cipherModeComboBox;
        private System.Windows.Forms.Label cipherModeLabel;
        private System.Windows.Forms.Label paddingLabel;
        private System.Windows.Forms.ComboBox paddingTypeComboBox;
        private System.Windows.Forms.ComboBox keySizeComboBox;
        private System.Windows.Forms.Label keySizeLabel;
        private System.Windows.Forms.ComboBox blockSizeComboBox;
        private System.Windows.Forms.Label blockSizesLabel;
    }
}
