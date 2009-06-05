using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Tools.Core;

namespace Tools.Failover
{
    /// <summary>
    /// Summary description for FailoverConfiguration.
    /// </summary>
    [Serializable]
    public class FailureConfiguration : Descriptor, IEnabled
    {
        #region Fields

        private List<NameValue<string, object>> _extensibilityItems =
            new List<NameValue<string, object>>();

        private FailureRetryRule _failureRetryRule = FailureRetryRule.DefaultRule;

        #endregion Fields

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

        #region Properties

        public FailureRetryRule FailureRetryRule
        {
            get { return _failureRetryRule; }
            set { _failureRetryRule = value; }
        }

        /// <summary>
        /// This property holds a collection of any extensibility configuration
        /// parameters that can be freely used by dynamic assemblies (like
        /// algorithms for submission delay, etc).
        /// That is the preffered mechanism for configuration extensibility before
        /// even categorizing it to the more specific classes. (preffered just to putting
        /// key/values in the config). (SD)
        /// </summary>
        [XmlArray]
        public List<NameValue<string, object>> ExtensibilityItems
        {
            get { return _extensibilityItems; }
            set { _extensibilityItems = value; }
        }

        #endregion Properties

        public FailureConfiguration(FailureRetryRule failureRetryRule)
        {
            _failureRetryRule = failureRetryRule;
        }
    }
}