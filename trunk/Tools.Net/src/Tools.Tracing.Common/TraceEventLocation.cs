using System;
using System.Xml.Serialization;

namespace Tools.Tracing.Common
{
    // TODO: Provide ToString() with nice formatting (SD)
    /// <summary>
    /// Summary description for TraceEventLocation.
    /// </summary>
    [Serializable]
    public sealed class TraceEventLocation
    {
        private string _hostName;
        private string _modulePath;
        private string _threadName;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public TraceEventLocation()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="className"></param>
        /// <param name="hostName"></param>
        /// <param name="processId"></param>
        public TraceEventLocation(
            string source,
            string methodName,
            string className,
            string hostName,
            string processId,
            string appDomainName,
            string modulePath,
            string threadName
            )
            : this()
        {
            Source = source;
            MethodName = methodName;
            ClassName = className;
            _hostName = hostName;
            ProcessId = processId;
            AppDomainName = appDomainName;
            _modulePath = modulePath;
            _threadName = threadName;
        }

        [XmlAttribute]
        public string Source { get; set; }

        [XmlAttribute]
        public string MethodName { get; set; }

        [XmlAttribute]
        public string ClassName { get; set; }

        [XmlAttribute]
        public string ModulePath
        {
            get { return _modulePath; }
            set { _modulePath = value; }
        }

        [XmlAttribute]
        public string HostName
        {
            get { return _hostName; }
            set { _hostName = value; }
        }

        [XmlAttribute]
        public string ProcessId { get; set; }

        [XmlAttribute]
        public string AppDomainName { get; set; }

        [XmlAttribute]
        public string ThreadName
        {
            get { return _threadName; }
            set { _threadName = value; }
        }

        public override string ToString()
        {
            return
                "Host=" + _hostName +
                ";Module=" + _modulePath +
                ";ThreadName=" + Environment.NewLine + _threadName;
            //+ System.Environment.NewLine + _methodName;
        }
    }
}