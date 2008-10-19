namespace Tools.Core.Threading
{
    //TODO: (SD) Optimize further for contention (locks optimizations)
    /// <summary>
    /// Summary description for SynchronizedCounter.
    /// </summary>
    public class SynchronizedCounter
    {
        #region Fields

        private readonly object _syncRoot = new object();
        private volatile int _value;

        public event VoidDelegate Zeroed;
        //{
        //    add
        //    {
        //     //lock(Zeroed)
        //     //{
        //         Zeroed += value;
        //     //}
        //    }
        //    remove
        //    {
        //        Zeroed -= value;
        //    }
        //}

        #endregion Fields

        #region Properties

        public int SyncValue
        {
            get
            {
                lock (_syncRoot)
                {
                    return _value;
                }
            }
            set
            {
                lock (_syncRoot)
                {
                    _value = value;
                }         
            }
        }

        public int Value
        {
            get { return _value; }
        }

        #endregion Properties

        #region Constructors

        public SynchronizedCounter()
        {
            _value = 0;
        }

        #endregion Constructors

        #region Methods

        private void OnZero()
        {
            if (Zeroed!=null)
            {
                Zeroed();
            }
        }

        public void SyncIncrement()
        {
            lock (_syncRoot)
            {
                Increment();
                if (_value == 0)
                {
                    OnZero();
                }
            }
        }

        public void SyncDecrement()
        {
            lock (_syncRoot)
            {
                Decrement();
                if (_value == 0)
                {
                    OnZero();
                }
            }
        }

        public void Increment()
        {
            _value++;
        }

        public void Decrement()
        {
            _value--;
        }

        #endregion Methods
    }
}