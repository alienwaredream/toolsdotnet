using System;
using Tools.Tracing.Client.Handler;
using Tools.Tracing.Common;

namespace Tools.Tracing.UI
{
	/// <summary>
	/// Summary description for RemoteConnectionInstance.
	/// </summary>
	public class EventsObserverInstance : RemoteConnectionInstance
	{
    private TraceEventDelegate _eventDelegate = null;

        public TraceEventDelegate EventDelegate
        {
            get
            {
                return _eventDelegate;
            }

            set
            {
                _eventDelegate = value;
            }
        }

        public EventsObserverInstance
            (
            RemoteConnectionConfiguration configuration,
            TraceEventDelegate eventDelegate
            ) : base(configuration)
        {
            _eventDelegate = eventDelegate;
        }
        public override void Connect()
        {
            base.Connect();

            ApplicationEventHandlerClient client =
                new ApplicationEventHandlerClient
                (
                Configuration.ServiceHost,
                Configuration.Port,
                Configuration.Uri
                );

            if (EventDelegate != null)
            {
                client.EventHandled += EventDelegate;
            }
            else
            {
                throw new Exception("EventDelegate is null and cannot be attached!");
            }
        }

        public override void Disconnect()
        {
            ApplicationEventHandlerClient client =
                new ApplicationEventHandlerClient
                (
                Configuration.ServiceHost,
                Configuration.Port,
                Configuration.Uri
                );

            if (EventDelegate != null)
            {
                client.EventHandled -= EventDelegate;
            }
            else
            {
                throw new Exception("EventDelegate is null and cannot be dettached!");
            }
            base.Disconnect();
        }
	}
}
