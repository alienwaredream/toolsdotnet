using System;
using System.Runtime.Remoting.Messaging;

namespace Tools.Coordination.Tests
{
    public class AsyncBench
    {
        private int param;

        internal int Param { get { return param;}}

        internal readonly Func<int, int> method;

        public AsyncBench(int param)
        {
            this.param = param;
            this.method = Method;
        }

        public AsyncBench(int param, Func<int, int> method) : this(param)
        {
            this.method = method;
        }

        public IAsyncResult BeginMethod()
        {
            return method.BeginInvoke(param, AsyncMethodCallback, new State {Field = param});
        }
        private int Method(int n)
        {
            return n + 1;
        }
        public void AsyncMethodCallback(IAsyncResult ar)
        {
            var asyncResult = ar as AsyncResult;

            try
            {
                param = (asyncResult.AsyncDelegate as Func<int, int>).EndInvoke(ar);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
    public class State { public int Field { get; set; } }
}
