using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using Tools.Common.Exceptions;
using Tools.Common.Asserts;
using System.Diagnostics;
using Tools.Common.Logging;

namespace Tools.Common.Wcf
{
    public class SafeClientInvoker<T> where T : class
    {
        #region Fields
        private ClientBase<T> _client; 
        #endregion

        #region Constructors
        public SafeClientInvoker(ClientBase<T> client)
        {
            _client = client;
        } 
        #endregion

        #region Methods
        
        #region Public methods

        
        public TResult InvokeWithResult<TResult>(Func<TResult> call, bool closeAfterCall)
        {
            ErrorTrap.AddAssertion(_client != null, "_client != null");
            ErrorTrap.AddAssertion(call != null, "call != null");

            ErrorTrap.RaiseTrappedErrors<ArgumentNullException>();

            try
            {
                TResult result = call();

                try
                {
                    if (closeAfterCall) _client.Close();
                }
                catch (Exception ex)
                {
                    HandleCloseException(ex);
                }

                return result;

            }
            catch (Exception ex) // TODO: (SD) Expand according to the logic 
            {
                HandleException(ex, true);
                throw ex;
            }
        }


        /// <summary>
        /// Invokes the client provided in the constructor with closing it after the call
        /// </summary>
        /// <typeparam name="TResult">Return value type</typeparam>
        /// <param name="call">Method delegate to call</param>
        /// <returns></returns>
        public TResult InvokeWithResult<TResult>(Func<TResult> call)
        {
            return InvokeWithResult<TResult>(call, true);
        }

        public void Invoke(Action call, bool closeAfterCall)
        {
            ErrorTrap.AddAssertion(_client != null, "_client != null");
            ErrorTrap.AddAssertion(call != null, "call != null");

            ErrorTrap.RaiseTrappedErrors<ArgumentNullException>();

            try
            {
                call();

                try
                {
                    if (closeAfterCall) _client.Close();
                }
                catch (Exception ex)
                {
                    HandleCloseException(ex); //(SD) Will call abort
                }
            }
            catch (Exception ex) // TODO: (SD) Expand according to the logic 
            {
                HandleException(ex, true); // Calls abort when second param is true

                throw ex; // It will never get here as there is a throw in the HandleException
            }
        }

        public void Invoke(Action call)
        {
            Invoke(call, true);
        }

        #endregion

        #region Private methods
        
        private void HandleException(Exception ex, bool abort)
        {
            //TODO: (SD) TLS context passing for logging, but that can be done on the level of EL then.

            #region Add extra info into the message
            if (_client != null && ex != null) ex.Data.Add("TargetEndpoint", String.Format(
                "Exception during calling enpoint {0}.{1}", _client.Endpoint.Address.Uri.AbsoluteUri,
                Environment.NewLine));
            #endregion

            #region Abort the client if required
            if (abort)
            {
                try
                {
                    if (_client != null) _client.Abort();
                }
                catch (Exception exx)
                {
                    if (_client != null && ex != null) ex.Data.Add("AbortExceptionData", String.Format(
                        "Exception during trying to abort the client for endpoint{0}. Review the problem with network communication:{1}{2}{1}",
                        _client.Endpoint.Address.Uri.AbsoluteUri,
                        Environment.NewLine,
                        exx.ToString()));
                }
            }
            #endregion

            #region Invoke external handlers
            Log.Source.TraceData(TraceEventType.Error, 4001, ex);
            #endregion

            throw ex;
        }

        private void HandleCloseException(Exception ex)
        {
            // Add extra info into the message
            if (_client != null && ex != null) ex.Data.Add("TargetEndpoint", String.Format(
                "Exception while trying to close the client {0} after call to {1}", _client.Endpoint.Name,
                _client.Endpoint.Address.Uri.AbsoluteUri));
            // (SD) Right now, it aborts unconditionaly, think about cases when this abort would be
            // conditional in case of the close exception (Faulted state of the channel)
            _client.Abort();
            // Invoke external handlers
            Log.Source.TraceData(TraceEventType.Error, 4002, ex);
        }
        
        #endregion

        #endregion
    }
}
