using System;
using UnityEngine;

namespace Metric.Levels {
    [Serializable]
    public class WaveData {
        [field: SerializeField] public WaveEnemy[] EnemyDataset { get; private set; }
    }
}