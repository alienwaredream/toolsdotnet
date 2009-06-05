using System;
using System.Xml.Serialization;

namespace Tools.Coordination.WorkItems
{
    /// <summary>
    /// Summary description for PrioritySlotsConfiguration.
    /// </summary>
    [Serializable]
    public class PrioritySlotsConfiguration
    {
        public SubmissionPriority SubmissionPriority { get; set; }

        public int Count { get; set; }

        public int Timeout { get; set; }
    }
}