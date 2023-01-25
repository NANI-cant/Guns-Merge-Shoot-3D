using UnityEngine;

namespace Metric {
    [CreateAssetMenu(fileName = "EnemyMetric", menuName = "Metric/Enemy Metric")]
    public class EnemyMetric: ScriptableObject {
        [field: SerializeField] public float MovementSpeed { get; private set; }
        [field: SerializeField] public float AttackSpeed { get; private set; }
        [field: SerializeField] public int Damage { get; private set; }
        [field: SerializeField] public float AttackRadius { get; private set; }
        [field: SerializeField] public float MaxHealth { get; private set; }
        [field: SerializeField] public float Power { get; private set; }

#if UNITY_EDITOR
        private void OnValidate() {
            Power = MovementSpeed + AttackSpeed + Damage + MaxHealth;
            Power /= 4f;
        }
#endif
    }
}