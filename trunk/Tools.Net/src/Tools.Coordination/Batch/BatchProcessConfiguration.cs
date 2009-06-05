namespace Tools.Coordination.Batch
{
    public class BatchProcessConfiguration
    {
        public int RecurrenceMilliseconds { get; set; }
        public int JobBatchSize { get; set; }
        public int UnlockReservationMilliseconds { get; set; }
    }
}