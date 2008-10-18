namespace Tools.UI.Windows.Descriptors
{
    partial class SettingsEditorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsEditorForm));
            this.settingsTabControl = new System.Windows.Forms.TabControl();
            this.mainTabPage = new System.Windows.Forms.TabPage();
            this.mainApplicationPreferencesControl = new Tools.UI.Windows.Descriptors.MainApplicationPreferencesControl();
            this.listSettingsTabPage = new System.Windows.Forms.TabPage();
            this.listViewSettingsControl = new Tools.UI.Windows.Descriptors.ListViewSettingsControl();
            this.isolatedStorageSettingsTabPage = new System.Windows.Forms.TabPage();
            this.isolatedStorageSettingsControl = new Tools.UI.Windows.Descriptors.IsolatedStorageSettingsControl();
            this.protectionTabPage = new System.Windows.Forms.TabPage();
            this.okButton = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.settingsTabControl.SuspendLayout();
            this.mainTabPage.SuspendLayout();
            this.listSettingsTabPage.SuspendLayout();
            this.isolatedStorageSettingsTabPage.SuspendLayout();
            this.protectionTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // settingsTabControl
            // 
            this.settingsTabControl.Controls.Add(this.mainTabPage);
            this.settingsTabControl.Controls.Add(this.listSettingsTabPage);
            this.settingsTabControl.Controls.Add(this.isolatedStorageSettingsTabPage);
            this.settingsTabControl.Controls.Add(this.protectionTabPage);
            this.settingsTabControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.settingsTabControl.Location = new System.Drawing.Point(0, 0);
            this.settingsTabControl.Name = "settingsTabControl";
            this.settingsTabControl.SelectedIndex = 0;
            this.settingsTabControl.Size = new System.Drawing.Size(292, 254);
            this.settingsTabControl.TabIndex = 0;
            // 
            // mainTabPage
            // 
            this.mainTabPage.Controls.Add(this.mainApplicationPreferencesControl);
            this.mainTabPage.Location = new System.Drawing.Point(4, 22);
            this.mainTabPage.Name = "mainTabPage";
            this.mainTabPage.Size = new System.Drawing.Size(284, 228);
            this.mainTabPage.TabIndex = 2;
            this.mainTabPage.Text = "Main";
            this.mainTabPage.UseVisualStyleBackColor = true;
            // 
            // mainApplicationPreferencesControl
            // 
            this.mainApplicationPreferencesControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainApplicationPreferencesControl.Location = new System.Drawing.Point(0, 0);
            this.mainApplicationPreferencesControl.Name = "mainApplicationPreferencesControl";
            this.mainApplicationPreferencesControl.Path = "";
            this.mainApplicationPreferencesControl.Size = new System.Drawing.Size(284, 228);
            this.mainApplicationPreferencesControl.TabIndex = 0;
            // 
            // listSettingsTabPage
            // 
            this.listSettingsTabPage.Controls.Add(this.listViewSettingsControl);
            this.listSettingsTabPage.Location = new System.Drawing.Point(4, 22);
            this.listSettingsTabPage.Name = "listSettingsTabPage";
            this.listSettingsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.listSettingsTabPage.Size = new System.Drawing.Size(284, 228);
            this.listSettingsTabPage.TabIndex = 0;
            this.listSettingsTabPage.Text = "List";
            this.listSettingsTabPage.UseVisualStyleBackColor = true;
            // 
            // listViewSettingsControl
            // 
            this.listViewSettingsControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewSettingsControl.Location = new System.Drawing.Point(3, 3);
            this.listViewSettingsControl.Name = "listViewSettingsControl";
            this.listViewSettingsControl.Size = new System.Drawing.Size(278, 222);
            this.listViewSettingsControl.TabIndex = 0;
            this.listViewSettingsControl.Load += new System.EventHandler(this.listViewSettingsControl_Load);
            // 
            // isolatedStorageSettingsTabPage
            // 
            this.isolatedStorageSettingsTabPage.Controls.Add(this.isolatedStorageSettingsControl);
            this.isolatedStorageSettingsTabPage.Location = new System.Drawing.Point(4, 22);
            this.isolatedStorageSettingsTabPage.Name = "isolatedStorageSettingsTabPage";
            this.isolatedStorageSettingsTabPage.Size = new System.Drawing.Size(284, 228);
            this.isolatedStorageSettingsTabPage.TabIndex = 1;
            this.isolatedStorageSettingsTabPage.Text = "Isolated Storage";
            this.isolatedStorageSettingsTabPage.UseVisualStyleBackColor = true;
            // 
            // isolatedStorageSettingsControl
            // 
            this.isolatedStorageSettingsControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.isolatedStorageSettingsControl.Location = new System.Drawing.Point(0, 0);
            this.isolatedStorageSettingsControl.Name = "isolatedStorageSettingsControl";
            this.isolatedStorageSettingsControl.Size = new System.Drawing.Size(284, 228);
            this.isolatedStorageSettingsControl.TabIndex = 0;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(29, 256);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "OK";
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(110, 256);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "button2";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(191, 256);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "button3";
            // 
            // SettingsEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 285);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.settingsTabControl);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsEditorForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Edit Settings";
            this.settingsTabControl.ResumeLayout(false);
            this.mainTabPage.ResumeLayout(false);
            this.listSettingsTabPage.ResumeLayout(false);
            this.isolatedStorageSettingsTabPage.ResumeLayout(false);
            this.protectionTabPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl settingsTabControl;
        private System.Windows.Forms.TabPage listSettingsTabPage;
        private System.Windows.Forms.TabPage isolatedStorageSettingsTabPage;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TabPage mainTabPage;
        private MainApplicationPreferencesControl mainApplicationPreferencesControl;
        private ListViewSettingsControl listViewSettingsControl;
        private IsolatedStorageSettingsControl isolatedStorageSettingsControl;
        private System.Windows.Forms.TabPage protectionTabPage;
    }
}