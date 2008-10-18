using System;

namespace Tools.UI.Windows.Descriptors
{
    [Serializable()]
    public class ListSettings : Tools.UI.Windows.Descriptors.IListSettings
    {
        public ListViewSettings ListViewSettings { get; set; }

        public ListSettings()
        {
            ListViewSettings = new ListViewSettings();
        }
    }
}
