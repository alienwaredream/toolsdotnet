using System;
using Tools.Core;

namespace Tools.Tracing.Common
{
    /// <summary>
    /// Summary description for TraceEventHandlerConfiguration.
    /// </summary>
    [Serializable]
    public class TraceEventHandlerConfiguration : Descriptor, IEnabled
    {
        #region IEnabled Implementation

        private bool _enabled = true;

        public event EventHandler EnabledChanged = null;

        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                if (_enabled != value)
                {
                    _enabled = value;
                    OnEnabledChanged();
                }
            }
        }

        protected virtual void OnEnabledChanged()
        {
            if (EnabledChanged != null)
            {
                EnabledChanged(this, EventArgs.Empty);
            }
        }

        #endregion

        public TraceEventHandlerConfiguration()
        {
            Filters = new TraceEventFilterConfigurationCollection();
        }

        public TraceEventFilterConfigurationCollection Filters { get; set; }
    }
}