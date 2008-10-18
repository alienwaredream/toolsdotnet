using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Tools.Core;
using Tools.Core.Configuration;


namespace Tools.Tracing.UI
{
	public class TextActivationArgumentControl : Tools.Tracing.UI.DescriptorControl
	{
		private System.Windows.Forms.TextBox valueTextBox;
		private System.Windows.Forms.Label label3;
		private System.ComponentModel.IContainer components = null;

		public TextActivationArgumentControl()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
		}
		public override Descriptor GetValue()
		{
			
			Descriptor d = base.GetValue();

			if (this.valueTextBox.Text == String.Empty)
			{
				this.errorProvider1.SetError(valueTextBox, "Value can't be empty!");
				return null;
			}
			this.errorProvider1.SetError(valueTextBox,String.Empty);
			if (d!=null)
			{
				return new TextActivationArgument(d.Name, d.Description, valueTextBox.Text);
			}
			return null;
		}
		public override void LoadValue(Descriptor val)
		{
			base.LoadValue(val);

			if (! (val is TextActivationArgument))
			{
				throw new ArgumentException("Wrong argument type of " + val.GetType().FullName 
					+ "when expected TextActivationArgument", "val");
			}

			this.valueTextBox.Text = (val as TextActivationArgument).Value;

		}
		public override void Clear()
		{
			base.Clear();

			this.valueTextBox.Text  = String.Empty;
			this.errorProvider1.SetError(valueTextBox,String.Empty);
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
			this.valueTextBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// valueTextBox
			// 
			this.valueTextBox.Location = new System.Drawing.Point(72, 152);
			this.valueTextBox.Name = "valueTextBox";
			this.valueTextBox.Size = new System.Drawing.Size(184, 20);
			this.valueTextBox.TabIndex = 4;
			this.valueTextBox.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(0, 152);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(48, 23);
			this.label3.TabIndex = 5;
			this.label3.Text = "Value";
			// 
			// TextActivationArgumentControl
			// 
			this.Controls.Add(this.label3);
			this.Controls.Add(this.valueTextBox);
			this.Name = "TextActivationArgumentControl";
			this.Size = new System.Drawing.Size(256, 176);
			this.Controls.SetChildIndex(this.valueTextBox, 0);
			this.Controls.SetChildIndex(this.label3, 0);
			this.ResumeLayout(false);

		}
		#endregion
	}
}

