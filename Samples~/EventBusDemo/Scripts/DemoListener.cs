using UnityEngine;
using HTDA.Framework.Events.Unity;

namespace HTDA.Framework.Events.Samples
{
    public sealed class DemoListener : MonoBehaviour
    {
        [Header("Auto add DisposeOnDestroy and dispose subscriptions on destroy")]
        [SerializeField] private bool autoDispose = true;

        private void Start()
        {
            var bus = DemoEventBusContext.Bus;
            if (bus == null)
            {
                Debug.LogWarning("[Listener] EventBus is null. Make sure DemoEventBusContext exists in scene.");
                return;
            }

            if (autoDispose)
            {
                // Ensure this object has a DisposeOnDestroy bag
                var disposer = this.GetOrAddDisposeOnDestroy();

                bus.Subscribe<PingEvent>(OnPing).AddTo(disposer);
                bus.Subscribe<DamageEvent>(OnDamage).AddTo(disposer);

                Debug.Log("[Listener] Subscribed with auto-dispose (DisposeOnDestroy).");
            }
            else
            {
                // Manual subscribe (for quick compare)
                bus.Subscribe<PingEvent>(OnPing);
                bus.Subscribe<DamageEvent>(OnDamage);

                Debug.Log("[Listener] Subscribed WITHOUT auto-dispose (manual).");
            }
        }

        private void OnPing(PingEvent e)
        {
            Debug.Log($"[Listener:{name}] Received PingEvent Count={e.Count}");
        }

        private void OnDamage(DamageEvent e)
        {
            Debug.Log($"[Listener:{name}] Received DamageEvent Amount={e.Amount}, Source={e.Source}");
        }

        private void OnDestroy()
        {
            // If you set autoDispose=false, you can test leak by commenting/uncommenting this:
            // var bus = DemoEventBusContext.Bus;
            // bus?.Unsubscribe<PingEvent>(OnPing);
            // bus?.Unsubscribe<DamageEvent>(OnDamage);
        }
    }
}