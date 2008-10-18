using System;

using Tools.Core;

namespace Tools.Tracing.Common
{
	/// <summary>
	/// Default/Base ApplicationEventTypeFilter.
	/// </summary>
	[Serializable()]
	public abstract class TraceEventFilter : ITraceEventFilter
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

		protected TraceEventFilter()
		{
			
		}

		#region ITraceEventFilter Members

		public virtual bool AcceptEvent(TraceEvent e)
		{
			return true;
		}

		#endregion
	
	}
}
