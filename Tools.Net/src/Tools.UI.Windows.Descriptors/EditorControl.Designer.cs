namespace Tools.UI.Windows.Descriptors
{
    partial class EditorControl
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
            this.editorTabControl = new System.Windows.Forms.TabControl();
            this.textViewPage = new System.Windows.Forms.TabPage();
            this.textRichTextBox = new System.Windows.Forms.RichTextBox();
            this.editorTabControl.SuspendLayout();
            this.textViewPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // editorTabControl
            // 
            this.editorTabControl.Controls.Add(this.textViewPage);
            this.editorTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editorTabControl.Location = new System.Drawing.Point(0, 0);
            this.editorTabControl.Name = "editorTabControl";
            this.editorTabControl.SelectedIndex = 0;
            this.editorTabControl.Size = new System.Drawing.Size(306, 227);
            this.editorTabControl.TabIndex = 0;
            // 
            // textViewPage
            // 
            this.textViewPage.Controls.Add(this.textRichTextBox);
            this.textViewPage.Location = new System.Drawing.Point(4, 22);
            this.textViewPage.Name = "textViewPage";
            this.textViewPage.Padding = new System.Windows.Forms.Padding(3);
            this.textViewPage.Size = new System.Drawing.Size(298, 201);
            this.textViewPage.TabIndex = 0;
            this.textViewPage.Text = "Xml";
            // 
            // textRichTextBox
            // 
            this.textRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textRichTextBox.Location = new System.Drawing.Point(3, 3);
            this.textRichTextBox.Name = "textRichTextBox";
            this.textRichTextBox.Size = new System.Drawing.Size(292, 195);
            this.textRichTextBox.TabIndex = 0;
            this.textRichTextBox.Text = "";
            // 
            // XmlEditorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.editorTabControl);
            this.Name = "XmlEditorControl";
            this.Size = new System.Drawing.Size(306, 227);
            this.editorTabControl.ResumeLayout(false);
            this.textViewPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl editorTabControl;
        private System.Windows.Forms.TabPage textViewPage;
        private System.Windows.Forms.RichTextBox textRichTextBox;
    }
}
