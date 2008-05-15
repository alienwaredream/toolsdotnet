using System;

namespace Tools.Common
{
	#region Interface IEnabled

	public interface IEnabled
	{
		bool Enabled{get;set;}
		event System.EventHandler EnabledChanged;
	}

	#endregion
}
