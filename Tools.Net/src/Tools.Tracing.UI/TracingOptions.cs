using System;
using System.ComponentModel;
using System.Xml.Serialization;
using Tools.Core.Utils;

namespace Tools.Tracing.UI
{
    /// <summary>
    /// Summary description for TracingOptions.
    /// </summary>
    [Serializable]
    public class TracingOptions
    {
        public TracingOptions()
        {
            // Set defaults where possible
            XQueryFilePath = AssemblyInfoUtility.ApplicationSettingsCommonDirectory +
                             @"\DefaultXQueryLibrary.xml";
            XPathFilePath = AssemblyInfoUtility.ApplicationSettingsCommonDirectory +
                            @"\DefaultXPathLibrary.xml";
            LogToTraceControl = true;
            PreCacheEvents = true;
        }

        [Category("Filters")]
        [Description("Filtering is enabled if true and disabled otherwise.")]
        [XmlAttribute]
        public bool EnableFilter { get; set; }

        [Category("Behavior"),
         Description("If true events are written to the internal collection, so their storage to file is faster then."),
         XmlAttribute]
        public bool PreCacheEvents { get; set; }

        [Category("Behavior")]
        [Description(
            "If greater than zero, append events to the file path provided when events count exceeds the field value")]
        [XmlAttribute]
        public bool AutoFlushToFileCount { get; set; }

        [Category("Behavior")]
        [Description("File path to append events to.")]
        [XmlAttribute]
        public string AutoFlushFilePath { get; set; }

        [Category("Behavior")]
        [Description("If greater than zero, list of the events is automaticaly cleared when exceeding the count value.")
        ]
        [XmlAttribute]
        public int AutoClearCount { get; set; }

        [Category("Behavior")]
        [Description("If true events in the monitor are saved as a workspace part.")]
        [XmlAttribute]
        public bool EventsAsWorkSpacePart { get; set; }

        [Category("Behavior"), Description("If true adds events."), XmlAttribute]
        public bool LogToTraceControl { get; set; }

        [Category("Filters"), Description("Path to the XPath library filters."), XmlAttribute]
        public string XPathFilePath { get; set; }

        [Category("Filters"), Description("Path to the XQuery library filters."), XmlAttribute]
        public string XQueryFilePath { get; set; }
    }
}