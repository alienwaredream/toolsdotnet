using System.Windows.Forms;

namespace Tools.UI.Windows.Descriptors
{
    public partial class ImageListHolderControl : UserControl
    {
        public ImageListHolderControl()
        {
            InitializeComponent();
        }

        public ImageList LockImageList
        {
            get { return locksImageList; }
        }
    }
}