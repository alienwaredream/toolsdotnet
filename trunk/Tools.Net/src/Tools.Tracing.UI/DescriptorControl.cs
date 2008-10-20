using System;
using System.ComponentModel;
using System.Windows.Forms;
using Tools.Core;

namespace Tools.Tracing.UI
{
    /// <summary>
    /// Summary description for DescriptorControl.
    /// </summary>
    public class DescriptorControl : UserControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private Container components;

        private RichTextBox descriptionRichTextBox;
        protected ErrorProvider errorProvider1;
        private Label label1;
        private Label label2;
        private TextBox nameTextBox;

        public DescriptorControl()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            // TODO: Add any initialization after the InitializeComponent call
        }

        public virtual Descriptor GetValue()
        {
            if (nameTextBox.Text == String.Empty)
            {
                errorProvider1.SetError(nameTextBox, "Value can't be empty!");
                return null;
            }
            errorProvider1.SetError(nameTextBox, String.Empty);
            return new Descriptor(nameTextBox.Text, descriptionRichTextBox.Text);
        }

        public virtual void LoadValue(Descriptor val)
        {
            nameTextBox.Text = val.Name;
            descriptionRichTextBox.Text = val.Description;
        }

        public virtual void Clear()
        {
            nameTextBox.Text = String.Empty;
            errorProvider1.SetError(nameTextBox, String.Empty);

            descriptionRichTextBox.Text = String.Empty;
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
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
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.descriptionRichTextBox = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider();
            this.SuspendLayout();
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(48, 16);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(208, 20);
            this.nameTextBox.TabIndex = 0;
            this.nameTextBox.Text = "";
            // 
            // descriptionRichTextBox
            // 
            this.descriptionRichTextBox.Location = new System.Drawing.Point(0, 64);
            this.descriptionRichTextBox.Name = "descriptionRichTextBox";
            this.descriptionRichTextBox.Size = new System.Drawing.Size(256, 80);
            this.descriptionRichTextBox.TabIndex = 1;
            this.descriptionRichTextBox.Text = "";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(0, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "Name";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(0, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 23);
            this.label2.TabIndex = 3;
            this.label2.Text = "Description:";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // DescriptorControl
            // 
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.descriptionRichTextBox);
            this.Controls.Add(this.nameTextBox);
            this.Name = "DescriptorControl";
            this.Size = new System.Drawing.Size(280, 150);
            this.ResumeLayout(false);
        }

        #endregion
    }
}