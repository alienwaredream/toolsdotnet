using Tools.Coordination.WorkItems;
using Tools.Core;

namespace Tools.Coordination.ProducerConsumer
{
    public class ConsumerConfiguration : Descriptor
    {
        private SubmissionType submissionType = SubmissionType.RegularSubmission;

        public int SubmissionInterval { get; set; }
        public SubmissionType SubmissionType
        {
            get { return submissionType;}
            set { submissionType = value; }
        }

        public int MaxTotalSubmittedItemsCount { get; set;}
        /// <summary>
        /// Period of time given to the job processor to accept the request for work.
        /// </summary>
        public int SubmissionQueuingProcessTimeout { get; set; }

        public ConsumerConfiguration()
        {
            MaxTotalSubmittedItemsCount = 1;
            SubmissionQueuingProcessTimeout = 20000;
        }
    }
}