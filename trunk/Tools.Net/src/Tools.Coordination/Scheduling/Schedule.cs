using System;
using Tools.Core;

namespace Tools.Coordination.Scheduling
{
    /// <summary>
    /// Single Thread use is supposed for a moment (SD).
    /// </summary>
    [Serializable]
    public abstract class Schedule : Descriptor
    {
        private DateTime _nextRunTime = DateTime.MinValue;

        public ScheduleDefinition Definition { get; set; }

        public DateTime NextRunTime
        {
            get { return _nextRunTime; }
        }

        public virtual TimeSpan TimeDiff2Run
        {
            get
            {
                //(SD) first fix the date
                DateTime now = DateTime.UtcNow;
                // if next time is in the past compared with now, return zero time span
                if (_nextRunTime < now) return TimeSpan.Zero;
                // else return the diff
                return (_nextRunTime - now);
            }
        }

        public abstract DateTime SetNextRunTime();

        protected virtual DateTime SetNextRunTime(DateTime nextRunTime)
        {
            return _nextRunTime = nextRunTime;
        }

        public virtual DateTime SetForImmidiateRun()
        {
            return SetNextRunTime(DateTime.UtcNow);
        }
    }
}