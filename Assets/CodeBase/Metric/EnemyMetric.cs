using UnityEngine;

namespace Metric {
    [CreateAssetMenu(fileName = "EnemyMetric", menuName = "Metric/Enemy Metric")]
    public class EnemyMetric: ScriptableObject {
        [field: SerializeField] public float MovementSpeed { get; private set; }
        [field: SerializeField] public float AttackSpeed { get; private set; }
        [field: SerializeField] public int Damage { get; private set; }
        [field: SerializeField] public float AttackRadius { get; private set; }
        [field: SerializeField] public float MaxHealth { get; private set; }

        [Space]
        [SerializeField] private float _coefficient;

#if UNITY_EDITOR
        private void OnValidate() {
            _coefficient = MovementSpeed + AttackSpeed + Damage + MaxHealth + AttackRadius;
            _coefficient /= 5f;
        }
#endif
    }
}