using System.ServiceModel;

namespace Tools.Collaboration.Contracts
{
    [ServiceContract(Namespace="http://http://code.google.com/p/toolsdotnet/")]
    public interface IPublisher
    {
        [OperationContract()]
        string GetName();
        [OperationContract()]
        void Publish(string message);
        [OperationContract(Name="PublishUnderAcitivy")]
        void Publish(string message, string activityId);
        [OperationContract()]
        void AddSubscriber(ISubscriber subscriber);
    }
}
