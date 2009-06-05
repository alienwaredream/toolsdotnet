using System;
using Tools.Core;

namespace Tools.Failover
{
    /// <summary>
    /// Summary description for ApplicationEventHandlerManagerConfiguration.
    /// </summary>
    [Serializable]
    public class FailoverManagerConfiguration : Descriptor
    {
        public FailoverManagerConfiguration()
        {
            FailoverConfigurations = new FailureConfigurationCollection();
        }

        public FailureConfigurationCollection FailoverConfigurations { get; set; }
    }
}