using System.ComponentModel;
using System.Windows.Forms;

namespace Tools.Tracing.UI
{
    /// <summary>
    /// Summary description for TabContentControl.
    /// </summary>
    public class TabContentControl : UserControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private Container components;

        private TabControl tabControl1;

        public TabContentControl()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            // TODO: Add any initialization after the InitializeComponent call
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Location = new System.Drawing.Point(24, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(576, 352);
            this.tabControl1.TabIndex = 0;
            // 
            // TabContentControl
            // 
            this.Controls.Add(this.tabControl1);
            this.Name = "TabContentControl";
            this.Size = new System.Drawing.Size(600, 352);
            this.ResumeLayout(false);
        }

        #endregion
    }
}