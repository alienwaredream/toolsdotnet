using System;
using Tools.Coordination.WorkItems;
using Tools.Core;
using Tools.Core.Context;
using TIBCO.EMS;

namespace Tools.Coordination.Ems
{
    public class EmsWorkItem : RequestWorkItem
    {
        EmsReaderQueue queue;
        Message message;

        public EmsReaderQueue Queue { get { return queue; } }
        public Message Message { get { return message; } }

        #region Constructors

        public EmsWorkItem()
        {
        }

        public EmsWorkItem
            (
            decimal id,
            int externalEntityId,
            WorkItemState workItemState,
            SubmissionPriority submissionPriority,
            byte[] messageBody,
            bool assigned,
            bool isRetry,
            string ownerName,
            ContextIdentifier contextIdentifier,
            EmsReaderQueue queue,
            Message message
            )
            : base
                (
                id,
                externalEntityId,
                workItemState,
                submissionPriority,
                messageBody,
                assigned,
                isRetry,
                ownerName,
                contextIdentifier
                )
        {
            this.queue = queue;
            this.message = message;
        }

        #endregion Constructors
    }
}
