using System;
using System.Xml.Serialization;

using Tools.Core;

namespace Tools.Tracing.UI
{
	// TODO: Refactor to the separate package (SD)
	// TODO: Implementing the IChangeEventRaiser interface may represent a mixed cohesion of the domain here
	// Interface implementation might be moved elsewhere then to address this (like Adaptor or Decorator). (SD)
	/// <summary>
	/// Summary description for RemoteConnectionConfiguration.
	/// </summary>
	[Serializable()]
	public sealed class RemoteConnectionConfiguration : Descriptor, IEnabled, IChangeEventRaiser
	{
		private ProtocolType	_protocolType	= ProtocolType.Tcp;
		private string			_uri			= null;
		private string			_serviceHost	= null;
		private string			_port			= null;
		
		#region IEnabled Implementation

		private bool _enabled = true;

		public event System.EventHandler EnabledChanged = null;

		[XmlAttribute()]
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
					// handle as IEnabled
					OnEnabledChanged();
					// handle as IChangeEventRaiser
					OnChanged();
				}

			}
		}

		
		private void OnEnabledChanged()
		{
			if (EnabledChanged!=null)
			{
				EnabledChanged(this, System.EventArgs.Empty);
			}
		}

		#endregion

		#region IChangeEventRaiser Members

		private void OnChanged()
		{
			if (Changed!=null) Changed(this, EventArgs.Empty);
		}

		public event System.EventHandler Changed;

		#endregion

		[XmlAttribute()]
		public ProtocolType	ProtocolType
		{
			get
			{
				return _protocolType;
			}
			set
			{
				if (_protocolType==value) return;
				// assign
				_protocolType = value;
				// handle as IChangeEventRaiser
				OnChanged();
			}
		}
		[XmlAttribute()]
		public string Uri
		{
			get
			{
				return _uri;
			}
			set
			{
				if (_uri==value) return;
				// assign
				_uri = value;
				// handle as IChangeEventRaiser
				OnChanged();
			}
		}	
		[XmlAttribute()]
		public string ServiceHost
		{
			get
			{
				return _serviceHost;
			}
			set
			{
				if (_serviceHost==value) return;
				// assign
				_serviceHost = value;
				// handle as IChangeEventRaiser
				OnChanged();
			}
		}	
		[XmlAttribute()]
		public string Port
		{
			get
			{
				return _port;
			}
			set
			{
				if (_port==value) return;
				// assign
				_port = value;
				// handle as IChangeEventRaiser
				OnChanged();
			}
		}
		
		
		#region Constructors

		public RemoteConnectionConfiguration()
		{
		}
		
		#endregion
	}
}
