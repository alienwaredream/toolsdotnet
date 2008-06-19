using Tools.Collaboration.Contracts;

namespace Tools.Collaboration.Publishing
{
    public class Publisher : IPublisher
    {

        #region IPublisher Members

        public string GetName()
        {
            throw new System.NotImplementedException();
        }

        public void Publish(string message)
        {
            throw new System.NotImplementedException();
        }

        public void Publish(string message, string activityId)
        {
            throw new System.NotImplementedException();
        }

        public void AddSubscriber(ISubscriber subscriber)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
