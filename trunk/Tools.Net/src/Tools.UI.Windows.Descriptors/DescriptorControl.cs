using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Tools.Core;

namespace Tools.UI.Windows.Descriptors
{
    public partial class DescriptorControl : UserControl, IChangeEventRaiser
    {
        private IDescriptor _descriptor;
        private bool _readOnly = false;

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
            get
            {
                return _readOnly;
            }
            set
            {
                if (_readOnly != value)
                {
                    _readOnly = value;
                    this.nameTextBox.ReadOnly = _readOnly;
                    this.descriptionRichTextBox.ReadOnly = _readOnly;
                }
            }
        }
        public void Clear()
        {
            this.nameTextBox.Text = null;
            this.descriptionRichTextBox.Text = null;
        }

        public DescriptorControl
            (
            )
        {
            InitializeComponent();

            this.nameTextBox.TextChanged += new EventHandler(nameTextBox_TextChanged);
            this.descriptionRichTextBox.TextChanged += new EventHandler(descriptionRichTextBox_TextChanged);

        }

        void descriptionRichTextBox_TextChanged(object sender, EventArgs e)
        {
            this._descriptor.Description = this.descriptionRichTextBox.Text;
            OnChanged();
        }

        void nameTextBox_TextChanged(object sender, EventArgs e)
        {
            this._descriptor.Name = this.nameTextBox.Text;
            OnChanged();
        }

        #region IChangeEventRaiser Members

        public event EventHandler Changed;

        private void OnChanged()
        {
            if (Changed != null)
            {
                Changed(this, EventArgs.Empty);
            }
        }

        #endregion
}
}
