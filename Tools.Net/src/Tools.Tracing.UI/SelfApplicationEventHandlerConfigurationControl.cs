using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Tools.Tracing.Common;

namespace Tools.Tracing.UI
{
	/// <summary>
	/// Summary description for SelfApplicationEventHandlerConfigurationControl.
	/// </summary>
	public class SelfApplicationEventHandlerConfigurationControl : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.ContextMenu controlContextMenu;
		private System.Windows.Forms.MenuItem loadMenuItem;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.MenuItem updateMenuItem;
		private EventHandlerManagerConfigurationEditorControl configurationControl = null;

		public SelfApplicationEventHandlerConfigurationControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			
			configurationControl = new EventHandlerManagerConfigurationEditorControl();
			configurationControl.Dock = DockStyle.Fill;
			configurationControl.Enabled = false;
			this.Controls.Add(configurationControl);
			configurationControl.ContextMenu.GetContextMenu().MenuItems.Add
				(
				"-"
				);
			configurationControl.ContextMenu.GetContextMenu().MergeMenu
				(
				this.ContextMenu
				);

		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.controlContextMenu = new System.Windows.Forms.ContextMenu();
			this.loadMenuItem = new System.Windows.Forms.MenuItem();
			this.updateMenuItem = new System.Windows.Forms.MenuItem();
			// 
			// controlContextMenu
			// 
			this.controlContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																							   this.loadMenuItem,
																							   this.updateMenuItem});
			// 
			// loadMenuItem
			// 
			this.loadMenuItem.Index = 0;
			this.loadMenuItem.Text = "Load Self Configuration";
			this.loadMenuItem.Click += new System.EventHandler(this.loadMenuItem_Click);
			// 
			// updateMenuItem
			// 
			this.updateMenuItem.Index = 1;
			this.updateMenuItem.Text = "Update Self Configuration";
			this.updateMenuItem.Click += new System.EventHandler(this.updateMenuItem_Click);
			// 
			// SelfApplicationEventHandlerConfigurationControl
			// 
			this.ContextMenu = this.controlContextMenu;
			this.Name = "SelfApplicationEventHandlerConfigurationControl";
			this.Size = new System.Drawing.Size(264, 184);

		}
		#endregion

		private void loadMenuItem_Click(object sender, System.EventArgs e)
		{
			try
			{
				configurationControl.Configuration =
					TraceEventHandlerManager.Instance.GetConfiguration();
				configurationControl.Enabled = true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, ex.ToString());
			}
		}

		private void updateMenuItem_Click(object sender, System.EventArgs e)
		{
			try
			{
				TraceEventHandlerManager.Instance.LoadConfiguration
					(
					configurationControl.Configuration
					);
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, ex.ToString());
			}
		}

	}
}
