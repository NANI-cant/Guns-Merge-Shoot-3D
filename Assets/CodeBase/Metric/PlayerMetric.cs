using UnityEngine;

namespace Metric {
    [CreateAssetMenu(fileName = "PlayerMetric", menuName = "Metric/Player Metric")]
    public class PlayerMetric: ScriptableObject {
        [field: SerializeField] public float AttackRadius { get; private set; }
        [field: SerializeField] public float MaxHealth { get; private set; }
    }
}