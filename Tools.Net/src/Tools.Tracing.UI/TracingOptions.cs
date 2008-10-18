using System;
using System.Xml.Serialization;
using System.ComponentModel;

using Tools.Core.Utils;

namespace Tools.Tracing.UI
{
	/// <summary>
	/// Summary description for TracingOptions.
	/// </summary>
	[Serializable()]
	public class TracingOptions
	{
		private bool _enableFilter = false;
		private bool _preCacheEvents = true;
		private bool _autoFlushToFileCount = false;
		private string _autoFlushFilePath = null;
		private int _autoClearCount = 0;
		private bool _eventsAsWorkSpacePart = false;
		private bool _logToTraceControl = true;
		private string _xPathFilePath =
			AssemblyInfoUtility.ApplicationSettingsCommonDirectory +
			@"\DefaultXPathLibrary.xml";
		private string _xQueryFilePath =
			AssemblyInfoUtility.ApplicationSettingsCommonDirectory +
			@"\DefaultXQueryLibrary.xml";

		[Category("Filters")]
		[Description("Filtering is enabled if true and disabled otherwise.")]
		[XmlAttribute()]
		public bool EnableFilter
		{
			get
			{
				return _enableFilter;
			}
			set
			{
				_enableFilter = value;
			}
		}
		[Category("Behavior")]
		[Description("If true events are written to the internal collection, so their storage to file is faster then.")]
		[XmlAttribute()]
		public bool PreCacheEvents
		{
			get
			{
				return _preCacheEvents;
			}
			set
			{
				_preCacheEvents = value;
			}
		}
		[Category("Behavior")]
		[Description("If greater than zero, append events to the file path provided when events count exceeds the field value")]
		[XmlAttribute()]
		public bool AutoFlushToFileCount
		{
			get
			{
				return _autoFlushToFileCount;
			}
			set
			{
				_autoFlushToFileCount = value;
			}
		}
		[Category("Behavior")]
		[Description("File path to append events to.")]
		[XmlAttribute()]
		public string AutoFlushFilePath
		{
			get
			{
				return _autoFlushFilePath;
			}
			set
			{
				_autoFlushFilePath = value;
			}
		}
		[Category("Behavior")]
		[Description("If greater than zero, list of the events is automaticaly cleared when exceeding the count value.")]
		[XmlAttribute()]
		public int AutoClearCount
		{
			get
			{
				return _autoClearCount;
			}
			set
			{
				_autoClearCount = value;
			}
		}
		[Category("Behavior")]
		[Description("If true events in the monitor are saved as a workspace part.")]
		[XmlAttribute()]
		public bool EventsAsWorkSpacePart
		{
			get
			{
				return _eventsAsWorkSpacePart;
			}
			set
			{
				_eventsAsWorkSpacePart = value;
			}
		}
		[Category("Behavior")]
		[Description("If true adds events.")]
		[XmlAttribute()]
		public bool LogToTraceControl
		{
			get
			{
				return _logToTraceControl;
			}
			set
			{
				_logToTraceControl = value;
			}
		}
		[Category("Filters")]
		[Description("Path to the XPath library filters.")]
		[XmlAttribute()]
		public string XPathFilePath
		{
			get
			{
				return _xPathFilePath;
			}
			set
			{
				_xPathFilePath = value;
			}
		}
		[Category("Filters")]
		[Description("Path to the XQuery library filters.")]
		[XmlAttribute()]
		public string XQueryFilePath
		{
			get
			{
				return _xQueryFilePath;
			}
			set
			{
				_xQueryFilePath = value;
			}
		}
		public TracingOptions()
		{
		}
	}
}
