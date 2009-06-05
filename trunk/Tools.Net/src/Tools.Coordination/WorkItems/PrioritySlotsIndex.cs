using System.Xml.Serialization;

namespace Tools.Coordination.WorkItems
{
    /// <summary>
    /// Summary description for PrioritySlotsConfiguration.
    /// </summary>
    public class PrioritySlotsIndex
    {
        private SubmissionPriority _submissionPriority = SubmissionPriority.Unassigned;

        public PrioritySlotsIndex
            (
            SubmissionPriority submissionPriority,
            int startIndex,
            int endIndex
            )
        {
            _submissionPriority = submissionPriority;
            StartIndex = startIndex;
            EndIndex = endIndex;
        }

        [XmlAttribute]
        public SubmissionPriority SubmissionPriority
        {
            get { return _submissionPriority; }
            set { _submissionPriority = value; }
        }

        [XmlAttribute]
        public int StartIndex { get; set; }

        [XmlAttribute]
        public int EndIndex { get; set; }
    }
}