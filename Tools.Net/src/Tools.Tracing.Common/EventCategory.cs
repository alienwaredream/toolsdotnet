using System;

namespace Tools.Tracing.Common
{
	/// <summary>
	/// Summary description for EventCategory.
	/// </summary>
	[Flags()]
	public enum EventCategory
	{
		None = 0,
		Debugging = 1,
		PerformanceTuning = 2,
		CapacityPlanning = 4,
		BehaviorTracking = 8,
		Configuration = 16,
		OperationalStatus = 32

	}
}
