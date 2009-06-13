using System.ComponentModel;
using System.Diagnostics;

namespace Tools.Monitoring.WindowsService
{
    [RunInstaller(true)]
    public partial class Installer : Tools.Processes.Host.Installer
    {
        public Installer()
        {
            InitializeComponent();

            SetupCounters();

            SetupEventLog();
        }

        protected void SetupEventLog()
        {
            // Create an instance of an EventLogInstaller.
            var eventLogInstaller = new EventLogInstaller();

            // Set the source name of the event log.
            eventLogInstaller.Source = "Foris-Monitoring";

            // Set the event log that the source writes entries to.
            eventLogInstaller.Log = "Foris-Monitoring";

            // Add eventLogInstaller to the Installer collection.
            Installers.Add(eventLogInstaller);

        }

        protected void SetupCounters()
        {
            //TODO: (SD) All of that will be setup with Spring.net
            var performanceCounterInstaller =
                new PerformanceCounterInstaller
                    {
                        CategoryName = ServiceName
                    };

            Installers.Add(performanceCounterInstaller);

            #region Processing - item processing times

            var turnoverTimeCounterCreation = new CounterCreationData
                                                {
                                                    CounterName = "Command Avg Execution time, ms",
                                                    CounterHelp = "Time, in milliseconds, to process the single command.",
                                                    CounterType = PerformanceCounterType.AverageTimer32
                                                };
            performanceCounterInstaller.Counters.Add(turnoverTimeCounterCreation);

            var turnoverTimeAverageCounterCreation = new CounterCreationData
                                                                       {
                                                                           CounterName = "Command Avg Execution time base, ms",
                                                                           CounterHelp =
                                                                               "Average time, in milliseconds, to process single command.",
                                                                           CounterType =
                                                                               PerformanceCounterType.AverageBase
                                                                       };
            performanceCounterInstaller.Counters.Add(turnoverTimeAverageCounterCreation);

            // Add a counter to collection of  performanceCounterInstaller.

            #endregion Processing - item processing time

            #region Processing - Statistics - commands

            var commandsTotalCounterCreation = new CounterCreationData {
                CounterName = "New Commands",
                CounterType = PerformanceCounterType.NumberOfItems32,
                CounterHelp = "Commands that came within specified period"
                };

            performanceCounterInstaller.Counters.Add(commandsTotalCounterCreation);

            var commandsUnprocessedCounterCreation = new CounterCreationData {
                CounterName = "Commands in process",
                CounterType = PerformanceCounterType.NumberOfItems32,
                CounterHelp = "Total of commands that are being currently processed"
                };
            performanceCounterInstaller.Counters.Add(commandsUnprocessedCounterCreation);

            var commandsProcessedCounterCreation = new CounterCreationData {
                CounterName = "Completed commands",
                CounterType = PerformanceCounterType.NumberOfItems32,
                CounterHelp = "Total of commands that have been processed within the defined period"
                };

            performanceCounterInstaller.Counters.Add(commandsProcessedCounterCreation);


            var commandsFailedCounterCreation = new CounterCreationData
            {
                CounterName = "Failed commands",
                CounterType = PerformanceCounterType.NumberOfItems32,
                CounterHelp = "Total of commands that failed within the defined period"
            };

            performanceCounterInstaller.Counters.Add(commandsFailedCounterCreation);

            #endregion

        }
    }
}