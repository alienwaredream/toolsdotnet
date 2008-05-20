using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace Tools.Common.Logging
{
    public class PerformanceCounterContainer
    {
        #region Global variables

        private PerfomanceCounterConfiguration _counterConfiguration = null;
        private PerformanceCounter _counter = null;

        private int eventsToSkipCounter = 0;

        private bool checkForSkip = true;
        private int eventsToSkipCount = 0;

        #endregion Global variables

        #region Properties

        public PerfomanceCounterConfiguration CounterConfiguration
        {
            get
            {
                return _counterConfiguration;
            }
            set
            {
                _counterConfiguration = value;
            }
        }
        public PerformanceCounter Counter
        {
            get
            {
                return _counter;
            }
            set
            {
                _counter = value;
            }
        }

        #endregion Properties

        public PerformanceCounterContainer
            (
            PerfomanceCounterConfiguration counterConfiguration,
            PerformanceCounter counter
            )
        {
            _counterConfiguration = counterConfiguration;
            _counter = counter;
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
