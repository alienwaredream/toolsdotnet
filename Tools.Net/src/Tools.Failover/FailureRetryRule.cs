using System;
using System.Xml.Serialization;
using Tools.Core;

namespace Tools.Failover
{
    // TODO: Prepared for architecture (SD).
    /// <summary>
    /// Summary description for FailureRetryRule.
    /// </summary>
    [Serializable]
    public class FailureRetryRule : Descriptor
    {
        private int _retryInterval = 1000;
        private bool _shouldRetry = true;

        public FailureRetryRule()
        {
        }

        public FailureRetryRule
            (
            int retryTime,
            int retryInterval,
            bool shouldRetry,
            bool exitOnRetryTimeExceeded
            )
        {
            RetryTime = retryTime;
            _retryInterval = retryInterval;
            _shouldRetry = shouldRetry;
            ExitOnRetryTimeExceeded = exitOnRetryTimeExceeded;
        }

        /// <summary>
        /// The total time in milliseconds to retry for the operation during the failure period.
        /// </summary>
        [XmlAttribute]
        public int RetryTime { get; set; }

        /// <summary>
        /// Interval (in milliseconds) between operation retry attempts during the failure period.
        /// </summary>
        [XmlAttribute]
        public int RetryInterval
        {
            get { return _retryInterval; }
            set { _retryInterval = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public bool ShouldRetry
        {
            get { return _shouldRetry; }
            set { _shouldRetry = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public bool ExitOnRetryTimeExceeded { get; set; }

        public static FailureRetryRule DefaultRule
        {
            get
            {
                return new FailureRetryRule
                    (
                    180000,
                    20000,
                    true,
                    false
                    );
            }
        }
    }
}