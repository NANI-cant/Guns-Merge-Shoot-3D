using UnityEngine;

namespace Metric.Levels {
    [CreateAssetMenu(fileName = "LevelData", menuName = "Levels/Level Data")]
    public class LevelData: ScriptableObject {
        [SerializeField] private StandardWaveData[] _wayPoints;

        public WayPointData[] WayPoints => _wayPoints;
    }
}