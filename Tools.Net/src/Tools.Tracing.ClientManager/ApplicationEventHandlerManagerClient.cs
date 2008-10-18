using System;

using Tools.Tracing.Common;
using Tools.Remoting.Client.Common;

namespace Tools.Tracing.Client.Manager
{
	/// <summary>
	/// Summary description for RemotableApplicationEventHandler.
	/// </summary>
	public class ApplicationEventHandlerManagerClient : 
		RemotingClient, 
		ITraceEventHandlerManager
	{

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
		
		public ApplicationEventHandlerManagerClient
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

		public void LoadConfiguration(TraceEventHandlerManagerConfiguration configuration)
		{
			(getTransparentProxy
				(
				typeof(ITraceEventHandlerManager)
				) as ITraceEventHandlerManager).LoadConfiguration(configuration);
		}
		public TraceEventHandlerManagerConfiguration GetConfiguration()
		{
			return (getTransparentProxy
				(
				typeof(ITraceEventHandlerManager)
				) as ITraceEventHandlerManager).GetConfiguration();
		}
		public void AddHandler(ITraceEventHandler handler)
		{
			throw new NotImplementedException
				(
				"Method ApplicationEventHandlerManagerClient.AddHandler is not yet implemented!"
				);
		}
		public void RemoveHandler(ITraceEventHandler handler)
		{
			throw new NotImplementedException
				(
				"Method ApplicationEventHandlerManagerClient.RemoveHandler is not yet implemented!"
				);
		}




	}
}
