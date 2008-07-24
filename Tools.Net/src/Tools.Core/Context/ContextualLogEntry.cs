using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Tools.Core.Context
{
    [Serializable()]
    [DataContract()]
    public class ContextualLogEntry : IContextIdentifierHolder
    {
        [DataMember()]
        public string Message { get; set; }

        #region IContextIdentifierHolder Members

        [DataMember()]
        public ContextIdentifier ContextIdentifier
        {
            get;
            set;
        }

        #endregion

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Message:").Append(Message);

            return sb.ToString();
        }
    }
}
