using System.Collections.Generic;
using System.Globalization;
using Tools.Coordination.ProducerConsumer;
using Tools.Logging;
using System.Diagnostics;
using Tools.Coordination.Core;
using System;

namespace Tools.Coordination.Sample.Implementation
{
    public class SampleJobConsumer
        : JobConsumer<Job>
    {

        #region Properties

        public PerformanceEventHandler PerformanceHandler { get; set; }



        #endregion

        public SampleJobConsumer
            (
            )
        {
            PerformanceHandler = new PerformanceEventHandler(
                new PerformanceEventHandlerConfiguration
                    {
                        Counters =
                            new List<PerfomanceCounterConfiguration>
                                {
                                    new PerfomanceCounterConfiguration
                                        {
                                            Name = "Family Preprocessing time, ms",
                                            ClearOnStart = true,
                                            CounterType = PerformanceCounterType.AverageTimer32,
                                            EventId = "Family Preprocessing time, ms",
                                            Description = "Time, in milliseconds, to preprocess single family."
                                        },
                                    new PerfomanceCounterConfiguration
                                        {
                                            Name = "Family Preprocessing time base, ms",
                                            ClearOnStart = true,
                                            CounterType = PerformanceCounterType.AverageBase,
                                            EventId = "Family Preprocessing time, ms",
                                            Description = "Time, in milliseconds, to preprocess single family."
                                        },
                                },
                        CategoryName = "Wds Eligibility Load Preprocessing #",
                        Description = "Eligibility load performance counters.",
                        EnableSetupOnInitialization = false,
                        MachineName = ".",
                        Name = ""
                    });

            PerformanceHandler.Enabled = true;
        }
        protected override void LogJobCompletion(JobProcessedEventArgs e)
        {
            PerformanceHandler.HandleEvent("Family Preprocessing time, ms",
                                           (e.WorkItem.CompletedAt - e.WorkItem.RetrievedAt).Ticks);
            Log.Source.TraceData(TraceEventType.Verbose,
                                 999,
                                 String.Format(CultureInfo.InvariantCulture,
                                               "Recurrence turnover time (ms) is {0},  ticks {1}",
                                               (e.WorkItem.CompletedAt - e.WorkItem.RetrievedAt).
                                                   TotalMilliseconds,
                                               (e.WorkItem.CompletedAt - e.WorkItem.RetrievedAt).Ticks));
        }
    }
}