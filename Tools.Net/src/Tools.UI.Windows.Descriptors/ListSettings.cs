using System;

namespace Tools.UI.Windows.Descriptors
{
    [Serializable]
    public class ListSettings : IListSettings
    {
        public ListSettings()
        {
            ListViewSettings = new ListViewSettings();
        }

        #region IListSettings Members

        public ListViewSettings ListViewSettings { get; set; }

        #endregion
    }
}