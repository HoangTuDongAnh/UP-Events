using System;
using System.Collections.Generic;

namespace HTDA.Framework.Events
{
    public sealed class CompositeDisposable : IDisposable
    {
        private List<IDisposable> _items;
        private bool _disposed;

        public CompositeDisposable(int capacity = 4)
        {
            _items = new List<IDisposable>(capacity);
        }

        public void Add(IDisposable disposable)
        {
            if (disposable == null) return;

            if (_disposed)
            {
                disposable.Dispose();
                return;
            }

            _items.Add(disposable);
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;

            for (int i = _items.Count - 1; i >= 0; i--)
            {
                try { _items[i]?.Dispose(); }
                catch { /* swallow */ }
            }

            _items.Clear();
        }
    }

    public static class Disposable
    {
        public static readonly IDisposable Empty = new EmptyDisposable();

        private sealed class EmptyDisposable : IDisposable
        {
            public void Dispose() { }
        }
    }
}