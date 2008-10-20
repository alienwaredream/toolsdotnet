using System.IO.IsolatedStorage;
using System.Windows.Forms;

namespace Tools.UI.Windows.Descriptors
{
    public partial class IsolatedStorageSettingsControl : UserControl
    {
        private readonly EnumEditControl<IsolatedStorageScope> enumEditControl;
        private IsolatedStorageSettings _settings;


        public IsolatedStorageSettingsControl
            (
            )
        {
            InitializeComponent();

            _settings = new IsolatedStorageSettings();

            enumEditControl =
                new EnumEditControl<IsolatedStorageScope>
                    (
                    _settings.IsolationScope
                    );

            enumEditControl.Top = useIsolatedStorageCheckBox.Bottom + 10;
            enumEditControl.Width = ClientRectangle.Width;
            enumEditControl.Height = ClientRectangle.Height - enumEditControl.Top;
            enumEditControl.Anchor = ((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right;

            Controls.Add(enumEditControl);
        }

        public IsolatedStorageSettings Settings
        {
            get
            {
                return new IsolatedStorageSettings
                    (
                    useIsolatedStorageCheckBox.Checked,
                    enumEditControl.Value
                    );
            }
            set { _settings = value; }
        }
    }
}