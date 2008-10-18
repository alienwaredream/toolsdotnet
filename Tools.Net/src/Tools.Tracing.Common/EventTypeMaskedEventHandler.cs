using System;
using System.Diagnostics;

namespace Tools.Tracing.Common
{
	/// <summary>
	/// Summary description for EventLogEventHandler.
	/// </summary>
	public abstract class EventTypeMaskedEventHandler : ITraceEventHandler
	{
		private ITraceEventHandlerCollection	_handlersChain	= new ITraceEventHandlerCollection();

		private TraceEventTypeMask mask = TraceEventTypeMask.All;

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

		public ITraceEventHandlerCollection HandlersChain 
		{
			get 
			{
				return _handlersChain;
			}
		}

		public EventTypeMaskedEventHandler(string mask)
			: this()
		{
			this.mask = 
				(TraceEventTypeMask)Enum.Parse(typeof(TraceEventTypeMask), mask, true) ;
		}
		public EventTypeMaskedEventHandler()
		{

		}
		public abstract void HandleEvent(TraceEvent traceEvent);

		protected bool ShouldHandleEvent(TraceEvent traceEvent)
		{
			// Check for negative cases
			if (!Enabled) return false;
			if (!Convert.ToBoolean((((short)traceEvent.Type)& (short)mask))) return false;
			// If none of negative cases applies, return true
			return true;
		}
	}
}