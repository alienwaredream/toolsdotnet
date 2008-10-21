using System;
using System.Xml.Serialization;
using Tools.Core;

namespace Tools.Remoting.Client.Common
{
    /// <summary>
    /// Summary description for RemoteConnectionConfiguration.
    /// </summary>
    [Serializable]
    public sealed class RemoteConnectionConfiguration : Descriptor, IEnabled, IChangeEventRaiser
    {
        private string _port;
        private ProtocolType _protocolType = ProtocolType.Tcp;
        private string _serviceHost;
        private string _uri;

        #region IEnabled Implementation

        private bool _enabled = true;

        public event EventHandler EnabledChanged = null;

        [XmlAttribute]
        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                if (_enabled != value)
                {
                    _enabled = value;
                    // handle as IEnabled
                    OnEnabledChanged();
                    // handle as IChangeEventRaiser
                    OnChanged();
                }
            }
        }


        private void OnEnabledChanged()
        {
            if (EnabledChanged != null)
            {
                EnabledChanged(this, EventArgs.Empty);
            }
        }

        #endregion

        [XmlAttribute]
        public ProtocolType ProtocolType
        {
            get { return _protocolType; }
            set
            {
                if (_protocolType == value) return;
                // assign
                _protocolType = value;
                // handle as IChangeEventRaiser
                OnChanged();
            }
        }

        [XmlAttribute]
        public string Uri
        {
            get { return _uri; }
            set
            {
                if (_uri == value) return;
                // assign
                _uri = value;
                // handle as IChangeEventRaiser
                OnChanged();
            }
        }

        [XmlAttribute]
        public string ServiceHost
        {
            get { return _serviceHost; }
            set
            {
                if (_serviceHost == value) return;
                // assign
                _serviceHost = value;
                // handle as IChangeEventRaiser
                OnChanged();
            }
        }

        [XmlAttribute]
        public string Port
        {
            get { return _port; }
            set
            {
                if (_port == value) return;
                // assign
                _port = value;
                // handle as IChangeEventRaiser
                OnChanged();
            }
        }

        #region Constructors

        #endregion

        #region IChangeEventRaiser Members

        public event EventHandler Changed;

        #endregion

        private void OnChanged()
        {
            if (Changed != null) Changed(this, EventArgs.Empty);
        }
    }
}