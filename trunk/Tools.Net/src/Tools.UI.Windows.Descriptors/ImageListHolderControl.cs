using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Tools.UI.Windows.Descriptors
{
    public partial class ImageListHolderControl : UserControl
    {
        public ImageList LockImageList
        {
            get
            {
                return this.locksImageList;
            }
        }

        public ImageListHolderControl()
        {
            InitializeComponent();
        }
    }
}
