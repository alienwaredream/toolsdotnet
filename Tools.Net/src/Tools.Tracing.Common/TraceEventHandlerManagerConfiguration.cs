using System;
using System.Xml.Serialization;
using Tools.Core;

namespace Tools.Tracing.Common
{
    /// <summary>
    /// Summary description for TraceEventHandlerManagerConfiguration.
    /// </summary>
    [Serializable]
    public class TraceEventHandlerManagerConfiguration : Descriptor
    {
        private string _fallbackLogMaxFileSizeBytes = "2000000";
        private LocationDetailLevel _locationDetailLevel = LocationDetailLevel.Basic;

        public TraceEventHandlerManagerConfiguration()
        {
            Handlers = new TraceEventHandlerConfigurationCollection();
        }

        public TraceEventHandlerConfigurationCollection Handlers { get; set; }

        [XmlAttribute]
        public string BaseEventSourceName { get; set; }

        [XmlAttribute]
        public LocationDetailLevel LocationDetailLevel
        {
            get { return _locationDetailLevel; }
            set { _locationDetailLevel = value; }
        }

        [XmlAttribute]
        public string FallbackLogMaxFileSizeBytes
        {
            get { return _fallbackLogMaxFileSizeBytes; }
            set { _fallbackLogMaxFileSizeBytes = value; }
        }

        [XmlAttribute]
        public string FallbackLogRootLocation { get; set; }
    }
}