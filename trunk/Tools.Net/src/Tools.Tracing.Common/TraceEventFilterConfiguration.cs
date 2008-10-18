using System;

using Tools.Core;


namespace Tools.Tracing.Common
{
	/// <summary>
	/// Summary description for TraceEventFilterConfiguration.
	/// </summary>
	[Serializable()]
	public class TraceEventFilterConfiguration : Descriptor, IEnabled
	{
		#region IEnabled Implementation

		private bool _enabled = true;

		public event System.EventHandler EnabledChanged = null;

		public bool Enabled
		{
			get
			{
				return _enabled;
			}
			set
			{
				if (_enabled != value)
				{
					_enabled = value;
					OnEnabledChanged();
				}

			}
		}

		protected virtual void OnEnabledChanged()
		{
			if (EnabledChanged!=null)
			{
				EnabledChanged(this, System.EventArgs.Empty);
			}
		}

		#endregion
	}
}
