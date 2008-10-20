using System;
using Tools.Core;

namespace Tools.Tracing.UI
{
    /// <summary>
    /// Summary description for ConnectionInstance.
    /// </summary>
    public abstract class ServiceConnectionInstance : IServiceConnector, IChangeEventRaiser
    {
        private bool _connected;

        #region IChangeEventRaiser Members

        public event EventHandler Changed;

        #endregion

        #region IServiceConnector implementation

        public event ServiceConnectionDelegate Connected;
        public event ServiceConnectionDelegate Disconnected;

        public bool IsConnected
        {
            get { return _connected; }
        }

        public virtual void Connect()
        {
            // TODO: connect
            _connected = true;
            OnConnected();
            OnChanged();
        }

        public virtual void Disconnect()
        {
            // TODO: disconnect
            _connected = false;
            OnDisconnected();
            OnChanged();
        }

        protected void OnConnected()
        {
            if (Connected != null) Connected(this, EventArgs.Empty);
        }

        protected void OnDisconnected()
        {
            if (Disconnected != null) Disconnected(this, EventArgs.Empty);
        }

        #endregion

        protected virtual void OnChanged()
        {
            if (Changed != null) Changed(this, EventArgs.Empty);
        }
    }
}