using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Tools.UI.Windows.Descriptors
{
    // TODO: To be very generic and reflection based in the future, for now just a 
    // prototype vresion.
    public partial class EditorControl : UserControl
    {
        public override string Text
        {
            get
            {
                return this.textRichTextBox.Text;
            }
            set
            {
                this.textRichTextBox.Text = value;
            }
        }

        public EditorControl()
        {
            InitializeComponent();
        }
    }
}
