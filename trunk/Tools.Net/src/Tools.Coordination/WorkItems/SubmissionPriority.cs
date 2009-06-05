namespace Tools.Coordination.WorkItems
{
    /// <summary>
    /// Provides the priority with which the item should be processed.
    /// As for job sending scenario Normal corresponds to the regular
    /// submission and Low corresponds to pending submission.
    /// </summary>
    public enum SubmissionPriority
    {
        Unassigned = 0,
        /// <summary>
        /// High priority submission should be applied
        /// </summary>
        High = 1,
        /// <summary>
        /// Normal priority submission should be applied
        /// </summary>
        Normal = 2,
        /// <summary>
        /// 
        /// </summary>
        //BellowNormal = 30,
        /// <summary>
        /// Low priority submission should be applied
        /// </summary>
        Low = 3
    }
}