using System.Collections.Generic;
using System.Windows.Forms;
using Tools.Core.Context;

namespace Tools.UI.Windows.Descriptors
{
    public partial class ContextHolderPointersControl : UserControl
    {
        private List<ContextHolderIdDescriptorPointer> _pointers = new List<ContextHolderIdDescriptorPointer>(20);

        public ContextHolderPointersControl()
        {
            InitializeComponent();
        }

        public List<ContextHolderIdDescriptorPointer> Pointers
        {
            get { return _pointers; }
            set { _pointers = value; }
        }
    }
}