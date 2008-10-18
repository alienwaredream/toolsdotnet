using System;
using System.Xml.Serialization;

namespace Tools.Tracing.Common
{
	// TODO: Provide ToString() with nice formatting (SD)
	/// <summary>
	/// Summary description for TraceEventLocation.
	/// </summary>
	[Serializable()]
	public sealed class TraceEventLocation
	{
		private string _source			= null;
		private string _methodName		= null;
		private string _className		= null;
		private string _modulePath		= null;
		private string _hostName		= null;
		private string _processId		= null;
		private string _appDomainName	= null;
		private string _threadName		= null;

		[XmlAttribute()]
		public string Source
		{
			get
			{
				return _source;
			}
			set
			{
				_source = value;
			}
		}
		[XmlAttribute()]
		public string MethodName
		{
			get
			{
				return _methodName;
			}
			set
			{
				_methodName = value;
			}
		}
		[XmlAttribute()]
		public string ClassName
		{
			get
			{
				return _className;
			}
			set
			{
				_className = value;
			}
		}
		[XmlAttribute()]
		public string ModulePath
		{
			get
			{
				return _modulePath;
			}
			set
			{
				_modulePath = value;
			}
		}
		[XmlAttribute()]
		public string HostName
		{
			get
			{
				return _hostName;
			}
			set
			{
				_hostName = value;
			}
		}
		[XmlAttribute()]
		public string ProcessId
		{
			get
			{
				return _processId;
			}
			set
			{
				_processId = value;
			}
		}
		[XmlAttribute()]
		public string AppDomainName
		{
			get
			{
				return _appDomainName;
			}
			set
			{
				_appDomainName = value;
			}
		}
		[XmlAttribute()]
		public string ThreadName
		{
			get
			{
				return _threadName;
			}
			set
			{
				_threadName = value;
			}
		}
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
			_source = source;
			_methodName	= methodName;
			_className = className;
			_hostName = hostName;
			_processId = processId;
			_appDomainName = appDomainName;
			_modulePath = modulePath;
			_threadName = threadName;
		}
		public override string ToString()
		{
			return 
					"Host=" + _hostName + 
					";Module=" + _modulePath +
					";ThreadName=" + System.Environment.NewLine + _threadName; 
					//+ System.Environment.NewLine + _methodName;
		}
	}
}
