using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Tools.Tests.Helpers
{
    /// <summary>
    /// Consider itself to be completed with set waithandle
    /// </summary>
    public class TrivialAsyncResultMock : IAsyncResult
    {
        // set to true so should just pass on
        private WaitHandle waitHandle = new ManualResetEvent(true);

        public TrivialAsyncResultMock()
        {

        }
        public TrivialAsyncResultMock(object state) : this()
        {
            AsyncState = state;
        }
        #region IAsyncResult Members

        public object AsyncState
        {
            get;
            set;
        }

        public System.Threading.WaitHandle AsyncWaitHandle
        {
            get { return waitHandle; }
        }

        public bool CompletedSynchronously
        {
            get;
            set;
        }

        public bool IsCompleted
        {
            get;
            set;
        }

        #endregion
    }
}
