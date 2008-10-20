using System;
using System.IO.IsolatedStorage;
using System.Xml.Serialization;

namespace Tools.UI.Windows.Descriptors
{
    [Serializable]
    public class IsolatedStorageSettings
    {
        private IsolatedStorageScope _isolationScope = IsolatedStorageScope.None;

        public IsolatedStorageSettings()
        {
        }

        public IsolatedStorageSettings
            (
            bool useIsolatedStore,
            IsolatedStorageScope isolationScope
            )
        {
            UseIsolatedStore = useIsolatedStore;
            _isolationScope = isolationScope;
        }

        [XmlAttribute]
        public bool UseIsolatedStore { get; set; }

        [XmlAttribute]
        public IsolatedStorageScope IsolationScope
        {
            get { return _isolationScope; }
            set { _isolationScope = value; }
        }
    }
}