using System;

namespace Tools.Tracing.Common
{
    // TODO: Handle exceptions.
    public class TraceEventHandlerEventStub :
        MarshalByRefObject
    {
        public event TraceEventDelegate EventHandled = null;

        private void OnEventHandled(TraceEvent e)
        {
            if (EventHandled != null)
            {
                EventHandled(new TraceEventArgs {Event = e});
            }
        }

        public virtual void HandleEvent(TraceEventArgs e)
        {
            OnEventHandled(e.Event);
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }
    }
}