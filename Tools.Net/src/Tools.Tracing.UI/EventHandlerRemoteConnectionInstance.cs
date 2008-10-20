#region Using directives

using System;
using Tools.Tracing.ClientHandler;
using Tools.Tracing.Common;

#endregion

namespace Tools.Tracing.UI
{
    public class EventHandlerRemoteConnectionInstance : RemoteConnectionInstance
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

        public EventHandlerRemoteConnectionInstance
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

            TraceEventHandlerClient client =
                new TraceEventHandlerClient
                (
                Configuration.ServiceHost,
                Configuration.Port,
                Configuration.Uri
                );

            if (EventDelegate != null)
            {
                client.EventHandled += new TraceEventDelegate(EventDelegate);
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
