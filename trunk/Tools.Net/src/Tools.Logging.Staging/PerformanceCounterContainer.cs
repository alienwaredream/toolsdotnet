using System.Diagnostics;
using System.Threading;

namespace Tools.Logging
{
    public class PerformanceCounterContainer
    {
        #region Global variables

        private readonly int eventsToSkipCount;
        private PerfomanceCounterConfiguration _counterConfiguration;

        private bool checkForSkip = true;
        private int eventsToSkipCounter;

        #endregion Global variables

        #region Properties

        public PerfomanceCounterConfiguration CounterConfiguration
        {
            get { return _counterConfiguration; }
            set { _counterConfiguration = value; }
        }

        public PerformanceCounter Counter { get; set; }

        #endregion Properties

        public PerformanceCounterContainer
            (
            PerfomanceCounterConfiguration counterConfiguration,
            PerformanceCounter counter
            )
        {
            _counterConfiguration = counterConfiguration;
            Counter = counter;
            // assign derived values, that is done in order to avoid property method call, still
            // this encapsulation to the container itself is a performance overhead for measurements, so
            // at least to pay back some (SD).
            eventsToSkipCount = _counterConfiguration.EventsToSkipCount;
        }

        public bool Applicable
        {
            get
            {
                if (checkForSkip)
                {
                    if (Interlocked.Increment(ref eventsToSkipCounter) > eventsToSkipCount)
                    {
                        checkForSkip = false;
                        return true;
                    }
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}