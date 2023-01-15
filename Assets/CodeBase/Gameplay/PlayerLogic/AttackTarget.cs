using System;
using UnityEngine;

namespace Gameplay.PlayerLogic {
    [Serializable]
    public class AttackTarget {
        public Transform Transform { get; }
        public IDamageable Damageable { get; }

        public AttackTarget(IDamageable damageable, Transform transform) {
            Transform = transform;
            Damageable = damageable;
        }
    
        public override bool Equals(object obj) 
            => (obj as AttackTarget).Damageable == Damageable;
    }
}