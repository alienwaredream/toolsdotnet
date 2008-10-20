using System.ServiceModel;

namespace Tools.Wcf.Host
{
    public class HostedServicesEnumerator : IStatusQuerable
    {
        #region IStatusQuerable Members

        [OperationBehavior(Impersonation = ImpersonationOption.Required)]
        public string QueryForStatus()
        {
            return WcfHostProgram.QueryForServices();
        }

        #endregion
    }
}