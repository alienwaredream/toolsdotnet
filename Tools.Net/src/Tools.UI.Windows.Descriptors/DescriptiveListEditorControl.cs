using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows.Forms;
using Tools.Core.Utils;

namespace Tools.UI.Windows.Descriptors
{
    public partial class DescriptiveListEditorControl : UserControl
    {
        // two separate providers introduced here in order to support different marks
        // resolution for both editor and the list.
        private readonly DescriptiveNameValueDomainsProvider _editorDomainsProvider;
        private readonly DescriptiveNameValueDomainsProvider _listDomainsProvider;
        private string _fileName = String.Empty;
        private ApplicationPreferences _preferences = ApplicationPreferences.GetDefaultPreferences();
        private DescriptiveList<Container<ListSettings, DescriptiveList<DescriptiveNameValue<string>>>> containers;
        //private IsolatedStorageSettings isolatedStorageSettings;

        /// <summary>
        /// File name from where the current list was read from and
        /// where it was stored fot the last time.
        /// </summary>
        public string FileName
        {
            get { return _fileName; }
            set
            {
                _fileName = value;
                filePathToolStripStatusLabel.Text =
                    "File:" + _fileName;
            }
        }

        public DescriptiveNameValue<string> SelectedValue
        {
            get { return descriptiveNameValueControl.CurrentValue; }
        }

        /// <summary>
        /// Returns selected value in the list or null otherwise.
        /// </summary>
        //public DescriptiveNameValue<string> SelectedValue
        //{
        //    get
        //    {
        //        return this.dnvListControl.SelectedValue;
        //    }
        //}
        private IDictionary getRolloutListsValues()
        {
            IDictionary<string, string> dic = new Dictionary<string, string>();

            if (containers == null) return dic as IDictionary;

            foreach (var c in containers)
            {
                foreach (var dnv in c.ContainerObject)
                {
                    if (!dic.ContainsKey("$" + c.Name + ":" + dnv.Name))
                    {
                        dic.Add("$" + c.Name + ":" + dnv.Name, dnv.Value);
                    }
                }
            }
            return dic as IDictionary;
        }

        private void dnvListControl_ValueSelected(object sender, ValueSelectedEventArgs<DescriptiveNameValue<string>> e)
        {
            //this.descriptiveNameValueControl.Value = 
            descriptiveNameValueControl.AcceptChanges();
            //this.dnvListControl.
            //if (e.CurrentValue
            descriptiveNameValueControl.SourceValue = e.CurrentValue;
        }

        private void descriptiveNameValueControl1_Load(object sender, EventArgs e)
        {
        }

        private void marksPresentationToolStripButton_Click(object sender, EventArgs e)
        {
            if (marksPresentationToolStripButton.Checked)
            {
                _editorDomainsProvider.MarkValues = getRolloutListsValues();
                descriptiveNameValueControl.MarksViewType = MarksPresentationType.Decoded;
                descriptiveNameValueControl.ReadOnly = true;
            }
            else
            {
                descriptiveNameValueControl.MarksViewType = MarksPresentationType.Encoded;
                descriptiveNameValueControl.ReadOnly = false;
            }

            //this.descriptiveNameValueControl.MarksViewType =
        }

        private void settingsToolStripButton_Click(object sender, EventArgs e)
        {
            var editorForm =
                new SettingsEditorForm();
            editorForm.Preferences = _preferences;
            editorForm.ShowDialog();
            if (setNewPreferences(_preferences, editorForm.Preferences))
            {
                _preferences = editorForm.Preferences;
                SerializationUtility.Serialize2File(_preferences, _preferences.Path, false, false);
            }
        }

        private bool setNewPreferences
            (
            ApplicationPreferences oldPreferences,
            ApplicationPreferences newPreferences
            )
        {
            try
            {
                // Call all recepients one by one and provide actions for rolling
                // changes back
                // = editorForm.Preferences;
                _preferences.IsolatedStorageSettings = newPreferences.IsolatedStorageSettings;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Changes will not be applied! Reason:" + ex);
                return false;
            }
        }

        #region File operations

