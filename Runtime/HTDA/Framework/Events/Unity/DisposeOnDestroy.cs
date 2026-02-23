using System;
using UnityEngine;

namespace HTDA.Framework.Events.Unity
{
    public sealed class DisposeOnDestroy : MonoBehaviour
    {
        private CompositeDisposable _cd;

        public CompositeDisposable Bag => _cd ??= new CompositeDisposable();

        public void Add(IDisposable d) => Bag.Add(d);

        private void OnDestroy()
        {
            _cd?.Dispose();
            _cd = null;
        }
    }
}