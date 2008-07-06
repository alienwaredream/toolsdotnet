using System.ServiceModel;

namespace Tools.Collaboration.Contracts
{
    [ServiceContract(Namespace = "http://http://code.google.com/p/toolsdotnet/")]
    public interface ISubscriber
    {
        [OperationContract()]
        string GetName();
        [OperationContract()]
        string GetUrl();
        [OperationContract()]
        void Notify(string message);
        [OperationContract(Name="NotifyUnderActivity")]
        void Notify(string message, string activityId);
    }
}
