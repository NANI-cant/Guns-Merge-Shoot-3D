using Metric.Levels.Stages;
using UnityEngine;

namespace Metric.Levels {
    [CreateAssetMenu(fileName = "LevelData", menuName = "Levels/Level Data")]
    public class Level: ScriptableObject {
        [SerializeField] private EnemyId[] _availableEnemies;
        [SerializeField] private Stage[] _stages;
        
        public Stage[] Stages => _stages;
        public EnemyId[] AvailableEnemies => _availableEnemies;
    }
}