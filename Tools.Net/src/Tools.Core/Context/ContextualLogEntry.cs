using System;
using System.Runtime.Serialization;
using System.Text;

namespace Tools.Core.Context
{
    [Serializable]
    [DataContract]
    public class ContextualLogEntry : IContextIdentifierHolder
    {
        [DataMember]
        public string Message { get; set; }

        #region IContextIdentifierHolder Members

        [DataMember]
        public ContextIdentifier ContextIdentifier { get; set; }

        #endregion

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append("Message:").Append(Message);

            return sb.ToString();
        }
    }
}