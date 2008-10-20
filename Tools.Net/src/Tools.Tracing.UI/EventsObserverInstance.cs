using System;
using Tools.Tracing.ClientHandler;
using Tools.Tracing.Common;

namespace Tools.Tracing.UI
{
    /// <summary>
    /// Summary description for RemoteConnectionInstance.
    /// </summary>
    public class EventsObserverInstance : RemoteConnectionInstance
    {
        public EventsObserverInstance
            (
            RemoteConnectionConfiguration configuration,
            TraceEventDelegate eventDelegate
            ) : base(configuration)
        {
            EventDelegate = eventDelegate;
        }

        public TraceEventDelegate EventDelegate { get; set; }

        public override void Connect()
        {
            base.Connect();

            var client =
                new TraceEventHandlerClient
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
            var client =
                new TraceEventHandlerClient
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