using System;
using System.Collections.Generic;

namespace Tools.Commands.Implementation
{
    public class MessageShim
    {
        private Dictionary<string, string> properties = new Dictionary<string, string>();
        public string CorrelationId { get; set; }
        public Dictionary<string, string> Properties { get { return properties; } set { properties = value; } }
        public string Text { get; set; }
    }
}