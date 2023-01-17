using System;
using Gameplay.PlayerLogic;
using UnityEngine;

namespace Gameplay.HealthLogic {
    public class Health : MonoBehaviour, IDamageable{
        public event Action<float, bool> HitTaken;
        public event Action<Health> Died;
		
        public float MaxHealth { get; private set; }
        public float CurrentHealth { get; private set; }

        public void Construct(float maxHealth) {
            MaxHealth = maxHealth;
            CurrentHealth = MaxHealth;
        }

        public void TakeDamage(float damage, bool isCrit) {
            CurrentHealth -= damage;
            HitTaken?.Invoke(damage, isCrit);

            if (CurrentHealth <= 0) Died?.Invoke(this);
        }
    }
}