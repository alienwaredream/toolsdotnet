using System;

namespace Tools.Tracing.Common
{
	/// <summary>
	/// Summary description for TraceEventTypeMask.
	/// </summary>
	[Flags()]
	public enum TraceEventTypeMask : byte
	{
		Uncategorised	= 0,
		/// <summary>
		/// 
		/// </summary>
		Error			= 1,
		/// <summary>
		/// 
		/// </summary>
		Warning			= 2,
		/// <summary>
		/// General.
		/// </summary>
		Info			= 4,
		/// <summary>
		/// 
		/// </summary>
		Verbose			= 16,
		/// <summary>
		/// All event types.
		/// </summary>
		All = Uncategorised | Error | Warning | Info | Verbose
		

	}
}
