using System;

namespace Tools.Common.Process
{
	/// <summary>
	/// Summary description for ProcessMessage.
	/// </summary>
	public enum ProcessMessage
	{
        None = 0,
		// TODO: Assign numbers and add to DB (SD)
		Initialized		= 12400,
		StartRequested	= 12401,
		Started			= 12402,
		StopRequested	= 12403,
		Stopped			= 12404,
	}
}
