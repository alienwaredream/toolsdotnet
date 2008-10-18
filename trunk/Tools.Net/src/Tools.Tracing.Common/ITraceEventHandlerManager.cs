using System;

namespace Tools.Tracing.Common
{
	// TODO: that will be factored out to the another package (SD). 
	// Take care of dependency to the TraceEventHandlerManagerConfiguration
	/// <summary>
	/// Summary description for ITraceEventHandlerManager.
	/// </summary>
	public interface ITraceEventHandlerManager
	{
		void LoadConfiguration(TraceEventHandlerManagerConfiguration configuration);
		TraceEventHandlerManagerConfiguration GetConfiguration();

		void AddHandler(ITraceEventHandler handler);
		void RemoveHandler(ITraceEventHandler handler);

	}
}
