using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace Tools.Tracing.UI
{
	/// <summary>
	/// Summary description for PerformanceHandlerConfigurationControl.
	/// </summary>
	public class PerformanceHandlerConfigurationControl : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.PropertyGrid propertyGrid1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button saveButton;
		private System.Windows.Forms.Button loadButton;
		private System.Windows.Forms.TreeView treeView1;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public PerformanceHandlerConfigurationControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call

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
			this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.saveButton = new System.Windows.Forms.Button();
			this.loadButton = new System.Windows.Forms.Button();
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.SuspendLayout();
			// 
			// propertyGrid1
			// 
			this.propertyGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.propertyGrid1.CommandsVisibleIfAvailable = true;
			this.propertyGrid1.LargeButtons = false;
			this.propertyGrid1.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.propertyGrid1.Location = new System.Drawing.Point(312, 0);
			this.propertyGrid1.Name = "propertyGrid1";
			this.propertyGrid1.Size = new System.Drawing.Size(464, 360);
			this.propertyGrid1.TabIndex = 0;
			this.propertyGrid1.Text = "propertyGrid1";
			this.propertyGrid1.ViewBackColor = System.Drawing.SystemColors.Window;
			this.propertyGrid1.ViewForeColor = System.Drawing.SystemColors.WindowText;
			// 
			// textBox1
			// 
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox1.Location = new System.Drawing.Point(0, 360);
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new System.Drawing.Size(656, 20);
			this.textBox1.TabIndex = 5;
			this.textBox1.Text = "";
			// 
			// saveButton
			// 
			this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.saveButton.Location = new System.Drawing.Point(720, 360);
			this.saveButton.Name = "saveButton";
			this.saveButton.Size = new System.Drawing.Size(56, 23);
			this.saveButton.TabIndex = 4;
			this.saveButton.Text = "Save";
			// 
			// loadButton
			// 
			this.loadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.loadButton.Location = new System.Drawing.Point(656, 360);
			this.loadButton.Name = "loadButton";
			this.loadButton.Size = new System.Drawing.Size(64, 23);
			this.loadButton.TabIndex = 3;
			this.loadButton.Text = "Load";
			// 
			// treeView1
			// 
			this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left)));
			this.treeView1.ImageIndex = -1;
			this.treeView1.Location = new System.Drawing.Point(0, 0);
			this.treeView1.Name = "treeView1";
			this.treeView1.SelectedImageIndex = -1;
			this.treeView1.Size = new System.Drawing.Size(312, 360);
			this.treeView1.TabIndex = 6;
			// 
			// PerformanceHandlerConfigurationControl
			// 
			this.Controls.Add(this.treeView1);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.saveButton);
			this.Controls.Add(this.loadButton);
			this.Controls.Add(this.propertyGrid1);
			this.Name = "PerformanceHandlerConfigurationControl";
			this.Size = new System.Drawing.Size(776, 384);
			this.ResumeLayout(false);

		}
		#endregion
	}
}
