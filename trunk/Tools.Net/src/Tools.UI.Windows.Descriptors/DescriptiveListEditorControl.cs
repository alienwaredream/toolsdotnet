using System;
using System.Collections.Generic;
using System.Collections;
using System.Windows.Forms;
using System.IO.IsolatedStorage;
using Tools.Core.Utils;


namespace Tools.UI.Windows.Descriptors
{
	public partial class DescriptiveListEditorControl : UserControl
	{
        private DescriptiveList<Container<ListSettings, DescriptiveList<DescriptiveNameValue<string>>>> containers = null;
        private ApplicationPreferences _preferences = ApplicationPreferences.GetDefaultPreferences();
		private string _fileName = String.Empty;
        // two separate providers introduced here in order to support different marks
        // resolution for both editor and the list.
		private DescriptiveNameValueDomainsProvider _editorDomainsProvider;
        private DescriptiveNameValueDomainsProvider _listDomainsProvider;
        //private IsolatedStorageSettings isolatedStorageSettings;

		/// <summary>
		/// File name from where the current list was read from and
		/// where it was stored fot the last time.
		/// </summary>
		public string FileName
		{
			get
			{
				return _fileName;
			}
			set
			{
				_fileName = value;
				filePathToolStripStatusLabel.Text =
					"File:" + _fileName;
			}

		}
        public DescriptiveNameValue<string> SelectedValue
        {
            get
            {
                return this.descriptiveNameValueControl.CurrentValue;
            }
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

            foreach (Container<ListSettings, DescriptiveList<DescriptiveNameValue<string>> >c in containers)
            {
                foreach (DescriptiveNameValue<string> dnv in c.ContainerObject)
                {
                    if (!dic.ContainsKey("$" + c.Name + ":" + dnv.Name))
                    {
                        dic.Add("$" + c.Name + ":" + dnv.Name, dnv.Value);
                    }
                }
            }
            return dic as IDictionary;
        }
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

            DescriptiveList<DescriptiveNameValue<string>> listEntry =
                new DescriptiveList<DescriptiveNameValue<string>>
            (
            "Main",
            "Main list"
            );

            containers =
                new DescriptiveList<Container<ListSettings, DescriptiveList<DescriptiveNameValue<string>>>>
                ();
            Container<ListSettings, DescriptiveList<DescriptiveNameValue<string>>> container =
                new Container<ListSettings, DescriptiveList<DescriptiveNameValue<string>>>
                (
                "Main",
                "Main list of values",
                new ListSettings(),
                listEntry
                );

            containers.Add(container);

			dnvListControl =
                new ContainersControl<ListSettings, DescriptiveList<DescriptiveNameValue<string>>, DescriptiveNameValue<string>>
                (
                containers,
                _listDomainsProvider
				);

			dnvListControl.Dock = DockStyle.Fill;
			
			this.splitContainer.Panel2.Controls.Add
			(
			dnvListControl
			);

			dnvListControl.ValueSelected += new ValueSelectedDelegate<DescriptiveNameValue<string>>(dnvListControl_ValueSelected);
			
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

        void dnvListControl_ValueSelected(object sender, ValueSelectedEventArgs<DescriptiveNameValue<string>> e)
		{
			//this.descriptiveNameValueControl.Value = 
			descriptiveNameValueControl.AcceptChanges();
			//this.dnvListControl.
			//if (e.CurrentValue
			this.descriptiveNameValueControl.SourceValue = e.CurrentValue;
		}

		private void descriptiveNameValueControl1_Load(object sender, EventArgs e)
		{

        }
        #region File operations

        private void saveAllToolStripButton_Click(object sender, EventArgs e)
		{
            System.IO.IsolatedStorage.IsolatedStorageFile isolStorageFile = null;

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
            System.IO.IsolatedStorage.IsolatedStorageFile isolStorageFile = null;

            if (!_preferences.IsolatedStorageSettings.UseIsolatedStore)
            {
                if (!System.IO.File.Exists(filePath))
                    return;
            }
            else
            {
                isolStorageFile =
                IsolatedStorageFile.GetStore
                (
                this._preferences.IsolatedStorageSettings.IsolationScope,
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
                        typeof(DescriptiveList<Container<ListSettings, DescriptiveList<DescriptiveNameValue<string>>>>)
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
				typeof(ICollection<DescriptiveNameValue<string>>).FullName +
				System.Environment.NewLine + ex.ToString()
				);

			}
        }
        
#endregion File operations

        private void marksPresentationToolStripButton_Click(object sender, EventArgs e)
		{
			if (marksPresentationToolStripButton.Checked)
			{
                this._editorDomainsProvider.MarkValues = getRolloutListsValues();
				this.descriptiveNameValueControl.MarksViewType = MarksPresentationType.Decoded;
				this.descriptiveNameValueControl.ReadOnly = true;
			}
			else
			{
				this.descriptiveNameValueControl.MarksViewType = MarksPresentationType.Encoded;
				this.descriptiveNameValueControl.ReadOnly = false;
			}

			//this.descriptiveNameValueControl.MarksViewType =
			
		}

        private void settingsToolStripButton_Click(object sender, EventArgs e)
        {
            SettingsEditorForm editorForm =
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
                MessageBox.Show("Changes will not be applied! Reason:" + ex.ToString());
                return false;
            }
        }
	}
}