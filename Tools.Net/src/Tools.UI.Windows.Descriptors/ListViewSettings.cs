using System;
using System.Xml.Serialization;

namespace Tools.UI.Windows.Descriptors {
    
    

    /// <summary>
    ///     // This class allows you to handle specific events on the settings class:
    ///  The SettingChanging event is raised before a setting's value is changed.
    ///  The PropertyChanged event is raised after a setting's value is changed.
    ///  The SettingsLoaded event is raised after the setting values are loaded.
    ///  The SettingsSaving event is raised before the setting values are saved.
    /// </summary>
    [Serializable()]
    public sealed partial class ListViewSettings : Tools.UI.Windows.Descriptors.IListViewSettings 
    {
        private bool _showListNameDescription = false;

        [XmlAttribute()]
        public bool ShowListNameDescription
        {
            get { return _showListNameDescription; }
            set { _showListNameDescription = value; }
        }
        
        public ListViewSettings() {
            // // To add event handlers for saving and changing settings, uncomment the lines below:
            //
            // this.SettingChanging += this.SettingChangingEventHandler;
            //
            // this.SettingsSaving += this.SettingsSavingEventHandler;
            //
        }
    }
}