        private void saveAllToolStripButton_Click(object sender, EventArgs e)
        {
            IsolatedStorageFile isolStorageFile = null;

            if (!_preferences.IsolatedStorageSettings.UseIsolatedStore)
            {
                DialogResult dResult =
                    saveFileDialog.ShowDialog
                        (
                        );
                if (dResult != DialogResult.OK || String.IsNullOrEmpty(saveFileDialog.FileName)) return;

                try
                {
                    SerializationUtility.Serialize2File
                        (
                        dnvListControl.Containers,
                        saveFileDialog.FileName,
                        false,
                        false
                        );
                    FileName = saveFileDialog.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show
                        (
                        ex.ToString()
                        );
                }
            }
            else
            {
                try
                {
                    //isolStorageFile =
                    //IsolatedStorageFile.GetStore
                    //(
                    //this._preferences.IsolatedStorageSettings.IsolationScope,
                    //null,
                    //null
                    //);

                    //using (IsolatedStorageFileStream isolFileStream =
                    //    new IsolatedStorageFileStream
                    //(
                    //"isolLibrary.xml",
                    //System.IO.FileMode.Create,
                    //isolStorageFile
                    //))
                    //{
                    //    SerializationUtility.SerializeXml2Stream
                    //   (
                    //   containers,
                    //   isolFileStream
                    //   );
                    //}
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            DialogResult dResult =
                openFileDialog.ShowDialog
                    (
                    );
            if (dResult != DialogResult.OK) return;

            LoadFromFile(openFileDialog.FileName);
        }

        public void LoadFromFile(string filePath)
        {
            if (String.IsNullOrEmpty(filePath))
                return;
            IsolatedStorageFile isolStorageFile = null;

            if (!_preferences.IsolatedStorageSettings.UseIsolatedStore)
            {
                if (!File.Exists(filePath))
                    return;
            }
            else
            {
                isolStorageFile =
                    IsolatedStorageFile.GetStore
                        (
                        _preferences.IsolatedStorageSettings.IsolationScope,
                        AppDomain.CurrentDomain.ApplicationIdentity
                        );
                // isolStorageFile.
            }

            DescriptiveList<DescriptiveList<DescriptiveNameValue<string>>> testDnvCollection = null;

            try
            {
                containers =
                    (DescriptiveList<Container<ListSettings, DescriptiveList<DescriptiveNameValue<string>>>>)
                    SerializationUtility.DeserializeFromFile
                        (
                        filePath,
                        typeof (DescriptiveList<Container<ListSettings, DescriptiveList<DescriptiveNameValue<string>>>>)
                        );
                dnvListControl.Containers = containers;
                //    testDnvCollection;
                FileName = filePath;
            }
            catch (Exception ex)
            {
                MessageBox.Show
                    (
                    "Probably not a correct document of type " +
                    typeof (ICollection<DescriptiveNameValue<string>>).FullName +
                    Environment.NewLine + ex
                    );
            }
        }

        #endregion File operations

        #region Constructors

        public DescriptiveListEditorControl()
        {
            InitializeComponent();

            //Hashtable paramsHashtable = 
            //    new Hashtable();
            //paramsHashtable["%coder"] = "SD";

            _editorDomainsProvider =
                new DescriptiveNameValueDomainsProvider
                    (
                    getRolloutListsValues()
                    );
            _listDomainsProvider =
                new DescriptiveNameValueDomainsProvider
                    (
                    getRolloutListsValues()
                    );

            #region dnv list control

            var listEntry =
                new DescriptiveList<DescriptiveNameValue<string>>
                    (
                    "Main",
                    "Main list"
                    );

            containers =
                new DescriptiveList<Container<ListSettings, DescriptiveList<DescriptiveNameValue<string>>>>
                    ();
            var container =
                new Container<ListSettings, DescriptiveList<DescriptiveNameValue<string>>>
                    (
                    "Main",
                    "Main list of values",
                    new ListSettings(),
                    listEntry
                    );

            containers.Add(container);

            dnvListControl =
                new ContainersControl
                    <ListSettings, DescriptiveList<DescriptiveNameValue<string>>, DescriptiveNameValue<string>>
                    (
                    containers,
                    _listDomainsProvider
                    );

            dnvListControl.Dock = DockStyle.Fill;

            splitContainer.Panel2.Controls.Add
                (
                dnvListControl
                );

            dnvListControl.ValueSelected += dnvListControl_ValueSelected;

            #endregion dnv list control

            if (!DesignMode)
            {
                saveFileDialog.InitialDirectory =
                    AssemblyInfoUtility.ApplicationSettingsCommonDirectory;
                openFileDialog.InitialDirectory =
                    AssemblyInfoUtility.ApplicationSettingsCommonDirectory;
            }

            initializeDescriptiveNameValueControl
                (
                _editorDomainsProvider
                );
        }

        #endregion Constructors
    }
}