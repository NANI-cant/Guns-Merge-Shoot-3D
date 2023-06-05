using UnityEngine;

namespace Metric {
    [CreateAssetMenu(fileName = "Difficult", menuName = "Game/Difficult")]
    public class DifficultMetric: ScriptableObject {
        [SerializeField] private float _enemyHealthCoefficient = 1.3f;
        [SerializeField] private float _bossHealthCoefficient = 1.22f;
        [SerializeField] private float _enemyDamageCoefficient = 1.3f;
        [SerializeField] private float _playerHealthCoefficient = 1.5f;

        public int GetEnemyHealth(int health, int level) {
            float newHealth = health * Mathf.Pow(_enemyHealthCoefficient, level);
            return (int) Mathf.Round(newHealth);
        }

        public int GetBossHealth(int health, int level) {
            float newHealth = health * Mathf.Pow(_bossHealthCoefficient, level);
            return (int) Mathf.Round(newHealth);
        }

        public int GetPlayerHealth(int health, int weaponLevel) {
            float newHealth = health * Mathf.Pow(_playerHealthCoefficient, weaponLevel);
            return (int) Mathf.Round(newHealth);
        }

        public int GetEnemyDamage(int damage, int level) {
            float newDamage = damage * Mathf.Pow(_enemyDamageCoefficient, level);
            return (int) Mathf.Round(newDamage);
        }
    }
}