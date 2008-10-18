using System;
using System.Configuration;
using Tools.Core.Context;
using Tools.Core.Utils;

namespace Tools.Tracing.Common
{
	// TODO: Refactor to singleton
	/// <summary>
	/// Summary description for TraceEventHandlerManager.
	/// </summary>
	public sealed class TraceEventHandlerManager : ITraceEventHandlerManager
	{
		
		private static	object										syncRoot	= new object();
		private static	TraceEventHandlerManager				_instance	= null;

		private			object										_confLock	= new object();
		private			TraceEventHandlerManagerConfiguration	config		= null;

		private			ITraceEventHandlerCollection			_handlers	= null;
		private			ContextIdentifier							contextIdentifier =
			new ContextIdentifier();
		private ITraceEventHandler _fallbackHandler;


		public static TraceEventHandlerManager Instance
		{
			get
			{
				if (_instance == null)
				{
					lock (syncRoot)
					{
						if (_instance == null)
						{
							_instance = new TraceEventHandlerManager();
						}
					}
				}
				return _instance;
			}
		}

		/// <summary>
		/// DEtails level for location
		/// </summary>
		public LocationDetailLevel LocationDetailLevel
		{
			get
			{
				lock (_confLock)
				{
					if (config!=null) 
					{

						return config.LocationDetailLevel;

					}
				}
				return LocationDetailLevel.Basic;
			}
		}
		public ITraceEventHandler FallbackHandler
		{
			get
			{
				return _fallbackHandler;
			}
		}

		/// <summary>
		/// Synchronization object, think about wrapping it away from the public access (SD)
		/// </summary>
		internal object ConfLock
		{
			get
			{
				return _confLock;
			}
		}
		internal ITraceEventHandlerCollection	Handlers
		{
			get
			{
				return _handlers;
			}
		}
		public string GetBaseSourceName()
		{
			lock (_confLock)
			{
				return ((config==null) ? "Application" : config.BaseEventSourceName);
			}
		}
		// Until enabling via remoting
		private TraceEventHandlerManager()
		{
		}
        private TraceEventHandlerManagerConfiguration getConfiguration()
        {
            return config;
        }
		#region ITraceEventHandlerManager Members

		public void LoadConfiguration(TraceEventHandlerManagerConfiguration configuration)
		{
			// TODO:  Require specific permission to be granted (SD). 
			// Force this via remoting boundaries as well.

			lock (_confLock)
			{
			    this.config = SerializationUtility.GetObjectCloneBinary(configuration) as TraceEventHandlerManagerConfiguration;
			}
				
				

		}
		public TraceEventHandlerManagerConfiguration GetConfiguration()
		{
			lock (_confLock)
			{
				if (config == null)
				{
					config = getConfiguration();
				}
				// TODO: Client should cope with the fact that null can be created.
				if (config == null) return null;
				
				 

				return SerializationUtility.GetObjectCloneBinary(config)
					as TraceEventHandlerManagerConfiguration;
			}
		}

		public void AddHandler(ITraceEventHandler handler)
		{
			lock (_confLock)
			{
				// The same synchronization level with the GetHandlersChain()
				_handlers.Add(handler);
			}
		}
		public void RemoveHandler(ITraceEventHandler handler)
		{
			lock (_confLock)
			{
				// The same synchronization level with the GetHandlersChain()
				_handlers.Remove(handler);
			}		
		}

		#endregion
	}
}
