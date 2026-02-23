using UnityEngine;
using HTDA.Framework.Events;

namespace HTDA.Framework.Events.Samples
{
    /// <summary>
    /// Sample-only global access for EventBus.
    /// In your real game, you can wire it via ServiceRegistry/DI/Bootstrap instead.
    /// </summary>
    public sealed class DemoEventBusContext : MonoBehaviour
    {
        public static IEventBus Bus { get; private set; }

        [Header("Auto create a new bus on Awake")]
        [SerializeField] private bool createOnAwake = true;

        private void Awake()
        {
            if (!createOnAwake) return;

            Bus = new EventBus();
            Debug.Log("[UP-Events Sample] EventBus created.");
        }

        private void OnDestroy()
        {
            if (ReferenceEquals(Bus, null)) return;

            Bus.Clear();
            Bus = null;
            Debug.Log("[UP-Events Sample] EventBus cleared.");
        }
    }
}