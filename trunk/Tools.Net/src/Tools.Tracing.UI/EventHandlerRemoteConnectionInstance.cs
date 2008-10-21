#region Using directives

using System;
using Tools.Remoting.Client.Common;
using Tools.Tracing.ClientHandler;
using Tools.Tracing.Common;

#endregion

namespace Tools.Tracing.UI
{
    public class EventHandlerRemoteConnectionInstance : RemoteConnectionInstance
    {
        public EventHandlerRemoteConnectionInstance
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
                throw new Exception("EventDelegate is null and cannot be assigned!");
            }
        }

        public override void Disconnect()
        {
        }
    }
}