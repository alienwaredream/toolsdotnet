using System;

namespace Tools.Tracing.Common
{
	/// <summary>
	/// Summary description for ITraceEventFilterContainer.
	/// </summary>
	public interface ITraceEventFilterContainer
	{
		ITraceEventFilterCollection	Filters {get;}

		void AddFilter(ITraceEventFilter filter);
		void RemoveFilter(ITraceEventFilter filter);

	}
}
