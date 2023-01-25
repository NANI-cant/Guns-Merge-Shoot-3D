using UnityEngine;

namespace Metric.Levels {
    [CreateAssetMenu(fileName = "LevelData", menuName = "Levels/Level Data")]
    public class LevelData: ScriptableObject {
        [field:SerializeField] public StandartWaveData[] Waves { get; private set; }
    }
}