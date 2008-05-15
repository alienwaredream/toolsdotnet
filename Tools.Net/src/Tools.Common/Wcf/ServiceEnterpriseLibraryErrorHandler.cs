using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;
using System.Diagnostics;
using Tools.Common.Logging;

namespace Tools.Common.Wcf
{
    public class ServiceEnterpriseLibraryErrorHandler : IErrorHandler
    {
        #region Private fields

        private string handlingPolicyName;

        #endregion

        public ServiceEnterpriseLibraryErrorHandler()
        {
            this.handlingPolicyName = "Default";
        }
        public ServiceEnterpriseLibraryErrorHandler(string handlingPolicyName) : this()
        {
            this.handlingPolicyName = handlingPolicyName;
        }
        public void ProvideFault(Exception error, MessageVersion version, ref Message fault) { }

        public bool HandleError(Exception error)
        {
            Log.Source.TraceData(TraceEventType.Error, 5001, error);
            return true;
        }
    }
}
