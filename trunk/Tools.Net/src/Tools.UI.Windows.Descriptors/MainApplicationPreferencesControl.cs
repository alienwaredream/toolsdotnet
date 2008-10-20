using System.Windows.Forms;

namespace Tools.UI.Windows.Descriptors
{
    public partial class MainApplicationPreferencesControl : UserControl
    {
        public MainApplicationPreferencesControl()
        {
            InitializeComponent();
        }

        public string Path
        {
            get { return pathTextBox.Text; }
            set { pathTextBox.Text = value; }
        }
    }
}