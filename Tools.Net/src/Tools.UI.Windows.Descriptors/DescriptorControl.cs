using System;
using System.Windows.Forms;
using Tools.Core;

namespace Tools.UI.Windows.Descriptors
{
    public partial class DescriptorControl : UserControl, IChangeEventRaiser
    {
        private IDescriptor _descriptor;
        private bool _readOnly;

        public DescriptorControl
            (
            )
        {
            InitializeComponent();

            nameTextBox.TextChanged += nameTextBox_TextChanged;
            descriptionRichTextBox.TextChanged += descriptionRichTextBox_TextChanged;
        }

        public IDescriptor Descriptor
        {
            get
            {
                if (_descriptor == null) return null;

                _descriptor.Name = nameTextBox.Text;
                _descriptor.Description = descriptionRichTextBox.Text;
                return _descriptor;
            }
            set
            {
                nameTextBox.Clear();
                descriptionRichTextBox.Clear();

                _descriptor = value;

                if (_descriptor == null) return;

                nameTextBox.Text = _descriptor.Name;
                descriptionRichTextBox.Text = _descriptor.Description;
            }
        }

        public bool ReadOnly
        {
            get { return _readOnly; }
            set
            {
                if (_readOnly != value)
                {
                    _readOnly = value;
                    nameTextBox.ReadOnly = _readOnly;
                    descriptionRichTextBox.ReadOnly = _readOnly;
                }
            }
        }

        #region IChangeEventRaiser Members

        public event EventHandler Changed;

        #endregion

        public void Clear()
        {
            nameTextBox.Text = null;
            descriptionRichTextBox.Text = null;
        }

        private void descriptionRichTextBox_TextChanged(object sender, EventArgs e)
        {
            _descriptor.Description = descriptionRichTextBox.Text;
            OnChanged();
        }

        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {
            _descriptor.Name = nameTextBox.Text;
            OnChanged();
        }

        private void OnChanged()
        {
            if (Changed != null)
            {
                Changed(this, EventArgs.Empty);
            }
        }
    }
}