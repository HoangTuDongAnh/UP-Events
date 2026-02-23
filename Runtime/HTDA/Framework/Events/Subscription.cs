using System;

namespace HTDA.Framework.Events
{
    internal sealed class Subscription<T> : IDisposable
    {
        private IEventBus _bus;
        private Action<T> _handler;

        public Subscription(IEventBus bus, Action<T> handler)
        {
            _bus = bus;
            _handler = handler;
        }

        public void Dispose()
        {
            // idempotent
            var bus = _bus;
            var handler = _handler;

            _bus = null;
            _handler = null;

            if (bus != null && handler != null)
                bus.Unsubscribe(handler);
        }
    }
}