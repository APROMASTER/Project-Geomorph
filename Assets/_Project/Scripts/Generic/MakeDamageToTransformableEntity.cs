using UnityEngine;
using UnityEngine.Events;

namespace APROMASTER
{
    public class MakeDamageToTransformableEntity : MonoBehaviour
    {
        [SerializeField] private int damagePower = 10;
        [SerializeField] private UnityEvent OnTrigger;

        private void OnTriggerEnter2D(Collider2D other) 
        {
            DealDamage(other);
        }

        private void OnTriggerStay2D(Collider2D other) {
            DealDamage(other);
        }

        private void DealDamage(Collider2D other)
        {
            if (!other.TryGetComponent(out TransformableEntity transformableEntity)) return;
            Health health = other.GetComponent<Health>();
            if (health == null) return;
            health.ReceiveDamage(damagePower);
            OnTrigger?.Invoke();
        }
    }
}
