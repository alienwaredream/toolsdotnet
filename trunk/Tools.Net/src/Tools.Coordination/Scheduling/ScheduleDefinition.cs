using System;
using Tools.Coordination.Scheduling;

namespace Tools.Coordination.Scheduling
{
    /// <summary>
    /// Summary description for ScheduleDefinition.
    /// </summary>
    [Serializable]
    public class ScheduleDefinition
    {
        #region Properties

        public DateTime StartDate
        {
            get;
            set;
        }

        public DateTime EndDate
        {
            get;
            set;
        }

        public RecurrencePattern Recurrence { get; set; }

        #endregion Properties
    }
}