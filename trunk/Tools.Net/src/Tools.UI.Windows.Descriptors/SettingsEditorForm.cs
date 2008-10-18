using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Tools.UI.Windows.Descriptors
{
    public partial class SettingsEditorForm : Form
    {
        private ApplicationPreferences _preferences;

        public ApplicationPreferences Preferences
        {
            get 
            {
                ApplicationPreferences pref = new ApplicationPreferences();
                pref.IsolatedStorageSettings = isolatedStorageSettingsControl.Settings;
                pref.Path = this.mainApplicationPreferencesControl.Path;
                return pref;
            }
            set 
            {
                this.isolatedStorageSettingsControl.Settings = value.IsolatedStorageSettings;
                this.mainApplicationPreferencesControl.Path = value.Path;
            }
        }

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

        private void listViewSettingsControl_Load(object sender, EventArgs e)
        {

        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}