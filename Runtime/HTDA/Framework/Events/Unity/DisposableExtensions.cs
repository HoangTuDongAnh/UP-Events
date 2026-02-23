using System;
using UnityEngine;

namespace HTDA.Framework.Events.Unity
{
    public static class DisposableExtensions
    {
        public static IDisposable AddTo(this IDisposable d, CompositeDisposable bag)
        {
            bag?.Add(d);
            return d;
        }

        public static IDisposable AddTo(this IDisposable d, DisposeOnDestroy owner)
        {
            owner?.Add(d);
            return d;
        }

        public static IDisposable AddTo(this IDisposable d, DisposeOnDisable owner)
        {
            owner?.Add(d);
            return d;
        }

        public static DisposeOnDestroy GetOrAddDisposeOnDestroy(this Component c)
        {
            if (c == null) return null;
            var comp = c.GetComponent<DisposeOnDestroy>();
            return comp != null ? comp : c.gameObject.AddComponent<DisposeOnDestroy>();
        }

        public static DisposeOnDisable GetOrAddDisposeOnDisable(this Component c)
        {
            if (c == null) return null;
            var comp = c.GetComponent<DisposeOnDisable>();
            return comp != null ? comp : c.gameObject.AddComponent<DisposeOnDisable>();
        }
    }
}