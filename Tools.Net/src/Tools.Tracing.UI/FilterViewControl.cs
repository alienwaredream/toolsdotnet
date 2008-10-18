using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace Tools.Tracing.UI
{
	/// <summary>
	/// Summary description for FilterViewControl.
	/// </summary>

	public class FilterViewControl : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader pathColumnHeader;
		private System.Windows.Forms.ColumnHeader expressionColumnHeader;
		private ApplicationEventFilter _filter = null;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ApplicationEventFilter Filter
		{
			get
			{
				return _filter;
			}
			set
			{
				if (_filter == value) return;
				_filter = value;
				_filter.Changed += new EventHandler(_filter_Changed);
				dataBind();
			}
		}

		public FilterViewControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// filter = new TraceEventFilter();

		}
		private void dataBind()
		{
			this.listView1.Items.Clear();
			foreach (FilterEntry fe in _filter.FilterEntries)
			{
				if (!fe.Enabled) continue;
				ListViewItem lvi = 
					new ListViewItem
					(
					new string[]
					{
						fe.Path,
						fe.Expression
					}
					);
				this.listView1.Items.Add(lvi);
			}
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
			this.listView1 = new System.Windows.Forms.ListView();
			this.pathColumnHeader = new System.Windows.Forms.ColumnHeader();
			this.expressionColumnHeader = new System.Windows.Forms.ColumnHeader();
			this.SuspendLayout();
			// 
			// listView1
			// 
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.pathColumnHeader,
																						this.expressionColumnHeader});
			this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.Location = new System.Drawing.Point(0, 0);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(424, 150);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			// 
			// pathColumnHeader
			// 
			this.pathColumnHeader.Text = "Path";
			this.pathColumnHeader.Width = 302;
			// 
			// expressionColumnHeader
			// 
			this.expressionColumnHeader.Text = "Expression";
			this.expressionColumnHeader.Width = 118;
			// 
			// FilterViewControl
			// 
			this.Controls.Add(this.listView1);
			this.Name = "FilterViewControl";
			this.Size = new System.Drawing.Size(424, 150);
			this.ResumeLayout(false);

		}
		#endregion

		private void _filter_Changed(object sender, EventArgs e)
		{
			dataBind();
		}
	}
}
