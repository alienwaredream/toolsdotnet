using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Tools.Collaboration.Contracts
{
    [DataContract()]
    public class SubscriberData
    {
        [DataMember()]
        public string Url { get; set; }
        [DataMember()]
        public string Name { get; set; }
    }
}
