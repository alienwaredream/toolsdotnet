using Tools.Collaboration.Contracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Remoting.Messaging;
using Tools.Core.Asserts;

namespace Tools.Collaboration.Publishing
{
    //internal delegate NotificationAsyncState SubscriberNotificationDelegate(string message, strinSubscriberData subscriber, NotificationAsyncState state)
    
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
                        state = Notify(message, subscriber, state);
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

        //private void Publish(

        private NotificationAsyncState Notify(string message, SubscriberData subscriber, NotificationAsyncState state)
        {
            Action<string> action = new Action<string>(new SubscriberProxy(subscriber).Notify);
            state = new NotificationAsyncState { Message = message, Subscriber = subscriber };
            action.BeginInvoke(message, new AsyncCallback(HandleNotificationResult), state);
            return state;
        }
        private NotificationAsyncState Notify(string message, string activityId, SubscriberData subscriber, NotificationAsyncState state)
        {
            Action<string, string> action = new Action<string, string>(new SubscriberProxy(subscriber).Notify);
            state = new NotificationAsyncState { Message = message, Subscriber = subscriber, ActivityId = activityId };
            action.BeginInvoke(message, activityId, new AsyncCallback(HandleNotificationResult), state);
            return state;
        }
        private NotificationAsyncState NotifySubscriber(string message, SubscriberData subscriber, NotificationAsyncState state)
        {
            Action<string> action = new Action<string>(new SubscriberProxy(subscriber).Notify);
            state = new NotificationAsyncState { Message = message, Subscriber = subscriber };
            action.BeginInvoke(message, new AsyncCallback(HandleNotificationResult), state);
            return state;
        }

        public void Publish(string message, string activityId)
        {
            lock (subsSyncRoot)
            {
                foreach (SubscriberData subscriber in subscribers.Values)
                {

                    NotificationAsyncState state = null;

                    try
                    {
                        state = Notify(message, activityId, subscriber, state);
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

        public void AddSubscriber(SubscriberData subscriber)
        {
            ErrorTrap.AddRaisableAssertion<ArgumentNullException>(subscriber != null, "subscriber != null");
            subscribers.Add(subscriber.Name, subscriber);
        }

        #endregion
    }
}
