using System;
using System.Xml.Serialization;

namespace Tools.UI.Windows.Descriptors
{
    /// <summary>
    ///     // This class allows you to handle specific events on the settings class:
    ///  The SettingChanging event is raised before a setting's value is changed.
    ///  The PropertyChanged event is raised after a setting's value is changed.
    ///  The SettingsLoaded event is raised after the setting values are loaded.
    ///  The SettingsSaving event is raised before the setting values are saved.
    /// </summary>
    [Serializable]
    public sealed class ListViewSettings : IListViewSettings
    {
        #region IListViewSettings Members

        [XmlAttribute]
        public bool ShowListNameDescription { get; set; }

        #endregion
    }
}