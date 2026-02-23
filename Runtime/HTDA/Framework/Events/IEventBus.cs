using System;

namespace HTDA.Framework.Events
{
    public interface IEventBus
    {
        /// <summary>
        /// Subscribe to events of type T. Returns a disposable subscription.
        /// </summary>
        IDisposable Subscribe<T>(Action<T> handler);

        /// <summary>
        /// Unsubscribe a previously subscribed handler. Safe to call multiple times.
        /// </summary>
        void Unsubscribe<T>(Action<T> handler);

        /// <summary>
        /// Publish an event instance of type T.
        /// </summary>
        void Publish<T>(T evt);

        /// <summary>
        /// Remove all subscriptions of all event types.
        /// </summary>
        void Clear();
    }
}