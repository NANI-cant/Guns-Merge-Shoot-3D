using System;
using UnityEngine;

namespace Metric.Levels {
    [Serializable]
    public class Pack {
        [field: SerializeField] public EnemyId[] EnemyIds { get; private set; }
    }
}