using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tools.Collaboration.Contracts;

namespace Tools.Collaboration.Publishing
{
    internal class SubscriberProxy : ISubscriber
    {
        SubscriberData subscriberData;

        internal SubscriberProxy(SubscriberData data)
        {
            this.subscriberData = data;
        }

        #region ISubscriber Members

        public string GetName()
        {
            throw new NotImplementedException();
        }

        public string GetUrl()
        {
            throw new NotImplementedException();
        }

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
