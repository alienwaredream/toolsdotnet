using System;
using System.Xml.Serialization;
using Tools.Core;

namespace Tools.Tracing.Common
{
    /// <summary>
    /// Summary description for BaseEvent.
    /// </summary>
    [Serializable]
    public class EventIdentifier : Descriptor, IIdentifierHolder
    {
        #region IIdentifierHolder Members

        [XmlAttribute]
        public int Id { get; set; }

        #endregion
    }
}