using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Tools.Core;
using Tools.Core.Configuration;


namespace Tools.Tracing.UI
{
	public class HashActivationArgumentControl : Tools.Tracing.UI.DescriptorControl
	{
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox hashNameTextBox;
		private System.ComponentModel.IContainer components = null;

		public HashActivationArgumentControl()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
		}
		public override Descriptor GetValue()
		{
			
			Descriptor d = base.GetValue();

			if (this.hashNameTextBox.Text == String.Empty)
			{
				this.errorProvider1.SetError(hashNameTextBox, "Value can't be empty!");
				return null;
			}
			this.errorProvider1.SetError(hashNameTextBox,String.Empty);
			if (d!=null)
			{
				return new HashActivationArgument(d.Name, d.Description, hashNameTextBox.Text);
			}
			return null;
		}
		public override void LoadValue(Descriptor val)
		{
			base.LoadValue(val);

			if (! (val is HashActivationArgument))
			{
				throw new ArgumentException("Wrong argument type of " + val.GetType().FullName 
					+ "when expected HashActivationArgument", "val");
			}

			this.hashNameTextBox.Text = (val as HashActivationArgument).HashEntryName;

		}
		public override void Clear()
		{
			base.Clear();

			this.hashNameTextBox.Text  = String.Empty;
			this.errorProvider1.SetError(hashNameTextBox,String.Empty);
		}
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label3 = new System.Windows.Forms.Label();
			this.hashNameTextBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 152);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(64, 23);
			this.label3.TabIndex = 7;
			this.label3.Text = "HashName";
			// 
			// hashNameTextBox
			// 
			this.hashNameTextBox.Location = new System.Drawing.Point(72, 152);
			this.hashNameTextBox.Name = "hashNameTextBox";
			this.hashNameTextBox.Size = new System.Drawing.Size(184, 20);
			this.hashNameTextBox.TabIndex = 8;
			this.hashNameTextBox.Text = "";
			// 
			// HashActivationArgumentControl
			// 
			this.Controls.Add(this.label3);
			this.Controls.Add(this.hashNameTextBox);
			this.Name = "HashActivationArgumentControl";
			this.Size = new System.Drawing.Size(256, 176);
			this.Controls.SetChildIndex(this.hashNameTextBox, 0);
			this.Controls.SetChildIndex(this.label3, 0);
			this.ResumeLayout(false);

		}
		#endregion
	}
}

