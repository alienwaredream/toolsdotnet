using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Tools.UI.Windows.Descriptors
{
    public partial class IsolatedStorageSettingsControl : UserControl
    {
        private IsolatedStorageSettings _settings;
        private EnumEditControl<System.IO.IsolatedStorage.IsolatedStorageScope> enumEditControl;

        public IsolatedStorageSettings Settings
        {
            get 
            {
                return new IsolatedStorageSettings
                (
                this.useIsolatedStorageCheckBox.Checked,
                enumEditControl.Value
                );
                }
            set { _settings = value; }
        }



        public IsolatedStorageSettingsControl
            (
            )
        {
            InitializeComponent();

            _settings = new IsolatedStorageSettings();

            enumEditControl =
                new EnumEditControl<System.IO.IsolatedStorage.IsolatedStorageScope>
                (
                _settings.IsolationScope
                );

            enumEditControl.Top = this.useIsolatedStorageCheckBox.Bottom + 10;
            enumEditControl.Width = this.ClientRectangle.Width;
            enumEditControl.Height = this.ClientRectangle.Height - enumEditControl.Top;
            enumEditControl.Anchor = ((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right;
            
            Controls.Add(enumEditControl);

        }
    }
}
