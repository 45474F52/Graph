using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Graph.Core
{
    internal class Messenger : IMessenger
    {
        private ConcurrentDictionary<Type, SynchronizedCollection<Subscription>> Subscriptions =
            new ConcurrentDictionary<Type, SynchronizedCollection<Subscription>>();

        private ConcurrentDictionary<Type, object> CurrentState = new ConcurrentDictionary<Type, object>();

        public void Send<TMessage>(TMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            if (!Subscriptions.ContainsKey(typeof(TMessage)))
            {
                Subscriptions.TryAdd(typeof(TMessage), new SynchronizedCollection<Subscription>());
            }

            CurrentState.AddOrUpdate(typeof(TMessage), (t) => message, (t, o) => message);

            foreach (Subscription subscription in Subscriptions[typeof(TMessage)])
            {
                subscription.Action(message);
            }
        }

        public void Subscribe<TMessage>(object subscriber, Action<object> action)
        {
            if (!Subscriptions.ContainsKey(typeof(TMessage)))
            {
                Subscriptions.TryAdd(typeof(TMessage), new SynchronizedCollection<Subscription>());
            }

            Subscription newSubscriber = new Subscription(subscriber, action);

            Subscriptions[typeof(TMessage)].Add(newSubscriber);

            if (CurrentState.ContainsKey(typeof(TMessage)))
            {
                newSubscriber.Action(CurrentState[typeof(TMessage)]);
            }
        }

        public void Unsubscribe<TMessage>(object subscriber)
        {
            if (Subscriptions.ContainsKey(typeof(TMessage)))
            {
                Subscription subscription = Subscriptions[typeof(TMessage)].FirstOrDefault(s => s.Subscriber == subscriber);
                if (subscription != null)
                {
                    Subscriptions[typeof(TMessage)].Remove(subscription);
                }
            }
        }

        private protected class Subscription
        {
            public object Subscriber { get; private set; }
            public Action<object> Action { get; private set; }

            public Subscription(object subscriber, Action<object> action)
            {
                Subscriber = subscriber;
                Action = action;
            }
        }
    }
}