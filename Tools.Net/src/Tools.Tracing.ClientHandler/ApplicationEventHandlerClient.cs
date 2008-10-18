using System;
using System.Diagnostics;
using Tools.Remoting.Client.Common;
using Tools.Tracing.Common;

namespace Tools.Tracing.Client.Handler
{
	/// <summary>
	/// Summary description for RemotableApplicationEventHandler.
	/// </summary>
	public class ApplicationEventHandlerClient : 
		RemotingClient, 
		ITraceEventHandler,
		ITraceEventHandlingPublisher,
		ITraceEventFilterContainer
	{
		public event TraceEventDelegate EventHandled
		{
			add 
			{
				(getTransparentProxy
					(
					typeof(ITraceEventHandlingPublisher),
					true
					) 
					as ITraceEventHandlingPublisher).EventHandled += value;
			}
			remove
			{
				(getTransparentProxy
					(
					typeof(ITraceEventHandlingPublisher),
					true
					) 
					as ITraceEventHandlingPublisher).EventHandled -= value;
			}

		}

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
		
		public ApplicationEventHandlerClient
			(
			string	serviceHost, 
			string servicePort,
			string objectUriPath
			)
			: base
			(
			serviceHost,
			Convert.ToInt32(servicePort),
			objectUriPath
			)
		{
		}

//		private void OnEventHandled(TraceEvent e)
//		{
////			if (this.EventHandled!=null)
////			{
////				EventHandled(new TraceEventArgs(e));
////			}
//			throw new NotSupportedException
//				(
//				"Method OnEventHandled of the ApplicationEventHandlerClient is not intended to be called." +
//				" Verify your program code."
//				);
//		}
//		public override object InitializeLifetimeService()
//		{
//			return null;
//		}

		#region ITraceEventHandler Members

		public void HandleEvent(TraceEvent traceEvent)
		{
			if (traceEvent==null) return;
//			throw new NotImplementedException
//				(
//				"Client method of the ApplicationEventHandlerWrapper called!" +
//				"Setup remoting logging correctly and try again!"
//				);
			try
			{
				(getTransparentProxy
					(
					typeof(ITraceEventHandler),
					true
					) 
					as ITraceEventHandler).HandleEvent
					(
					traceEvent
					);
			}
			catch (Exception ex)
			{
				// TODO: Introduce an option so that writing to the fallback wouldn't
				// be allowed. (SD)
				TraceEventHandlerManager.Instance.FallbackHandler.HandleEvent
					(
					new TraceEvent
					(
					0,
                    TraceEventType.Error,
					ApplicationLifeCycleType.Runtime,
					EventCategory.Debugging,
					"Exception occured when trying to write to the remote log." +
					"With url " + this.ProtocolSchema + "://" 
					+ this.ServiceHost + ":" + this.ServicePort + @"/" +
					this.ObjectUriPath + ". Exception text: " +
					ex.ToString(),
					traceEvent.ContextIdentifier,
					null
					));
				TraceEventHandlerManager.Instance.FallbackHandler.HandleEvent
					(
					traceEvent
					);
			}
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
