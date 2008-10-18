using System;

using Tools.Tracing.Common;
using Tools.Remoting.Client.Common;

namespace Tools.Tracing.Client
{
	/// <summary>
	/// Summary description for RemotableApplicationEventHandler.
	/// </summary>
	public class ApplicationEventHandlerWrapper : 
		MarshalByRefObject /*RemotingClient*/, 
		ITraceEventHandler,
		ITraceEventFilterContainer
	{
		public event TraceEventDelegate EventHandled = null;

		#region ITraceEventFilterContainer Implementation

		private ITraceEventFilterCollection	_filtersChain	= new ITraceEventFilterCollection();
		
		public ITraceEventFilterCollection	Filters
		{
			get
			{
				return _filtersChain;
			}
		}
		
		
		public void AddFilter(ITraceEventFilter filter)
		{
			lock (_filtersChain)
			{
				_filtersChain.Add(filter);
			}
		}
		public void RemoveFilter(ITraceEventFilter filter)
		{
			lock (_filtersChain)
			{
				_filtersChain.Remove(filter);
			}
		}
		
		#endregion

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
		
		public ApplicationEventHandlerWrapper()
		{

		}

		private void OnEventHandled(TraceEvent e)
		{
			if (this.EventHandled!=null)
			{
                EventHandled(new TraceEventArgs { Event = e });
			}
		}
		public override object InitializeLifetimeService()
		{
			return null;
		}

		#region ITraceEventHandler Members

		public void HandleEvent(TraceEvent traceEvent)
		{
			throw new NotImplementedException
				(
				"Client method of the ApplicationEventHandlerWrapper called!" +
				"Setup remoting logging correctly and try again!"
				);
		}
		public void AddHandler(ITraceEventHandler handler)
		{
			throw new NotImplementedException
				(
				"Client method of the ApplicationEventHandlerWrapper called!" +
				"Setup remoting logging correctly and try again!"
				);	
		}
		public void RemoveHandler(ITraceEventHandler handler)
		{
			throw new NotImplementedException
				(
				"Client method of the ApplicationEventHandlerWrapper called!" +
				"Setup remoting logging correctly and try again!"
				);	
		}

		public void Check()
		{
			throw new NotImplementedException
				(
				"Client method of the ApplicationEventHandlerWrapper called!" +
				"Setup remoting logging correctly and try again!"
				);	
		}
		#endregion
	}
}
