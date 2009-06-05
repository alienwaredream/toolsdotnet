namespace Tools.Failover
{
    /// <summary>
    /// Single thread use only.
    /// </summary>
    public class FailuresCounter
    {
        private int _failedCount;
        private int _normalCount;

        public FailuresCounter()
        {
        }

        public FailuresCounter
            (
            int normalCount,
            int failedCount
            )
        {
            _normalCount = normalCount;
            _failedCount = failedCount;
        }

        public int NormalCount
        {
            get { return _normalCount; }
            set { _normalCount = value; }
        }

        public int FailedCount
        {
            get { return _failedCount; }
            set { _failedCount = value; }
        }

        public void Reset()
        {
            _failedCount = 0;
            _normalCount = 0;
        }
    }
}