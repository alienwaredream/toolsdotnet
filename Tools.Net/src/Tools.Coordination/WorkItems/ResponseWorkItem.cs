using System;
using Tools.Core.Context;

namespace Tools.Coordination.WorkItems
{
    /// <summary>
    /// Work item used for requests.
    /// </summary>
    [Serializable]
    public class ResponseWorkItem : WorkItem
    {
        #region Public properties overrides

        public override WorkItemType Type
        {
            get { return WorkItemType.Response; }
        }

        #endregion Public properties overrides

        #region Constructors

        public ResponseWorkItem()
        {
        }

        public ResponseWorkItem
            (
            long id,
            int externalEntityId,
            WorkItemState workItemState,
            SubmissionPriority submissionPriority,
            byte[] messageBody,
            bool assigned,
            bool isRetry,
            string ownerName,
            ContextIdentifier contextIdentifier
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
        }

        #endregion Constructors
    }
}