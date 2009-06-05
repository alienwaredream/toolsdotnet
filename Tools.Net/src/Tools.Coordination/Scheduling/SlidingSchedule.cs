using System;

namespace Tools.Coordination.Scheduling
{
    /// <summary>
    /// Single thread assumption for a moment (SD)
    /// </summary>
    public class SlidingSchedule : Schedule
    {

        public override DateTime SetNextRunTime()
        {
            return base.SetNextRunTime
                (
                DateTime.UtcNow + TimeSpan.FromMilliseconds(Definition.Recurrence.MillisecondRecurrence)
                );
        }
    }
}