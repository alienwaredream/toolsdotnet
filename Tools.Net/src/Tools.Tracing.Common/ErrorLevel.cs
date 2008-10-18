using System;

namespace Tools.Tracing.Common
{
	/// <summary>
	/// Summary description for ErrorLevel.
	/// </summary>
	public enum ErrorLevel : byte
	{
		Unknown		= 0,
		Minor		= 1,
		General		= 2,
		Critical	= 4
	}
}
