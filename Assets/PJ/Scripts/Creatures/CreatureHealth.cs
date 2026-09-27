using UnityEngine;

namespace PokemonJason.Creatures
{
    public sealed class CreatureHealth : MonoBehaviour
    {
        public int CurrentHealth { get; private set; }
        public int MaxHealth { get; private set; }
        public bool IsDead => CurrentHealth <= 0;

        public void Configure(CreatureData data)
        {
            MaxHealth = data != null ? data.MaxHealth : 1;
            CurrentHealth = MaxHealth;
        }

        public void TakeDamage(int amount)
        {
            if (IsDead || amount <= 0)
            {
                return;
            }

            CurrentHealth = Mathf.Max(0, CurrentHealth - amount);

            if (CurrentHealth == 0)
            {
                HandleDeath();
            }
        }

        private void HandleDeath()
        {
            // Death behavior will be defined by the combat/lifecycle system.
        }
    }
}
