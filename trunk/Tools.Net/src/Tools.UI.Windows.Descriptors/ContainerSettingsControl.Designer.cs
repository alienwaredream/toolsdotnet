namespace Tools.UI.Windows.Descriptors
{
    partial class ContainerSettingsControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContainerSettingsControl));
            this.contentsTabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.contentsTabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contentsTabControl
            // 
            this.contentsTabControl.Controls.Add(this.tabPage1);
            this.contentsTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentsTabControl.Location = new System.Drawing.Point(0, 0);
            this.contentsTabControl.Name = "contentsTabControl";
            this.contentsTabControl.SelectedIndex = 0;
            this.contentsTabControl.Size = new System.Drawing.Size(292, 283);
            this.contentsTabControl.TabIndex = 0;

            // 
            // ContainerSettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 283);
            this.Controls.Add(this.contentsTabControl);
            this.Name = "ContainerSettingsControl";
            this.Text = "ContainerSettingsControl";
            this.contentsTabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl contentsTabControl;
        private System.Windows.Forms.TabPage tabPage1;
    }
}