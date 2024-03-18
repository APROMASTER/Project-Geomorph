using UnityEngine;
using UnityEngine.Events;

namespace APROMASTER
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int maxHealth = 100;
        [SerializeField] private int currentHealth;
        [SerializeField] UnityEvent<int> OnReceiveDamage;
        [SerializeField] UnityEvent<int> OnReceiveHealth;
        [SerializeField] UnityEvent OnZeroHealth;

        public int CurrentHealth
        {
            get => currentHealth;
            set => currentHealth = value;
        }

        public void ReceiveDamage(int damageAmount)
        {
            CurrentHealth -= damageAmount;
            OnReceiveDamage?.Invoke(CurrentHealth);
            if (CurrentHealth <= 0)
            {
                OnZeroHealth?.Invoke();
            }
        }

        public void GainHealth(int healthAmount)
        {
            CurrentHealth += healthAmount;
            if (CurrentHealth >= maxHealth)
            {
                CurrentHealth = maxHealth;
            }
            OnReceiveHealth?.Invoke(CurrentHealth);
        }

        // private void OnEnable() 
        // {
        //     currentHealth = maxHealth;
        // }
    }
}
