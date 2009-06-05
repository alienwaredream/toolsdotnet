using System;
using System.Xml.Serialization;
using Tools.Core;

namespace Tools.Failover
{
    // TODO: Prepared for architecture (SD).
    /// <summary>
    /// Summary description for FailoverRetryRule.
    /// </summary>
    [Serializable]
    public class FailoverRetryRule : Descriptor
    {
        private int _retryInterval = 1000;

        public FailoverRetryRule()
        {
        }

        public FailoverRetryRule(int retryTime, int retryInterval)
        {
            RetryTime = retryTime;
            _retryInterval = retryInterval;
        }

        /// <summary>
        /// The total time in milliseconds to retry for the operation during the failure period.
        /// </summary>
        [XmlAnyAttribute]
        public int RetryTime { get; set; }

        /// <summary>
        /// Interval (in milliseconds) between operation retry attempts during the failure period.
        /// </summary>
        [XmlAnyAttribute]
        public int RetryInterval
        {
            get { return _retryInterval; }
            set { _retryInterval = value; }
        }

        public static FailoverRetryRule DefaultRule
        {
            get
            {
                return new FailoverRetryRule
                    (
                    180000,
                    20000
                    );
            }
        }
    }
}