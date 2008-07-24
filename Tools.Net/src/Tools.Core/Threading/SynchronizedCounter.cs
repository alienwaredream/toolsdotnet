using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tools.Core.Threading
{
    //TODO: (SD) Optimize further for contention (locks optimizations)
    /// <summary>
    /// Summary description for SynchronizedCounter.
    /// </summary>
    public class SynchronizedCounter
    {
        #region Fields

        private int _value;
        private object _syncRoot = new object();

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
        }

        public int Value
        {
            get
            {
                return _value;
            }
        }

        #endregion Properties

        #region Constructors

        public SynchronizedCounter()
        {
            _value = 0;
        }


        #endregion Constructors

        #region Methods

        public void SyncIncrement()
        {
            lock (_syncRoot)
            {
                _value++;
            }
        }

        public void SyncDecrement()
        {
            lock (_syncRoot)
            {
                _value--;
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
