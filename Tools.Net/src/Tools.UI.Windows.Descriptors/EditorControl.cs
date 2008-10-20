using System.Windows.Forms;

namespace Tools.UI.Windows.Descriptors
{
    // TODO: To be very generic and reflection based in the future, for now just a 
    // prototype vresion.
    public partial class EditorControl : UserControl
    {
        public EditorControl()
        {
            InitializeComponent();
        }

        public override string Text
        {
            get { return textRichTextBox.Text; }
            set { textRichTextBox.Text = value; }
        }
    }
}