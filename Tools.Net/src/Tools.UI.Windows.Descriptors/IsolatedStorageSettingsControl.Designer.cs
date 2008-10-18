namespace Tools.UI.Windows.Descriptors
{
    partial class IsolatedStorageSettingsControl
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
            this.useIsolatedStorageCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // useIsolatedStorageCheckBox
            // 
            this.useIsolatedStorageCheckBox.AutoSize = true;
            this.useIsolatedStorageCheckBox.Location = new System.Drawing.Point(3, 3);
            this.useIsolatedStorageCheckBox.Name = "useIsolatedStorageCheckBox";
            this.useIsolatedStorageCheckBox.Size = new System.Drawing.Size(157, 17);
            this.useIsolatedStorageCheckBox.TabIndex = 0;
            this.useIsolatedStorageCheckBox.Text = "Use isolated storage for data";
            // 
            // IsolatedStorageSettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.useIsolatedStorageCheckBox);
            this.Name = "IsolatedStorageSettingsControl";
            this.Size = new System.Drawing.Size(280, 241);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox useIsolatedStorageCheckBox;
    }
}
