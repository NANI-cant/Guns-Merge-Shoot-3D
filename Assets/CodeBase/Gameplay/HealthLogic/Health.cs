using System;
using Gameplay.PlayerLogic;
using UnityEngine;

namespace Gameplay.HealthLogic {
    public class Health : MonoBehaviour, IDamageable{
        public event Action HitTaken;
        public event Action Died;
		
        public float MaxHealth { get; private set; }
        public float CurrentHealth { get; private set; }

        public void Construct(float maxHealth) {
            MaxHealth = maxHealth;
            CurrentHealth = MaxHealth;
        }

        public void TakeDamage(float damage) {
            CurrentHealth -= damage;
            HitTaken?.Invoke();

            if (CurrentHealth <= 0) Died?.Invoke();
        }
    }
}