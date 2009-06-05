using System;
using System.Runtime.Serialization;

namespace Tools.Coordination.Core
{
    /// <summary>
    /// Encapsulates a message for the Work Indication Queue.
    /// </summary>
    [Serializable]
    public class WorkIndicationMessage : ISerializable
    {
        // Fields
        private int segmentId;
        private decimal jobId;

        /// <summary>
        /// Constructor
        /// </summary>
        public WorkIndicationMessage()
        {
        }


        /// <summary>
        /// Deserialization constructor.
        /// </summary>
        /// <param name="si">Serialization information.</param>
        /// <param name="context">Streaming context.</param>
        protected WorkIndicationMessage(SerializationInfo si, StreamingContext context)
        {
            jobId = si.GetDecimal("JobId");
            segmentId = si.GetInt32("OperationDivisionID");
        }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="operationDivisionID">Operation division ID of the database in which the order is stored.</param>
        /// <param name="orderID">ID of the order.</param>
        public WorkIndicationMessage(int operationDivisionID, decimal orderID)
        {
            jobId = orderID;
            segmentId = operationDivisionID;
        }


        /// <summary>
        /// Gets or sets the ID of the order / work item.
        /// </summary>
        public decimal JobId
        {
            get { return jobId; }
            set { jobId = value; }
        }


        /// <summary>
        /// Gets or sets the operation division ID of the order / work item.
        /// </summary>
        public int OperationDivisionID
        {
            get { return segmentId; }
            set { segmentId = value; }
        }

        #region ISerializable Members

        /// <summary>
        /// Gets this object's data for serialization.
        /// </summary>
        /// <param name="info">Serialization information.</param>
        /// <param name="context">Streaming context.</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("JobId", jobId);
            info.AddValue("OperationDivisionID", segmentId);
        }

        #endregion
    }
}