using System;

using Tools.Core;


namespace Tools.Tracing.Common
{
	/// <summary>
	/// Summary description for TraceEventHandlerConfiguration.
	/// </summary>
	[Serializable()]
	public class TraceEventHandlerConfiguration : Descriptor, IEnabled
	{
		private TraceEventFilterConfigurationCollection	_filters				= null;

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

		public TraceEventFilterConfigurationCollection Filters
		{
			get
			{
				return _filters;
			}
			set
			{
				_filters = value;
			}
		}
		public TraceEventHandlerConfiguration()
		{
			_filters = new TraceEventFilterConfigurationCollection();
		}
	}
}
