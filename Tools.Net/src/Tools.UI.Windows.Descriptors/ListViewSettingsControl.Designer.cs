namespace Tools.UI.Windows.Descriptors
{
    partial class ListViewSettingsControl
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
            this.showNameDescriptionCheckBox = new System.Windows.Forms.CheckBox();
            this.protectionSettingsControl1 = new Tools.UI.Windows.Descriptors.SymmetricEncryptionSettingsControl();
            this.SuspendLayout();
            // 
            // showNameDescriptionCheckBox
            // 
            this.showNameDescriptionCheckBox.AutoSize = true;
            this.showNameDescriptionCheckBox.Location = new System.Drawing.Point(3, 3);
            this.showNameDescriptionCheckBox.Name = "showNameDescriptionCheckBox";
            this.showNameDescriptionCheckBox.Size = new System.Drawing.Size(172, 17);
            this.showNameDescriptionCheckBox.TabIndex = 0;
            this.showNameDescriptionCheckBox.Text = "Show list name and description";
            // 
            // protectionSettingsControl1
            // 
            this.protectionSettingsControl1.Location = new System.Drawing.Point(0, 26);
            this.protectionSettingsControl1.Name = "protectionSettingsControl1";
            this.protectionSettingsControl1.Size = new System.Drawing.Size(254, 174);
            this.protectionSettingsControl1.TabIndex = 1;
            // 
            // ListViewSettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.protectionSettingsControl1);
            this.Controls.Add(this.showNameDescriptionCheckBox);
            this.Name = "ListViewSettingsControl";
            this.Size = new System.Drawing.Size(257, 213);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox showNameDescriptionCheckBox;
        private SymmetricEncryptionSettingsControl protectionSettingsControl1;
    }
}
