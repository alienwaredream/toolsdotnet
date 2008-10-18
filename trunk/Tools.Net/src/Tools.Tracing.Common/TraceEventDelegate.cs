using System;

namespace Tools.Tracing.Common
{
	/// <summary>
	/// Summary description for TraceEventHandlerDelegate.
	/// </summary>
	[Serializable()]
	public delegate void TraceEventDelegate
	(
		TraceEventArgs eventArgs
	);

}
