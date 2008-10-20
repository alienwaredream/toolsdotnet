using System;
using System.Windows.Forms;

namespace Tools.UI.Windows.Descriptors
{
    public partial class SettingsEditorForm : Form
    {
        private ApplicationPreferences _preferences;

        public SettingsEditorForm
            (
            )
        {
            _preferences = ApplicationPreferences.GetDefaultPreferences();
            InitializeComponent();

            // this.isolatedStorageSettingsControl
        }

        public SettingsEditorForm
            (
            ApplicationPreferences preferences
            )
        {
            Preferences = preferences;
            InitializeComponent();
        }

        public ApplicationPreferences Preferences
        {
            get
            {
                var pref = new ApplicationPreferences();
                pref.IsolatedStorageSettings = isolatedStorageSettingsControl.Settings;
                pref.Path = mainApplicationPreferencesControl.Path;
                return pref;
            }
            set
            {
                isolatedStorageSettingsControl.Settings = value.IsolatedStorageSettings;
                mainApplicationPreferencesControl.Path = value.Path;
            }
        }

        private void listViewSettingsControl_Load(object sender, EventArgs e)
        {
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}