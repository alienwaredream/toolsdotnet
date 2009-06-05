using System;
using System.Xml.Serialization;

namespace Tools.Coordination.Scheduling
{
    /// <summary>
    /// Summary description for ScheduleDefinition.
    /// </summary>
    [Serializable]
    public class RecurrencePattern
    {
        [XmlAttribute]
        public int MillisecondRecurrence { get; set; }
    }
}