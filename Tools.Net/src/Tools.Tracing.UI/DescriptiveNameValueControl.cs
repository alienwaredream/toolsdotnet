using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Xml;

using Tools.Core;
using Tools.UI.Windows.Descriptors;

namespace Tools.Tracing.UI
{
	/// <summary>
	/// Summary description for XPathStatementControl.
	/// </summary>
	public class DescriptiveNameValueControl : System.Windows.Forms.UserControl
	{

		#region Global Declaration

		private System.Windows.Forms.RichTextBox statementRichTextBox;
		private System.Windows.Forms.TextBox nameTextBox;
		private System.Windows.Forms.Label nameLabel;
		private System.Windows.Forms.Label descriptionLabel;
		private System.Windows.Forms.TextBox descriptionTextBox;
		private System.Windows.Forms.ListView statementsListView;
		private System.Windows.Forms.ContextMenu generalContextMenu;
		// TODO: public just for the proof of concept (SD)
		public System.Windows.Forms.MenuItem cancelFilterMenuItem;
		public System.Windows.Forms.MenuItem applyMenuItem;

		private List<DescriptiveNameValue<string>> dnvCollection =
            new List<DescriptiveNameValue<string>>();
		private System.Windows.Forms.ColumnHeader nameColumnHeader;
		private System.Windows.Forms.ColumnHeader valueColumnHeader;
		private System.Windows.Forms.ColumnHeader descriptionColumnHeader;


		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		//
		//public event ApplyRequested 
		#endregion Global Declaration

		#region Constructors

		public DescriptiveNameValueControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call

		}

		#endregion Constructors

		#region Properties

		public string CurrentValue
		{
			get
			{
				return statementRichTextBox.Text;
			}
		}

		#endregion Properties

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
			this.statementRichTextBox = new System.Windows.Forms.RichTextBox();
			this.nameTextBox = new System.Windows.Forms.TextBox();
			this.nameLabel = new System.Windows.Forms.Label();
			this.descriptionLabel = new System.Windows.Forms.Label();
			this.descriptionTextBox = new System.Windows.Forms.TextBox();
			this.statementsListView = new System.Windows.Forms.ListView();
			this.nameColumnHeader = new System.Windows.Forms.ColumnHeader();
			this.valueColumnHeader = new System.Windows.Forms.ColumnHeader();
			this.descriptionColumnHeader = new System.Windows.Forms.ColumnHeader();
			this.generalContextMenu = new System.Windows.Forms.ContextMenu();
			this.applyMenuItem = new System.Windows.Forms.MenuItem();
			this.cancelFilterMenuItem = new System.Windows.Forms.MenuItem();
			this.SuspendLayout();
			// 
			// statementRichTextBox
			// 
			this.statementRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.statementRichTextBox.Location = new System.Drawing.Point(0, 0);
			this.statementRichTextBox.Name = "statementRichTextBox";
			this.statementRichTextBox.Size = new System.Drawing.Size(336, 56);
			this.statementRichTextBox.TabIndex = 0;
			this.statementRichTextBox.Text = "";
			// 
			// nameTextBox
			// 
			this.nameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.nameTextBox.Location = new System.Drawing.Point(48, 56);
			this.nameTextBox.Name = "nameTextBox";
			this.nameTextBox.Size = new System.Drawing.Size(288, 20);
			this.nameTextBox.TabIndex = 2;
			// 
			// nameLabel
			// 
			this.nameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.nameLabel.Location = new System.Drawing.Point(0, 56);
			this.nameLabel.Name = "nameLabel";
			this.nameLabel.Size = new System.Drawing.Size(48, 23);
			this.nameLabel.TabIndex = 3;
			this.nameLabel.Text = "Name";
			// 
			// descriptionLabel
			// 
			this.descriptionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.descriptionLabel.Location = new System.Drawing.Point(0, 77);
			this.descriptionLabel.Name = "descriptionLabel";
			this.descriptionLabel.Size = new System.Drawing.Size(48, 23);
			this.descriptionLabel.TabIndex = 5;
			this.descriptionLabel.Text = "Desc";
			// 
			// descriptionTextBox
			// 
			this.descriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.descriptionTextBox.Location = new System.Drawing.Point(48, 77);
			this.descriptionTextBox.Name = "descriptionTextBox";
			this.descriptionTextBox.Size = new System.Drawing.Size(288, 20);
			this.descriptionTextBox.TabIndex = 4;
			// 
			// statementsListView
			// 
			this.statementsListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.statementsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameColumnHeader,
            this.valueColumnHeader,
            this.descriptionColumnHeader});
			this.statementsListView.FullRowSelect = true;
			this.statementsListView.GridLines = true;
			this.statementsListView.Location = new System.Drawing.Point(0, 104);
			this.statementsListView.Name = "statementsListView";
			this.statementsListView.Size = new System.Drawing.Size(336, 72);
			this.statementsListView.TabIndex = 6;
			this.statementsListView.View = System.Windows.Forms.View.Details;
			// 
			// nameColumnHeader
			// 
			this.nameColumnHeader.Text = "Name";
			// 
			// valueColumnHeader
			// 
			this.valueColumnHeader.Text = "Value";
			this.valueColumnHeader.Width = 100;
			// 
			// descriptionColumnHeader
			// 
			this.descriptionColumnHeader.Text = "Description";
			this.descriptionColumnHeader.Width = 170;
			// 
			// generalContextMenu
			// 
			this.generalContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.applyMenuItem,
            this.cancelFilterMenuItem});
			this.generalContextMenu.Popup += new System.EventHandler(this.generalContextMenu_Popup);
			// 
			// applyMenuItem
			// 
			this.applyMenuItem.Index = 0;
			this.applyMenuItem.Text = "&Apply xPath filter";
			this.applyMenuItem.Click += new System.EventHandler(this.applyMenuItem_Click);
			// 
			// cancelFilterMenuItem
			// 
			this.cancelFilterMenuItem.Index = 1;
			this.cancelFilterMenuItem.Text = "&Cancel xPathFilter";
			// 
			// DescriptiveNameValueControl
			// 
			this.ContextMenu = this.generalContextMenu;
			this.Controls.Add(this.statementsListView);
			this.Controls.Add(this.descriptionLabel);
			this.Controls.Add(this.descriptionTextBox);
			this.Controls.Add(this.nameLabel);
			this.Controls.Add(this.nameTextBox);
			this.Controls.Add(this.statementRichTextBox);
			this.Name = "DescriptiveNameValueControl";
			this.Size = new System.Drawing.Size(336, 176);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void generalContextMenu_Popup(object sender, System.EventArgs e)
		{
		
		}

		private void applyMenuItem_Click(object sender, System.EventArgs e)
		{
		
		}
	}
}
