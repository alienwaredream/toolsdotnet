using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel.Dispatcher;

namespace Tools.Common.Wcf
{
    internal class ExceptionHandlingOperationInvoker : IOperationInvoker
    {
        #region Private fields

        private IOperationInvoker _innerOperationInvoker;

        #endregion

        #region Constructors
        public ExceptionHandlingOperationInvoker()
        {
        }

        public ExceptionHandlingOperationInvoker(IOperationInvoker innerOperationInvoker)
        {
            this._innerOperationInvoker = innerOperationInvoker;
        }

        #endregion

        #region IOperationInvoker Members
        object[] IOperationInvoker.AllocateInputs()
        {
            return this._innerOperationInvoker.AllocateInputs();
        }

        object IOperationInvoker.Invoke(object instance, object[] inputs, out object[] outputs)
        {
            object retVal = null;

            retVal = this._innerOperationInvoker.Invoke(instance, inputs, out outputs);

            return retVal;
        }

        IAsyncResult IOperationInvoker.InvokeBegin(object instance, object[] inputs, AsyncCallback callback, object state)
        {
            return this._innerOperationInvoker.InvokeBegin(instance, inputs, callback, state);
        }
        object IOperationInvoker.InvokeEnd(object instance, out object[] outputs, IAsyncResult result)
        {
            return this._innerOperationInvoker.InvokeEnd(instance, out outputs, result);
        }
        bool IOperationInvoker.IsSynchronous
        {
            get { return this._innerOperationInvoker.IsSynchronous; }
        }
        #endregion
    }
}
