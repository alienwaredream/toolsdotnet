using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;

namespace Tools.Wcf.Host
{

    public class HostedServicesEnumerator : IStatusQuerable
    {
        #region IStatusQuerable Members

        [OperationBehavior(Impersonation=ImpersonationOption.Required)]
        public string QueryForStatus()
        {
            return WcfHostProgram.QueryForServices();
        }

        #endregion
    }
}
