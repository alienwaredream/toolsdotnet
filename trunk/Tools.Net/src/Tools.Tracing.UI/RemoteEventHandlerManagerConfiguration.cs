using System;

using Tools.Tracing.Common;

namespace Tools.Tracing.UI
{
	/// <summary>
	/// Summary description for RemoteEventHandlerManagerConfiguration.
	/// </summary>
	[Serializable()]
	public class RemoteEventHandlerManagerConfiguration
	{
		private RemoteConnectionConfiguration				_remoteConnectionConfiguration = null;
		private TraceEventHandlerManagerConfiguration	traceEventHandlerManagerConfiguration = null;
		
		public RemoteConnectionConfiguration RemoteConnectionConfiguration
		{
			get
			{
				return _remoteConnectionConfiguration;
			}
			set
			{
				_remoteConnectionConfiguration = value;
			}
		}
		public TraceEventHandlerManagerConfiguration	TraceEventHandlerManagerConfiguration
		{
			get
			{
				return traceEventHandlerManagerConfiguration;
			}
			set
			{
				traceEventHandlerManagerConfiguration = value;
			}
		}
		public RemoteEventHandlerManagerConfiguration()
		{
		}
		public RemoteEventHandlerManagerConfiguration
			(
			RemoteConnectionConfiguration				remoteConnectionConfiguration,
			TraceEventHandlerManagerConfiguration	traceEventHandlerManagerConfiguration
			)
		{
			_remoteConnectionConfiguration = remoteConnectionConfiguration;
			traceEventHandlerManagerConfiguration = traceEventHandlerManagerConfiguration;
		}
	}
}
