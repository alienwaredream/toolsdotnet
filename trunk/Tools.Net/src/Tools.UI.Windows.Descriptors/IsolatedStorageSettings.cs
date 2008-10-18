using System;
using System.Collections.Generic;
using System.Text;
using System.IO.IsolatedStorage;
using System.Xml.Serialization;

namespace Tools.UI.Windows.Descriptors
{
    [Serializable()]
    public class IsolatedStorageSettings
    {
        private bool _useIsolatedStore = false;

        [XmlAttribute()]
        public bool UseIsolatedStore
        {
            get { return _useIsolatedStore; }
            set { _useIsolatedStore = value; }
        }
        private System.IO.IsolatedStorage.IsolatedStorageScope _isolationScope = IsolatedStorageScope.None;

        [XmlAttribute()]
        public System.IO.IsolatedStorage.IsolatedStorageScope IsolationScope
        {
            get { return _isolationScope; }
            set { _isolationScope = value; }
        }
        public IsolatedStorageSettings()
        {
        }
        public IsolatedStorageSettings
            (
            bool useIsolatedStore,
            IsolatedStorageScope isolationScope
            )
        {
            _useIsolatedStore = useIsolatedStore;
            _isolationScope = isolationScope;
        }
    }
}
