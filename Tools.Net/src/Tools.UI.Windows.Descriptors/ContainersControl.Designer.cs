using System;

namespace Tools.UI.Windows.Descriptors
{
    public partial class ContainersControl<SettingsType, ContainedType, T>
        where ContainedType : new()
        where SettingsType : IListSettings, new() 
        where T : ICloneable, new()
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
            this.components = new System.ComponentModel.Container();
            this.containersTabControl = new System.Windows.Forms.TabControl();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.addNewListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeTheListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // containersTabControl
            // 
            this.containersTabControl.ContextMenuStrip = this.contextMenuStrip;
            this.containersTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.containersTabControl.Location = new System.Drawing.Point(0, 0);
            this.containersTabControl.Name = "containersTabControl";
            this.containersTabControl.SelectedIndex = 0;
            this.containersTabControl.Size = new System.Drawing.Size(259, 202);
            this.containersTabControl.TabIndex = 0;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.addNewListToolStripMenuItem,
            this.removeTheListToolStripMenuItem,
            this.toolStripSeparator2,
            this.propertiesToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(165, 104);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(161, 6);
            // 
            // addNewListToolStripMenuItem
            // 
            this.addNewListToolStripMenuItem.Name = "addNewListToolStripMenuItem";
            this.addNewListToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.addNewListToolStripMenuItem.Text = "Add New List";
            this.addNewListToolStripMenuItem.Click += new System.EventHandler(this.addNewListToolStripMenuItem_Click);
            // 
            // removeTheListToolStripMenuItem
            // 
            this.removeTheListToolStripMenuItem.Name = "removeTheListToolStripMenuItem";
            this.removeTheListToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.removeTheListToolStripMenuItem.Text = "Remove The List";
            this.removeTheListToolStripMenuItem.Click += new System.EventHandler(this.removeTheListToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(161, 6);
            // 
            // propertiesToolStripMenuItem
            // 
            this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.propertiesToolStripMenuItem.Text = "Properties";
            this.propertiesToolStripMenuItem.Click += new System.EventHandler(this.propertiesToolStripMenuItem_Click);
            // 
            // ContainersControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.containersTabControl);
            this.Name = "ContainersControl";
            this.Size = new System.Drawing.Size(259, 202);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl containersTabControl;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem addNewListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeTheListToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;
    }
}
