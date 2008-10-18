using System;

using Tools.Core;

namespace Tools.Tracing.Common
{
	/// <summary>
	/// </summary>
	public interface ITraceEventHandler : IEnabled
	{
		void HandleEvent(TraceEvent traceEvent);
	}
}
