namespace Tools.Tracing.UI
{
	partial class MonitorPanel
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
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
            this.scrollableListView = new Tools.Tracing.UI.ScrollableListView();
            this.SuspendLayout();
            // 
            // scrollableListView
            // 
            this.scrollableListView.AllowColumnReorder = true;
            this.scrollableListView.CountToShow = 10;
            this.scrollableListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scrollableListView.FullRowSelect = true;
            this.scrollableListView.HideSelection = false;
            this.scrollableListView.Location = new System.Drawing.Point(0, 0);
            this.scrollableListView.MinIndex = 0;
            this.scrollableListView.MultiSelect = false;
            this.scrollableListView.Name = "scrollableListView";
            this.scrollableListView.Size = new System.Drawing.Size(801, 211);
            this.scrollableListView.TabIndex = 1;
            this.scrollableListView.UseCompatibleStateImageBehavior = false;
            this.scrollableListView.View = System.Windows.Forms.View.Details;
            this.scrollableListView.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // MonitorPanel
            // 
            this.Controls.Add(this.scrollableListView);
            this.Name = "MonitorPanel";
            this.Size = new System.Drawing.Size(801, 211);
            this.ResumeLayout(false);

		}

		#endregion

        private ScrollableListView scrollableListView;
	}
}
