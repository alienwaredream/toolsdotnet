using System;
using System.Collections.Generic;
using System.Text;

using Tools.Core.Utils;

namespace Tools.UI.Windows.Descriptors
{
    [Serializable()]
    public class ApplicationPreferences
    {
        #region Globals
        private ListViewSettings _listViewSettings;
        private string _path;
        private IsolatedStorageSettings _isolatedStorageSettings; 

        #endregion

        #region Properties
        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }

        public ListViewSettings ListViewSettings
        {
            get { return _listViewSettings; }
            set { _listViewSettings = value; }
        }


        public IsolatedStorageSettings IsolatedStorageSettings
        {
            get { return _isolatedStorageSettings; }
            set { _isolatedStorageSettings = value; }
        } 
        #endregion

        #region Constructors
        public ApplicationPreferences()
        {
            _listViewSettings = new ListViewSettings();
            _isolatedStorageSettings = new IsolatedStorageSettings();
        } 
        #endregion

        public static ApplicationPreferences GetDefaultPreferences()
        {
            ApplicationPreferences retVal =  new ApplicationPreferences();
            retVal.Path = AssemblyInfoUtility.ApplicationSettingsCommonDirectory + @"\preferences.xml";
            return retVal;
        }

    }
}
