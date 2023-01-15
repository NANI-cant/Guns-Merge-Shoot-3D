using System;
using UnityEngine;

namespace Metric.Levels {
    [Serializable]
    public class WaveEnemy {
        [SerializeField] public EnemyId Enemy;
        [SerializeField] public int Count;
    }
}