
namespace Tools.UI.Windows.Descriptors
{
	partial class DescriptiveNameValueControl
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
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.valueRichTextBox = new System.Windows.Forms.RichTextBox();
            this.collapsibleContainer1 = new Tools.UI.Windows.Descriptors.CollapsibleContainer();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // splitContainer1
            // 
            this.splitContainer1.DataBindings.Add(new System.Windows.Forms.Binding("SplitterDistance", global::Tools.UI.Windows.Descriptors.Settings1.Default, "SplitterPosition", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.valueRichTextBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.collapsibleContainer1);
            this.splitContainer1.Size = new System.Drawing.Size(249, 167);
            this.splitContainer1.SplitterDistance = global::Tools.UI.Windows.Descriptors.Settings1.Default.SplitterPosition;
            this.splitContainer1.TabIndex = 9;
            this.splitContainer1.Text = "splitContainer1";
            // 
            // valueRichTextBox
            // 
            this.valueRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.valueRichTextBox.Location = new System.Drawing.Point(0, 0);
            this.valueRichTextBox.MinimumSize = new System.Drawing.Size(179, 20);
            this.valueRichTextBox.Name = "valueRichTextBox";
            this.valueRichTextBox.Size = new System.Drawing.Size(249, 83);
            this.valueRichTextBox.TabIndex = 8;
            this.valueRichTextBox.Text = "";
            this.valueRichTextBox.TextChanged += new System.EventHandler(this.statementRichTextBox_TextChanged);
            // 
            // collapsibleContainer1
            // 
            this.collapsibleContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.collapsibleContainer1.ContainedControl = null;
            this.collapsibleContainer1.Location = new System.Drawing.Point(0, 0);
            this.collapsibleContainer1.Name = "collapsibleContainer1";
            this.collapsibleContainer1.Size = new System.Drawing.Size(249, 80);
            this.collapsibleContainer1.TabIndex = 8;
            this.collapsibleContainer1.Title = null;
            // 
            // DescriptiveNameValueControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.MinimumSize = new System.Drawing.Size(179, 107);
            this.Name = "DescriptiveNameValueControl";
            this.Size = new System.Drawing.Size(249, 167);
            this.Load += new System.EventHandler(this.DescriptiveNameValueControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ErrorProvider errorProvider1;
		private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RichTextBox valueRichTextBox;
        private CollapsibleContainer collapsibleContainer1;
	}
}
