using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tools.Collaboration.Contracts;

namespace Tools.Collaboration.Contracts
{
    public class NotificationAsyncState
    {
        public SubscriberData Subscriber { get; set; }
        public string Message { get; set; }
        public string ActivityId { get; set; }
    }
}
