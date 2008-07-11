using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tools.Collaboration.Contracts;

namespace Tools.Collaboration.Subscriptions
{
    public class Subscriber : ISubscriber
    {
        private string name;
        private string url;

        public Subscriber(string url)
        {
            this.name = AppDomain.CurrentDomain.FriendlyName + ":" + Guid.NewGuid();
            this.url = url;
        }

        public Subscriber(string name, string url)
        {
            this.name = name;
            this.url = url;
        }

        #region ISubscriber Members

        public void Notify(string message)
        {
            throw new NotImplementedException();
        }

        public void Notify(string message, string activityId)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
