using System;
using Tools.Core.Utils;

namespace Tools.UI.Windows.Descriptors
{
    [Serializable]
    public class ApplicationPreferences
    {
        #region Globals

        #endregion

        #region Properties

        public string Path { get; set; }

        public ListViewSettings ListViewSettings { get; set; }


        public IsolatedStorageSettings IsolatedStorageSettings { get; set; }

        #endregion

        #region Constructors

        public ApplicationPreferences()
        {
            ListViewSettings = new ListViewSettings();
            IsolatedStorageSettings = new IsolatedStorageSettings();
        }

        #endregion

        public static ApplicationPreferences GetDefaultPreferences()
        {
            var retVal = new ApplicationPreferences();
            retVal.Path = AssemblyInfoUtility.ApplicationSettingsCommonDirectory + @"\preferences.xml";
            return retVal;
        }
    }
}