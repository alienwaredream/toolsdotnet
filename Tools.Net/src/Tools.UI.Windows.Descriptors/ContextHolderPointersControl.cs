using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Tools.Core;
using Tools.Core.Context;

namespace Tools.UI.Windows.Descriptors
{
    public partial class ContextHolderPointersControl : UserControl
    {
        private List<ContextHolderIdDescriptorPointer> _pointers = new List<ContextHolderIdDescriptorPointer>(20);

        public List<ContextHolderIdDescriptorPointer> Pointers
        {
            get { return _pointers; }
            set 
            { 
                _pointers = value;
            }
        }

        public ContextHolderPointersControl()
        {
            InitializeComponent();
        }
    }
}
