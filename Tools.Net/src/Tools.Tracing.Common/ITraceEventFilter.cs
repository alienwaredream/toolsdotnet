using System;

using Tools.Core;

namespace Tools.Tracing.Common
{
	/// <summary>
	///	Provides an interface for the filtering of the <see cref="TraceEvent"/>.
	///	
	/// </summary>
	public interface ITraceEventFilter : IEnabled
	{
		bool AcceptEvent(TraceEvent e);
	}
}
