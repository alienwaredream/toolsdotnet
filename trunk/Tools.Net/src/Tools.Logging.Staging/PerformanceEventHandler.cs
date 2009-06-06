using System;
using System.Diagnostics;

namespace Tools.Logging
{
    /// <summary>
    /// Only one instance per AppDomain is assumed to be in place. There is no check, will be 
    /// changed to singleton pattern if required in the future (SD).
    /// </summary>
    public class PerformanceEventHandler
    {
        private readonly PerformanceEventHandlerConfiguration config;

        private readonly object syncLock = new object();
        private PerformanceCounterContainer[] counterContainers;
        private bool initialized;

        #region Constructors

        public PerformanceEventHandler(PerformanceEventHandlerConfiguration config)
        {
            this.config = config;
        }

        #endregion Constructors

        private void setupCategory()
        {
            if (!config.EnableSetupOnInitialization) return;

            var CCDC = new CounterCreationDataCollection();
            // Fill CounterCreationDataCollection
            bool categoryExists = PerformanceCounterCategory.Exists(config.CategoryName);

            foreach (PerfomanceCounterConfiguration counterConfig in config.Counters)
            {
                var c = new CounterCreationData();
                if (
                    !categoryExists ||
                    !PerformanceCounterCategory.CounterExists
                         (
                         counterConfig.Name,
                         config.CategoryName
                         ))
                {
                    c.CounterType = counterConfig.CounterType;
                    c.CounterName = counterConfig.Name;
                    c.CounterHelp = counterConfig.Description;
                    CCDC.Add(c);
                }
            }
            if (CCDC.Count > 0)
            {
                //				int i = 0;
                string suffix = String.Empty;
                // Create the category.
                //				while (i < config.MaxOfDynamicCategories)
                //				{
                if (!PerformanceCounterCategory.Exists(config.CategoryName + suffix))
                {
                    PerformanceCounterCategory.Create
                        (
                        config.CategoryName + suffix,
                        config.Description,
                        CCDC);
                    return;
                }

                Log.TraceData(Log.Source,TraceEventType.Error, 1001,
                                     "Category" + config.CategoryName +
                                     " already exists, and new counters can't be added. Delete the category " +
                                     " (unlodctr) and recreate with new set of counters"
                    );
            }
        }

        private void createCounters()
        {
            //counters = new PerformanceCounter[config.Counters.Count];
            counterContainers = new PerformanceCounterContainer[config.Counters.Count];
            // Create the counters.
            for (int i = 0; i < counterContainers.Length; i++)
            {
                var counter =
                    new PerformanceCounter
                        (
                        config.CategoryName,
                        config.Counters[i].Name,
                        false
                        );

                if (config.Counters[i].ClearOnStart) counter.RawValue = 0;

                counterContainers[i] =
                    new PerformanceCounterContainer
                        (
                        config.Counters[i],
                        counter
                        );
            }
        }

        public void SetupConfiguration()
        {
            lock (syncLock)
            {
                if (initialized) return;

                try
                {
                    // The absence of the section itself is not an error,
                    // but we just disable the handling through this handler in case of this
                    // or if there are no counters to write to.
                    if (config == null || config.Counters == null || config.Counters.Count == 0)
                    {
                        Enabled = false;
                        return;
                    }

                    // Setup counters category.
                    setupCategory();
                    createCounters();

                    initialized = true;
                }
                catch (Exception ex)
                {
                    Log.TraceData(Log.Source,TraceEventType.Error,
                                         1001,
                                         ex);
                    Enabled = false;
                    initialized = true;
                }
            }
        }

        public void HandleEvent(string eventId, object value)
        {
            if (!Enabled) return;

            SetupConfiguration();

            for (int i = 0; i < counterContainers.Length; i++)
            {
                PerformanceCounterType counterType = counterContainers[i].CounterConfiguration.CounterType;

                if (counterContainers[i].CounterConfiguration.EventId == eventId)
                {
                    if (!counterContainers[i].Applicable) continue;

                    if (config.Counters[i].CounterType == PerformanceCounterType.RateOfCountsPerSecond32)
                    {
                        counterContainers[i].Counter.Increment();
                        continue;
                    }
                    if (config.Counters[i].CounterType == PerformanceCounterType.AverageCount64
                        )
                    {
                        counterContainers[i].Counter.IncrementBy
                            (
                            Convert.ToInt64(value)/TimeSpan.TicksPerMillisecond
                            );
                        continue;
                    }
                    if (config.Counters[i].CounterType == PerformanceCounterType.AverageTimer32)
                    {
                        counterContainers[i].Counter.IncrementBy(Convert.ToInt64(value));
                        continue;
                    }
                    if (config.Counters[i].CounterType == PerformanceCounterType.AverageBase
                        )
                    {
                        //counterContainers[i].Counter.RawValue = 100;
                        counterContainers[i].Counter.Increment();
                        continue;
                    }
                    if (config.Counters[i].CounterType == PerformanceCounterType.NumberOfItems64
                        )
                    {
                        counterContainers[i].Counter.RawValue = Convert.ToInt64(value);
                        //counterContainers[i].Counter.Increment();
                        continue;
                    }
                }
            }
        }

        #region IEnabled Implementation

        private bool _enabled = true;

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

        public event EventHandler EnabledChanged = null;

        protected virtual void OnEnabledChanged()
        {
            if (EnabledChanged != null)
            {
                EnabledChanged(this, EventArgs.Empty);
            }
        }

        #endregion
    }
}