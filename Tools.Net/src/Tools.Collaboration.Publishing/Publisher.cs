using Tools.Collaboration.Contracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Remoting.Messaging;

namespace Tools.Collaboration.Publishing
{
    public class Publisher : IPublisher
    {
        #region Fields

        private object subsSyncRoot = new object();
        private IDictionary<string, SubscriberData> subscribers = new Dictionary<string, SubscriberData>();
        private string name;

        #endregion

        #region IPublisher Members

        public Publisher()
        {
            name = AppDomain.CurrentDomain.FriendlyName + "." + Guid.NewGuid();
        }
        public Publisher(string name)
        {
            this.name = name;
        }

        public string GetName()
        {
            return name;
        }
        private void HandleNotificationResult(IAsyncResult ar)
        {
            AsyncResult resultHandle = null;
            Action<string> action = null;
            NotificationAsyncState state = null;

            try
            {
                resultHandle = ar as AsyncResult;
                action = resultHandle.AsyncDelegate as Action<string>;
                state = ar.AsyncState as NotificationAsyncState;

                action.EndInvoke(ar);
            }
            catch (Exception ex)
            {
                ex.Data.Add(Log.Source.Name, String.Format(CultureInfo.InvariantCulture,
                    "Exception during invocation of the subscriber with name {0}, " + 
                    "when trying to log message {1} with activityID {2}",
                    state.Subscriber.Name, state.Message, state.ActivityId));

                Log.Source.TraceData(TraceEventType.Error, 0, ex);
            }

        }

        public void Publish(string message)
        {
            lock (subsSyncRoot)
            {
                foreach (SubscriberData subscriber in subscribers.Values)
                {

                    NotificationAsyncState state = null;

                    try
                    {
                        Action<string> action = new Action<string>(new SubscriberProxy(subscriber).Notify);
                        state = new NotificationAsyncState { Message = message, Subscriber = subscriber };
                        action.BeginInvoke(message, new AsyncCallback(HandleNotificationResult), state);
                    }
                    catch (Exception ex)
                    {
                        ex.Data.Add(Log.Source.Name, String.Format(CultureInfo.InvariantCulture,
                            "Exception while beginning the invocation of notification for the subscriber with name {0}, " +
                            "when trying to log message {1} with activityID {2}",
                            state.Subscriber.Name, state.Message, state.ActivityId));

                        Log.Source.TraceData(TraceEventType.Error, 0, ex);
                    }
                }
            }
        }

        public void Publish(string message, string activityId)
        {
            throw new System.NotImplementedException();
        }

        public void AddSubscriber(SubscriberData subscriber)
        {

            subscribers.Add(subscriber.Name, subscriber);
        }

        #endregion
    }
}
