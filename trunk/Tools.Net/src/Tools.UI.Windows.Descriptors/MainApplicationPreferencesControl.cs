using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Tools.UI.Windows.Descriptors
{
    public partial class MainApplicationPreferencesControl : UserControl
    {
        public string Path
        {
            get { return pathTextBox.Text; }
            set { pathTextBox.Text = value; }
        }


        public MainApplicationPreferencesControl()
        {
            InitializeComponent();
        }
    }
}
