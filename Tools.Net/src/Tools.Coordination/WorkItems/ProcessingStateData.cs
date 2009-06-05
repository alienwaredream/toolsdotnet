using System;

namespace Tools.Coordination.WorkItems
{

    #region ProcessingStateData class

    /// <summary>
    /// Summary description for ProcessingStateData.
    /// </summary>
    [Serializable]
    public class ProcessingStateData
    {
        #region Fields

        private readonly WorkItemSlotCollection _retrievedItems;
        private readonly WorkItemCollection _submittedItems;

        #endregion

        #region Properties

        public WorkItemSlotCollection RetrievedItems
        {
            get { return _retrievedItems; }
        }

        public WorkItemCollection SubmittedItems
        {
            get { return _submittedItems; }
        }

        #endregion

        #region Constructors

        public ProcessingStateData
            (
            WorkItemSlotsConfiguration
                configuration
            )
        {
            _retrievedItems = WorkItemSlotCollection.Create(configuration);
            _submittedItems = new WorkItemCollection();
        }

        #endregion
    }

    #endregion
}