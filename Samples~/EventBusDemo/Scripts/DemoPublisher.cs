using UnityEngine;

namespace HTDA.Framework.Events.Samples
{
    public sealed class DemoPublisher : MonoBehaviour
    {
        [Header("Keys")]
        public KeyCode pingKey = KeyCode.Space;
        public KeyCode damageKey = KeyCode.D;

        private int _pingCount;

        private void Update()
        {
            var bus = DemoEventBusContext.Bus;
            if (bus == null) return;

            if (Input.GetKeyDown(pingKey))
            {
                _pingCount++;
                bus.Publish(new PingEvent(_pingCount));
                Debug.Log($"[Publisher] Publish PingEvent({_pingCount})");
            }

            if (Input.GetKeyDown(damageKey))
            {
                bus.Publish(new DamageEvent(amount: 10, source: gameObject.name));
                Debug.Log("[Publisher] Publish DamageEvent(10)");
            }
        }
    }
}