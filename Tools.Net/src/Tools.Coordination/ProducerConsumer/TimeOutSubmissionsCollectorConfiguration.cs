
namespace Tools.Coordination.ProducerConsumer
{
    public class TimeOutSubmissionsCollectorConfiguration
    {

        public int TimeOutSubmissionsCollectorRemoveTimeout { get; set; }
        public int TimeOutSubmissionsCollectorInterval { get; set; }
        public int TimeOutSubmissionsCollectorClearItems { get; set; }
        public int TimeOutSubmissionsCollectorCollectionShutdownTimeout { get; set; }
        public int TimeOutSubmissionsCollectorFinalCollectionTimeout { get; set; }

        /// <summary>
        /// If set to true, then QWI will be cleaned from the SIC once
        /// <see cref="TimeOutSubmissionsCollectorRemoveTimeout"/> is exceeded
        /// during the current TSC run.
        /// </summary>
        public bool ClearItems { get; set;}
        /// <summary>
        /// The interval to delay the execution of the TSC loop.
        /// </summary>
        public int Interval { get; set;}
        /// <summary>
        /// The time interval in ms after which QWI should be removed from the SIC, even if
        /// response from the external component has not come yet. For performance reasons
        /// timespanned property of RemoveTimeoutOutTimeSpan should be used.
        /// </summary>
        public int RemoveTimeout { get; set;}
        /// <summary>
        /// The time interval in ms after which QWI presence in the SIC should be notified
        /// as response from the external component has not come yet. For performance reasons
        /// timespanned property of ResponseTimeOutTimeSpan should be used.
        /// </summary>
        public int ResponseTimeout { get; set;}
        //
        public int CollectionShutdownTimeout { get; set;}

        public int FinalCollectionTimeout { get; set;}

        public TimeOutSubmissionsCollectorConfiguration()
        {
            //TimeOutSubmissionsCollectorResponseTimeout = 120000;
            TimeOutSubmissionsCollectorRemoveTimeout = 300000;
            TimeOutSubmissionsCollectorInterval = 30000;
            TimeOutSubmissionsCollectorCollectionShutdownTimeout = 3000;
            TimeOutSubmissionsCollectorFinalCollectionTimeout = 6000;
        }

    }
}