using System;
using System.Collections.Generic;

namespace HTDA.Framework.Events
{
    /// <summary>
    /// Small, type-based event bus.
    /// - Not thread-safe
    /// - Minimal allocations
    /// </summary>
    public sealed class EventBus : IEventBus
    {
        private readonly Dictionary<Type, Delegate> _map = new Dictionary<Type, Delegate>(64);

        public IDisposable Subscribe<T>(Action<T> handler)
        {
            if (handler == null) return Disposable.Empty;

            var type = typeof(T);
            if (_map.TryGetValue(type, out var existing))
                _map[type] = (Action<T>)existing + handler;
            else
                _map[type] = handler;

            return new Subscription<T>(this, handler);
        }

        public void Unsubscribe<T>(Action<T> handler)
        {
            if (handler == null) return;

            var type = typeof(T);
            if (!_map.TryGetValue(type, out var existing)) return;

            var next = (Action<T>)existing - handler;
            if (next == null) _map.Remove(type);
            else _map[type] = next;
        }

        public void Publish<T>(T evt)
        {
            var type = typeof(T);
            if (_map.TryGetValue(type, out var existing))
            {
                var cb = (Action<T>)existing;
                cb.Invoke(evt);
            }
        }

        public void Clear() => _map.Clear();
    }
}