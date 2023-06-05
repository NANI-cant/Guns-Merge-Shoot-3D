using UnityEngine;

namespace Metric {
    [CreateAssetMenu(fileName = "EnemyMetric", menuName = "Metric/Enemy Metric")]
    public class EnemyMetric: ScriptableObject {
        [SerializeField][Min(float.Epsilon)] private float _movementSpeed;
        [SerializeField][Min(float.Epsilon)] private float _attackSpeed;
        [SerializeField][Min(1)] private int _damage;
        [SerializeField][Min(1)] private float _attackRadius;
        [SerializeField][Min(1)] private int _maxHealth;
        [SerializeField] private float _power;

        public float MovementSpeed => _movementSpeed;
        public float AttackSpeed => _attackSpeed;
        public int Damage => _damage;
        public float AttackRadius => _attackRadius;
        public int MaxHealth => _maxHealth;
        public float Power => _power;

#if UNITY_EDITOR
        private void OnValidate() {
            _power =  Damage + MaxHealth;
            _power = Power / 2f;
        }
#endif
    }
}