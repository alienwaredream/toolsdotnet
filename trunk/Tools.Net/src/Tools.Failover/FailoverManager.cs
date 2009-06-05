using System;
using System.Configuration;
using Tools.Core;
using Tools.Core.Context;

namespace Tools.Failover
{
    // TODO: Subject to apply generics:
    // further candidates: event handlers manager, ... (SD)
    /// <summary>
    /// Summary description for FailoverManager.
    /// </summary>
    public sealed class FailoverManager : Descriptor, IEnabled
    {
        private static readonly object syncRoot = new object();
        private static FailoverManager _instance;

        private readonly object _confLock = new object();

        private readonly ContextIdentifier contextIdentifier =
            new ContextIdentifier();

        private FailoverManagerConfiguration config;

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


        private void OnEnabledChanged()
        {
            if (EnabledChanged != null)
            {
                EnabledChanged(this, EventArgs.Empty);
            }
        }

        #endregion

        private FailoverManager()
        {
            // try to initialize
            initialize();
        }

        public static FailoverManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (syncRoot)
                    {
                        if (_instance == null)
                        {
                            _instance = new FailoverManager();
                        }
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// Synchronization object, think about wrapping it away from the public access (SD)
        /// </summary>
        public object ConfLock
        {
            get { return _confLock; }
        }

        public FailoverManagerConfiguration Configuration
        {
            get { return config; }
        }

        // Until enabling via remoting

        public FailureHandler CreateFailureHandler
            (
            string configurationName
            )
        {
            if (config != null && config.FailoverConfigurations[configurationName] != null)
            {
                return new FailureHandler
                    (
                    config.FailoverConfigurations[configurationName]
                    );
            }
            else
            {
                return new FailureHandler
                    (
                    new FailureConfiguration
                        (
                        FailureRetryRule.DefaultRule
                        ));
            }
        }

        private FailoverManagerConfiguration getConfiguration()
        {
            object configSectionTest =
                ConfigurationSettings.GetConfig
                    (
                    "Tools.Failover.FailoverManagerConfiguration"
                    );

            if (configSectionTest == null) return null;

            if (!(configSectionTest is FailoverManagerConfiguration))
            {
                throw new FailoverManagerException
                    (
                    String.Format
                        (
                        "Configuration object of the wrong type {0} . The type expected is {1}",
                        configSectionTest.GetType().FullName,
                        typeof (FailoverManagerConfiguration).FullName
                        ),
                    FailoverManagerMessage.ConfigurationError,
                    contextIdentifier
                    );
            }

            return configSectionTest as FailoverManagerConfiguration;
        }

        private void initialize
            (
            FailoverManagerConfiguration externalConfiguration
            )
        {
            // TODO: The bellow is going to be a part of the utility class (SD)
            // Which might also be advantageous for the explicit character of the 
            // dependency to the TypesSectionReader!
            // So exception of the FailoverManagerException type is present only 
            // temporarily here.
            lock (_confLock)
            {
                try
                {
                    config = (externalConfiguration == null) ? getConfiguration() : externalConfiguration;
                }
                catch (Exception ex)
                {
                    throw new FailoverManagerException
                        (
                        "An exception encountered during the configuration read attempt. See file log for details." +
                        ex,
                        FailoverManagerMessage.ConfigurationError,
                        contextIdentifier
                        );
                }

                // The absence of the section itself is not an error
                if (config == null || config.FailoverConfigurations == null || config.FailoverConfigurations.Count == 0)
                {
                    Enabled = false;
                    return;
                }
                Enabled = true;
            }
        }

        // synchronized
        private void initialize()
        {
            initialize(null);
        }

        public bool FailoverApplicable(string failoverName)
        {
            lock (_confLock)
            {
                if (!Instance.Enabled) return false;

                FailureConfiguration fc =
                    Instance.Configuration.FailoverConfigurations[failoverName];

                return (fc != null && fc.Enabled);
            }
        }
    }
}