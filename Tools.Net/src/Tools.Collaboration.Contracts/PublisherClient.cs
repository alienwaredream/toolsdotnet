using System;

namespace Tools.Collaboration.Contracts
{

    namespace Tools.Common.DataTables
    {

        public partial class PublisherClient : System.ServiceModel.ClientBase<IPublisher>, IPublisher
        {

            /// <summary>
            /// Initializes a new instance of the <see cref="SystemMessageServiceClient"/> class.
            /// </summary>
            public PublisherClient()
            {
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="SystemMessageServiceClient"/> class.
            /// </summary>
            /// <param name="endpointConfigurationName">Name of the endpoint configuration.</param>
            public PublisherClient(string endpointConfigurationName)
                :
                    base(endpointConfigurationName)
            {
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="SystemMessageServiceClient"/> class.
            /// </summary>
            /// <param name="endpointConfigurationName">Name of the endpoint configuration.</param>
            /// <param name="remoteAddress">The remote address.</param>
            public PublisherClient(string endpointConfigurationName, string remoteAddress)
                :
                    base(endpointConfigurationName, remoteAddress)
            {
            }

            #region IPublisher Members

            public string GetName()
            {
                return base.Channel.GetName();
            }

            public void Publish(string message)
            {
                base.Channel.Publish(message);
            }

            public void Publish(string message, string activityId)
            {
                base.Channel.Publish(message, activityId);
            }

            public void AddSubscriber(SubscriberData subscriber)
            {
                base.Channel.AddSubscriber(subscriber);
            }

            #endregion
        }
    }

}
