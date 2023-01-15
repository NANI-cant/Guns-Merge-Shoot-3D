using System.Collections.Generic;
using UnityEngine;

namespace Metric.Levels {
    [CreateAssetMenu(fileName = "LevelData", menuName = "Levels/Level Data")]
    public class LevelData: ScriptableObject {
        [field:SerializeField] public WaveData[] Waves { get; private set; }
    }
}