using System;
using System.Xml.Serialization;

using Tools.Core;


namespace Tools.Tracing.Common
{
	/// <summary>
	/// Summary description for TraceEventHandlerManagerConfiguration.
	/// </summary>
	[Serializable()]
	public class TraceEventHandlerManagerConfiguration : Descriptor
	{
		
		private TraceEventHandlerConfigurationCollection _handlers = null;
		private string _baseEventSourceName = null;
		private LocationDetailLevel _locationDetailLevel = LocationDetailLevel.Basic;
		private string _fallbackLogMaxFileSizeBytes = "2000000";
		private string _fallbackLogRootLocation = null;

		public TraceEventHandlerConfigurationCollection Handlers
		{
			get
			{
				return _handlers;
			}
			set
			{
				_handlers = value;
			}
		}
		[XmlAttribute()]
		public string BaseEventSourceName
		{
			get
			{
				return _baseEventSourceName;
			}
			set
			{
				_baseEventSourceName = value;
			}
		}
		[XmlAttribute()]
		public LocationDetailLevel LocationDetailLevel
		{
			get
			{
				return _locationDetailLevel;
			}
			set
			{
				_locationDetailLevel = value;
			}
		}
		[XmlAttribute()]
		public string FallbackLogMaxFileSizeBytes
		{
			get
			{
				return _fallbackLogMaxFileSizeBytes;
			}
			set
			{
				_fallbackLogMaxFileSizeBytes = value;
			}
		}
		[XmlAttribute()]
		public string FallbackLogRootLocation
		{
			get
			{
				return _fallbackLogRootLocation;
			}
			set
			{
				_fallbackLogRootLocation = value;
			}
		}
		public TraceEventHandlerManagerConfiguration()
		{
			_handlers = new TraceEventHandlerConfigurationCollection();
		}
	}
}
