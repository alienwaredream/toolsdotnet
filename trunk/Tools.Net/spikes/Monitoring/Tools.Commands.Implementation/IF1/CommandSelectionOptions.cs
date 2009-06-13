using System;

namespace Tools.Commands.Implementation
{
    public class CommandSelectionOptions
    {
        string partitionName = "default";
        Decimal batchSize = 1;
        Int32 timeout = 10000;
        string activityId = Guid.NewGuid().ToString();

        public string PartitionName { get { return partitionName; } set { partitionName = value; } }
        public string BatchId { get; set; }
        public Decimal CommandTypeId { get; set; }
        public Decimal BatchSize { get { return batchSize; } set { batchSize = value; } }
        public string MachineName { get; set; }
        public string ActivityId { get { return activityId; } set { activityId = value; } }
        public Int32 Timeout { get { return timeout; } set { timeout = value; } }
    }
}