using System;

namespace Tools.Coordination.WorkItems
{
    /// <summary>
    /// Summary description for WorkItemLookupToken.
    /// </summary>
    [Serializable]
    public class WorkItemLookupToken
    {
        public int ExternalEntityId { get; set; }
        public decimal WorkItemId { get; set; }
        public SubmissionPriority Priority { get; set; }
    }
}