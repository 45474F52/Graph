using System;

namespace Graph.Core
{
    internal interface IMessenger
    {
        void Send<TMessage>(TMessage message);
        void Subscribe<TMessage>(object subscriber, Action<object> action);
        void Unsubscribe<TMessage>(object subscriber);
    }
}