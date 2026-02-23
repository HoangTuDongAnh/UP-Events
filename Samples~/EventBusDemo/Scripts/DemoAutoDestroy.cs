using UnityEngine;

namespace HTDA.Framework.Events.Samples
{
    public sealed class DemoAutoDestroy : MonoBehaviour
    {
        [SerializeField] private float destroyAfterSeconds = 5f;

        private float _t;

        private void Update()
        {
            _t += Time.deltaTime;
            if (_t >= destroyAfterSeconds)
            {
                Debug.Log($"[AutoDestroy] Destroying {name}. If auto-dispose works, it will stop receiving events.");
                Destroy(gameObject);
            }
        }
    }
}